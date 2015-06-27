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
        public Environment(Vector2 screenSize)
        {
            _entities = new List<Entity>();
            _screenSize = screenSize;
        }

        private List<Entity> _entities;
        private Vector2 _screenSize;

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
                new Point(_screenSize.X / 2,_screenSize.Y / 2),
                10, 
                10);
        }
    }
}
