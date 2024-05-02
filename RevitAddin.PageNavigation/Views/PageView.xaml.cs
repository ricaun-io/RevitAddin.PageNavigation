using ricaun.Revit.Mvvm;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace RevitAddin.PageNavigation.Views
{
    [PropertyChanged.AddINotifyPropertyChangedInterface]
    public partial class PageView : Window
    {
        public class PageModel
        {
            public PageModel(Page page)
            {
                Page = page;
            }
            public Page Page { get; set; }
            override public string ToString() => Page?.Title ?? nameof(Page);
        }
        public ObservableCollection<PageModel> Pages { get; set; } = new ObservableCollection<PageModel>();
        public string Text { get; set; } = "Text";
        public IRelayCommand Command { get; set; } = new RelayCommand(() => MessageBox.Show("Hello!"));
        public IRelayCommand ChangePageCommand { get; set; }
        private void ChangePage(PageModel pageModel)
        {
            Frame.Navigate(pageModel.Page);
        }
        public PageView(params Page[] pages)
        {
            ChangePageCommand = new RelayCommand<PageModel>(ChangePage);

            InitializeComponent();
            InitializeWindow();
            Frame.Navigating += (sender, e) =>
            {
                if (e.Content is FrameworkElement element)
                    element.DataContext = DataContext;
            };

            foreach (var p in pages)
            {
                Pages.Add(new PageModel(p));
            }

            if (Pages.Count > 0)
            {
                var page = Pages[0].Page;
                Frame.Navigate(page);
                Title = page?.Title ?? nameof(PageView);
            }

            this.KeyDown += (sender, e) =>
            {
                if (e.Key == System.Windows.Input.Key.Back)
                {
                    if (this.Frame.CanGoBack)
                        this.Frame.GoBack();
                }
            };
        }

        #region InitializeWindow
        private void InitializeWindow()
        {
            this.MinHeight = 480;
            this.MinWidth = 480;
            this.Height = MinHeight;
            this.Width = MinWidth;
            this.ShowInTaskbar = false;
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            new System.Windows.Interop.WindowInteropHelper(this) { Owner = Autodesk.Windows.ComponentManager.ApplicationWindow };

            this.KeyDown += (sender, e) =>
            {
                if (e.Key == System.Windows.Input.Key.Escape)
                {
                    this.Close();
                }
            };
        }
        #endregion
    }
}