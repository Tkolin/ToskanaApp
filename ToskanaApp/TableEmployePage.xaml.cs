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
    /// Логика взаимодействия для TableEmployePage.xaml
    /// </summary>
    public partial class TableEmployePage : Page
    {
        public TableEmployePage()
        {
            InitializeComponent();
        }

        public void UpdateDataGrid()
        {
            List<Employe> employes = ToskanaDBEntities.GetContext().Employe.ToList();

            if(tBox.Text.Length > 0)
            {
                string src = tBox.Text.ToLower();
                employes = employes.Where(e=> e.FirstName.ToLower().Contains(src) ||
                                              e.LastName.ToLower().Contains(src) ||
                                              e.Patronymic.ToLower().Contains(src) ||
                                              e.PhoneNumber.ToLower().Contains(src) ||
                                              e.User.Login.ToLower().Contains(src) ||
                                              e.Position.Name.ToLower().Contains(src)
                                              ).ToList(); 
            }
            dataGrid.ItemsSource = employes;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            UpdateDataGrid();
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (dataGrid.SelectedItem == null)
                return;

            Employe employe = dataGrid.SelectedItem as Employe;
            NavigationService.Navigate(new EditEmployePage(employe));
        }

        private void btnDel_Click(object sender, RoutedEventArgs e)
        {
            if (dataGrid.SelectedItem == null)
                return;

            Employe employe = dataGrid.SelectedItem as Employe;
            try
            {
                ToskanaDBEntities.GetContext().Employe.Remove(employe);
                ToskanaDBEntities.GetContext().SaveChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new EditEmployePage());
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            tBox.Text = "";
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateDataGrid();
        }
    }
}
