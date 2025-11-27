using System.Windows;
using ShelterApp.Services;
using ShelterApp.Views.Pages;

namespace ShelterApp.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            if (SessionManager.IsAdmin)
            {
                AdminButton.Visibility = Visibility.Visible;
            }

            MainFrame.Navigate(new CatalogPage());
        }

        private void CatalogButton_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new CatalogPage());
        }

        private void FavoritesButton_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new FavoritesPage());
        }

        private void ProfileButton_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new ProfilePage());
        }

        private void AdminButton_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new AdminPage());
        }

        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            var authService = new AuthService();
            authService.Logout();

            var loginWindow = new LoginWindow();
            loginWindow.Show();
            this.Close();
        }
    }
}
