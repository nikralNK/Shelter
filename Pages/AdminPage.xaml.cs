using System.Windows;
using System.Windows.Controls;
using ShelterApp.Models;
using ShelterApp.Repositories;

namespace ShelterApp.Pages
{
    public partial class AdminPage : Page
    {
        private readonly AnimalRepository animalRepository;

        public AdminPage()
        {
            InitializeComponent();
            animalRepository = new AnimalRepository();
            LoadAnimals();
        }

        private void LoadAnimals()
        {
            var animals = animalRepository.GetAll();
            AnimalsDataGrid.ItemsSource = animals;
        }

        private void AddAnimal_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Функция добавления животного", "Информация",
                MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void EditAnimal_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var animal = button?.DataContext as Animal;

            if (animal != null)
            {
                MessageBox.Show($"Редактирование: {animal.Name}", "Информация",
                    MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void DeleteAnimal_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var animal = button?.DataContext as Animal;

            if (animal != null)
            {
                var result = MessageBox.Show($"Удалить {animal.Name}?", "Подтверждение",
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
    }
}
