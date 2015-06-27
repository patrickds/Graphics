using Graphics.Core;
using Graphics.Math;
using System.Windows;
using System.Windows.Media;

namespace Graphics.Playground
{
    public class Viewport : UIElement
    {
        public Viewport()
        {
            _environment = new Environment();
        }

        private Environment _environment;

        private void SetEnvironmentSize()
        {
            _environment.Width = this.RenderSize.Width;
            _environment.Height = this.RenderSize.Height;
        }

        private void Render(DrawingContext drawingContext)
        {
            _environment.OnRender(drawingContext);
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            this.SetEnvironmentSize();
            this.Render(drawingContext);
            base.OnRender(drawingContext);
        }
    }
}
