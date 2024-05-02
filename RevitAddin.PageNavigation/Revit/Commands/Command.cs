using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Windows.Controls;

namespace RevitAddin.PageNavigation.Revit.Commands
{
    [Transaction(TransactionMode.Manual)]
    public class Command : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elementSet)
        {
            UIApplication uiapp = commandData.Application;

            var pageIndex = 0;
            var pages = new Page[]
            {
                new Views.Pages.HomePage(),
                new Views.Pages.ConfigPage()
            };

            var pageView = new Views.PageView(pages[pageIndex]);
            pageView.Frame.NavigationUIVisibility = System.Windows.Navigation.NavigationUIVisibility.Hidden;

            pageView.KeyDown += (sender, e) =>
            {
                if (e.Key == System.Windows.Input.Key.Up)
                {
                    pageIndex = (pageIndex + 1) % pages.Length;
                    pageView.Frame.Navigate(pages[pageIndex]);
                }
                if (e.Key == System.Windows.Input.Key.Down)
                {
                    pageIndex = (pages.Length + pageIndex - 1) % pages.Length;
                    pageView.Frame.Navigate(pages[pageIndex]);
                }
            };

            pageView.ShowDialog();

            return Result.Succeeded;
        }
    }
}
