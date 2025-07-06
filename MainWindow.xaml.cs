using GuitarWorkshopApp.Data;
using GuitarWorkshopApp.Models;
using GuitarWorkshopApp.Views;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;
using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace GuitarWorkshopApp
{
    public partial class MainWindow : Window
    {
        internal GuitarWorkshopContext _context;


        public MainWindow()
        {
            InitializeComponent();
            _context = new GuitarWorkshopContext();
            LoadData();
            MessageBox.Show(_context.Guitars.Count().ToString());
        }

        internal void LoadData()
        {
            GuitarPanel.ItemsSource = _context.Guitars.ToList();

            OrderPanel.ItemsSource = _context.Orders
                .Include(o => o.Guitar)
                .Include(o => o.Client)
                .ToList();

            ClientPanel.ItemsSource = _context.Clients.ToList();

            ConsumablePanel.ItemsSource = _context.Consumables.ToList();
        }

        // Удаление клиента
        private void DeleteClientCard_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var clientId = (int)button.Tag;
            var client = _context.Clients.Find(clientId);

            if (_context.Orders.Any(o => o.ClientId == clientId))
            {
                MessageBox.Show("Нельзя удалить клиента с активными заказами");
                return;
            }

            _context.Clients.Remove(client);
            _context.SaveChanges();
            LoadData();
        }



        // Добавление гитары
        private void AddGuitar_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new Views.GuitarDialog();
            if (dialog.ShowDialog() == true)
            {
                if (!int.TryParse(dialog.Year, out int year))
                {
                    MessageBox.Show("Год должен быть числом.");
                    return;
                }
                var guitar = new Guitars
                {
                    Type = dialog.GuitarType,
                    Year = year,
                    Materials = dialog.Materials
                };
                _context.Guitars.Add(guitar);
                _context.SaveChanges();
                LoadData();
            }
        }

        // Удаление гитары
        private void DeleteGuitar_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.Tag is int guitarId)
            {
                var guitar = _context.Guitars.FirstOrDefault(g => g.Id == guitarId);
                if (guitar != null)
                {
                    _context.Guitars.Remove(guitar);
                    _context.SaveChanges();
                    LoadData();
                }
            }
        }

        // Добавление заказа
        private void AddOrder_Click(object sender, RoutedEventArgs e)
        {
            // Пример простого диалога для заказа
            var dialog = new OrderDialog(_context.Guitars.ToList());
            if (dialog.ShowDialog() == true)
            {
                var order = new Orders
                {
                    Status = dialog.Status,
                    Cost = dialog.Cost,
                    Deadline = dialog.Deadline,
                    GuitarId = dialog.SelectedGuitarId
                };
                _context.Orders.Add(order);
                _context.SaveChanges();
                LoadData();
            }
        }

        private void DeleteGuitarCard_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.Tag is int guitarId)
            {
                var guitar = _context.Guitars.Find(guitarId);
                if (guitar != null)
                {
                    // Проверка на связанные заказы
                    if (_context.Orders.Any(o => o.GuitarId == guitarId))
                    {
                        MessageBox.Show("Нельзя удалить гитару с активными заказами");
                        return;
                    }

                    _context.Guitars.Remove(guitar);
                    _context.SaveChanges();
                    LoadData();
                }
            }
        }

        private void EditGuitar_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn && btn.Tag is int guitarId) // Исправлено: получаем Id
            {
                // Находим объект в базе данных по Id
                var guitar = _context.Guitars.FirstOrDefault(g => g.Id == guitarId);
                if (guitar != null)
                {
                    var dialog = new EditDialog(guitar, this); // Передаем найденный объект
                    if (dialog.ShowDialog() == true)
                    {
                        _context.SaveChanges();
                        LoadData();
                    }
                }
            }
        }


        // Аналогичные методы для других сущностей:
        private void EditOrder_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn && btn.Tag is int guitarId) // Исправлено: получаем Id
            {
                // Находим объект в базе данных по Id
                var guitar = _context.Orders.FirstOrDefault(g => g.Id == guitarId);
                if (guitar != null)
                {
                    var dialog = new EditDialog(guitar, this); // Передаем найденный объект
                    if (dialog.ShowDialog() == true)
                    {
                        _context.SaveChanges();
                        LoadData();
                    }
                }
            }
        }

        private void EditClient_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn && btn.Tag is int guitarId) // Исправлено: получаем Id
            {
                // Находим объект в базе данных по Id
                var guitar = _context.Clients.FirstOrDefault(g => g.Id == guitarId);
                if (guitar != null)
                {
                    var dialog = new EditDialog(guitar, this); // Передаем найденный объект
                    if (dialog.ShowDialog() == true)
                    {
                        _context.SaveChanges();
                        LoadData();
                    }
                }
            }
        }

        private void EditMaterial_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn && btn.Tag is int guitarId) // Исправлено: получаем Id
            {
                // Находим объект в базе данных по Id
                var guitar = _context.Consumables.FirstOrDefault(g => g.Id == guitarId);
                if (guitar != null)
                {
                    var dialog = new EditDialog(guitar, this); // Передаем найденный объект
                    if (dialog.ShowDialog() == true)
                    {
                        _context.SaveChanges();
                        LoadData();
                    }
                }
            }
        }

        // Добавление клиента
        private void AddClient_Click(object sender, RoutedEventArgs e)
        {
            var name = Microsoft.VisualBasic.Interaction.InputBox("Введите имя клиента:", "Добавить клиента", "Новый клиент");
            if (string.IsNullOrWhiteSpace(name))
                return;
            var contact = Microsoft.VisualBasic.Interaction.InputBox("Введите контактную информацию:", "Добавить клиента", "Контактная информация");
            if (string.IsNullOrWhiteSpace(contact))
                return;

            var clients = new Clients
            {
                Name = name,
                ContactInfo = contact
            };
            _context.Clients.Add(clients);
            _context.SaveChanges();
            LoadData();
        }
        

        // Добавление расходного материала
        private void AddConsumable_Click(object sender, RoutedEventArgs e)
        {
            var name = Microsoft.VisualBasic.Interaction.InputBox("Введите название расходного материала:", "Добавить расходный материал", "Новый расходный материал");
            if (string.IsNullOrWhiteSpace(name))
                return;
            var quantityStr = Microsoft.VisualBasic.Interaction.InputBox("Введите количество:", "Добавить расходный материал", "10");
            if (!int.TryParse(quantityStr, out int quantity))
            {
                MessageBox.Show("Количество должно быть числом.");
                return;
            }

            var consumables = new Consumables
            {
                Name = name,
                Quantity = quantity
            };
            _context.Consumables.Add(consumables);
            _context.SaveChanges();
            LoadData();
        }


        private void DeleteOrderCard_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.Tag is int orderId)
            {
                var order = _context.Orders.Find(orderId);
                if (order != null)
                {
                    _context.Orders.Remove(order);
                    _context.SaveChanges();
                    LoadData();
                }
            }
        }

        private void DeleteConsumableCard_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.Tag is int consumableId)
            {
                var consumable = _context.Consumables.Find(consumableId);
                if (consumable != null)
                {
                    _context.Consumables.Remove(consumable);
                    _context.SaveChanges();
                    LoadData();
                }
            }
        }

        // Экспорт заказов в CSV
        private void ExportOrders_Click(object sender, RoutedEventArgs e)
        {
            var saveFileDialog = new SaveFileDialog
            {
                Filter = "CSV файлы (*.csv)|*.csv",
                Title = "Экспорт заказов",
                FileName = $"Заказы_{DateTime.Now:yyyyMMdd_HHmmss}.csv"
            };

            if (saveFileDialog.ShowDialog() == true)
            {
                try
                {
                    var orders = _context.Orders
                        .Include(o => o.Guitar)
                        .Include(o => o.Client)
                        .ToList();

                    var csvContent = new StringBuilder();
                    csvContent.AppendLine("ID;Статус;Стоимость;Срок;Гитара;Клиент");

                    foreach (var order in orders)
                    {
                        csvContent.AppendLine(
                            $"{order.Id};" +
                            $"{order.Status};" +
                            $"{order.Cost};" +
                            $"{order.Deadline:dd.MM.yyyy};" +
                            $"{order.Guitar?.Type};" +
                            $"{order.Client?.Name}");
                    }

                    File.WriteAllText(saveFileDialog.FileName, csvContent.ToString(), Encoding.UTF8);
                    MessageBox.Show("Экспорт завершен успешно!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка экспорта: {ex.Message}");
                }
            }
        }

        



    }
}
