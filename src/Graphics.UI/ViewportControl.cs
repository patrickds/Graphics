using Graphics.Core;
using Graphics.Math;
using Graphics.Models;
using Graphics.UI.MouseControllers;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            this.CreateTestEntities();
        }

        #endregion

        #region Attributes And Properties

        private Environment _environment;

        #endregion

        #region Private Methods

        private void CreateTestEntities()
        {
            _environment.Add(Cube.Create(0.2, new Vector4(0.2, 0.2, 0, 1)));
            this.InvalidateVisual();
        }

        private void SetEnvironmentSize()
        {
            _environment.Width = this.RenderSize.Width;
            _environment.Height = this.RenderSize.Height;
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
            this.SetEnvironmentSize();
            this.Render(drawingContext);
        }

        #endregion

        #region Public Methods

        public void Transform(Matrix4 transformation)
        {
            _environment.Transform(transformation);
            this.InvalidateVisual();
        }

        #endregion
    }
}