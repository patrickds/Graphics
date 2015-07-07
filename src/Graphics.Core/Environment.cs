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
            _camera = new Camera(1, 100, _width / _height);
            this.AddDefaultEntities();
        }

        #endregion

        #region Attributes And Properties

        private List<Entity> _entities;
        private Camera _camera;
        private double _width = 800d;
        private double _height = 600d;
       
        #endregion

        #region Private Methods

        #region Transformations

        public Matrix4 GetRenderTransformation2()
        {
            //camera coordinate system
            var u = _camera.Right;
            var v = _camera.Up;
            var n = _camera.Gaze;

            //view volume params
            double left = _camera.LeftBottomNear.X;
            double right = _camera.RightTopFar.X;
            double bottom = _camera.LeftBottomNear.Y;
            double top = _camera.RightTopFar.Y;
            double near = _camera.LeftBottomNear.Z;
            double far = 2;

            //camera translation transformation: 
            //map world origin to camera origin
            Matrix4 t0 = new Matrix4(
                1, 0, 0, -_camera.Position.X,
                0, 1, 0, -_camera.Position.Y,
                0, 0, 1, -_camera.Position.Z,
                0, 0, 0, 1);

            //camera rotation transformation: 
            //rotate world to align with camera coordinate system (coordinates will now be in camera vector space)
            Matrix4 t1 = new Matrix4(
                u.X, u.Y, u.Z, 0,
                v.X, v.Y, v.Z, 0,
                n.X, n.Y, n.Z, 0,
                0, 0, 0, 1);

            //perspective transformation:
            //distort world in a way it makes closer things bigger and distant smaller
            Matrix4 t2 = new Matrix4(
                near, 0, 0, 0,
                0, near, 0, 0,
                0, 0, near + far, -far * near,
                0, 0, 1, 0);

            //parallel projection transformation:
            //map world coordinates into a canonical view volume (ranging from -1 to 1)
            Matrix4 t3 = new Matrix4(
                2.0d / (right - left), 0, 0, -((right + left) / (right - left)),
                0, 2.0d / (top - bottom), 0, -((top + bottom) / (top - bottom)),
                0, 0, 2.0d / (near - far), -((near + far) / (near - far)),
                0, 0, 0, 1);

            //viewport mapping transformation:
            //convert the canonical coordinates to the viewport coordinates, so it scales our view to fit the viewport
            Matrix4 t4 = new Matrix4(
                _width / 2.0d, 0, 0, (_width / 2.0d) - 0.5d,
                0, _height / 2.0d, 0, (_height / 2.0d) - 0.5d,
                0, 0, 1, 0,
                0, 0, 0, 1);

            Matrix4 projectionTransformMatrix = t4 * t3 * t2 * t1 * t0;

            return projectionTransformMatrix;
        }

        private Matrix4 GetRenderTransformation()
        {
            return
                this.GetScreenTransformation() *
                this.GetOrthographicViewVolumeTransformation() *
                this.GetPerspectiveTransformation() *
                this.GetWorldSpaceToCameraSpace();
        }

        private Matrix4 GetScreenTransformation()
        {
            var halfWidth = _width / 2d;
            var halfHeight = _height / 2d;

            var translate = new Vector3(halfWidth - 0.5d, halfHeight - 0.5d, 0);
            var scale = new Vector3(halfWidth, halfHeight, 1);

            return Matrix4.CreateTranslation(translate) * Matrix4.CreateScale(scale);
        }

        private Matrix4 GetOrthographicViewVolumeTransformation()
        {
            double left = _camera.LeftBottomNear.X;
            double right = _camera.RightTopFar.X;
            double bottom = _camera.LeftBottomNear.Y;
            double top = _camera.RightTopFar.Y;
            double near = _camera.LeftBottomNear.Z;
            double far = _camera.RightTopFar.Z;

            var translateX = -(left + right) / 2d;
            var translateY = -(bottom + top) / 2d;
            var translateZ = -(near + far) / 2d;

            var scaleX = 2d / (right - left);
            var scaleY = 2d / (top - bottom);
            var scaleZ = 2d / (near - far);

            var translation = Matrix4.CreateTranslation(new Vector3(translateX, translateY, translateZ));
            var scaling = Matrix4.CreateScale(new Vector3(scaleX, scaleY, scaleZ));

            return translation * scaling;

            //Optimized version caus of constant values
            //return new Matrix4(
            //    scaleX, 0, 0, -((r + l) / (r - l)),
            //    0, scaleY, 0, -((t + b) / (t - b)),
            //    0, 0, scaleZ, -((n + f) / (n - f)),
            //    0, 0, 0, 1);
        }

        private Matrix4 GetWorldSpaceToCameraSpace()
        {
            Vector4 u = _camera.Right;
            Vector4 v = _camera.Up;
            Vector4 n = _camera.Gaze;

            var rotation = new Matrix4(
                u.X, u.Y, u.Z, 0,
                v.X, v.Y, v.Z, 0,
                n.X, n.Y, n.Z, 0,
                0, 0, 0, 1);

            var cameraPosition = _camera.Position.ToVector3();
            var translation = Matrix4.CreateTranslation(-cameraPosition);

            return rotation * translation;
        }

        private Matrix4 GetPerspectiveTransformation()
        {
            var near = _camera.Near;
            var far = _camera.Far;

            var scaling = Matrix4.CreateScale(new Vector3(near, near, near + far));
            var translation = Matrix4.CreateTranslation(new Vector3(0, 0, -far * near));

            return new Matrix4(
                near, 0, 0, 0,
                0, near, 0, 0,
                0, 0, near + far, -far * near,
                0, 0, 1, 0);

            //return translation * scaling;
        }

        #endregion

        private void UpdateAspectRatio()
        {
            _camera.UpdateAspectRatio(_width / _height);
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

        public void SetSize(double width, double height)
        {
            _width = width;
            _height = height;
            this.UpdateAspectRatio();
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
            var transformation = this.GetRenderTransformation2());

            foreach (var entity in _entities)
            {
                entity.OnRender(drawingContext, transformation);
            }
        }

        #endregion
    }
}