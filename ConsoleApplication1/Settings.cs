using System.Configuration;

namespace ConsoleApplication1
{
    public class Settings : ISettings
    {
        public string GetKey(string keyName)
        {
            return ConfigurationManager.AppSettings["NotifyUrl"];
        }
    }
}