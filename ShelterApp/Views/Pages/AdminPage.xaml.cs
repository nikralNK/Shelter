using System.Windows;
using System.Windows.Controls;
using ShelterApp.Models;
using ShelterApp.Repository;

namespace ShelterApp.Views.Pages
{
    public partial class AdminPage : Page
    {
        private readonly AnimalRepository animalRepository;
        private readonly ApplicationRepository applicationRepository;

        public AdminPage()
        {
            InitializeComponent();
            animalRepository = new AnimalRepository();
            applicationRepository = new ApplicationRepository();

            LoadAnimals();
            LoadApplications();
        }

        private void LoadAnimals()
        {
            var animals = animalRepository.GetAll();
            AnimalsDataGrid.ItemsSource = animals;
        }

        private void LoadApplications()
        {
            var applications = applicationRepository.GetAll();
            ApplicationsDataGrid.ItemsSource = applications;
        }

        private void AddAnimal_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Функционал добавления животного", "Информация",
                MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void EditAnimal_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var animal = button?.DataContext as Animal;

            if (animal != null)
            {
                MessageBox.Show($"Редактирование животного: {animal.Name}", "Информация",
                    MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void DeleteAnimal_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var animal = button?.DataContext as Animal;

            if (animal != null)
            {
                var result = MessageBox.Show($"Удалить животное {animal.Name}?", "Подтверждение",
                    MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    animalRepository.Delete(animal.Id);
                    LoadAnimals();
                    MessageBox.Show("Животное удалено", "Успех",
                        MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }

        private void ApproveApplication_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var application = button?.DataContext as Application;

            if (application != null)
            {
                applicationRepository.UpdateStatus(application.Id, "Одобрена");
                LoadApplications();
                MessageBox.Show("Заявка одобрена", "Успех",
                    MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void RejectApplication_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var application = button?.DataContext as Application;

            if (application != null)
            {
                applicationRepository.UpdateStatus(application.Id, "Отклонена");
                LoadApplications();
                MessageBox.Show("Заявка отклонена", "Информация",
                    MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }
}
