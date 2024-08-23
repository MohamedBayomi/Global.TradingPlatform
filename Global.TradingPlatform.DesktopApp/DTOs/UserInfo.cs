namespace Global.TradingPlatform.DesktopApp
{
    internal class UserInfo
    {
        public UserInfo(string username, bool isAuthenticated)
        {
            Username = username;
            IsAuthenticated = isAuthenticated;
            
            CurrentUser = this;
        }

        public bool IsAuthenticated { get; set; }
        public string Username { get; set; }

        public static UserInfo CurrentUser { get; set; }
    }
}
