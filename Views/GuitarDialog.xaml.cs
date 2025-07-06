using System.Windows;

namespace GuitarWorkshopApp.Views
{
    public partial class GuitarDialog : Window
    {
        public string GuitarType { get; set; }
        public string Year { get; set; }
        public string Materials { get; set; }

        public GuitarDialog()
        {
            DataContext = this;
            InitializeComponent();
            Year = System.DateTime.Now.Year.ToString();
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(GuitarType) ||
                string.IsNullOrWhiteSpace(Year) ||
                string.IsNullOrWhiteSpace(Materials))
            {
                MessageBox.Show("Пожалуйста, заполните все поля.");
                return;
            }
            if (!int.TryParse(Year, out _))
            {
                MessageBox.Show("Год должен быть числом.");
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