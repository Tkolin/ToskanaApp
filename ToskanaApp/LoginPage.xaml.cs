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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ToskanaApp
{
    /// <summary>
    /// Логика взаимодействия для LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        private void btnAuth_Click(object sender, RoutedEventArgs e)
        {
          

            if(ToskanaDBEntities.GetContext().User.Where(
                u => u.Login == tBoxLogin.Text && u.Password == tBoxPass.Text).Count() > 0)
            {
                User user = ToskanaDBEntities.GetContext().User.Where(
                u => u.Login == tBoxLogin.Text && u.Password == tBoxPass.Text).First();
                NavigationService.Navigate(new MainMenyPage(user));
            }
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
