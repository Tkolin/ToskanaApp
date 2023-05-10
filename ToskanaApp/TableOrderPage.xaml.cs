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
    /// Логика взаимодействия для TableOrderPage.xaml
    /// </summary>
    public partial class TableOrderPage : Page
    {
        public TableOrderPage()
        {
            InitializeComponent();
        }
        public void UpdateDataGrid()
        {
            List<Order> orders = ToskanaDBEntities.GetContext().Order.ToList();

            if (tBox.Text.Length > 0)
            {
                string src = tBox.Text.ToLower();
                orders = orders.Where(e => e.Dish.Name.ToLower().Contains(src) ||
                                              e.Dish.CalorieContent.ToString().ToLower().Contains(src) ||
                                              e.Employe.LastName.ToString().ToLower().Contains(src) ||
                                              e.Employe.Patronymic.ToString().ToLower().Contains(src)
                                              ).ToList();
            }
            if (dPicStart.SelectedDate != null)
            {
                DateTime src = dPicStart.SelectedDate.Value;
                orders = orders.Where(e => e.Date>=src).ToList();
            }
            if (dPicEnd.SelectedDate != null)
            {
                DateTime src = dPicEnd.SelectedDate.Value;
                orders = orders.Where(e => e.Date <= src).ToList();
            }


            dataGrid.ItemsSource = orders;
        }
        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnDel_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {

        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void btnReset_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void dPicStart_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void dPicEnd_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
