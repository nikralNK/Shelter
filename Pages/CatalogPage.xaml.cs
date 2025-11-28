using System.Windows;
using System.Windows.Controls;
using ShelterApp.Helpers;
using ShelterApp.Repositories;

namespace ShelterApp.Pages
{
    public partial class CatalogPage : Page
    {
        private readonly AnimalRepository animalRepository;

        public CatalogPage()
        {
            InitializeComponent();
            try
            {
                animalRepository = new AnimalRepository();
                LoadAnimals();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show($"Ошибка подключения к базе данных:\n{ex.Message}\n\nУбедитесь, что PostgreSQL запущен и база данных создана.",
                    "Ошибка подключения", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void LoadAnimals()
        {
            try
            {
                var animals = animalRepository.GetAll();
                AnimalsItemsControl.ItemsSource = animals;
            }
            catch (System.Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки данных:\n{ex.Message}",
                    "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Filter_Changed(object sender, SelectionChangedEventArgs e)
        {
            if (TypeComboBox == null || GenderComboBox == null || SizeComboBox == null || animalRepository == null)
                return;

            try
            {
                var type = (TypeComboBox.SelectedItem as ComboBoxItem)?.Content.ToString();
                var gender = (GenderComboBox.SelectedItem as ComboBoxItem)?.Content.ToString();
                var size = (SizeComboBox.SelectedItem as ComboBoxItem)?.Content.ToString();

                var animals = animalRepository.GetFiltered(type, gender, size);
                AnimalsItemsControl.ItemsSource = animals;
            }
            catch (System.Exception ex)
            {
                MessageBox.Show($"Ошибка фильтрации:\n{ex.Message}",
                    "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ResetFilter_Click(object sender, RoutedEventArgs e)
        {
            if (animalRepository == null)
                return;

            TypeComboBox.SelectedIndex = 0;
            GenderComboBox.SelectedIndex = 0;
            SizeComboBox.SelectedIndex = 0;
            LoadAnimals();
        }

        private void AnimalCard_Click(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
        }

        private void Details_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button?.Tag != null && int.TryParse(button.Tag.ToString(), out int animalId))
            {
                Manager.MainFrame.Navigate(new AnimalDetailPage(animalId));
            }
        }
    }
}
