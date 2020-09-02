using DAN_LIX_Kristina_Garcia_Francisco.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace DAN_LIX_Kristina_Garcia_Francisco
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Main window
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = new GameViewModel(this);
        }

        /// <summary>
        /// Gets the button that was clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Slide_Clicked(object sender, RoutedEventArgs e)
        {
            var game = DataContext as GameViewModel;
            var button = sender as Button;
            game.ClickedSlide(button.DataContext);
        }

        /// <summary>
        /// Play again button, it restarts the game when clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PlayAgain_C(object sender, RoutedEventArgs e)
        {
            var game = DataContext as GameViewModel;
            game.Restart();
        }
    }
}
