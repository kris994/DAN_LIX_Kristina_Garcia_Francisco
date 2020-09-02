using DAN_LIX_Kristina_Garcia_Francisco.Models;
using System.Windows.Media;

namespace DAN_LIX_Kristina_Garcia_Francisco.ViewModels
{
    /// <summary>
    /// Preview memory pictures
    /// </summary>
    class PictureViewModel : ViewModelBase
    {
        #region Variables
        /// <summary>
        /// Pictures
        /// </summary>
        private Pictures model;
        /// <summary>
        /// Picture ID
        /// </summary>
        public int Id { get; private set; }
        /// <summary>
        /// Picture status
        /// </summary>
        private bool isViewed;
        private bool isMatched;
        private bool isFailed;
        #endregion

        #region Constructor
        /// <summary>
        /// Picture constructor
        /// </summary>
        /// <param name="model">pictures model</param>
        public PictureViewModel(Pictures model)
        {
            this.model = model;
            Id = model.Id;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Check if the picture is being viewed by the user
        /// </summary>
        public bool IsViewed
        {
            get
            {
                return isViewed;
            }
            private set
            {
                isViewed = value;
                OnPropertyChanged("SlideImage");
                OnPropertyChanged("BorderBrush");
                OnPropertyChanged("IsSelectable");
            }
        }

        /// <summary>
        /// Trying to match the pictures
        /// </summary>
        public bool IsMatched
        {
            get
            {
                return isMatched;
            }
            private set
            {
                isMatched = value;
                OnPropertyChanged("SlideImage");
                OnPropertyChanged("BorderBrush");
                OnPropertyChanged("IsSelectable");
            }
        }

        /// <summary>
        /// Check if matching failed
        /// </summary>
        public bool IsFailed
        {
            get
            {
                return isFailed;
            }
            private set
            {
                isFailed = value;
                OnPropertyChanged("SlideImage");
                OnPropertyChanged("BorderBrush");
                OnPropertyChanged("IsSelectable");
            }
        }

        /// <summary>
        /// Check if user can select the image
        /// </summary>
        public bool IsSelectable
        {
            get
            {
                if (IsMatched)
                    return false;
                if (IsViewed)
                    return false;

                return true;
            }
        }

        /// <summary>
        /// Display the images from the assets
        /// </summary>
        public string SlideImage
        {
            get
            {
                if (IsMatched)
                    return model.ImageSource;
                if (IsViewed)
                    return model.ImageSource;

                return @"../../Assets/Pictures/memory_image.jpg";
            }
        }

        /// <summary>
        /// Color the image border based on their state
        /// </summary>
        public Brush BorderBrush
        {
            get
            {
                if (IsFailed)
                    return Brushes.Red;
                if (IsMatched)
                    return Brushes.Green;
                if (IsViewed)
                    return Brushes.Yellow;

                return Brushes.Black;
            }
        }
        #endregion

        /// <summary>
        /// Mark pictures if they matched
        /// </summary>
        public void MarkMatched()
        {
            IsMatched = true;
        }

        /// <summary>
        /// Mark pictures if they did not match
        /// </summary>
        public void MarkFailed()
        {
            IsFailed = true;
        }

        /// <summary>
        /// Close picture peek
        /// </summary>
        public void ClosePeek()
        {
            IsViewed = false;
            IsFailed = false;
            OnPropertyChanged("IsSelectable");
            OnPropertyChanged("SlideImage");
        }

        /// <summary>
        /// Let user view the picture
        /// </summary>
        public void PeekAtImage()
        {
            IsViewed = true;
            OnPropertyChanged("SlideImage");
        }
    }
}
