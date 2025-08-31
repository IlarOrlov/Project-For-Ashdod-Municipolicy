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
    /// Interaction logic for Delete_Quiz_Succeeded_Msg.xaml
    /// </summary>
    public partial class Delete_Quiz_Succeeded_Msg : Window
    {
        public Delete_Quiz_Succeeded_Msg()
        {
            InitializeComponent();
        }

        private void Continue_Click(object sender, RoutedEventArgs e)
        {
            NavigationService navigation = All_Pages.GetFrame().NavigationService;
            navigation.Navigate(All_Pages.GetAdminMenuPage());
            this.Visibility = Visibility.Hidden;
        }
    }
}
