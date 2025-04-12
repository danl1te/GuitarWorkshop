using GuitarWorkshop.Controls;
using GuitarWorkshop.Data.DbContext;
using GuitarWorkshop.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Windows;

namespace GuitarWorkshop.View
{
    public partial class MainWindow : Window
    {
        private readonly GuitarDbContext _context;

        public MainWindow()
        {
            InitializeComponent();
            _context = new GuitarDbContext();
            LoadInitialData();
        }

        private void LoadInitialData()
        {
            try
            {
                // Загрузка клиентов
                OrderClientComboBox.ItemsSource = _context.Clients
                    .AsNoTracking()
                    .Select(c => new
                    {
                        c.Id,
                        c.LastName,
                        c.FirstName
                    })
                    .ToList();

                // Загрузка гитар
                OrderGuitarComboBox.ItemsSource = _context.Guitars
                    .AsNoTracking()
                    .Include(g => g.Client)
                    .Select(g => new
                    {
                        g.Id,
                        g.Model,
                        g.Brand,
                        ClientName = g.Client != null ? g.Client.LastName : ""
                    })
                    .ToList();

                // Загрузка услуг
                OrderServiceComboBox.ItemsSource = _context.RepairServices
                    .AsNoTracking()
                    .Select(s => new
                    {
                        s.Id,
                        s.Name
                    })
                    .ToList();

                // Типы гитар
                GuitarTypeComboBox.ItemsSource = new[]
                {
                    "Acoustic", "Electric", "Bass", "Classical"
                };

                RefreshDataGrids();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки данных: {ex.Message}",
                    "Ошибка",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }

        private void RefreshDataGrids()
        {
            try
            {
                // Клиенты
                ClientsDataGrid.ItemsSource = _context.Clients
                    .AsNoTracking()
                    .Select(c => new
                    {
                        c.Id,
                        c.LastName,
                        c.FirstName,
                        Phone = c.Phone ?? "-",
                        Email = c.Email ?? "-",
                        RegDate = c.RegistrationDate.ToString("dd.MM.yyyy")
                    })
                    .ToList();

                // Гитары
                GuitarsDataGrid.ItemsSource = _context.Guitars
                    .AsNoTracking()
                    .Include(g => g.Client)
                    .Select(g => new
                    {
                        g.Id,
                        g.Brand,
                        g.Model,
                        g.Year,
                        Serial = g.SerialNumber ?? "-",
                        g.Type,
                        Owner = g.Client != null ? g.Client.LastName : "-"
                    })
                    .ToList();

                // Заказы
                OrdersDataGrid.ItemsSource = _context.Orders
                    .AsNoTracking()
                    .Include(o => o.Client)
                    .Include(o => o.Guitar)
                    .Include(o => o.RepairService)
                    .Select(o => new
                    {
                        o.Id,
                        Date = o.OrderDate.ToString("dd.MM.yyyy"),
                        Client = o.Client != null ? o.Client.LastName : "-",
                        Guitar = o.Guitar != null ? $"{o.Guitar.Brand} {o.Guitar.Model}" : "-",
                        Service = o.RepairService != null ? o.RepairService.Name : "-",
                        o.Status,
                        Price = o.Price.ToString("C")
                    })
                    .ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка обновления данных: {ex.Message}",
                    "Ошибка",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }

        private void AddClientButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(ClientFirstNameTextBox.Text) ||
                string.IsNullOrWhiteSpace(ClientLastNameTextBox.Text))
            {
                MessageBox.Show("Имя и фамилия обязательны для заполнения");
                return;
            }

            var client = new Client
            {
                FirstName = ClientFirstNameTextBox.Text.Trim(),
                LastName = ClientLastNameTextBox.Text.Trim(),
                Phone = string.IsNullOrWhiteSpace(ClientPhoneTextBox.Text) ?
                       null : ClientPhoneTextBox.Text.Trim(),
                Email = string.IsNullOrWhiteSpace(ClientEmailTextBox.Text) ?
                       null : ClientEmailTextBox.Text.Trim(),
                Address = string.IsNullOrWhiteSpace(ClientAddressTextBox.Text) ?
                         null : ClientAddressTextBox.Text.Trim(),
                Notes = string.IsNullOrWhiteSpace(ClientNotesTextBox.Text) ?
                       null : ClientNotesTextBox.Text.Trim(),
                RegistrationDate = DateTime.Now
            };

            try
            {
                _context.Clients.Add(client);
                _context.SaveChanges();
                RefreshDataGrids();
                ClearClientForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при добавлении клиента: {ex.Message}");
            }
        }

        private void AddGuitarButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(GuitarBrandTextBox.Text) ||
                string.IsNullOrWhiteSpace(GuitarModelTextBox.Text))
            {
                MessageBox.Show("Бренд и модель обязательны для заполнения");
                return;
            }

            if (!int.TryParse(GuitarYearTextBox.Text, out int year) || year < 1900 || year > DateTime.Now.Year)
            {
                MessageBox.Show("Укажите корректный год выпуска");
                return;
            }

