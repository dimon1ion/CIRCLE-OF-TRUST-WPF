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

namespace WPF_CSharpXAML_hw4
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new DataSource();
        }

        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (((DataSource)DataContext).SelectedIndex >= 0)
            {
                int selectedImage = Int32.Parse((sender as Image).Name[1].ToString());
                foreach (var item in ((sender as Image).Parent as StackPanel).Children)
                {
                    if (item is Image)
                    {
                        int indexImage = Int32.Parse((item as Image).Name[1].ToString());
                        if (indexImage <= selectedImage)
                        {
                            (item as Image).Source = (ImageSource)new ImageSourceConverter().ConvertFrom(AppDomain.CurrentDomain.BaseDirectory + "\\StarEnable.png");
                            ((DataSource)DataContext).Users[((DataSource)DataContext).SelectedIndex].Rates[indexImage] = AppDomain.CurrentDomain.BaseDirectory + "\\StarEnable.png";
                        }
                        else
                        {
                            (item as Image).Source = (ImageSource)new ImageSourceConverter().ConvertFrom(AppDomain.CurrentDomain.BaseDirectory + "\\StarDisable.png");
                            ((DataSource)DataContext).Users[((DataSource)DataContext).SelectedIndex].Rates[indexImage] = AppDomain.CurrentDomain.BaseDirectory + "\\StarDisable.png";
                        }
                    }
                }
            }

        }
    }
}
