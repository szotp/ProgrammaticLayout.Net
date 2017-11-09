namespace ProgrammaticLayout
{
    using System;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using UIKit;

    public delegate void Style<in T>(T view);
    public delegate void LayoutStyle(LayoutBuilder layout);

    /// <summary>
    /// Library of styles that can be used during layout creation.
    /// </summary>
    public class StyleLibrary
    {
        public float DefaultLayoutPriority = 999;

        void Activate(NSLayoutConstraint constraint, [CallerMemberName] string identifier = null)
        {
            constraint.SetIdentifier(identifier);
            constraint.Priority = DefaultLayoutPriority;
            constraint.Active = true;
        }

        /// <summary>
        /// Sets high hugging priority so that the view will resize easily. Should not be called multiple times for the same level.
        /// </summary>
        /// <returns>The hug.</returns>
        public Style<UIView> AvoidResizing() => (view) =>
        {
            view.SetContentCompressionResistancePriority(1000, UILayoutConstraintAxis.Horizontal);
            view.SetContentCompressionResistancePriority(1000, UILayoutConstraintAxis.Vertical);
            view.SetContentHuggingPriority(1000, UILayoutConstraintAxis.Horizontal);
            view.SetContentHuggingPriority(1000, UILayoutConstraintAxis.Vertical);
        };

        public Style<UIView> AspectRatio(float value) => (view) => Activate(view.WidthAnchor.ConstraintEqualTo(view.HeightAnchor, value));

        public Style<LayoutBuilder> BottomLeft(float value = 0) => Padding(leading: value, trailing: null, top: null, bottom: value);
        public Style<LayoutBuilder> BottomRight(float value = 0) => Padding(leading: null, trailing: value, top: null, bottom: value);
        public Style<LayoutBuilder> TopLeft(float value = 0) => Padding(leading: value, trailing: null, top: value, bottom: null);
        public Style<LayoutBuilder> TopRight(float value = 0) => Padding(leading: null, trailing: value, top: value, bottom: null);

        public Style<LayoutBuilder> TopAndSides(float value = 0) => Padding(leading: value, trailing: value, top: value, bottom: null);
        public Style<LayoutBuilder> BottomAndSides(float value = 0) => Padding(leading: value, trailing: value, top: null, bottom: value);


        public Style<UIView> Height(float value) => (view) => Activate(view.HeightAnchor.ConstraintEqualTo(value));
        public Style<UIView> Width(float value) => (view) => Activate(view.WidthAnchor.ConstraintEqualTo(value));
        public Style<UIView> Size(float value) => Width(value) + Height(value);
        public Style<UIView> Background(UIColor color) => (view) => view.BackgroundColor = color;

        public Style<UILabel> FontSize(float size) => (view) => view.Font = UIFont.SystemFontOfSize(size);
        public Style<UILabel> TextCentered => (view) => view.TextAlignment = UITextAlignment.Center;
        public Style<UILabel> Multiline => (view) => view.Lines = 0;
        public Style<UILabel> TextColor(UIColor lightGray) => view => view.TextColor = lightGray;

        public Style<UIView> WidthSameAs(UIView other) => (view) => Activate(view.WidthAnchor.ConstraintEqualTo(other.WidthAnchor));
        public Style<UIView> HeightSameAs(UIView other) => (view) => Activate(view.HeightAnchor.ConstraintEqualTo(other.HeightAnchor));

        public Style<T> Combine<T>(params Style<T>[] styles) where T : UIView
        {
            return (view) =>
            {
                var builder = view as LayoutBuilder;

                foreach (var item in styles)
                {
                    if (builder != null && item is Style<UIView> style)
                    {
                        style(builder.OuterContainer);
                        continue;
                    }

                    item(view);
                }
            };
        }


        public Style<UIView> CornerRadius(float value)
        {
            return (view) => view.Layer.CornerRadius = value;
        }

        public Style<UIView> Id(string value) => (view) =>
        {
            view.AccessibilityIdentifier = value;
            view.AccessibilityLabel = value;
        };

        public Style<UIView> Border(UIColor color, nfloat width)
        {
            return (view) =>
            {
                view.Layer.BorderColor = color.CGColor;
                view.Layer.BorderWidth = width;
            };
        }

        public Style<LayoutBuilder> Scrollable => (view) => view.Scrollable = true;
        public Style<LayoutBuilder> Padding(float value) => (view) => view.Padding(value);
        public Style<LayoutBuilder> Padding(float? leading, float? trailing, float? top, float? bottom) => (view) => view.Padding(leading, trailing, top, bottom);
        public Style<LayoutBuilder> Spacing(float value) => (view) => view.Spacing(value);
        public Style<LayoutBuilder> Margins(float value) => (view) => view.Margins(value);
        public Style<LayoutBuilder> Horizontal => (view) => view.StackingOptions.Axis = UILayoutConstraintAxis.Horizontal;
        public Style<LayoutBuilder> Distribution(UIStackViewDistribution value) => (layout) => layout.StackingOptions.Distribution = value;
        public Style<LayoutBuilder> Alignment(UIStackViewAlignment value) => (layout) => layout.StackingOptions.Alignment = value;
        public Style<LayoutBuilder> AlignmentCentered => (layout) => layout.StackingOptions.Alignment = UIStackViewAlignment.Center;
        public Style<LayoutBuilder> UseLayoutGuides => (layout) => layout.UseLayoutGuides();
        public Style<LayoutBuilder> Centered => (layout) =>
        {
            layout.PaddingOptions.Default = null;
            layout.PaddingOptions.CenterY = 0;
            layout.PaddingOptions.CenterX = 0;
        };

        public Style<LayoutBuilder> FillEqually => (layout) => layout.StackingOptions.Distribution = UIStackViewDistribution.FillEqually;

        public Style<LayoutBuilder> CenterY() => (layout) =>
        {
            if (layout.PaddingOptions.Default.HasValue)
            {
                var value = layout.PaddingOptions.Default.Value;
                layout.PaddingOptions.Default = null;
                layout.PaddingOptions.Leading = value;
                layout.PaddingOptions.Trailing = value;
            }

            layout.PaddingOptions.CenterY = 0;
        };

        public Style<LayoutBuilder> CenterX() => (layout) =>
        {
            if (layout.PaddingOptions.Default.HasValue)
            {
                var value = layout.PaddingOptions.Default.Value;
                layout.PaddingOptions.Default = null;
                layout.PaddingOptions.Top = value;
                layout.PaddingOptions.Bottom = value;
            }

            layout.PaddingOptions.CenterY = 0;
        };

        public static (ViewToolbox, StyleLibrary) Start()
        {
            var toolbox = new ViewToolbox();
            return (toolbox, toolbox.Styles);
        }

        /// <summary>
        /// Returns constraint created by this object or PaddingOptions.
        /// Identifier would be the same as method name or property associated with constraint.
        /// <example><code>s.GetConstraint(View, nameof(StyleLibrary.Height))</code></example>  
        /// <example><code>s.GetConstraint(View, nameof(PaddingOptions.Top))</code></example>  
        /// </summary>
        public NSLayoutConstraint GetConstraint(UIView view, string identifier) => view.Constraints.First(x => x.GetIdentifier() == identifier);

        public float OnePixel => 1 / (float)UIScreen.MainScreen.Scale;
    }
}
