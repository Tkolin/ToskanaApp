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
    /// Логика взаимодействия для EditOrderPage.xaml
    /// </summary>
    public partial class EditOrderPage : Page
    {
        Order order;
        bool edit;
        public EditOrderPage()
        {
            InitializeComponent();
            edit = true;
            this.order = new Order();
        }
        public EditOrderPage(Order order)
        {
            InitializeComponent();
            edit = true;
            this.order = order;
        }
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            N1.ItemsSource = ToskanaDBEntities.GetContext().Dish.ToList();
            N1.DisplayMemberPath = "Name";

            N4.ItemsSource = ToskanaDBEntities.GetContext().Employe.ToList();
            N4.DisplayMemberPath = "LastName";

            if (edit)
            {
                N1.SelectedItem = order.Dish;
                N2.SelectedDate = order.Date;
                N3.Text = order.Count.ToString();
                N4.SelectedItem = order.Employe;
            }
        }
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            order.Dish = N1.SelectedItem as Dish;
            order.Date = N2.SelectedDate;
            order.Count = Convert.ToInt32( N3.Text);
            order.Employe = N4.SelectedItem as Employe;


            try
            {
                if (!edit)
                    ToskanaDBEntities.GetContext().Order.Add(order);
                ToskanaDBEntities.GetContext().SaveChanges();
                NavigationService.GoBack();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void backBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }


    }
}
