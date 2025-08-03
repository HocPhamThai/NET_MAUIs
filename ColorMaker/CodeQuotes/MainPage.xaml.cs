namespace CodeQuotes
{
    public partial class MainPage : ContentPage
    {
        private List<string> _quotes = new List<string>();
        public MainPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await LoadMauiAsset();
        }
        public async Task LoadMauiAsset()
        {
            using var stream = await FileSystem.OpenAppPackageFileAsync("quotes.txt");
            using var reader = new StreamReader(stream);

            while (reader.Peek() != -1)
            {
                _quotes.Add(reader?.ReadLine() ?? $"\"\" - \"\"");
            }
        }

        private void BtnGenerateCode_Clicked(object sender, EventArgs e)
        {
            var random = new Random();
            var color1 = colors[random.Next(colors.Count)];
            var color2 = colors[random.Next(colors.Count)];
            var color3 = colors[random.Next(colors.Count)];

            var gradientStops = new GradientStopCollection
            {
                new GradientStop(color1, 0f),
                new GradientStop(color2, .5f),
                new GradientStop(color3, 1f),
            };
            var linearGradientBrush = new LinearGradientBrush(gradientStops, new Point(0, 0), new Point(1, 1));

            this.gridBackground.Background = linearGradientBrush;
            this.LblQuote.Text = _quotes[random.Next(_quotes.Count)];
        }

        public List<Color> colors = new List<Color>
        {
            Colors.Red,
            Colors.Green,
            Colors.Blue,
            Colors.Yellow,
            Colors.Orange,
            Colors.Purple,
            Colors.Pink,
            Colors.Brown,
            Colors.Gray,
            Colors.Cyan
        };
    }

}
