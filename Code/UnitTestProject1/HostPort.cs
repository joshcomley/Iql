namespace Iql.Tests.Server
{
    class HostPort
    {
        private static int _port = 6600;
        public static int Next()
        {
            return _port++;
        }
    }
}