using Graphics.Math;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Graphics.Core
{
    public class Environment
    {
        public Environment()
        {
            _entities = new List<Entity>();
        }

        private List<Entity> _entities;

        public double Width { get; set; }
        public double Height { get; set; }

        private void Add(Entity entity)
        {
            _entities.Add(entity);
        }

        private void Remove(Entity entity)
        {
            _entities.Remove(entity);
        }
        
        public void OnRender(DrawingContext drawingContext)
        {
            drawingContext.DrawEllipse(
                Brushes.Black, 
                new Pen(Brushes.Blue, 4),
                new Point(this.Width / 2, this.Height / 2),
                10, 
                10);
        }
    }
}
