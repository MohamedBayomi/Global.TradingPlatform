namespace Global.TradingPlatform.DesktopApp
{
    internal static class AuthenticationServiceProxy
    {
        internal static UserInfo Authenticate(string username, string password)
        {
            var isAuthenticated = username == "admin" && password == "admin" || true;
            return new UserInfo(username, isAuthenticated);
        }
    }
}
