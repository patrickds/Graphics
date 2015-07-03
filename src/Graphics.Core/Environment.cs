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
        #region Constructors

        public Environment()
        {
            _entities = new List<Entity>();
            this.AddDefaultEntities();
        }

        #endregion

        #region Attributes And Properties

        private List<Entity> _entities;

        public double Width { get; set; }
        public double Height { get; set; }

        #endregion

        #region PrivateMethods

        private Matrix4 GetScreenTransformation()
        {
            var halfWidth = this.Width / 2;
            var halfHeight = this.Height / 2;
            var centerXForPixels = halfWidth - 1 / 2;
            var centerYForPixels = halfHeight - 1 / 2;

            var translate = new Vector3(centerXForPixels, centerYForPixels, 0);
            var scale = new Vector3(halfWidth, halfHeight, 1);

            var translation = Matrix4.CreateTranslation(translate);
            var scaling = Matrix4.CreateScale(scale);

            return translation * scaling;
        }

        private void AddDefaultEntities()
        {
            _entities.Add(new Origin());
        }

        #endregion

        #region Public Methods

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
            var transformation = this.GetScreenTransformation();

            foreach (var entity in _entities)
            {
                entity.OnRender(drawingContext, transformation);
            }
        }

        #endregion
    }
}