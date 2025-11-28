using System.Windows;
using System.Windows.Controls;
using ShelterApp.Helpers;
using ShelterApp.Pages;
using ShelterApp.Services;

namespace ShelterApp.Windows
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Manager.MainFrame = MainFrame;
            Manager.MainFrame.Navigate(new CatalogPage());

            if (SessionManager.CurrentUser != null && SessionManager.CurrentUser.Role == "Admin")
            {
                AdminButton.Visibility = Visibility.Visible;
            }
        }

        private void CatalogButton_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new CatalogPage());
        }

        private void FavoritesButton_Click(object sender, RoutedEventArgs e)
        {
            if (SessionManager.CurrentUser == null)
            {
                MessageBox.Show("Необходимо войти в систему", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            Manager.MainFrame.Navigate(new FavoritesPage());
        }

        private void ProfileButton_Click(object sender, RoutedEventArgs e)
        {
            if (SessionManager.CurrentUser == null)
            {
                MessageBox.Show("Необходимо войти в систему", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            Manager.MainFrame.Navigate(new ProfilePage());
        }

        private void AdminButton_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new AdminPage());
        }

        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Вы уверены, что хотите выйти?", "Подтверждение",
                MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                SessionManager.CurrentUser = null;
                var loginWindow = new LoginWindow();
                loginWindow.Show();
                this.Close();
            }
        }

        private void MainFrame_ContentRendered(object sender, System.EventArgs e)
        {
            if (MainFrame.CanGoBack)
            {
                MainFrame.RemoveBackEntry();
            }
        }
    }
}
