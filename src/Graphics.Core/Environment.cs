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

        public void Add(Entity entity)
        {
            _entities.Add(entity);
        }

        public void Remove(Entity entity)
        {
            _entities.Remove(entity);
        }

        public void Transform(Matrix4 matrix)
        {
            foreach (var entity in _entities)
            {
                entity.Transform(matrix);
            }
        }

        public void OnRender(DrawingContext drawingContext)
        {
            var transformation = this.GetTransformation();

            foreach (var entity in _entities)
            {
                entity.OnRender(drawingContext, transformation);
            }
        }

        private Matrix4 GetTransformation()
        {
            var halfWidth = this.Width / 2;
            var halfHeight = this.Height / 2;

            var translation = Matrix4.CreateTranslation(new Vector3(halfWidth - 1 / 2, halfHeight - 1 / 2, 0));
            var scaling = Matrix4.CreateScale(new Vector3(halfWidth, halfHeight, 1));

            return translation * scaling;
        }
    }
}
