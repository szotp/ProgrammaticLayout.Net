using System;
using UIKit;
using ProgrammaticLayout;

namespace ProgrammaticLayout.Example
{
    public partial class ViewController : UIViewController
    {
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            var (ui, s) = StyleLibrary.Start();

            var containerStyle = s.Background(UIColor.Gray) + s.Width(200) + s.CornerRadius(4);
            var fieldStyle = s.Background(UIColor.White);
            var buttonStyle = s.Background(UIColor.Black) + s.Height(50) + s.CornerRadius(4);

            ui.ApplyStyles(View, s.Background(UIColor.LightGray)); //View from UIViewController
            ui.StackIn(View, s.Centered).With(
                ui.StackIn(ui.Space(containerStyle), s.Margins(8)).With(
                    ui.TextField("Login", fieldStyle),
                    ui.TextField("Password", fieldStyle),
                    ui.Stack(s.Horizontal, s.FillEqually, s.Spacing(8)).With(
                        ui.SystemButton("Sign in", buttonStyle),
                        ui.SystemButton("Sign up", buttonStyle)
                    )
                )
            );
        }
    }
}
