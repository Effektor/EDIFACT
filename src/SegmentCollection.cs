using EDIFACT.Segments;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace EDIFACT
{
    public class SegmentCollection : IEnumerable<Segment>
    {
        internal List<Segment> segments = new List<Segment>();

        public Segment this[int i] => segments[i];
            

        List<Segment> l;
        public SegmentCollection()
        {
        }

        public SegmentCollection(IEnumerable<Segment> segments)
        {            
            this.segments.AddRange(segments);
        }


        public void AddSegments(IEnumerable<Segment> enumerable)
        {            
            this.segments.AddRange(enumerable.Select(x => x));
        }


        public Segment AddSegment(string tag)
        {
            var s = new Segment(tag);
            segments.Add(s);
            return s;
        }

        public void Add(Segment segment)
        {
            this.segments.Add(segment);
        }

        IEnumerator<Segment> IEnumerable<Segment>.GetEnumerator()
        {
            return ((IEnumerable<Segment>)this.segments).GetEnumerator();
        }

        public IEnumerator<string> GetStringEnumerator()
        {
            return segments.Select(x => x.ToString()).GetEnumerator();
        }

        public IEnumerator GetEnumerator()
        {
            return ((IEnumerable<Segment>)this.segments).GetEnumerator();
        }
    }
}
