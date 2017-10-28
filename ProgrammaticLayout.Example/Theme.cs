using System;
using UIKit;

namespace ProgrammaticLayout.Example
{
    public class Theme
    {
        public Theme()
        {
        }

        public void StyleScreenView(UIView view)
        {
            view.BackgroundColor = UIColor.LightGray;
        }

        public void StyleHeaderLabel(UILabel label)
        {
            label.TextAlignment = UITextAlignment.Center;
            label.Font = UIFont.SystemFontOfSize(48);
        }

        public static Theme Default { get; } = new Theme();
    }
}
