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
using Client_WPF.Classes;
using Client_WPF.ProjectService;

namespace Client_WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            // -- creating the project service that connects us with the WCF service --
            Service1Client serviceProject = new Service1Client();

            // -- setting the frame, the project service and all the needed pages and windows for the program --
            All_Pages.SetFrame(this.myFrame);
            All_Pages.SetProjectService(serviceProject);
            All_Pages.CreateMessagesAndPages();
        }

        private void Register_Click(object sender, RoutedEventArgs e)
        {
            // Navigate to your first page
            myFrame.Navigate(All_Pages.GetRegisterPage());

            // Bring frame to front
            Panel.SetZIndex(myFrame, 2);

            // Optionally hide the intro Viewbox (title + button)
            IntroUI.Visibility = Visibility.Collapsed;
        }

        private void myFrame_Navigated(object sender, NavigationEventArgs e)
        {

        }
    }
}