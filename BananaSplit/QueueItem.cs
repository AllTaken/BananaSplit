using System;
using System.Collections.Generic;
using System.Linq;

namespace BananaSplit
{
    public class QueueItem(string fileName)
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string FileName { get; set; } = fileName;
        public bool Scanned { get; set; } = false;
        public DateTime LastScanned { get; set; }
        public ICollection<BlackFrame> BlackFrames { get; set; } = [];
        public TimeSpan Duration { get; set; }
        public float? Fps { get; set; } = null;
        public int NumFrames { get; set; } = 0;

        public List<Segment> GetSegments()
        {
            List<Segment> segments = [];

            var selectedFrames = BlackFrames.Where(bf => bf.Selected).ToList();

            if (selectedFrames.Count > 0)
            {
                Segment start = GetStartSegment(selectedFrames);

                Segment end = GetEndingSegment(selectedFrames);

                List<Segment> additionalSegments = GetAdditionalSegments(selectedFrames);

                segments.Add(start);
                segments.AddRange(additionalSegments);
                segments.Add(end);
            }

            return segments;
        }

        private static List<Segment> GetAdditionalSegments(List<BlackFrame> selectedFrames)
        {
            var additionalSegments = new List<Segment>();

            // Loop through to get any additional segments.
            for (var i = 0; i < selectedFrames.Count - 1; i++)
            {
                additionalSegments.Add(new Segment()
                {
                    Start = selectedFrames[i].Marker,
                    End = selectedFrames[i + 1].Marker
                });
            }

            return additionalSegments;
        }

        private Segment GetEndingSegment(List<BlackFrame> selectedFrames)
        {
            // The last segment starts at the end of the last black frame to the end of the video
            return new Segment()
            {
                Start = selectedFrames[^1].Marker,
                End = Duration
            };
        }

        private static Segment GetStartSegment(List<BlackFrame> selectedFrames)
        {
            // The first segment starts at the beginning of the video and ends at the start of the first black frame
            return new Segment()
            {
                Start = new TimeSpan(0, 0, 0),
                End = selectedFrames[0].Marker
            };
        }
    }
}
