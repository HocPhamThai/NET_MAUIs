
namespace ColorMaker
{
    public partial class MainPage : ContentPage
    {
        bool isGenerateRandom;

        public MainPage()
        {
            InitializeComponent();
        }

        private void Slider_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            if (!isGenerateRandom) { 
                var red = RedSlider.Value;
                var green = GreenSlider.Value;
                var blue = BlueSlider.Value;

                var color = Color.FromRgb(red, green, blue);

                SetColor(color);
            }
        }

        private void SetColor(Color color)
        {
            Container.BackgroundColor = color;
            btnRandom.BackgroundColor = color;
            lblHex.Text = color.ToHex();
        }

        private void btnRandom_Clicked(object sender, EventArgs e)
        {
            isGenerateRandom = true;
            // Random number
            Random random = new Random();
            byte red = (byte)random.Next(0, 256);
            byte green = (byte)random.Next(0, 256);
            byte blue = (byte)random.Next(0, 256);

            Color color = Color.FromRgb(red, green, blue);

            RedSlider.Value = color.Red;
            GreenSlider.Value = color.Green;
            BlueSlider.Value = color.Blue;

            SetColor(color);
            isGenerateRandom = false;
        }

        private async void ImageButton_Clicked(object sender, EventArgs e)
        {
            await Clipboard.Default.SetTextAsync(lblHex.Text);
            await DisplayAlert("Copied", "Hex color code copied to clipboard.", "OK");
        }
    }
}