            var guitar = new Guitar
            {
                Brand = GuitarBrandTextBox.Text.Trim(),
                Model = GuitarModelTextBox.Text.Trim(),
                Year = year,
                SerialNumber = string.IsNullOrWhiteSpace(GuitarSerialTextBox.Text) ?
                             null : GuitarSerialTextBox.Text.Trim(),
                Type = GuitarTypeComboBox.SelectedItem?.ToString() ?? "Acoustic",
                ConditionDescription = string.IsNullOrWhiteSpace(GuitarConditionTextBox.Text) ?
                                      null : GuitarConditionTextBox.Text.Trim(),
                ClientId = (int?)GuitarClientComboBox.SelectedValue
            };

            try
            {
                _context.Guitars.Add(guitar);
                _context.SaveChanges();
                RefreshDataGrids();
                ClearGuitarForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при добавлении гитары: {ex.Message}");
            }
        }

        private void CreateOrderButton_Click(object sender, RoutedEventArgs e)
        {
            if (OrderClientComboBox.SelectedItem == null ||
                OrderGuitarComboBox.SelectedItem == null)
            {
                MessageBox.Show("Выберите клиента и гитару");
                return;
            }

            if (!decimal.TryParse(OrderPriceTextBox.Text, out decimal price) || price <= 0)
            {
                MessageBox.Show("Укажите корректную стоимость");
                return;
            }

            var order = new Order
            {
                ClientId = (int)OrderClientComboBox.SelectedValue,
                GuitarId = (int)OrderGuitarComboBox.SelectedValue,
                RepairServiceId = (int?)OrderServiceComboBox.SelectedValue,
                Description = string.IsNullOrWhiteSpace(OrderDescriptionTextBox.Text) ?
                            null : OrderDescriptionTextBox.Text.Trim(),
                Price = price,
                Status = "Новый",
                OrderDate = DateTime.Now
            };

            try
            {
                _context.Orders.Add(order);
                _context.SaveChanges();
                RefreshDataGrids();
                ClearOrderForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при создании заказа: {ex.Message}");
            }
        }

        private void GenerateActButton_Click(object sender, RoutedEventArgs e)
        {
            if (OrdersDataGrid.SelectedItem == null)
            {
                MessageBox.Show("Выберите заказ для формирования акта");
                return;
            }

            // Здесь будет код генерации акта
            MessageBox.Show("Акт успешно сформирован");
        }

        private void CompleteOrderButton_Click(object sender, RoutedEventArgs e)
        {
            if (OrdersDataGrid.SelectedItem == null)
            {
                MessageBox.Show("Выберите заказ");
                return;
            }

            try
            {
                dynamic selectedOrder = OrdersDataGrid.SelectedItem;
                var order = _context.Orders.Find(selectedOrder.Id);

                if (order != null)
                {
                    order.Status = "Выполнен";
                    order.CompletionDate = DateTime.Now;
                    _context.SaveChanges();
                    RefreshDataGrids();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при обновлении заказа: {ex.Message}");
            }
        }

        private void SearchClientButton_Click(object sender, RoutedEventArgs e)
        {
            
            if (ClientSearchTextBox.Text != null && ClientSearchTextBox.Text != "Поиск клиентов" && ClientSearchTextBox.Text != " " )
            {
                var searchText = ClientSearchTextBox.Text.ToLower();
                ClientsDataGrid.ItemsSource = _context.Clients
                    .AsNoTracking()
                    .Where(c => c.LastName.ToLower().Contains(searchText) ||
                               c.FirstName.ToLower().Contains(searchText) ||
                               (c.Phone != null && c.Phone.Contains(searchText)))
                    .Select(c => new
                    {
                        c.Id,
                        c.LastName,
                        c.FirstName,
                        Phone = c.Phone ?? "-",
                        Email = c.Email ?? "-"
                    })
                    .ToList();
            }
            else
            {
                RefreshDataGrids();
            }
        }

        private void ClearClientForm()
        {
            ClientFirstNameTextBox.Text = "";
            ClientLastNameTextBox.Text = "";
            ClientPhoneTextBox.Text = "";
            ClientEmailTextBox.Text = "";
            ClientAddressTextBox.Text = "";
            ClientNotesTextBox.Text = "";
        }

        private void ClearGuitarForm()
        {
            GuitarBrandTextBox.Text = "";
            GuitarModelTextBox.Text = "";
            GuitarYearTextBox.Text = "";
            GuitarSerialTextBox.Text = "";
            GuitarConditionTextBox.Text = "";
            GuitarTypeComboBox.SelectedIndex = -1;
            GuitarClientComboBox.SelectedIndex = -1;
        }

        private void ClearOrderForm()
        {
            OrderClientComboBox.SelectedIndex = -1;
            OrderGuitarComboBox.SelectedIndex = -1;
            OrderServiceComboBox.SelectedIndex = -1;
            OrderDescriptionTextBox.Text = "";
            OrderPriceTextBox.Text = "";
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            _context?.Dispose();
        }
    }
}