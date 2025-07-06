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
using System.Windows.Shapes;
using GuitarWorkshopApp.Models;

namespace GuitarWorkshopApp.Views
{
    /// <summary>
    /// Логика взаимодействия для OrderDialog.xaml
    /// </summary>
    public partial class OrderDialog : Window
    {
        public string Status { get; set; }
        public decimal Cost { get; set; }
        public System.DateTime Deadline { get; set; } = System.DateTime.Now.AddDays(7);
        public int SelectedGuitarId { get; set; }
        public List<Guitars> Guitars { get; }

        public OrderDialog(List<Guitars> guitars)
        {
            Guitars = guitars;
            InitializeComponent();
            DataContext = this;
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(Status) || SelectedGuitarId == 0)
            {
                MessageBox.Show("Пожалуйста, заполните все обязательные поля.");
                return;
            }
            DialogResult = true;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
