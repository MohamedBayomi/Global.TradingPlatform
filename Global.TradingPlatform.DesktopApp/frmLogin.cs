namespace Global.TradingPlatform.DesktopApp
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            var username = txtUsername.Text;
            var password = txtPassword.Text;

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Please enter username and password");
                return;
            }

            var result = AuthenticationServiceProxy.Authenticate(username, password);
            if (result.IsAuthenticated)
            {
                this.Hide();
                var maingrid = new frmMainGrid();
                maingrid.ShowDialog();
                this.Close();
            }
            else
            {
                MessageBox.Show("Invalid username or password");
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}