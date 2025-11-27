using System;
using System.Windows;
using ShelterApp.Services;

namespace ShelterApp.Views
{
    public partial class LoginWindow : Window
    {
        private readonly AuthService authService;

        public LoginWindow()
        {
            InitializeComponent();
            authService = new AuthService();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            ErrorTextBlock.Visibility = Visibility.Collapsed;

            var username = UsernameTextBox.Text.Trim();
            var password = PasswordBox.Password;

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                ErrorTextBlock.Text = "Заполните все поля";
                ErrorTextBlock.Visibility = Visibility.Visible;
                return;
            }

            var user = authService.Login(username, password);
            if (user != null)
            {
                var mainWindow = new MainWindow();
                mainWindow.Show();
                this.Close();
            }
            else
            {
                ErrorTextBlock.Text = "Неверный логин или пароль";
                ErrorTextBlock.Visibility = Visibility.Visible;
            }
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            var registerWindow = new RegisterWindow();
            registerWindow.ShowDialog();
        }

        private void ResetAdminPassword_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                authService.ResetPassword("admin", "admin");
                authService.ResetPassword("user1", "user1");
                MessageBox.Show("Пароли сброшены!\nadmin:admin\nuser1:user1", "Успех",
                    MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
