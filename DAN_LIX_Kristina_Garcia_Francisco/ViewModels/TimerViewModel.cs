using System;
using System.Windows.Threading;

namespace DAN_LIX_Kristina_Garcia_Francisco.ViewModels
{
    /// <summary>
    /// Times view model
    /// </summary>
    class TimerViewModel : ViewModelBase
    {
        #region Variables
        /// <summary>
        /// Played Timer
        /// </summary>
        private DispatcherTimer playedTimer;
        /// <summary>
        /// Total time played
        /// </summary>
        private TimeSpan timePlayed;
        public GameInfoViewModel GameInfo { get; private set; }
        #endregion

        #region Constructor
        /// <summary>
        /// Tmie constructor
        /// </summary>
        /// <param name="time">the time</param>
        /// <param name="GameInfo">The Game Info</param>
        public TimerViewModel(TimeSpan time, GameInfoViewModel GameInfo)
        {
            playedTimer = new DispatcherTimer();
            playedTimer.Interval = time;
            playedTimer.Tick += PlayedTimer_Tick;
            timePlayed = new TimeSpan();
            this.GameInfo = GameInfo;
        }

        /// <summary>
        /// Default constructor
        /// </summary>
        public TimerViewModel()
        {

        }
        #endregion

        #region Property
        /// <summary>
        /// Time
        /// </summary>
        public TimeSpan Time
        {
            get
            {
                return timePlayed;
            }
            set
            {
                timePlayed = value;
                OnPropertyChanged("Time");
            }
        }

        /// <summary>
        /// Left Time
        /// </summary>
        public TimeSpan LeftTime
        {
            get
            {
                return timePlayed;
            }
            set
            {
                timePlayed = value;
                OnPropertyChanged("Time");
            }
        }
        #endregion

        /// <summary>
        /// Start the timer
        /// </summary>
        public void Start()
        {
            playedTimer.Start();
        }

        /// <summary>
        /// Stop the timer
        /// </summary>
        public void Stop()
        {
            playedTimer.Stop();
        }

        /// <summary>
        /// Count timer ticks
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PlayedTimer_Tick(object sender, EventArgs e)
        {
            Time = timePlayed.Add(new TimeSpan(0, 0, 1));
            // The game is over in 1 minute
            if (timePlayed.Minutes == 1)
            {
                GameInfo.GameStatus(false);
                playedTimer.Stop();
            }
        }
    }
}
