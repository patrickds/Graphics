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
            _camera = new Camera(1, 10000, 800d/600d);
            this.AddDefaultEntities();
        }

        #endregion

        #region Attributes And Properties

        private List<Entity> _entities;
        private Camera _camera;

        public double Width { get; set; }
        public double Height { get; set; }

        #endregion

        #region Private Methods

        //public void Update()
        //{
        //    _camera.Update();
        //    _wcs.Update();

        //    foreach (var entity in _entities)
        //        entity.Update();

        //    //camera coordinate system
        //    Vector3 u = _camera.Right;
        //    Vector3 v = _camera.Up;
        //    Vector3 n = _camera.Direction;

        //    //view volume params
        //    double left = _camera.LeftBottomNear.X;
        //    double right = _camera.RightTopFar.X;
        //    double bottom = _camera.LeftBottomNear.Y;
        //    double top = _camera.RightTopFar.Y;
        //    double near = _camera.LeftBottomNear.Z;
        //    double far = _camera.RightTopFar.Z;

        //    //camera translation transformation: 
        //    //map world origin to camera origin
        //    Matrix4 t0 = new Matrix4(
        //        1, 0, 0, -_camera.Position.X,
        //        0, 1, 0, -_camera.Position.Y,
        //        0, 0, 1, -_camera.Position.Z,
        //        0, 0, 0, 1);

        //    //camera rotation transformation: 
        //    //rotate world to align with camera coordinate system (coordinates will now be in camera vector space)
        //    Matrix4 t1 = new Matrix4(
        //        u.X, u.Y, u.Z, 0,
        //        v.X, v.Y, v.Z, 0,
        //        n.X, n.Y, n.Z, 0,
        //        0, 0, 0, 1);

        //    //perspective transformation:
        //    //distort world in a way it makes closer things bigger and distant smaller
        //    Matrix4 t2 = new Matrix4(
        //        near, 0, 0, 0,
        //        0, near, 0, 0,
        //        0, 0, near + far, -far * near,
        //        0, 0, 1, 0);

        //    //parallel projection transformation:
        //    //map world coordinates into a canonical view volume (ranging from -1 to 1)
        //    Matrix4 t3 = new Matrix4(
        //        2.0d / (right - left), 0, 0, -((right + left) / (right - left)),
        //        0, 2.0d / (top - bottom), 0, -((top + bottom) / (top - bottom)),
        //        0, 0, 2.0d / (near - far), -((near + far) / (near - far)),
        //        0, 0, 0, 1);

        //    //viewport mapping transformation:
        //    //convert the canonical coordinates to the viewport coordinates, so it scales our view to fit the viewport
        //    Matrix4 t4 = new Matrix4(
        //        _width / 2.0d, 0, 0, (_width / 2.0d) - 0.5d,
        //        0, _height / 2.0d, 0, (_height / 2.0d) - 0.5d,
        //        0, 0, 1, 0,
        //        0, 0, 0, 1);

        //    Matrix4 projectionTransformMatrix = t4 * t3 * t2 * t1 * t0;

        //    foreach (var entity in _entities)
        //        entity.ApplyTransform(projectionTransformMatrix);

        //    _wcs.ApplyTransform(projectionTransformMatrix);

        //    this.AdjustWCSToShowInBottomLeftSideOfScreen();
        //}

        private Matrix4 GetTransformation()
        {
            return 
                this.GetScreenTransformation() * 
                this.GetOrthographicViewVolumeTransformation() * 
                this.GetPerspectiveTransformation() * 
                this.GetWorldSpaceToCameraSpace();
        }

        private Matrix4 GetScreenTransformation()
        {
            var halfWidth = this.Width / 2;
            var halfHeight = this.Height / 2;

            var translate = new Vector3(halfWidth - 0.5, halfHeight - 0.5, 0);
            var scale = new Vector3(halfWidth, halfHeight, 1);

            return Matrix4.CreateTranslation(translate) * Matrix4.CreateScale(scale);
        }

        private Matrix4 GetOrthographicViewVolumeTransformation()
        {
            //view volume params
            double l = _camera.LeftBottomNear.X;
            double r = _camera.RightTopFar.X;
            double b = _camera.LeftBottomNear.Y;
            double t = _camera.RightTopFar.Y;
            double n = _camera.LeftBottomNear.Z;
            double f = _camera.RightTopFar.Z;

            var translateX = -((l + r) / 2);
            var translateY = -((b + t) / 2);
            var translateZ = -((n + f) / 2);

            var scaleX = 2 / r - l;
            var scaleY = 2 / t - b;
            var scaleZ = 2 / n - f;

            var translation = Matrix4.CreateTranslation(new Vector3(translateX, translateY, translateZ));
            var scaling = Matrix4.CreateScale(new Vector3(scaleX, scaleY, scaleZ));

            //parallel projection transformation:
            //map world coordinates into a canonical view volume (ranging from -1 to 1)
            Matrix4 transformation = new Matrix4(
                2.0d / (r - l), 0, 0, -((r + l) / (r - l)),
                0, 2.0d / (t - b), 0, -((t + b) / (t - b)),
                0, 0, 2.0d / (n - f), -((n + f) / (n - f)),
                0, 0, 0, 1);

            //should be optimized caus of fixed values
            return transformation;
        }

        //Implement this shit
        private Matrix4 GetWorldSpaceToCameraSpace()
        {
            //camera coordinate system
            Vector4 u = _camera.Right;
            Vector4 v = _camera.Up;
            Vector4 n = _camera.Gaze;

            var rotation = new Matrix4(
                u.X, u.Y, u.Z, 0,
                v.X, v.Y, v.Z, 0,
                n.X, n.Y, n.Z, 0,
                0,   0,   0,  1);

            var cameraPosition = _camera.Position.ToVector3();
            var translation = Matrix4.CreateTranslation(-cameraPosition);

            return rotation * translation;
        }

        private Matrix4 GetPerspectiveTransformation()
        {
            var near = _camera.Near;
            var far = _camera.Far;

            var scaling = Matrix4.CreateScale(new Vector3(near, near, near+far));
            var translation = Matrix4.CreateTranslation(new Vector3(0, 0, -far * near));

            Matrix4 t2 = new Matrix4(
                near, 0, 0, 0,
                0, near, 0, 0,
                0, 0, near + far, -far * near,
                0, 0, 1, 0);

            var t22 = translation * scaling;

            return t2;
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
            var transformation = this.GetTransformation();

            foreach (var entity in _entities)
            {
                entity.OnRender(drawingContext, transformation);
            }
        }

        #endregion
    }
}