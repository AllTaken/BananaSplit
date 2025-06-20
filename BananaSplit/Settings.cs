﻿using AutoMapper;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Windows.Forms;

namespace BananaSplit
{
    public enum ProcessingType
    {
        [Display(Name = "Matroska Chapters")]
        MatroskaChapters,
        [Display(Name = "Split and Encode")]
        SplitAndEncode,
        [Display(Name = "MKVToolNix Split")]
        MkvToolNixSplit
    }

    public enum RenameType
    {
        [Display(Name = "Prefix")]
        Prefix,
        [Display(Name = "Suffix")]
        Suffix,
        [Display(Name = "Append After")]
        AppendAfter,
        [Display(Name = "Replace")]
        Replace,
        [Display(Name = "Increment")]
        Increment
    }


    public class Settings
    {
        private readonly IMapper mapper;

        public double BlackFrameDuration { get; set; } = 0.04;
        public double BlackFrameThreshold { get; set; } = 0.98;
        public double BlackFramePixelThreshold { get; set; } = 0.15;
        public string FmpegArguments { get; set; } = "-i \"{source}\" -ss {start} -t {duration} -c:v copy -c:a copy -map 0 \"{destination}\"";
        public ProcessingType ProcessType { get; set; } = ProcessingType.MkvToolNixSplit;
        public bool ShowLog { get; set; } = false;
        public bool DeleteOriginal { get; set; } = false;
        public double ReferenceFrameOffset { get; set; } = 1;
        public string RenameFindText { get; set; } = "";
        public string RenameNewText { get; set; } = "{i}";
        public RenameType RenameType { get; set; } = RenameType.Increment;
        public bool RenameOriginal { get; set; } = true;
        public int IncrementMultiplier { get; set; } = 2;
        public int StartIndex { get; set; } = 1;
        public int Padding { get; set; } = 2;
        public int SplitterDistance { get; set; } = 700;
        public int? Top { get; set; } = null;
        public int? Left { get; set; } = null;
        public int? Width { get; set; } = null;
        public int? Height { get; set; } = null;
        public FormWindowState? WindowState { get; set; }
        public int ThumbnailSize { get; set; } = 256;

        public Settings(IMapper mapper)
        {            
            this.mapper = mapper;
            Load();
        }

        [JsonConstructor]
        protected Settings()
        {            
        }

        public void Load()
        {
            try
            {
                var json = File.ReadAllText("Settings.json");
                mapper.Map(JsonConvert.DeserializeObject<Settings>(json), this);
            }
            catch
            {
                Save();
            }
        }


        public void Save()
        {
            var json = JsonConvert.SerializeObject(this, Formatting.Indented);

            File.WriteAllText("Settings.json", json);
        }
    }
}
