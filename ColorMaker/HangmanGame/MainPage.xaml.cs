using HangmanGame.Models;

namespace HangmanGame
{
    public partial class MainPage : ContentPage
    {
        private Person person = new()
        {
            Name = "John Doe",
            Age = 30,
            Email = "JohnDoe@gmail.com"   
        };

        public MainPage()
        {
            InitializeComponent();
            BindingContext = person;
        }

        private void OnCounterClicked(object? sender, EventArgs e)
        {
            person.Name = "Hoc Pham Pro Coder";
            person.Age = 25;
            person.Email = "Lord@gmail.com";
        }
    }
}
