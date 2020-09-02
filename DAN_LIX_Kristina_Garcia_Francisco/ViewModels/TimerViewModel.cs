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
        /// <summary>
        /// Seconds that passed
        /// </summary>
        private const int playSeconds = 1;
        #endregion

        #region Constructor
        /// <summary>
        /// Tmie constructor
        /// </summary>
        /// <param name="time"></param>
        public TimerViewModel(TimeSpan time)
        {
            playedTimer = new DispatcherTimer();
            playedTimer.Interval = time;
            playedTimer.Tick += PlayedTimer_Tick;
            timePlayed = new TimeSpan();
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
        }
    }
}
