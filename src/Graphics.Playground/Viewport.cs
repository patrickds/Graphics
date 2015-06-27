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
            _environment = new Environment(new Vector2(350, 525));
        }

        private Environment _environment;

        protected override void OnRender(DrawingContext drawingContext)
        {
            _environment.OnRender(drawingContext);
            base.OnRender(drawingContext);
        }
    }
}
