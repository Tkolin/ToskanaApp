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
    /// Логика взаимодействия для EditDishPage.xaml
    /// </summary>
    public partial class EditDishPage : Page
    {
        Dish dish;
        bool edit;

        public EditDishPage()
        {
            InitializeComponent();
            edit = false;
            this.dish = new Dish();
        }
        public EditDishPage(Dish dish)
        {
            InitializeComponent();
            edit = true;
            this.dish = dish;
        }
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {

            dataGrid2.ItemsSource = ToskanaDBEntities.GetContext().Ingredient.ToList();


            if (edit)
            {
                B1.Text = dish.Name;
                B1.Text = dish.Description;
                B1.Text = dish.CalorieContent.ToString();
                dataGrid1.ItemsSource = ToskanaDBEntities.GetContext().IngredientForDish.Where(i =>
                            i.DishID == dish.ID).ToList();

            }

        }

        private void btnDel_Click(object sender, RoutedEventArgs e)
        {
            if (dataGrid1.SelectedItem == null)
                return;

            IngredientForDish ingredientForDish = dataGrid1.SelectedItem as IngredientForDish;
            try
            {
                ToskanaDBEntities.GetContext().IngredientForDish.Remove(ingredientForDish);
                ToskanaDBEntities.GetContext().SaveChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (dataGrid2.SelectedItem == null || B1.Text.Length < 1)
                return;

            IngredientForDish ingredientForDish = new IngredientForDish();
            Ingredient ingredient = dataGrid2.SelectedItem as Ingredient;
            try
            {
                if(!edit)
                {
                    dish.Name = B1.Text;
                    dish.Description = B2.Text;
                    dish.CalorieContent = Convert.ToInt32(B3.Text);
                    ToskanaDBEntities.GetContext().Dish.Add(dish);
                    ToskanaDBEntities.GetContext().SaveChanges();
                }
                ingredientForDish.Dish = dish;
                ingredientForDish.Ingredient = ingredient;
                ingredientForDish.Count = Convert.ToInt32( tBoxCount.Text);

                ToskanaDBEntities.GetContext().IngredientForDish.Add(ingredientForDish);
                ToskanaDBEntities.GetContext().SaveChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            dish.Name = B1.Text;
            dish.Description = B2.Text;
            dish.CalorieContent = Convert.ToInt32(B3.Text);

            try
            {
                if (!edit)
                    ToskanaDBEntities.GetContext().Dish.Add(dish);
                ToskanaDBEntities.GetContext().SaveChanges();
                NavigationService.GoBack();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
