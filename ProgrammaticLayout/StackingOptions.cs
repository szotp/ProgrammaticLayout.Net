namespace ProgrammaticLayout
{
    using UIKit;

    /// <summary>
    /// Describes parameters of the stackView that will be created by LayoutBuilder.
    /// </summary>
    public class StackingOptions
    {
        public UILayoutConstraintAxis Axis = UILayoutConstraintAxis.Vertical;
        public float Spacing = 0;
        public UIStackViewDistribution Distribution = UIStackViewDistribution.Fill;
        public UIStackViewAlignment Alignment = default(UIStackViewAlignment);

        public void Apply(LayoutBuilder arrangement)
        {
            if (arrangement.InnerContainer is UIStackView stack)
            {
                stack.Axis = Axis;
                stack.Spacing = Spacing;
                stack.Distribution = Distribution;
                stack.Alignment = Alignment;
            }
        }

        public UIView Build(UIView[] children)
        {
            if (children == null || children.Length == 0) return null;
            if (children.Length == 1) return children[0];

            return new UIStackView(children);
        }
    }
}
