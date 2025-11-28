using System;
using System.Windows;
using System.Windows.Controls;
using ShelterApp.Helpers;
using ShelterApp.Models;
using ShelterApp.Repositories;

namespace ShelterApp.Pages
{
    public partial class AnimalDetailPage : Page
    {
        private readonly AnimalRepository animalRepository;
        private readonly int animalId;
        private Animal currentAnimal;

        public AnimalDetailPage(int id)
        {
            InitializeComponent();
            animalRepository = new AnimalRepository();
            animalId = id;
            LoadAnimal();
        }

        private void LoadAnimal()
        {
            currentAnimal = animalRepository.GetById(animalId);
            if (currentAnimal != null)
            {
                AnimalNameTextBlock.Text = currentAnimal.Name;
                TypeTextBlock.Text = currentAnimal.Type ?? "Не указано";
                BreedTextBlock.Text = currentAnimal.Breed ?? "Не указано";
                AgeTextBlock.Text = currentAnimal.Age + " лет";
                GenderTextBlock.Text = currentAnimal.Gender ?? "Не указано";
                SizeTextBlock.Text = currentAnimal.Size ?? "Не указано";
                TemperamentTextBlock.Text = currentAnimal.Temperament ?? "Информация отсутствует";
                StatusTextBlock.Text = currentAnimal.CurrentStatus ?? "Доступен";
            }
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.GoBack();
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

            MessageBox.Show("Заявка успешно подана! Мы свяжемся с вами в ближайшее время.",
                "Успех", MessageBoxButton.OK, MessageBoxImage.Information);

            NameTextBox.Clear();
            PhoneTextBox.Clear();
            EmailTextBox.Clear();
        }
    }
}
