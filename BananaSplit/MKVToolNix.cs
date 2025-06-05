using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Xml.Serialization;

namespace BananaSplit
{
    public class MkvToolNix
    {
        private Process MkvProcess { get; set; }

        public MkvToolNix()
        {
            MkvProcess = new Process();

            MkvProcess.StartInfo.UseShellExecute = false;
            MkvProcess.StartInfo.RedirectStandardError = true;
            MkvProcess.StartInfo.RedirectStandardOutput = true;
            MkvProcess.StartInfo.CreateNoWindow = true;
        }

        public string RemuxToMatroska(string filepath)
        {
            var basename = Path.GetFileNameWithoutExtension(filepath);
            var path = Path.GetDirectoryName(filepath);
            var extensionlessPath = Path.Combine(path, basename);

            MkvProcess.StartInfo.FileName = "mkvmerge.exe";
            MkvProcess.StartInfo.Arguments = $"-o \"{extensionlessPath}.mkv\" \"{filepath}\"";

            MkvProcess.Start();
            MkvProcess.WaitForExit();

            return extensionlessPath + ".mkv";
        }

        public void InjectChapters(string filepath, Chapters chapters)
        {
            var temporaryXmlFile = Path.GetRandomFileName();
            var temporaryOutputFile = Path.GetRandomFileName();

            XmlSerializer serializer = new XmlSerializer(typeof(Chapters));
            XmlSerializerNamespaces namespaces = new XmlSerializerNamespaces();
            namespaces.Add("", "");

            using (StreamWriter writer = new(temporaryXmlFile))
            {
                serializer.Serialize(writer, chapters, namespaces);
                writer.Close();
            }

            MkvProcess.StartInfo.FileName = "mkvmerge.exe";
            MkvProcess.StartInfo.Arguments = $"--chapters \"{temporaryXmlFile}\" -o \"{temporaryOutputFile}\" \"{filepath}\"";
            MkvProcess.Start();

            MkvProcess.WaitForExit();

            File.Delete(filepath);
            File.Move(temporaryOutputFile, filepath);
            File.Delete(temporaryXmlFile);
        }

        public static Chapters GenerateChapters(IEnumerable<TimeSpan> segments)
        {
            Chapters chapters = new()
            {
                EditionEntry = new()
                {
                    EditionFlagHidden = 0,
                    EditionFlagDefault = 0,
                    EditionUID = 1,
                    ChapterAtom = []
                }
            };

            int i = 1;
            foreach (var segment in segments)
            {
                chapters.EditionEntry.ChapterAtom.Add(GenerateChapter(segment, i++));
            }

            return chapters;
        }

        private static ChapterAtom GenerateChapter(TimeSpan segment, int index)
        {
            var chapterAtom = new ChapterAtom();
            var timestamp = $"{segment.Hours:D2}:{segment.Minutes:D2}:{segment.Seconds:D2}.{segment.Milliseconds}";

            chapterAtom.ChapterUID = index;
            chapterAtom.ChapterFlagHidden = 0;
            chapterAtom.ChapterFlagEnabled = 1;
            chapterAtom.ChapterTimeStart = timestamp;
            chapterAtom.ChapterDisplay = new ChapterDisplay()
            {
                ChapterLanguage = "eng",
                ChapterString = timestamp
            };

            return chapterAtom;
        }

        public void SplitSegments(string source, string destination, List<Segment> segments, DataReceivedEventHandler outputHandler)
        {
            if (segments.Count <= 1)
            {
                File.Copy(source, destination.Replace("%03d", "1"), true);
                return;
            }

            // make sure the segments are in order
            segments.Sort((a, b) => a.End.CompareTo(b.End));

            // last segment's end would be the end of the file
            segments.RemoveAt(segments.Count - 1); 

            var cuts = string.Join(",", segments.Select(segment => $"{segment.End.Hours:D2}:{segment.End.Minutes:D2}:{segment.End.Seconds:D2}.{segment.End.Milliseconds}"));

            MkvProcess.StartInfo.FileName = "mkvmerge.exe";
            MkvProcess.StartInfo.Arguments = $"-o \"{destination}\" --split timecodes:{cuts} \"{source}\"";
            MkvProcess.StartInfo.RedirectStandardOutput = false;
            MkvProcess.StartInfo.RedirectStandardError = true;

            MkvProcess.ErrorDataReceived += (s, evt) => outputHandler(s, evt);
            MkvProcess.Start();

            MkvProcess.WaitForExit();
        }




    }



    [XmlRoot(ElementName = "ChapterDisplay")]
    public class ChapterDisplay
    {
        [XmlElement(ElementName = "ChapterString")]
        public string ChapterString { get; set; }
        [XmlElement(ElementName = "ChapterLanguage")]
        public string ChapterLanguage { get; set; }
    }

    [XmlRoot(ElementName = "ChapterAtom")]
    public class ChapterAtom
    {
        [XmlElement(ElementName = "ChapterUID")]
        public int ChapterUID { get; set; }
        [XmlElement(ElementName = "ChapterTimeStart")]
        public string ChapterTimeStart { get; set; }
        [XmlElement(ElementName = "ChapterFlagHidden")]
        public int ChapterFlagHidden { get; set; }
        [XmlElement(ElementName = "ChapterFlagEnabled")]
        public int ChapterFlagEnabled { get; set; }
        [XmlElement(ElementName = "ChapterDisplay")]
        public ChapterDisplay ChapterDisplay { get; set; }
    }

    [XmlRoot(ElementName = "EditionEntry")]
    public class EditionEntry
    {
        [XmlElement(ElementName = "EditionFlagHidden")]
        public int EditionFlagHidden { get; set; }
        [XmlElement(ElementName = "EditionFlagDefault")]
        public int EditionFlagDefault { get; set; }
        [XmlElement(ElementName = "EditionUID")]
        public int EditionUID { get; set; }
        [XmlElement(ElementName = "ChapterAtom")]
        public List<ChapterAtom> ChapterAtom { get; set; }
    }

    [XmlRoot(ElementName = "Chapters")]
    public class Chapters
    {
        [XmlElement(ElementName = "EditionEntry")]
        public EditionEntry EditionEntry { get; set; }
    }
}
