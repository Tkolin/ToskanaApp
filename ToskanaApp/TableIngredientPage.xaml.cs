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
    /// Логика взаимодействия для TableIngredientPage.xaml
    /// </summary>
    public partial class TableIngredientPage : Page
    {
        public TableIngredientPage()
        {
            InitializeComponent();
        }
        public void UpdateDataGrid()
        {
            List<Ingredient> ingredients = ToskanaDBEntities.GetContext().Ingredient.ToList();

            if (tBox.Text.Length > 0)
            {
                string src = tBox.Text.ToLower();
                ingredients = ingredients.Where(e => e.Name.ToLower().Contains(src) ||
                                              e.Unit.Name.ToLower().Contains(src)
                                              ).ToList();
            }



            dataGrid.ItemsSource = ingredients;
        }
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnDel_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {

        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void btnReset_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
