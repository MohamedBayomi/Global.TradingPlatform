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
        }

        private void newOrderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var submitScreen = new frmSubmitNewOrder();
            submitScreen.Show();
        }

        private void frmMainGrid_Load(object sender, EventArgs e)
        {
            orders = StreamerServiceProxy.GetSnapshotOrders();
            dataGridView1.DataSource = orders;

            StreamerServiceProxy.Subscribe();
        }
    }
}
