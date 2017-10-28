using System;
using UIKit;
using Xamarin.Interactive;
using Xamarin.Interactive.Logging;
using Xamarin.Interactive.Representations;
using Xamarin.Interactive.Representations.Reflection;
using Xamarin.Interactive.Serialization;



namespace ProgrammaticLayout
{
    class ProgrammaticLayoutRepresentationProvider : RepresentationProvider
    {
        public override System.Collections.Generic.IEnumerable<object> ProvideRepresentations(object obj)
        {
            if (obj is UIView view)
            {
                if (view.Frame.Height > 500) yield break;

                if (view.Frame.Width == 0 || view.Frame.Height == 0)
                {
                    view.Frame = new CoreGraphics.CGRect(0, 0, 200, 200);
                }

                UIGraphics.BeginImageContext(view.Bounds.Size);

                view.DrawViewHierarchy(view.Bounds, true);
                var image = UIGraphics.GetImageFromCurrentImageContext();
                UIGraphics.EndImageContext();

                var png = image.AsPNG().ToArray();
                yield return new Image(ImageFormat.Png, png);
            }
            else
            {
                yield break;
            }
        }
    }
}
