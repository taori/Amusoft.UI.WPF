using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Amusoft.UI.WPF.Adorners;
using Amusoft.UI.WPF.Notifications;

namespace Amusoft.UI.WPF.NugetIntegration
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
//            if (ScreenAnchorAdornerManager.Instance[Screen.PrimaryScreen].TryGetPresenter(AnchorPosition.Left, out var presenter))
//            {
//                presenter.Content = new Border()
//                {
//                    Width = 400,
//                    Height = 400,
//                    Background = Brushes.Red
//                };
//            }

            var manager = NotificationHostManager.GetHostByScreen(Screen.PrimaryScreen);
//
//            var manager = NotificationHostManager.GetHostByVisual(this.Content as Visual);
            foreach (AnchorPosition value in Enum.GetValues(typeof(AnchorPosition)))
            {
                manager.DisplayAsync(new SimpleNotification($"Test notification {value}."), value);
            }
        }
    }
}
