namespace Global.TradingPlatform.DesktopApp
{
    public partial class frmSubmitNewOrder : Form
    {
        private Guid ClordID;
        public frmSubmitNewOrder()
        {
            InitializeComponent();

            ClordID = Guid.NewGuid();
            txtClordID.Text = ClordID.ToString();
            txtCreatedBy.Text = UserInfo.CurrentUser.Username;
        }

        private async void btnSubmit_Click(object sender, EventArgs e)
        {
            var order = new OrderRequest
            {
                ClordID = ClordID,

                Symbol = txtSymbol.Text,
                Side = txtSide.Text,
                Quantity = int.Parse(txtQuantity.Text),
                Price = decimal.Parse(txtPrice.Text),
                CreatedBy = txtCreatedBy.Text,
            };

            try
            {
                var result = await SubmissionProxy.SubmitAsync(order);
                txtOrderID.Text = result.OrderID.ToString();
                txtStatus.Text = result.Status.ToString();
                txtExecutedQuantity.Text = result.ExecutedQuantity.ToString();
                txtRemainingQuantity.Text = result.RemainingQuantity.ToString();
                MessageBox.Show($"Order submitted successfully! [OrderID: {result.OrderID}]\r\n[ClordID: {result.ClordID}]");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error submitting order: {ex.Message}");
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClordID = Guid.NewGuid();
            
            txtQuantity.Text = "1000";
            txtPrice.Text = "6.5";
            txtOrderID.Clear();
            txtStatus.Clear();
            txtExecutedQuantity.Clear();
            txtRemainingQuantity.Clear();

            txtClordID.Text = ClordID.ToString();
        }
    }
}