using Graphics.Core;
using Graphics.Math;
using Graphics.Models;
using Graphics.UI.MouseControllers;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Graphics.UI
{
    internal class ViewportControl : Control
    {
        #region Constructors

        public ViewportControl()
        {
            _environment = new Environment();
            this.SizeChanged += ViewportControl_SizeChanged;

            this.CreateTestEntities();
        }

        #endregion

        #region Attributes And Properties

        protected Environment _environment;

        #endregion

        #region Private Methods

        private void CreateTestEntities()
        {
            var cube = Cube.Create(10, new Vector4(0,10,0,1));
            _environment.Add(cube);
            this.InvalidateVisual();

            var numCubes = 5;
            double xpos = 0;
            double zpos = 0;
            foreach (var z in Enumerable.Range(1, numCubes))
            {
                foreach (var x in Enumerable.Range(1, numCubes))
                {
                    _environment.Add(Cube.Create(10, new Vector4(xpos, 10, zpos, 1)));
                    _environment.Add(Cube.Create(10, new Vector4(-xpos, 10, zpos, 1)));
                    _environment.Add(Cube.Create(10, new Vector4(-xpos, 10, -zpos, 1)));
                    _environment.Add(Cube.Create(10, new Vector4(xpos, 10, -zpos, 1)));
                    xpos += 30;
                }

                zpos += 30;
                xpos = 0;
            }

        }

        private void Render(DrawingContext drawingContext)
        {
            _environment.OnRender(drawingContext);
        }

        #endregion

        #region Overriden Methods

        protected override void OnMouseDown(MouseButtonEventArgs e)
        {
            MouseControllerManager.Instance.OnMouseDown(this, e);
        }

        protected override void OnMouseUp(MouseButtonEventArgs e)
        {
            MouseControllerManager.Instance.OnMouseUp(this, e);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            MouseControllerManager.Instance.OnMouseMove(this, e);
        }

        protected override void OnMouseWheel(MouseWheelEventArgs e)
        {
            MouseControllerManager.Instance.OnMouseWheel(this, e);
        }

        protected override HitTestResult HitTestCore(PointHitTestParameters hitTestParameters)
        {
            return new PointHitTestResult(this, hitTestParameters.HitPoint);
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);
            this.Render(drawingContext);
        }

        #endregion

        #region Public Methods

        public void Rotate(double xRadians, double yRadians)
        {
            _environment.Rotate(xRadians, yRadians);
            this.InvalidateVisual();
        }

        public void Translate(Vector4 translation)
        {
            _environment.Tranlate(translation);
            this.InvalidateVisual();
        }

        public void Zoom(double factor)
        {
            _environment.Zoom(factor);
            this.InvalidateVisual();
        }

        #endregion

        #region Signed Events Methods

        private void ViewportControl_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            _environment.SetSize(e.NewSize.Width, e.NewSize.Height);
            this.InvalidateVisual();
        }

        #endregion
    }
}