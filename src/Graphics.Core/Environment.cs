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
            _camera = new Camera();
            this.AddDefaultEntities();
        }

        #endregion

        #region Attributes And Properties

        private List<Entity> _entities;
        private Camera _camera;

        public double Width { get; set; }
        public double Height { get; set; }

        #endregion

        #region PrivateMethods

        private Matrix4 GetScreenTransformation()
        {
            var halfWidth = this.Width / 2;
            var halfHeight = this.Height / 2;
            var centerXForPixels = halfWidth - 1 / 2;
            var centerYForPixels = halfHeight - 1 / 2; // - 1/2 for fixing pixels center

            var translate = new Vector3(centerXForPixels, centerYForPixels, 0);
            var scale = new Vector3(halfWidth, halfHeight, 1);

            var translation = Matrix4.CreateTranslation(translate);
            var scaling = Matrix4.CreateScale(scale);

            return translation * scaling;
        }

        private Matrix4 GetOrthographicViewVolumeTransformation()
        {
            double l = 0;
            double r = 0;
            double t = 0;
            double b = 0;
            double n = 0;
            double f = 0;

            var translateX = - ((l + r) / 2);
            var translateY = - ((b + t) / 2);
            var translateZ = - ((n + f) / 2);

            var scaleX = 2 / r - l;
            var scaleY = 2 / t - b;
            var scaleZ = 2 / n - f;

            var translation = Matrix4.CreateTranslation(new Vector3(translateX, translateY, translateZ));
            var scaling = Matrix4.CreateScale(new Vector3(scaleX, scaleY, scaleZ));
            //should be optimized caus of fixed values
            return translation * scaling;
        }

        //Implement this shit
        private Matrix4 GetWorldSpaceToCameraSpace()
        {
            var rotation = new Matrix4(_camera.Gaze.X, _camera.Gaze.Y, _camera.Gaze.Z, 0,
                                       _camera.Up.X,    _camera.Up.Y,   _camera.Up.Z,  0,
                                       _camera.Side.X, _camera.Side.Y, _camera.Side.Z, 0,
                                              0,              0,              0,       1);
            var cameraPosition = _camera.Position.ToVector3();
            var translation = Matrix4.CreateTranslation(-cameraPosition);

            return translation * rotation;
        }

        private Matrix4 GetRenderTransformation()
        {
            return this.GetScreenTransformation() * 
                   this.GetOrthographicViewVolumeTransformation()  * 
                   /* * this.GetPerspectiveProjection() */
                   this.GetWorldSpaceToCameraSpace();
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
            var transformation = this.GetRenderTransformation();

            foreach (var entity in _entities)
            {
                entity.OnRender(drawingContext, transformation);
            }
        }

        #endregion
    }
}