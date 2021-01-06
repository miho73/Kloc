using System;
using System.Diagnostics;
using System.Threading;
using Windows.ApplicationModel.Core;
using Windows.Media.Core;
using Windows.System;
using Windows.UI;
using Windows.UI.Core;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

namespace Kloc
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();
            MainPg.Background = new ImageBrush 
            { 
                ImageSource = new BitmapImage(new Uri(BaseUri, "Assets/bg.png")), 
                Stretch = Stretch.None 
            };

            var coreTitleBar = CoreApplication.GetCurrentView().TitleBar;
            ApplicationViewTitleBar titleBar = ApplicationView.GetForCurrentView().TitleBar;
            titleBar.ButtonBackgroundColor = Colors.Transparent;
            titleBar.ButtonForegroundColor = Colors.Transparent;
            titleBar.ButtonHoverBackgroundColor = Colors.White;
            titleBar.ButtonHoverForegroundColor = Colors.Black;
            titleBar.ButtonInactiveBackgroundColor = Colors.White;
            titleBar.ButtonInactiveForegroundColor = Colors.Black;
            titleBar.ButtonPressedBackgroundColor = Colors.White;
            titleBar.ButtonPressedForegroundColor = Colors.Black;
            coreTitleBar.ExtendViewIntoTitleBar = true;
            
            Window.Current.SetTitleBar(Base);
        }

        public async void UpdateTime()
        {
            while (true)
            {
                DateTime now = DateTime.Now;
                await Date.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                {
                    Date.Text = now.ToString(@"yyyy/MM/dd");
                });
                await Time.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                {
                    Time.Text = now.ToString(@"HH:mm:ss");
                });
                Thread.Sleep(100);
            }
        }

        private void MainPg_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            //Don't work...
            if(e.Key == VirtualKey.Escape)
            {
                Application.Current.Exit();
            }
        }
        private void Start()
        {
            Thread Updater = new Thread(new ThreadStart(UpdateTime));
            Updater.Start();
        }

        private void Base_Loaded(object sender, RoutedEventArgs e)
        {
            Start();
        }
    }
}
