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
            animalRepository = new AnimalRepository();
            LoadAnimals();
        }

        private void LoadAnimals()
        {
            var animals = animalRepository.GetAll();
            AnimalsItemsControl.ItemsSource = animals;
        }

        private void Filter_Changed(object sender, SelectionChangedEventArgs e)
        {
            if (TypeComboBox == null || GenderComboBox == null || SizeComboBox == null)
                return;

            var type = (TypeComboBox.SelectedItem as ComboBoxItem)?.Content.ToString();
            var gender = (GenderComboBox.SelectedItem as ComboBoxItem)?.Content.ToString();
            var size = (SizeComboBox.SelectedItem as ComboBoxItem)?.Content.ToString();

            var animals = animalRepository.GetFiltered(type, gender, size);
            AnimalsItemsControl.ItemsSource = animals;
        }

        private void ResetFilter_Click(object sender, RoutedEventArgs e)
        {
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
