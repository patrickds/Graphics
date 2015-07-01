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
        }

        #endregion

        #region Attributes And Properties

        private List<Entity> _entities;

        public double Width
        {
            get;
            set;
        }
        public double Height { get; set; }

        private Vector4 _origin = new Vector4(0, 0, 0, 1);

        #endregion

        #region PrivateMethods

        private Matrix4 GetTransformation()
        {
            var halfWidth = this.Width / 2;
            var halfHeight = this.Height / 2;

            var translate = new Vector3(halfWidth - 1 / 2, halfHeight - 1 / 2, 0);
            var scale = new Vector3(halfWidth, halfHeight, 1);

            var translation = Matrix4.CreateTranslation(translate);
            var scaling = Matrix4.CreateScale(scale);

            return translation * scaling;
        }

        private void Draw(DrawingContext drawingContext, Matrix4 transformation)
        {
            this.DrawOrigin(drawingContext, transformation);
        }

        private void DrawOrigin(DrawingContext drawingContext, Matrix4 transformation)
        {
            var origin = transformation * _origin;

            var bottomLeft = transformation * new Vector4(-0.2, -0.2, -0.2, 1);
            var bottomRight = transformation * new Vector4(0.2, -0.2, -0.2, 1);
            var top = transformation * new Vector4(0, 0.2, -0.2, 1);

            drawingContext.DrawEllipse(Brushes.Black, new Pen(Brushes.Black, 2), new Point(origin.X, origin.Y), 1, 1);

            drawingContext.DrawLine(new Pen(Brushes.Black, 2), new Point(bottomLeft.X, bottomLeft.Y), new Point(top.X, top.Y));
            drawingContext.DrawLine(new Pen(Brushes.Black, 2), new Point(top.X, top.Y), new Point(bottomRight.X, bottomRight.Y));
            drawingContext.DrawLine(new Pen(Brushes.Black, 2), new Point(bottomLeft.X, bottomLeft.Y), new Point(bottomRight.X, bottomRight.Y));
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
            var transformation = this.GetTransformation();
            this.Draw(drawingContext, transformation);

            foreach (var entity in _entities)
            {
                entity.OnRender(drawingContext, transformation);
            }
        }

        #endregion
    }
}