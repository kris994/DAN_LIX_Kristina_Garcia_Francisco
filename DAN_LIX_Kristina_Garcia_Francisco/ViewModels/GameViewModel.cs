using System;

namespace DAN_LIX_Kristina_Garcia_Francisco.ViewModels
{
    /// <summary>
    /// Flow of the game itself
    /// </summary>
    class GameViewModel : ViewModelBase
    {
        #region Properties
        /// <summary>
        /// Collection of picture slides
        /// </summary>
        public PictureCollectionViewModel Slides { get; private set; }
        /// <summary>
        /// Game information, if its won or lost
        /// </summary>
        public GameInfoViewModel GameInfo { get; private set; }
        /// <summary>
        /// Time spent in game
        /// </summary>
        public TimerViewModel Timer { get; private set; }
        /// <summary>
        /// Main Window
        /// </summary>
        readonly MainWindow main;
        #endregion

        #region Constructor
        /// <summary>
        /// Game Preview Constructor
        /// </summary>
        /// <param name="mainOpen"></param>
        public GameViewModel(MainWindow mainOpen)
        {
            main = mainOpen;
            SetupGame();
        }
        #endregion

        /// <summary>
        /// Sets up the basics needed for the game to start
        /// </summary>
        private void SetupGame()
        {
            // Loads are the pictures
            Slides = new PictureCollectionViewModel();
            // Loads the timer
            Timer = new TimerViewModel(new TimeSpan(0, 0, 1));
            // Loads the game information
            GameInfo = new GameInfoViewModel();     
            GameInfo.ClearInfo();

            // Get the images from the image folder then display to be memorized
            Slides.CreatePictures(@"../../Assets/Pictures");
            Slides.Memorize();

            // Game has started, begin the timer
            Timer.Start();

            // Update game information
            OnPropertyChanged("Slides");
            OnPropertyChanged("Timer");
            OnPropertyChanged("GameInfo");
        }

        /// <summary>
        /// Picture is clicked
        /// </summary>
        /// <param name="slide">The button we are clicking</param>
        public void ClickedSlide(object slide)
        {
            // Checks if its possible to select a slide
            if (Slides.CanSelect)
            {
                var selected = slide as PictureViewModel;
                Slides.SelectSlide(selected);
            }

            // Checks if the slides matched
            if (!Slides.AreSlidesActive)
            {
                Slides.CheckIfMatched();
            }

            // Updates the game status
            GameStatus();
        }

        /// <summary>
        /// Game status
        /// </summary>
        private void GameStatus()
        {
            // If all slides were selected, stop the game
            if (Slides.AllSlidesMatched)
            {
                GameInfo.GameStatus(true);
                Timer.Stop();
            }
        }

        /// <summary>
        /// Restart the game from begining
        /// </summary>
        public void Restart()
        {
            SetupGame();
        }
    }
}
