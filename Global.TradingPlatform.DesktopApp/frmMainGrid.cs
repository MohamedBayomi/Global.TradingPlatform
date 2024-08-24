namespace Global.TradingPlatform.DesktopApp
{
    public partial class frmMainGrid : Form
    {
        List<Order> orders = new();
        public frmMainGrid()
        {
            InitializeComponent();

            dataGridView1.DataSource = orders;
            // make grid column width auto size based on header and content
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.Text = $"Trading Platform - {UserInfo.CurrentUser.Username}";
        }

        private void newOrderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var submitScreen = new frmSubmitNewOrder();
            submitScreen.Show();
        }

        private void frmMainGrid_Load(object sender, EventArgs e)
        {
            var _connection = new OrdersHubClient();
            _connection.OnOrderUpdate += (sender, order) =>
            {
                //orders.Add(order);
                dataGridView1.Invoke(new Action(() =>
                {
                    //orders.Add(order);
                    dataGridView1.DataSource = null;
                    dataGridView1.DataSource = orders;
                }));
            };
            orders = _connection.GetAllOrders();


            dataGridView1.DataSource = null;
            dataGridView1.DataSource = orders;
        }
    }
}
