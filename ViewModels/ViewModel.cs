using GuitarWorkshop.Data.DbContext; // Для GuitarDbContext
using GuitarWorkshop.Data.Repositories;
using GuitarWorkshop.ViewModels; // Для RelayCommand
using System.Windows.Input;

namespace GuitarWorkshop.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private readonly GuitarDbContext _context;

        public ICommand ShowClientsCommand { get; }

        public MainViewModel()
        {
            _context = new GuitarDbContext();
            var clientRepo = new ClientRepository(_context);

            ShowClientsCommand = new RelayCommand(() =>
            {
                // Логика показа клиентов
            });
        }
    }
}