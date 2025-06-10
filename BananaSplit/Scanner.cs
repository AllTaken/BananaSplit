using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BananaSplit;
public partial class Scanner(StatusBarManager statusBarManager, Settings settings, LogForm logForm)
{
    private readonly Ffmpeg ffmpeg = new();

    public void StartScanningThread(Action<List<QueueItem>> itemsAction, List<QueueItem> queueItems)
    {
        new Thread(() => ScanQueueItems(itemsAction, queueItems)).Start();
    }


    private void ScanQueueItems(Action<List<QueueItem>> itemsAction, List<QueueItem> inputItems)
    {        
        ShowLog();

        statusBarManager.SetStatusBarProgressBarValue(0, inputItems.Count);

        var i = 0;
        int countedFrames = 0;

        var unscannedItems = inputItems.Where(qi => !qi.Scanned);
        // Get all video durations and fps for a better progress bar
        var totalNumFrames = GetDurationsAndFps(unscannedItems);

        // Parse items
        List<QueueItem> queueItems = [];
        foreach (var item in unscannedItems)
        {
            i++;

            statusBarManager.SetStatusBarLabelValue($"Detecting frames for {Path.GetFileName(item.FileName)}");
            item.Scanned = true;
            item.LastScanned = DateTime.Now;
            item.BlackFrames = ffmpeg.DetectBlackFrameIntervals(item.FileName, settings.BlackFrameDuration, settings.BlackFrameThreshold, settings.BlackFramePixelThreshold, (s, e) =>
            {
                string logMsg = e.Data;
                Log(logMsg);
                if (logMsg == null)
                {
                    return;
                }

                string framePattern = @"(\sframe:|frame=\s+)(?'frame'\d+)";
                Regex regex = new Regex(framePattern, RegexOptions.Singleline);

                Match m = regex.Match(logMsg);
                if (m.Success && int.TryParse(m.Groups["frame"].Value, out int frame))
                {
                    statusBarManager.SetStatusBarProgressBarValue(countedFrames + frame, totalNumFrames);
                }
            });
            countedFrames += item.NumFrames;
            statusBarManager.SetStatusBarProgressBarValue(countedFrames, totalNumFrames);

            var frameNum = 1;
            foreach (var frame in item.BlackFrames)
            {
                long offset = (long)(settings.ReferenceFrameOffset * TimeSpan.TicksPerSecond);
                TimeSpan referenceFramePosition = frame.End.Add(new TimeSpan(offset));

                statusBarManager.SetStatusBarLabelValue($"Generating frame {frameNum} of {item.BlackFrames.Count} at {referenceFramePosition}");
                frame.ReferenceFrame = new ReferenceFrame();
                var image = Utilities.BytesToImage(ffmpeg.ExtractFrame(item.FileName, referenceFramePosition, FfmpegLog));
                if (image != null)
                {
                    image.Save(@$"imagecache\{frame.Id}.png");
                    frame.ReferenceFrame.ImageFile = @$"imagecache\{frame.Id}.png";
                }                

                frameNum++;
            }

            queueItems.Add(item);
        }

        itemsAction(queueItems);

        statusBarManager.SetStatusBarLabelValue("Done!");
        statusBarManager.ClearStatusBarProgressBarValue();
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

    private void ShowLog()
    {
        if (settings.ShowLog)
            logForm.ShowLog();
    }

    private int GetDurationsAndFps(IEnumerable<QueueItem> items)
    {
        var totalNumFrames = 0;
        foreach (var item in items)
        {
            item.Duration = ffmpeg.GetDuration(item.FileName, (s, e) => LogMsgAndParseFps(e, item));

            item.NumFrames = (int)Math.Ceiling(item.Duration.TotalSeconds * (item.Fps ?? 0));
            totalNumFrames += item.NumFrames;
        }

        return totalNumFrames;
    }

    private void LogMsgAndParseFps(DataReceivedEventArgs e, QueueItem item)
    {
        string logMsg = e.Data;
        Log(logMsg);
        if (logMsg == null || item.Fps != null)
            return;

        Regex regex = FpsRegEx();

        Match m = regex.Match(logMsg);
        if (!m.Success || !float.TryParse(m.Groups["fps"].Value, out float fps))
            return;

        item.Fps = fps;
    }

    private void Log(string text)
    {
        if (logForm.Visible)
        {
            logForm.Invoke(new MethodInvoker(
               delegate ()
               {
                   logForm.Log(text);
               })
           );
        }
    }

    [GeneratedRegex(@"(?'fps'[.\d]+) fps,", RegexOptions.Singleline)]
    private static partial Regex FpsRegEx();
}
