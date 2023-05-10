using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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
    /// Логика взаимодействия для EditIngredientPage.xaml
    /// </summary>
    public partial class EditIngredientPage : Page
    {
        Ingredient ingredient;
        BitmapImage image;
        bool edit;
        public EditIngredientPage()
        {
            InitializeComponent();
            edit = false;
            this.ingredient = new Ingredient();
        }
        public EditIngredientPage(Ingredient ingredient)
        {
            InitializeComponent();
            edit = true;
            this.ingredient = ingredient;
        }
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            N2.ItemsSource = ToskanaDBEntities.GetContext().Unit.ToList();
            N2.DisplayMemberPath = "Name";

            if (edit)
            {
                N1.Text = ingredient.Name;
                N2.SelectedItem = ingredient.Unit;
                if (ingredient.Image != null)
                {//конвертация из базы
                    MemoryStream ms = new MemoryStream(ingredient.Image, 0, ingredient.Image.Length);
                    ms.Write(ingredient.Image, 0, ingredient.Image.Length);
                    image = ConvertToBitmap(ingredient.Image);
                    N3.Source = image;
                }
                else
                    N3.Source = null;
                              
            }
        }
        private void btnLoad_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();

            dlg.DefaultExt = ".png";
            dlg.Filter = "JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg|GIF Files (*.gif)|*.gif";

            Nullable<bool> result = dlg.ShowDialog();

            if (result == true)
            {
                string filename = dlg.FileName;
                image = new BitmapImage(new Uri(filename));
                N3.Source = image;
            }
        }

        private void btnDel_Click(object sender, RoutedEventArgs e)
        {
            image = null;
            N3.Source = null;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            ingredient.Name = N1.Text;
            ingredient.Unit = N2.SelectedItem as Unit; 
            if (image != null)
                ingredient.Image = ConvertToArray(image);

            try
            {
                if(!edit)
                    ToskanaDBEntities.GetContext().Ingredient.Add(ingredient);
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
        public BitmapImage ConvertToBitmap(byte[] value)
        {
            BitmapImage bitmap = new BitmapImage();
            try
            {
                bitmap.BeginInit();
                bitmap.StreamSource = new MemoryStream(value);
                bitmap.EndInit();
                return bitmap;
            }
            catch
            {
                MessageBox.Show("Ошибка изображения в базе данных");
            }
            return null;

        }

        public byte[] ConvertToArray(BitmapImage value)
        {
            BitmapImage image = (BitmapImage)value;
            BitmapEncoder encoder = new PngBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(image));
            using (MemoryStream ms = new MemoryStream())
            {
                encoder.Save(ms);
                return ms.ToArray();
            }
        }

    }
}
