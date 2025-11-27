using System;
using System.Windows;
using System.Windows.Controls;
using ShelterApp.Models;
using ShelterApp.Repository;
using ShelterApp.Services;

namespace ShelterApp.Views.Pages
{
    public partial class AnimalDetailPage : Page
    {
        private readonly AnimalRepository animalRepository;
        private readonly FavoriteRepository favoriteRepository;
        private readonly ApplicationRepository applicationRepository;
        private readonly GuardianRepository guardianRepository;
        private Animal currentAnimal;

        public AnimalDetailPage(int animalId)
        {
            InitializeComponent();
            animalRepository = new AnimalRepository();
            favoriteRepository = new FavoriteRepository();
            applicationRepository = new ApplicationRepository();
            guardianRepository = new GuardianRepository();

            LoadAnimal(animalId);
        }

        private void LoadAnimal(int animalId)
        {
            currentAnimal = animalRepository.GetById(animalId);
            if (currentAnimal != null)
            {
                AnimalNameTextBlock.Text = currentAnimal.Name;
                StatusTextBlock.Text = currentAnimal.CurrentStatus;
                TypeTextBlock.Text = currentAnimal.Type;
                BreedTextBlock.Text = currentAnimal.Breed ?? "Не указано";
                AgeTextBlock.Text = currentAnimal.Age + " лет";
                GenderTextBlock.Text = currentAnimal.Gender ?? "Не указано";
                SizeTextBlock.Text = currentAnimal.Size ?? "Не указано";
                TemperamentTextBlock.Text = currentAnimal.Temperament ?? "Информация отсутствует";
            }
        }

        private void AddToFavorites_Click(object sender, RoutedEventArgs e)
        {
            if (SessionManager.CurrentUser == null)
            {
                MessageBox.Show("Необходимо войти в систему", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            favoriteRepository.Add(SessionManager.CurrentUser.Id, currentAnimal.Id);
            MessageBox.Show("Животное добавлено в избранное!", "Успех",
                MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void SubmitApplication_Click(object sender, RoutedEventArgs e)
        {
            var name = NameTextBox.Text.Trim();
            var phone = PhoneTextBox.Text.Trim();
            var email = EmailTextBox.Text.Trim();

            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(phone) || string.IsNullOrEmpty(email))
            {
                MessageBox.Show("Заполните все поля", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var guardian = new Guardian
            {
                Name = name,
                Number = phone,
                Email = email
            };

            int guardianId = guardianRepository.Add(guardian);

            var application = new Models.Application
            {
                IdGuardian = guardianId,
                IdAnimal = currentAnimal.Id,
                SubmissionDate = DateTime.Now,
                ApplicationStatus = "В рассмотрении"
            };

            applicationRepository.Add(application);

            MessageBox.Show("Заявка успешно подана!", "Успех",
                MessageBoxButton.OK, MessageBoxImage.Information);

            NameTextBox.Clear();
            PhoneTextBox.Clear();
            EmailTextBox.Clear();
        }
    }
}
