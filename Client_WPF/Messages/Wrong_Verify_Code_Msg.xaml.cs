﻿using System;
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
using System.Windows.Shapes;

namespace Client_WPF.Messages
{
    /// <summary>
    /// Interaction logic for Wrong_Verify_Code_Msg.xaml
    /// </summary>
    public partial class Wrong_Verify_Code_Msg : Window
    {
        public Wrong_Verify_Code_Msg()
        {
            InitializeComponent();
        }

        private void Continue_Click(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Hidden;
        }
    }
}
