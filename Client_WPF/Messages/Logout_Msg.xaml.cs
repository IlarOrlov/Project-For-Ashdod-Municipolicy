using Client_WPF.Classes;
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

namespace Client_WPF.Messages
{
    /// <summary>
    /// Interaction logic for Logout_Msg.xaml
    /// </summary>
    public partial class Logout_Msg : Window
    {
        public Logout_Msg()
        {
            InitializeComponent();
        }

        private void Yes_Click(object sender, RoutedEventArgs e)
        {
            NavigationService navigation = All_Pages.GetFrame().NavigationService;
            navigation.Navigate(All_Pages.GetRegisterPage());
            this.Visibility = Visibility.Hidden;
        }

        private void No_Click(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Hidden;
        }
    }
}
