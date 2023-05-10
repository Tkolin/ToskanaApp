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
    /// Логика взаимодействия для TableDishPage.xaml
    /// </summary>
    public partial class TableDishPage : Page
    {
        public TableDishPage()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {

        }
        public void UpdateDataGrid()
        {
            List<Dish> dishes = ToskanaDBEntities.GetContext().Dish.ToList();

            if (tBox.Text.Length > 0)
            {
                string src = tBox.Text.ToLower();
                dishes = dishes.Where(e => e.Name.ToLower().Contains(src) ||
                                              e.Description.ToLower().Contains(src) ||
                                              e.CalorieContent.ToString().ToLower().Contains(src)
                                              ).ToList();
            }



            dataGrid.ItemsSource = dishes;
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {

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

        private void btnReset_Click(object sender, RoutedEventArgs e)
        {

        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
