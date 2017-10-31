namespace ProgrammaticLayout
{
    using UIKit;

    /// <summary>
    /// Library of views that can be instantiated during layout creation.
    /// </summary>
    public class ViewToolbox
    {
        public Style<UIView> All { get; set; }

        public T View<T>(T view, params Style<T>[] styles) where T : UIView
        {
            foreach (var item in styles)
            {
                item(view);
            }
            All?.Invoke(view);
            return view;
        }

        public void ApplyStyles<T>(T view, params Style<T>[] styles)
        {
            foreach (var item in styles)
            {
                item(view);
            }
        }

        public UILabel Label(string text = null, params Style<UILabel>[] styles)
        {
            return View(new UILabel()
            {
                Text = text
            }, styles);
        }

        public LayoutBuilder StackIn(UIView superview, params Style<LayoutBuilder>[] styles)
        {
            var view = superview ?? new UIView();
            var builder = new LayoutBuilder(view);
            foreach (var item in styles)
            {
                if(item is Style<UIView> style)
                {
                    style(view);
                    continue;
                }

                item(builder);
            }
            return builder;
        }

        public LayoutBuilder Stack(params Style<LayoutBuilder>[] styles)
        {
            return StackIn(new UIView(), styles);
        }

        public UIView Space(params Style<UIView>[] styles)
        {
            return View(new UIView(), styles);
        }

        public UIImageView Image(params Style<UIImageView>[] styles)
        {
            return View(new UIImageView() { ContentMode = UIViewContentMode.ScaleAspectFit}, styles);
        }

        public UITextField TextField(string placeholder, params Style<UITextField>[] styles)
        {
            return View(new UITextField() { Placeholder = placeholder }, styles);
        }

        public UIButton SystemButton(string title, params Style<UIButton>[] styles)
        {
            var button = new UIButton(UIButtonType.System);
            button.SetTitle(title, UIControlState.Normal);
            return View(button, styles);
        }
    }
}
