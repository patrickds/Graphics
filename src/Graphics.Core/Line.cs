using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Graphics.Core
{
    public class Line
    {
        public Line(Point start, Point end)
        {
            _start = start;
            _end = end;
        }

        private readonly Point _start;
        public Point Start { get { return _start; } }

        private readonly Point _end;
        public Point End { get { return _end; } }
    }
}