using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Incanto;
using Incanto.Themes;
using Incanto.Controls;
using Incanto.Helpers;

namespace IncantoTestApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public CurrentTheme theme;
        public MainWindow()
        {
            InitializeComponent();

            //foreach (var themed_control in ControlsFinder.GetLogicalChildren<IncantoControl>(this))
            //{
            //}
            //theme = CurrentTheme.LightTheme;
            ChangeToLightTheme();
        }

        private void ChangeToLightTheme()
        {
            ThemeEngine.SetCurrentThemeDictionary(this, new Uri("pack://application:,,,/Incanto;component/Themes/LightTheme.xaml"));
            theme = CurrentTheme.LightTheme;
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            if (theme == CurrentTheme.DarkTheme)
            {
                foreach (var themed_control in ControlsFinder.GetLogicalChildren<IncantoControl>(this))
                {
                    ThemeEngine.SetCurrentThemeDictionary(this, new Uri("pack://application:,,,/Incanto;component/Themes/LightTheme.xaml"));
                }
                theme = CurrentTheme.LightTheme;
            }
            else
            {
                foreach (var themed_control in ControlsFinder.GetLogicalChildren<IncantoControl>(this))
                {
                    ThemeEngine.SetCurrentThemeDictionary(this, new Uri("pack://application:,,,/Incanto;component/Themes/DarkTheme.xaml"));
                }
                theme = CurrentTheme.DarkTheme;
            }
        }

        public enum CurrentTheme
        {
            DarkTheme,
            LightTheme

        }
    }
}
