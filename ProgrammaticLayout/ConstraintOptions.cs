using UIKit;

namespace ProgrammaticLayout
{
    /// <summary>
    /// Describes what kind of constraint should be created.
    /// </summary>
    public struct ConstraintOptions
    {
        public float Constant;
        public NSLayoutRelation Relation;

        public ConstraintOptions(float constant)
        {
            Relation = NSLayoutRelation.Equal;
            Constant = constant;
        }

        public static implicit operator ConstraintOptions(float constant)
        {
            return new ConstraintOptions(constant);
        }

        public NSLayoutConstraint Create(NSLayoutXAxisAnchor from, NSLayoutXAxisAnchor into)
        {
            NSLayoutConstraint constraint = null;
            switch (Relation)
            {
                case NSLayoutRelation.LessThanOrEqual: constraint = from.ConstraintLessThanOrEqualTo(into, Constant); break;
                case NSLayoutRelation.Equal: constraint = from.ConstraintEqualTo(into, Constant); break;
                case NSLayoutRelation.GreaterThanOrEqual: constraint = from.ConstraintGreaterThanOrEqualTo(into, Constant); break;
            }

            constraint.Active = true;
            return constraint;
        }

        public NSLayoutConstraint Create(NSLayoutYAxisAnchor from, NSLayoutYAxisAnchor into)
        {
            NSLayoutConstraint constraint = null;
            switch (Relation)
            {
                case NSLayoutRelation.LessThanOrEqual: constraint = from.ConstraintLessThanOrEqualTo(into, Constant); break;
                case NSLayoutRelation.Equal: constraint = from.ConstraintEqualTo(into, Constant); break;
                case NSLayoutRelation.GreaterThanOrEqual: constraint = from.ConstraintGreaterThanOrEqualTo(into, Constant); break;
            }

            constraint.Active = true;
            return constraint;
        }
    }
}