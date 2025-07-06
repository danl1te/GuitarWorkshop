using System;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;

namespace GuitarWorkshopApp.Views
{
    public partial class EditDialog : Window
    {
        private object _entity;
        private MainWindow _mainWindow;

        public EditDialog(object entity, MainWindow mainWindow)
        {
            InitializeComponent(); // Важно: файл XAML должен называться EditDialog.xaml
            _entity = entity;
            _mainWindow = mainWindow;
            DataContext = entity;
            GenerateFields();
        }

        private void GenerateFields()
        {
            if (EditFieldsPanel == null) return;

            foreach (var prop in _entity.GetType().GetProperties())
            {
                if (prop.Name == "Id") continue;

                var stackPanel = new StackPanel { Orientation = Orientation.Vertical, Margin = new Thickness(0, 5, 0, 5) };
                var textBlock = new TextBlock { Text = prop.Name, Margin = new Thickness(5, 0, 5, 2) };
                var textBox = new TextBox
                {
                    Text = prop.GetValue(_entity)?.ToString(),
                    Margin = new Thickness(5),
                    Tag = prop
                };

                stackPanel.Children.Add(textBlock);
                stackPanel.Children.Add(textBox);
                EditFieldsPanel.Children.Add(stackPanel);
            }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            foreach (StackPanel stackPanel in EditFieldsPanel.Children.OfType<StackPanel>())
            {
                if (stackPanel.Children.Count < 2) continue;

                var textBox = stackPanel.Children[1] as TextBox;
                if (textBox == null) continue;

                var prop = textBox.Tag as PropertyInfo;
                if (prop == null) continue;

                try
                {
                    // Обработка разных типов данных
                    if (prop.PropertyType == typeof(string))
                    {
                        prop.SetValue(_entity, textBox.Text);
                    }
                    else if (prop.PropertyType == typeof(int) || prop.PropertyType == typeof(int?))
                    {
                        if (int.TryParse(textBox.Text, out int intValue))
                        {
                            prop.SetValue(_entity, intValue);
                        }
                    }
                    else if (prop.PropertyType == typeof(decimal) || prop.PropertyType == typeof(decimal?))
                    {
                        if (decimal.TryParse(textBox.Text, out decimal decimalValue))
                        {
                            prop.SetValue(_entity, decimalValue);
                        }
                    }
                    else if (prop.PropertyType == typeof(DateTime) || prop.PropertyType == typeof(DateTime?))
                    {
                        if (DateTime.TryParse(textBox.Text, out DateTime dateValue))
                        {
                            prop.SetValue(_entity, dateValue);
                        }
                    }
                    // Добавьте другие типы по необходимости
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при сохранении свойства {prop.Name}: {ex.Message}");
                }
            }

            _mainWindow._context.SaveChanges();
            _mainWindow.LoadData();
            DialogResult = true;
            Close();
        }



        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Вы уверены, что хотите удалить эту запись?",
                               "Подтверждение удаления",
                               MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                // Удаляем объект через контекст главного окна
                _mainWindow._context.Remove(_entity);
                _mainWindow._context.SaveChanges();
                _mainWindow.LoadData();
                DialogResult = true;
                Close();
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}
