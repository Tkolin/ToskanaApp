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
    /// Логика взаимодействия для EditEmployePage.xaml
    /// </summary>
    public partial class EditEmployePage : Page
    {
        Employe employe;
        bool edit;
        public EditEmployePage()
        {
            InitializeComponent();
            edit = false;
            this.employe = new Employe();
        }
        public EditEmployePage(Employe employe)
        {
            InitializeComponent();
            edit = true;
            this.employe = employe;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            N4.ItemsSource = ToskanaDBEntities.GetContext().Gender.ToList();
            N4.DisplayMemberPath = "Name";
            N6.ItemsSource = ToskanaDBEntities.GetContext().Position.ToList();
            N6.DisplayMemberPath = "Name";
            N7.ItemsSource = ToskanaDBEntities.GetContext().User.ToList();
            N7.DisplayMemberPath = "Login";

            if (edit)
            {
                N1.Text = employe.FirstName;
                N2.Text = employe.LastName;
                N3.Text = employe.Patronymic;
                N4.SelectedItem = employe.Gender;
                N5.Text = employe.PhoneNumber;
                N6.SelectedItem = employe.Position;
                N7.SelectedItem = employe.User;
            }
        }
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            employe.FirstName = N1.Text;
            employe.LastName = N2.Text;
            employe.Patronymic = N3.Text;
            employe.Gender = N4.SelectedItem as Gender;
            employe.PhoneNumber = N5.Text;
            employe.Position = N6.SelectedItem as Position;
            employe.User = N7.SelectedItem as User;

            try
            {
                if(!edit)
                    ToskanaDBEntities.GetContext().Employe.Add(employe);
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
