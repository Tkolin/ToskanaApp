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

namespace ToskanaApp
{
    /// <summary>
    /// Логика взаимодействия для MainMenyPage.xaml
    /// </summary>
    public partial class MainMenyPage : Page
    {
        User user;
        public MainMenyPage()
        {
            InitializeComponent();
        }
        public MainMenyPage(User user)
        {
            InitializeComponent();
            this.user = user;
        }
        private void btnN1_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new TableOrderPage());
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void btnN4_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new TableIngredientPage());
        }

        private void btnN3_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new TableDishPage());
        }

        private void btnN2_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new TableEmployePage());
        }
    }
}
