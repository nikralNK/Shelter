using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ShelterApp.Models;
using ShelterApp.Repository;

namespace ShelterApp.Views.Pages
{
    public partial class CatalogPage : Page
    {
        private readonly AnimalRepository animalRepository;
        private List<Animal> allAnimals;

        public CatalogPage()
        {
            InitializeComponent();
            animalRepository = new AnimalRepository();
            LoadAnimals();
        }

        private void LoadAnimals()
        {
            allAnimals = animalRepository.GetAll();
            AnimalsItemsControl.ItemsSource = allAnimals;
        }

        private void ApplyFilterButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedType = (TypeComboBox.SelectedItem as ComboBoxItem)?.Content.ToString();
            var selectedGender = (GenderComboBox.SelectedItem as ComboBoxItem)?.Content.ToString();
            var selectedSize = (SizeComboBox.SelectedItem as ComboBoxItem)?.Content.ToString();

            var type = selectedType == "Все" ? null : selectedType;
            var gender = selectedGender == "Все" ? null : selectedGender;
            var size = selectedSize == "Все" ? null : selectedSize;

            var filteredAnimals = animalRepository.GetFiltered(type, gender, size);
            AnimalsItemsControl.ItemsSource = filteredAnimals;
        }

        private void AnimalCard_Click(object sender, MouseButtonEventArgs e)
        {
            var border = sender as Border;
            var animal = border?.DataContext as Animal;

            if (animal != null)
            {
                NavigationService?.Navigate(new AnimalDetailPage(animal.Id));
            }
        }
    }
}
