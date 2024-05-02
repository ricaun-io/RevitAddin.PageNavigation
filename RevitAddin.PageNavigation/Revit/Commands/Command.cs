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

            var pages = new Page[]
            {
                new Views.Pages.HomePage(),
                new Views.Pages.ConfigPage(),
                new Views.Pages.AboutPage(),
            };

            var pageView = new Views.PageView(pages);

            pageView.ShowDialog();

            return Result.Succeeded;
        }
    }
}
