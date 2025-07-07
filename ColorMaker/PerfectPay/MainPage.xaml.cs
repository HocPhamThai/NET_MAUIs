namespace PerfectPay
{
    public partial class MainPage : ContentPage
    {
        decimal bill;
        int tip;
        int noPersons = 1;

        public MainPage()
        {
            InitializeComponent();
        }

        private void txtBill_Completed(object sender, EventArgs e)
        {
            bill = decimal.Parse(txtBill.Text);
            CalculateTotal();
        }

        private void CalculateTotal()
        {
            // Total Tip
            var totalTip = (bill * tip) / 100;

            // Tips
            var tipByPerson = totalTip / noPersons;
            lblTipByPerson.Text = $"$ {tipByPerson}";

            // Subtotal
            var subtotal = bill / noPersons;
            lblSubtotal.Text = $"$ {subtotal}";

            // TotalbyPerson
            var totalByPerson = (bill + totalTip) / noPersons;
            lblTotal.Text = $"$ {totalByPerson}";
        }

        private void sldTip_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            tip = (int) sldTip.Value;
            lblTip.Text = $"Tip: {tip}%";
            CalculateTotal();
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            if (sender is Button)
            {
                var btn = (Button)sender;
                var percentage =
                     int.Parse(btn.Text.Replace("%", ""));
                sldTip.Value = percentage;
            }
        }

        private void btnMinus_Clicked(object sender, EventArgs e)
        {
            if (noPersons > 1)
            {
                noPersons--;
            }
            lblNoPersons.Text = noPersons.ToString();
            CalculateTotal();
        }

        private void btnPlus_Clicked(object sender, EventArgs e)
        {
            noPersons++;
            lblNoPersons.Text = noPersons.ToString();
            CalculateTotal();
        }

        private void txtBills_TextChanged(object sender, TextChangedEventArgs e)
        {
            decimal.TryParse(txtBillA?.Text, out var billA);
            decimal.TryParse(txtBillB?.Text, out var billB);
            decimal.TryParse(txtBillC?.Text, out var billC);
            decimal.TryParse(txtBillD?.Text, out var billD);

            var total = billA + billB + billC + billD;
            bill = total;
            txtBill.Text = $"$ {total}";
            CalculateTotal();
        }
    }
}
