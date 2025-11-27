using System.Windows.Controls;
using System.Windows.Input;
using ShelterApp.Models;
using ShelterApp.Repository;
using ShelterApp.Services;

namespace ShelterApp.Views.Pages
{
    public partial class FavoritesPage : Page
    {
        private readonly FavoriteRepository favoriteRepository;

        public FavoritesPage()
        {
            InitializeComponent();
            favoriteRepository = new FavoriteRepository();
            LoadFavorites();
        }

        private void LoadFavorites()
        {
            if (SessionManager.CurrentUser != null)
            {
                var favorites = favoriteRepository.GetFavoritesByUserId(SessionManager.CurrentUser.Id);
                FavoritesItemsControl.ItemsSource = favorites;
            }
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
