using System;
using CoreGraphics;
using Foundation;
using UIKit;
using Xamarin.Interactive;

namespace ProgrammaticLayout
{
    public class WorkbooksHelper
    {
        public UIView FreshView()
        {
            var window = UIApplication.SharedApplication.KeyWindow;
            if (window.RootViewController is UINavigationController navigationController)
            {
                navigationController.PopToRootViewController(false);
                var view = navigationController.TopViewController.View;

                foreach (var subview in view.Subviews)
                {
                    subview.RemoveFromSuperview();
                }
            }

            var vc = new UIViewController();
            vc.View.BackgroundColor = UIColor.White;
            var nc = new UINavigationController(vc);
            nc.PopToRootViewController(false);
            window.RootViewController = nc;
            return vc.View;
        }

        class CustomTableViewSource : UITableViewSource
        {
            public Type CellType;

            public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
            {
                return tableView.DequeueReusableCell(CellType.Name, indexPath);
            }

            public override nint RowsInSection(UITableView tableview, nint section)
            {
                return 1;
            }
        }

        UITableView MakeTableView(Type type)
        {
            var constructor = type.GetConstructor(new[] { typeof(IntPtr) });
            if (constructor == null)
            {
                Console.WriteLine("Cell needs (IntPtr ptr) constructor");
                return null;
            }

            var tv = new UITableView();
            tv.EstimatedRowHeight = 50;
            tv.RegisterClassForCellReuse(type, type.Name);
            tv.Source = new CustomTableViewSource { CellType = type };
            tv.Frame = new CGRect(0, 0, 320, 200);
            tv.ReloadData();

            return tv;
        }

        public UIView DisplayCell(Type type)
        {
            var tv = MakeTableView(type);
            return tv.CellAt(NSIndexPath.FromRowSection(0, 0));
        }

        public UITableView DisplayTableView(Type type)
        {
            var tv = MakeTableView(type);

            var (ui, s) = StyleLibrary.Start();
            ui.StackIn(FreshView()).With(tv);
            return tv;
        }
    }
}
