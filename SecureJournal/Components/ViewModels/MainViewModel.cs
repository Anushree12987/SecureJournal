using System.ComponentModel;
using System.Runtime.CompilerServices;
using SecureJournal.Services;

namespace SecureJournal.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private readonly SecurityService _securityService;

        public MainViewModel(string dbPath)
        {
            _securityService = new SecurityService(dbPath);
        }

        private string _currentView = "Dashboard";
        public string CurrentView
        {
            get => _currentView;
            set { _currentView = value; OnPropertyChanged(); }
        }

        private string? _username;
        public string? Username
        {
            get => _username;
            set { _username = value; OnPropertyChanged(); }
        }

        public bool Login(string username, string password)
        {
            var success = _securityService.Login(username, password);
            if (success)
                Username = username;
            return success;
        }

        public bool Register(string username, string password)
        {
            return _securityService.Register(username, password);
        }

        public void NavigateTo(string view)
        {
            CurrentView = view;
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string? name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}