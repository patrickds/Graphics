using Graphics.Core;
using Graphics.Math;
using Graphics.Models;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Graphics.Playground
{
    public class Viewport : Control
    {
        public Viewport()
        {
            _environment = new Environment();
            this.Loaded += (sender, e) => { this.CreateTestEntities(); };
        }
        
        private Environment _environment;

        private void CreateTestEntities()
        {
            var cube = Cube.Create(60);
            var translation = Matrix4.CreateTranslation(new Vector3(this.ActualWidth / 2, this.ActualHeight / 2, 0));

            cube.Transform(translation);
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
    }
}
