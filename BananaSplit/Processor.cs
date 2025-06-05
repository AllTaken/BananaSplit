using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BananaSplit;
public class Processor(Settings settings, LogForm logForm, StatusBarManager statusBarManager, Renamer renamer)
{
    private List<QueueItem> queueItems;
    private Thread ProcessingThread;
    private readonly Ffmpeg ffmpeg = new();
    private readonly MkvToolNix MkvTool = new();



    public void ProcessQueue(Action PreAction, Action PostAction, List<QueueItem> queueItems) {
        if (settings.ShowLog)
        {
            logForm.ShowLog();
        }
        this.queueItems = queueItems;

        ProcessingThread = new Thread(() => ProcessQueueItems(PreAction, PostAction));

        ProcessingThread.Start();
    }

    private void ProcessQueueItems(Action PreAction, Action PostAction)
    {
        PreAction();

        statusBarManager.SetStatusBarProgressBarValue(0, queueItems.Count);

        switch (settings.ProcessType)
        {
            case ProcessingType.MatroskaChapters:
                ProcessMatroskaChaptersInQueue();
                break;

            case ProcessingType.SplitAndEncode:
                ProcessSplitAndEncodeInQueue();
                break;

            case ProcessingType.MkvToolNixSplit:
                ProcessMkvToolNixSplitInQueue();
                break;
        }

        PostAction();

        statusBarManager.ClearStatusBarProgressBarValue();
    }

    private void ProcessMkvToolNixSplitInQueue()
    {
        for (var i = 0; i < queueItems.Count; i++)
        {
            QueueItem queueItem = queueItems[i];
            statusBarManager.SetStatusBarProgressBarValue(i + 1, queueItems.Count);
            statusBarManager.SetStatusBarLabelValue($"MKV Splitting for {Path.GetFileName(queueItem.FileName)}");

            ProcessMKVSplit(queueItem);
        }

        statusBarManager.SetStatusBarLabelValue("Done splitting!");
    }

    private void ProcessSplitAndEncodeInQueue()
    {
        for (var i = 0; i < queueItems.Count; i++)
        {
            QueueItem queueItem = queueItems[i];

            statusBarManager.SetStatusBarProgressBarValue(i + 1, queueItems.Count);
            statusBarManager.SetStatusBarLabelValue($"Encoding for {Path.GetFileName(queueItem.FileName)}");

            ProcessSplitAndEncode(queueItem);
        }

        statusBarManager.SetStatusBarLabelValue("Done encoding!");
    }

    private void ProcessMatroskaChaptersInQueue()
    {
        for (var i = 0; i < queueItems.Count; i++)
        {
            QueueItem queueItem = queueItems[i];

            statusBarManager.SetStatusBarProgressBarValue(i + 1, queueItems.Count);
            statusBarManager.SetStatusBarLabelValue($"Adding chapters for {Path.GetFileName(queueItem.FileName)}");

            ProcessMatroskaChapters(queueItem);
        }

        statusBarManager.SetStatusBarLabelValue("Done adding chapters!");
    }

    public void ProcessQueueItem(Action PreAction, Action PostAction, QueueItem queueItem)
    {
        queueItems = [queueItem];
        ProcessQueueItems(PreAction, PostAction);
    }

    private void ProcessMatroskaChapters(QueueItem queueItem)
    {
        List<TimeSpan> chapterTimeSpans =
        [
            // Always add the beginning as a chapter
            new TimeSpan(0, 0, 0),
            ];

        chapterTimeSpans.AddRange(
            queueItem.BlackFrames
                .Where(bf => bf.Selected)
                .Select(bf => bf.End.Subtract(bf.Duration / 2))
        );



        if (!ffmpeg.IsMatroska(queueItem.FileName, FfmpegLog))
        {
            var matroskaPath = MkvTool.RemuxToMatroska(queueItem.FileName);
            queueItem.FileName = matroskaPath;
        }

        var chapters = MkvToolNix.GenerateChapters(chapterTimeSpans);

        MkvTool.InjectChapters(queueItem.FileName, chapters);
    }

    private void FfmpegLog(object sender, DataReceivedEventArgs e)
    {
        if (settings.ShowLog)
        {
            logForm.Invoke(
                new MethodInvoker(
                    delegate ()
                    {
                        logForm.Log(e.Data);
                    }
                )
            );
        }
    }

    private void ProcessMKVSplit(QueueItem queueItem)
    {
        var segments = queueItem.GetSegments();

        var outputPath = Path.Combine(Path.GetDirectoryName(queueItem.FileName), "output");
        Directory.CreateDirectory(outputPath);

        var newName = Path.Combine(outputPath, Path.GetFileNameWithoutExtension(queueItem.FileName) + "-%03d.mkv");

        MkvTool.SplitSegments(queueItem.FileName, newName, segments.ToList(), FfmpegLog);
    }

    private void ProcessSplitAndEncode(QueueItem queueItem)
    {
        var segments = queueItem.GetSegments();

        var encodingFileName = queueItem.FileName;

        if (!renamer.RenameOriginalIfWanted(ref encodingFileName))
            return;

        for (int i = 0; i < segments.Count; i++)
        {
            var newName = renamer.GetNewName(queueItem.FileName, i + 1);

            ffmpeg.EncodeSegments(encodingFileName, newName, settings.FmpegArguments.Replace("\r\n", " "), segments[i], FfmpegLog);
        }
    }
}
