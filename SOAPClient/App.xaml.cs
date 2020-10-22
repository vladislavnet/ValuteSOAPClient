using System.Windows;

namespace SOAPClient
{
    public partial class App : Application
    {
        public App()
        {
            var VM = new SoapClientVM(new MainWindow());
        }
    }
}
