using System.Windows;
using System.Windows.Controls;
using ShelterApp.Repository;
using ShelterApp.Services;

namespace ShelterApp.Views.Pages
{
    public partial class ProfilePage : Page
    {
        private readonly UserRepository userRepository;
        private readonly ApplicationRepository applicationRepository;

        public ProfilePage()
        {
            InitializeComponent();
            userRepository = new UserRepository();
            applicationRepository = new ApplicationRepository();

            LoadUserData();
            LoadApplications();
        }

        private void LoadUserData()
        {
            if (SessionManager.CurrentUser != null)
            {
                UsernameTextBox.Text = SessionManager.CurrentUser.Username;
                FullNameTextBox.Text = SessionManager.CurrentUser.FullName;
                EmailTextBox.Text = SessionManager.CurrentUser.Email;
            }
        }

        private void LoadApplications()
        {
            if (SessionManager.CurrentUser != null)
            {
                var applications = applicationRepository.GetByUserId(SessionManager.CurrentUser.Id);
                ApplicationsListBox.ItemsSource = applications;
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (SessionManager.CurrentUser != null)
            {
                SessionManager.CurrentUser.FullName = FullNameTextBox.Text;
                SessionManager.CurrentUser.Email = EmailTextBox.Text;

                userRepository.Update(SessionManager.CurrentUser);

                MessageBox.Show("Данные успешно обновлены!", "Успех",
                    MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }
}
