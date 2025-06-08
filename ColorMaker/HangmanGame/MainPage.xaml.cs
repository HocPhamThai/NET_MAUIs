using HangmanGame.Models;
using System.ComponentModel;
using System.Diagnostics;

namespace HangmanGame
{
    public partial class MainPage : ContentPage, INotifyPropertyChanged
    {
        #region Fields
        readonly List<string> _words = new List<string>
        {
            "apple", "banana", "cherry", "date", "elderberry",
            "fig", "grape", "honeydew", "kiwi", "lemon"
        };
        private string answer = "";
        public List<char> Guessed = new List<char>();
        private int mistakes;
        private int maxMistakes = 6;
       
        private string currentImage = "img0.jpg";
        public string CurrentImage
        {
            get => currentImage;
            set
            {
                currentImage = value;
                OnPropertyChanged();
            }
        }
        private string gameStatus;
        public string GameStatus
        {
            get => gameStatus;
            set
            {
                gameStatus = value;
                OnPropertyChanged();
            }
        }

        private List<char> letters = new List<char>();
        public List<char> Letters
        {
            get => letters;
            set
            {
                letters = value;
                OnPropertyChanged();
            }
        }


        private string spotlight;
        public string Spotlight
        {
            get => spotlight;
            set
            {
                spotlight = value;
                OnPropertyChanged();
            }
        }

        private string message;
        public string Message
        {
            get => message;
            set
            {
                message = value;
                OnPropertyChanged();
            }
        }
        #endregion

        public MainPage()
        {
            InitializeComponent();
            Letters?.AddRange("abcdefghijqklmnopqrstuvwxyz".ToCharArray());
            BindingContext = this;
            PickWord();
            CalculateWord(answer, Guessed);
        }

        #region Game Engine logic
        private void PickWord()
        {
            answer = _words[new Random().Next(_words.Count)];
        }

        private void CalculateWord(string answer, List<char> guessed)
        {
            var temp = answer.Select(x => (guessed.IndexOf(x) >= 0) ? x : '_');

            Spotlight = string.Join(' ', temp);
        }

        #endregion

        private void Button_Clicked(object sender, EventArgs e)
        {
            var button = sender as Button;

            if (button != null && button.Text.Length == 1)
            {
                var letter = button.Text[0];
                button.IsEnabled = false;
                HandleGuessed(letter);
            }
        }

        private void HandleGuessed(char letter)
        {
            if (Guessed.IndexOf(letter) == -1)
            {
                Guessed.Add(letter);
            }
            if (answer.IndexOf(letter) >= 0)
            {
                CalculateWord(answer, Guessed);
                CheckIfGameWon();
            }
            else if (answer.IndexOf(letter) < 0)
            {
                mistakes++;
                UpdateGameStatus();
                CheckIfGameLost();
                CurrentImage = $"img{mistakes}.jpg";
            }
        }


        private void UpdateGameStatus()
        {
            if (mistakes <= maxMistakes)
            {
                GameStatus = $"Errors: {mistakes} of {maxMistakes}";
            }            
        }

        private void CheckIfGameWon()
        {
            if (Spotlight.Replace(" ", "") == answer)
            {
                Message = "You win!!";
                DisableLetters();
            }
        }
        private void CheckIfGameLost()
        {
            if (mistakes >= maxMistakes)
            {
                Message = $"You lose! The word was: {answer}";
                DisableLetters();
            }
        }

        private void DisableLetters()
        {
            foreach (var children in this.LettersContainer.Children)
            {
                var button = children as Button;
                button.IsEnabled = false;
            }
        }

        private void EnableLetters()
        {
            foreach (var children in this.LettersContainer.Children)
            {
                var button = children as Button;
                button.IsEnabled = true;
            }
        }

        private void ResetButton_onClicked(object sender, EventArgs e)
        {
            Reset();
        }

        private void Reset()
        {
            mistakes = 0;
            GameStatus = "Errors: 0 of 6";
            Guessed = new List<char>();
            CurrentImage = "img0.jpg";
            PickWord();
            CalculateWord(answer, Guessed);
            Message = string.Empty;
            EnableLetters();
        }
    }
}
