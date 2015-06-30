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
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Graphics.UI
{
    public partial class Viewport : UserControl
    {
        public Viewport()
        {
            _environment = new Environment();
            RenderOptions.SetEdgeMode(this, EdgeMode.Aliased);
            this.CreateTestEntities();
        }

        private Environment _environment;

        private void CreateTestEntities()
        {
            var cube = Cube.Create(0.2d);

            _environment.Add(cube);

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

        #region Overriden Methods

        protected override void OnMouseDown(MouseButtonEventArgs e)
        {
            MouseControllerManager.Instance.Current = new PanMouseController();
            MouseControllerManager.Instance.OnMouseDown(this, e);
            base.OnMouseUp(e);
        }

        protected override void OnMouseUp(MouseButtonEventArgs e)
        {
            MouseControllerManager.Instance.OnMouseUp(this, e);
            base.OnMouseUp(e);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            MouseControllerManager.Instance.OnMouseMove(this, e);
            base.OnMouseMove(e);
        }

        protected override void OnMouseWheel(MouseWheelEventArgs e)
        {
            MouseControllerManager.Instance.Current = new ZoomMouseController();
            MouseControllerManager.Instance.OnMouseWheel(this, e);
            base.OnMouseWheel(e);
        }

        protected override HitTestResult HitTestCore(PointHitTestParameters hitTestParameters)
        {
            return new PointHitTestResult(this, hitTestParameters.HitPoint);
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            this.SetEnvironmentSize();
            this.Render(drawingContext);
            base.OnRender(drawingContext);
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