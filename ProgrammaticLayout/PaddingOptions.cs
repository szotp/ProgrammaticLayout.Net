namespace ProgrammaticLayout
{
    using UIKit;

    /// <summary>
    /// Describes constraints created between superview and subview.
    /// </summary>
    public class PaddingOptions
    {
        public ConstraintOptions? Default = 0;
        public ConstraintOptions? Leading = null;
        public ConstraintOptions? Top = null;
        public ConstraintOptions? Trailing = null;
        public ConstraintOptions? Bottom = null;

        public NSLayoutYAxisAnchor TopAnchor = null;
        public NSLayoutYAxisAnchor BottomAnchor = null;
        public NSLayoutXAxisAnchor LeadingAnchor = null;
        public NSLayoutXAxisAnchor TrailingAnchor = null;

        public ConstraintOptions? CenterX;
        public ConstraintOptions? CenterY;

        public void Apply(UIView outerContainer, UIView innerContainer)
        {
            if (innerContainer == null) return;

            outerContainer.AddSubview(innerContainer);
            innerContainer.TranslatesAutoresizingMaskIntoConstraints = false;

            var topAnchor = TopAnchor ?? outerContainer.TopAnchor;
            var bottomAnchor = BottomAnchor ?? outerContainer.BottomAnchor;
            var leadingAnchor = LeadingAnchor ?? outerContainer.LeadingAnchor;
            var trailingAnchor = TrailingAnchor ?? outerContainer.TrailingAnchor;

            ConstraintOptions? padding = null;

            if (Default.HasValue)
            {
                padding = Default;
            }

            (Leading ?? padding)?.Create(nameof(Leading), innerContainer.LeadingAnchor, leadingAnchor);
            (Trailing ?? padding)?.Create(nameof(Trailing), trailingAnchor, innerContainer.TrailingAnchor);
            (Top ?? padding)?.Create(nameof(Top), innerContainer.TopAnchor, topAnchor);
            (Bottom ?? padding)?.Create(nameof(Bottom), bottomAnchor, innerContainer.BottomAnchor);

            CenterX?.Create(nameof(CenterX), innerContainer.CenterXAnchor, outerContainer.CenterXAnchor);
            CenterY?.Create(nameof(CenterY), innerContainer.CenterYAnchor, outerContainer.CenterYAnchor);


        }
    }
}
