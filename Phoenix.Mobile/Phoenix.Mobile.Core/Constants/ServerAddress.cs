namespace Phoenix.Mobile.Core.Constants
{
    public class ServerAddress
    {
#if DEBUG        
        //public const string ServerBaseUrl = "http://192.168.1.27:63199/api";
        public const string ServerBaseUrl = "http://192.168.1.8:63199/api";
        //public const string ServerBaseUrl = "http://172.31.98.215:63199/api";
       

#else
        //public const string ServerBaseUrl = "http://192.168.1.99:2345/api";
       
#endif
    }
}
