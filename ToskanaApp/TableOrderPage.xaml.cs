using Microsoft.Office.Interop.Excel;
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
    public partial class TableOrderPage : System.Windows.Controls.Page
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
            NavigationService.Navigate(new EditOrderPage());
        }

        private void btnDel_Click(object sender, RoutedEventArgs e)
        {
            if (dataGrid.SelectedItem == null)
                return;
            Order order = dataGrid.SelectedItem as Order;
            try
            {
                ToskanaDBEntities.GetContext().Order.Remove(order);
                ToskanaDBEntities.GetContext().SaveChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            UpdateDataGrid();
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (dataGrid.SelectedItem == null)
                return;

            Order order = dataGrid.SelectedItem as Order;
            NavigationService.Navigate(new EditOrderPage(order));
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateDataGrid();
        }

        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            dPicEnd.SelectedDate = null;
            dPicStart.SelectedDate = null;
            tBox.Text = "";
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            UpdateDataGrid();
        }

        private void dPicStart_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateDataGrid();
        }

        private void dPicEnd_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateDataGrid();
        }

        private void btnOtchet_Click(object sender, RoutedEventArgs e)
        {
            //подключение таблиц
            Microsoft.Office.Interop.Excel.Application app = new Microsoft.Office.Interop.Excel.Application();
            app.Visible = true;
            app.WindowState = XlWindowState.xlMaximized;

            //создание страницы
            Workbook wb = app.Workbooks.Add(XlWBATemplate.xlWBATWorksheet);
            Worksheet ws = wb.Worksheets[1];

            //18
            DateTime start = new DateTime(DateTime.Now.Year,1 ,1) ;
            DateTime end = DateTime.Now;
            if (dPicStart.SelectedDate != null)
                start = dPicStart.SelectedDate.Value;
            if(dPicEnd.SelectedDate != null)
                end = dPicEnd.SelectedDate.Value;
            //форматирование текста
            ws.StandardWidth = 18;

            ws.Range["A1:F1"].Merge();
            ws.Range["A1"].Value = "Отчёт по приготовлению обедов"; ws.Range["A1"].HorizontalAlignment = XlHAlign.xlHAlignCenter;

            ws.Range["B2"].Value = "Дата начала:";
            ws.Range["C2"].Value = start.Year + "." + start.Month + "." + start.Day;
            ws.Range["D2"].Value = "Дата окончания:";
            ws.Range["E2"].Value = end.Year + "." + end.Month + "." + end.Day;

            ws.Range["A4"].Value = "№"; ws.Range["A6"].ColumnWidth = 6;
            ws.Range["B4"].Value = "Дата приготовления"; ws.Range["B4"].ColumnWidth = 22;
            ws.Range["C4"].Value = "Название блюда";
            ws.Range["D4"].Value = "Кол-во";
            ws.Range["E4"].Value = "Повар"; ws.Range["e4"].ColumnWidth = 9;
 

            List<Order> bt = ToskanaDBEntities.GetContext().Order
                .Where(t => t.Date >= start && t.Date <= end).ToList();

            int startPoint = 5;
            int point = startPoint;
            foreach (Order t in bt)
            {
     
                ws.Range["A" + point].Value = point - 4;
                ws.Range["B" + point].Value = t.Date;
                ws.Range["C" + point].Value = t.Dish.Name;
                ws.Range["D" + point].Value = t.Count;
                ws.Range["E" + point].Value = t.Employe.LastName + " " + t.Employe.FirstName[0]+". " + t.Employe.Patronymic[0]+".";
 
                point++;
            }
            app.Calculation = XlCalculation.xlCalculationAutomatic;
            ws.Calculate();
        }
    }
}
