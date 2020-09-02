using DAN_LIX_Kristina_Garcia_Francisco.Models;
using System.Windows;

namespace DAN_LIX_Kristina_Garcia_Francisco.ViewModels
{
    /// <summary>
    /// Genereal information about the game state
    /// </summary>
    class GameInfoViewModel : ViewModelBase
    {
        #region Properties
        /// <summary>
        /// Checks if the game was lost
        /// </summary>
        private bool gameLost;
        /// <summary>
        /// Checks if the game was won
        /// </summary>
        private bool gameWon;
        #endregion

        /// <summary>
        /// Message about the lost game
        /// </summary>
        public Visibility LostMessage
        {
            get
            {
                if (gameLost)
                    return Visibility.Visible;

                return Visibility.Hidden;
            }
        }

        /// <summary>
        /// Message about the won game
        /// </summary>
        public Visibility WinMessage
        {
            get
            {
                if (gameWon)
                    return Visibility.Visible;

                return Visibility.Hidden;
            }
        }

        /// <summary>
        /// Checks if the game was won or lost before previewing it
        /// </summary>
        /// <param name="win">checks if the game was won</param>
        public void GameStatus(bool win)
        {
            if (!win)
            {
                gameLost = true;
                OnPropertyChanged("LostMessage");
            }

            if (win)
            {
                gameWon = true;
                OnPropertyChanged("WinMessage");
            }
        }

        /// <summary>
        /// Resterts the game information
        /// </summary>
        public void ClearInfo()
        {
            gameLost = false;
            gameWon = false;
            OnPropertyChanged("LostMessage");
            OnPropertyChanged("WinMessage");
        }
    }
}
