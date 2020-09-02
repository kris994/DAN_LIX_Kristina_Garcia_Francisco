using DAN_LIX_Kristina_Garcia_Francisco.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows.Threading;

namespace DAN_LIX_Kristina_Garcia_Francisco.ViewModels
{
    /// <summary>
    /// Pictures preview
    /// </summary>
    class PictureCollectionViewModel : ViewModelBase
    {
        #region Variables
        /// <summary>
        /// Collection of all pictures
        /// </summary>
        public ObservableCollection<PictureViewModel> MemorySlides { get; private set; }
        /// <summary>
        /// First matching side
        /// </summary>
        private PictureViewModel SelectedSlide1;
        /// <summary>
        /// Second Matching side
        /// </summary>
        private PictureViewModel SelectedSlide2;
        /// <summary>
        /// Peek time
        /// </summary>
        private DispatcherTimer peekTimer;
        /// <summary>
        /// Opening time
        /// </summary>
        private DispatcherTimer openingTimer;
        /// <summary>
        /// Peek time to spend
        /// </summary>
        private const int peekSeconds = 3;
        /// <summary>
        /// Open time to spend
        /// </summary>
        private const int openSeconds = 5;
        #endregion

        #region Constructor
        /// <summary>
        /// Picture Collection preview constructor
        /// </summary>
        public PictureCollectionViewModel()
        {
            peekTimer = new DispatcherTimer
            {
                Interval = new TimeSpan(0, 0, peekSeconds)
            };
            peekTimer.Tick += PeekTimer_Tick;

            openingTimer = new DispatcherTimer
            {
                Interval = new TimeSpan(0, 0, openSeconds)
            };
            openingTimer.Tick += OpeningTimer_Tick;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Checks if both sides are still active
        /// </summary>
        public bool AreSlidesActive
        {
            get
            {
                if (SelectedSlide1 == null || SelectedSlide2 == null)
                    return true;

                return false;
            }
        }

        /// <summary>
        /// Checks if all pictures were matched
        /// </summary>
        public bool AllSlidesMatched
        {
            get
            {
                foreach (var slide in MemorySlides)
                {
                    if (!slide.IsMatched)
                        return false;
                }

                return true;
            }
        }

        /// <summary>
        /// Can a side be selected
        /// </summary>
        public bool CanSelect { get; private set; }
        #endregion

        /// <summary>
        /// Creates the memory preview from images in the file
        /// </summary>
        /// <param name="imagesPath">Path to the image file</param>
        public void CreatePictures(string imagesPath)
        {
            // New list of pictures
            MemorySlides = new ObservableCollection<PictureViewModel>();
            var models = GetModelsFrom(@imagesPath);

            // Create memory pictures with matching pairs from models
            for (int i = 0; i < 8; i++)
            {
                // Create 2 matching pictures
                var newSlide = new PictureViewModel(models[i]);
                var newSlideMatch = new PictureViewModel(models[i]);
                // Add new picture to collection
                MemorySlides.Add(newSlide);
                MemorySlides.Add(newSlideMatch);
                // Initially display images for user
                newSlide.PeekAtImage();
                newSlideMatch.PeekAtImage();
            }

            ShuffleSlides();
            OnPropertyChanged("MemorySlides");
        }

        /// <summary>
        /// Matching two sides
        /// </summary>
        /// <param name="slide"></param>
        public void SelectSlide(PictureViewModel slide)
        {
            // Peek at the image
            slide.PeekAtImage();

            // Check if a side was selected
            if (SelectedSlide1 == null)
            {
                SelectedSlide1 = slide;
            }
            else if (SelectedSlide2 == null)
            {
                SelectedSlide2 = slide;
                HideUnmatched();
            }

            OnPropertyChanged("areSlidesActive");
        }

        /// <summary>
        /// Checks if the two pictures were matched
        /// </summary>
        /// <returns></returns>
        public bool CheckIfMatched()
        {
            // Checking by picture id
            if (SelectedSlide1.Id == SelectedSlide2.Id)
            {
                MatchCorrect();
                return true;
            }
            else
            {
                MatchFailed();
                return false;
            }
        }

        /// <summary>
        /// Pictures did not match
        /// </summary>
        private void MatchFailed()
        {
            // Unmark and clear the selection
            SelectedSlide1.MarkFailed();
            SelectedSlide2.MarkFailed();
            ClearSelected();
        }

        /// <summary>
        /// Selected sides matched
        /// </summary>
        private void MatchCorrect()
        {
            // Mark and clear the selection
            SelectedSlide1.MarkMatched();
            SelectedSlide2.MarkMatched();
            ClearSelected();
        }

        /// <summary>
        /// Clear all selection
        /// </summary>
        private void ClearSelected()
        {
            SelectedSlide1 = null;
            SelectedSlide2 = null;
            CanSelect = false;
        }

        /// <summary>
        /// Show all pictures
        /// </summary>
        public void RevealUnmatched()
        {
            foreach (var slide in MemorySlides)
            {
                if (!slide.IsMatched)
                {
                    peekTimer.Stop();
                    slide.MarkFailed();
                    slide.PeekAtImage();
                }
            }
        }

        /// <summary>
        /// Hide all unmatched pictures
        /// </summary>
        public void HideUnmatched()
        {
            peekTimer.Start();
        }

        /// <summary>
        /// Display slides for memorizing
        /// </summary>
        public void Memorize()
        {
            openingTimer.Start();
        }

        /// <summary>
        /// Get all picture models for creating picture views
        /// </summary>
        /// <param name="relativePath"></param>
        /// <returns></returns>
        private List<Pictures> GetModelsFrom(string relativePath)
        {
            // List of models for pictures
            var models = new List<Pictures>();
            // Get all image URIs in folder
            var images = Directory.GetFiles(@relativePath, "*.jpg", SearchOption.AllDirectories);
            // Picture id begin at 0
            var id = 0;

            foreach (string i in images)
            {
                models.Add(new Pictures() { Id = id, ImageSource = "/MemoryGame;component/" + i });
                id++;
            }

            return models;
        }

        /// <summary>
        /// Randomize the location of the slides in collection
        /// </summary>
        private void ShuffleSlides()
        {
            // Randomizing slide indexes
            var rnd = new Random();
            // Shuffle memory slides
            for (int i = 0; i < 64; i++)
            {
                MemorySlides.Reverse();
                MemorySlides.Move(rnd.Next(0, MemorySlides.Count), rnd.Next(0, MemorySlides.Count));
            }
        }

        /// <summary>
        /// Close pictures after showing them to the user
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OpeningTimer_Tick(object sender, EventArgs e)
        {
            foreach (var slide in MemorySlides)
            {
                slide.ClosePeek();
                CanSelect = true;
            }
            OnPropertyChanged("areSlidesActive");
            openingTimer.Stop();
        }

        /// <summary>
        /// Displey a selected card
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PeekTimer_Tick(object sender, EventArgs e)
        {
            foreach (var slide in MemorySlides)
            {
                if (!slide.IsMatched)
                {
                    slide.ClosePeek();
                    CanSelect = true;
                }
            }
            OnPropertyChanged("areSlidesActive");
            peekTimer.Stop();
        }
    }
}
