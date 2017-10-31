namespace ProgrammaticLayout
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using UIKit;

    /// <summary>
    /// Builds relation between superview and it's children.
    /// Note: LayoutBuilder inherits from UIView only to simplify the builder API.
    /// It lets us supply Style<UIView> and Style<LayoutBuilder> in the same array.
    /// </summary>
    public sealed class LayoutBuilder : UIView
    {
        public PaddingOptions PaddingOptions { get; } = new PaddingOptions();
        public StackingOptions StackingOptions { get; } = new StackingOptions();

        public UIView OuterContainer { get; private set; }
        public UIView InnerContainer { get; set; }
        public bool Scrollable { get; set; }

        bool useLayoutGuides = false;

        UIView _view;

        public LayoutBuilder(UIView view)
        {
            _view = view;
            OuterContainer = _view;
        }

        public LayoutBuilder Horizontal()
        {
            StackingOptions.Axis = UILayoutConstraintAxis.Horizontal;
            return this;
        }

        public LayoutBuilder UseLayoutGuides()
        {
            useLayoutGuides = true;
            return this;
        }

        public LayoutBuilder Padding(float value)
        {
            PaddingOptions.Default = value;
            return this;
        }

        public LayoutBuilder Padding(float? leading, float? trailing, float? top, float? bottom)
        {
            PaddingOptions.Default = null;
            PaddingOptions.Leading = leading;
            PaddingOptions.Trailing = trailing;
            PaddingOptions.Top = top;
            PaddingOptions.Bottom = bottom;

            return this;
        }

        public LayoutBuilder Centered()
        {
            StackingOptions.Alignment = UIStackViewAlignment.Center;
            return this;
        }

        public LayoutBuilder Spacing(float value)
        {
            StackingOptions.Spacing = value;
            return this;
        }

        public LayoutBuilder Margins(float value)
        {
            PaddingOptions.Default = value;
            StackingOptions.Spacing = value;
            return this;
        }

        public LayoutBuilder FillEqually()
        {
            StackingOptions.Distribution = UIStackViewDistribution.FillEqually;
            return this;
        }

        public UIView With(params UIView[] children)
        {
            OuterContainer = _view;

            InnerContainer = StackingOptions.Build(children.ToArray());

            if (Scrollable)
            {
                var inner = InnerContainer;
                var scrollView = new UIScrollView();
                InnerContainer = new LayoutBuilder(scrollView).With(InnerContainer);
                scrollView.WidthAnchor.ConstraintEqualTo(inner.WidthAnchor).Active = true;
            }

            StackingOptions?.Apply(this);

            if(useLayoutGuides && OuterContainer.NextResponder is UIViewController vc)
            {
                PaddingOptions.TopAnchor = vc.TopLayoutGuide.GetBottomAnchor();
                PaddingOptions.BottomAnchor = vc.BottomLayoutGuide.GetTopAnchor();
            }
                
            PaddingOptions?.Apply(OuterContainer, InnerContainer);

            return _view;
        }
    }
}
