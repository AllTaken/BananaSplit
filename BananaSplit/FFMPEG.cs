using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;

namespace BananaSplit
{
    public partial class Ffmpeg
    {
        private const string FfMpeg = "ffmpeg.exe";
        private const string FfProbe = "ffprobe.exe";

        private Process FfProcess { get; set; }

        public Ffmpeg()
        {
            FfProcess = new Process();

            FfProcess.StartInfo.UseShellExecute = false;
            FfProcess.StartInfo.RedirectStandardError = true;
            FfProcess.StartInfo.FileName = FfMpeg;
            FfProcess.StartInfo.CreateNoWindow = true;
        }

        public bool IsMatroska(string filePath, DataReceivedEventHandler outputHandler)
        {
            FfProcess.StartInfo.FileName = FfProbe;
            FfProcess.StartInfo.Arguments = $"\"{filePath}\"";

            var output = "";

            void errorDataHandler(object s, DataReceivedEventArgs evt)
            {
                output += evt.Data + "\n"; outputHandler(s, evt);
            }

            FfProcess.ErrorDataReceived += errorDataHandler;

            FfProcess.Start();
            FfProcess.BeginErrorReadLine();

            FfProcess.WaitForExit();
            FfProcess.ErrorDataReceived -= errorDataHandler;
            FfProcess.CancelErrorRead();

            return IsMatroskaRegex().IsMatch(output);
        }

        public TimeSpan GetDuration(string filePath, DataReceivedEventHandler outputHandler)
        {
            TimeSpan duration = TimeSpan.FromSeconds(0);

            FfProcess.StartInfo.FileName = FfProbe;
            FfProcess.StartInfo.Arguments = $"\"{filePath}\"";
            FfProcess.StartInfo.RedirectStandardOutput = false;
            FfProcess.StartInfo.RedirectStandardError = true;

            var output = "";
            void errorDataHandler(object s, DataReceivedEventArgs evt)
            {
                output += evt.Data + "\n"; outputHandler(s, evt);
            }
            FfProcess.ErrorDataReceived += errorDataHandler;

            FfProcess.Start();
            FfProcess.BeginErrorReadLine();

            FfProcess.WaitForExit();
            FfProcess.ErrorDataReceived -= errorDataHandler;
            FfProcess.CancelErrorRead();

            string pattern = @"^\s+Duration: (?<duration>\d+(?:.\d+)*)";

            var match = Regex.Match(output, pattern, RegexOptions.Multiline);
            if (match.Success)
            {
                var durationString = match.Groups["duration"].Value;
                TimeSpan.TryParse(durationString, new CultureInfo("en-US"), out duration);
            }
            return duration;
        }

        public byte[] ExtractFrame(string filePath, TimeSpan time, DataReceivedEventHandler outputHandler)
        {
            var timespan = $"{time.Hours:D2}:{time.Minutes:D2}:{time.Seconds:D2}.{time.Milliseconds}";

            FfProcess.StartInfo.FileName = FfMpeg;
            FfProcess.StartInfo.Arguments = $"-ss {timespan} -i \"{filePath}\" -vframes 1 -c:v png -f image2pipe -";
            FfProcess.StartInfo.RedirectStandardOutput = true;
            FfProcess.StartInfo.RedirectStandardError = true;

            // ffmpeg uses stderr for some reason
            void errorDataHandler(object s, DataReceivedEventArgs evt) => outputHandler(s, evt);
            FfProcess.ErrorDataReceived += errorDataHandler;

            FfProcess.Start();
            FfProcess.BeginErrorReadLine();

            using MemoryStream ms = new MemoryStream();
            FfProcess.StandardOutput.BaseStream.CopyTo(ms);

            FfProcess.WaitForExit();
            FfProcess.CancelErrorRead();
            FfProcess.ErrorDataReceived -= errorDataHandler;

            return ms.ToArray();
        }


