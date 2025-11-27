using System.Windows;
using ShelterApp.Services;

namespace ShelterApp.Views
{
    public partial class RegisterWindow : Window
    {
        private readonly AuthService authService;

        public RegisterWindow()
        {
            InitializeComponent();
            authService = new AuthService();
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            ErrorTextBlock.Visibility = Visibility.Collapsed;

            var username = UsernameTextBox.Text.Trim();
            var email = EmailTextBox.Text.Trim();
            var fullName = FullNameTextBox.Text.Trim();
            var password = PasswordBox.Password;
            var confirmPassword = ConfirmPasswordBox.Password;

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(email) ||
                string.IsNullOrEmpty(password))
            {
                ErrorTextBlock.Text = "Заполните обязательные поля";
                ErrorTextBlock.Visibility = Visibility.Visible;
                return;
            }

            if (password != confirmPassword)
            {
                ErrorTextBlock.Text = "Пароли не совпадают";
                ErrorTextBlock.Visibility = Visibility.Visible;
                return;
            }

            if (password.Length < 6)
            {
                ErrorTextBlock.Text = "Пароль должен быть не менее 6 символов";
                ErrorTextBlock.Visibility = Visibility.Visible;
                return;
            }

            bool success = authService.Register(username, password, email, fullName);
            if (success)
            {
                MessageBox.Show("Регистрация прошла успешно!", "Успех",
                    MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close();
            }
            else
            {
                ErrorTextBlock.Text = "Пользователь с таким логином или email уже существует";
                ErrorTextBlock.Visibility = Visibility.Visible;
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