        public void EncodeSegments(string source, string destination, string arguments, Segment segment, DataReceivedEventHandler outputHandler)
        {
            arguments = arguments.Replace("{source}", source);
            arguments = arguments.Replace("{destination}", destination);
            arguments = arguments.Replace("{start}", segment.Start.ToString("c"));
            arguments = arguments.Replace("{end}", segment.End.ToString("c"));
            arguments = arguments.Replace("{duration}", segment.Duration.ToString("c"));

            FfProcess.StartInfo.FileName = FfMpeg;
            FfProcess.StartInfo.Arguments = arguments;
            FfProcess.StartInfo.RedirectStandardOutput = false;
            FfProcess.StartInfo.RedirectStandardError = true;

            // ffmpeg uses stderr for some reason
            void errorDataHandler(object s, DataReceivedEventArgs evt) => outputHandler(s, evt);
            FfProcess.ErrorDataReceived += errorDataHandler;
            FfProcess.Start();
            FfProcess.BeginErrorReadLine();

            FfProcess.WaitForExit();
            FfProcess.ErrorDataReceived -= errorDataHandler;
            FfProcess.CancelErrorRead();
        }

        public ICollection<BlackFrame> DetectBlackFrameIntervals(string filePath, double blackFrameDuration, double blackFrameThreshold, double blackFramePixelThreshold, DataReceivedEventHandler outputHandler)
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-US");
            var arguments = $"-i \"{filePath}\" -vf blackdetect=d={blackFrameDuration}:pic_th={blackFrameThreshold}:pix_th={blackFramePixelThreshold} -an -f null -y /NUL";
            var output = RunBlackFrameDetection(arguments, outputHandler);

            string pattern = @"(?:blackdetect.+)(?:black_start:)(?<start>\d+(?:.\d+)*) (?:black_end:)(?<end>\d+(?:.\d+)*) (?:black_duration:)(?<duration>\d+(?:.\d+)*)";
            Regex regex = new Regex(pattern, RegexOptions.Multiline);
            var matches = regex.Matches(output);

            var frames = new List<BlackFrame>();
            foreach (var groups in matches.Select(match => match.Groups))
            {
                BlackFrame frame = ParseBlackFrame(groups);

                if (frame == null)
                    continue;

                frames.Add(frame);
            }

            return frames;
        }

        private static BlackFrame ParseBlackFrame(GroupCollection groups)
        {
            try
            {
                if (double.TryParse(groups["start"].Value, out var start) &&
                    double.TryParse(groups["end"].Value, out var end) &&
                    double.TryParse(groups["duration"].Value, out var duration))
                {
                    return new BlackFrame()
                    {
                        Start = TimeSpan.FromSeconds(start),
                        End = TimeSpan.FromSeconds(end),
                        Duration = TimeSpan.FromSeconds(duration),
                        Marker = GetMiddle(TimeSpan.FromSeconds(duration), TimeSpan.FromSeconds(end)),
                    };
                }
            }
            catch
            {
                return null;
            }
            return null;
        }

        private static TimeSpan GetMiddle(TimeSpan Duration, TimeSpan End)
        {
            var halfDuration = new TimeSpan(Duration.Ticks / 2);

            return End.Subtract(halfDuration);
        }

        private string RunBlackFrameDetection(string arguments, DataReceivedEventHandler outputHandler)
        {
            FfProcess.StartInfo.FileName = FfMpeg;
            FfProcess.StartInfo.RedirectStandardError = true;
            FfProcess.StartInfo.Arguments = arguments;

            // ffmpeg uses stderr for some reason
            var output = "";
            void errorDataHandler(object s, DataReceivedEventArgs evt)
            {
                output += evt.Data + "\n"; outputHandler(s, evt);
            }
            FfProcess.ErrorDataReceived += errorDataHandler;

            FfProcess.Start();
            FfProcess.BeginErrorReadLine();

            FfProcess.WaitForExit();
            FfProcess.ErrorDataReceived -= errorDataHandler;
            FfProcess.CancelErrorRead();
            return output;
        }

        [GeneratedRegex(@"^Input #\d+, matroska", RegexOptions.Multiline)]
        private static partial Regex IsMatroskaRegex();
    }
}
