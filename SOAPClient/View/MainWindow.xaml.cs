using SOAPClient.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace SOAPClient
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IView
    {
        public MainWindow()
        {
            InitializeComponent();
            dpDate.SelectedDate = DateTime.Now;
        }

        public IViewModel ViewModel
        {
            get => DataContext as IViewModel;
            set => DataContext = value;
        }

        //private void btnGetChCodeValute_Click(object sender, RoutedEventArgs e)
        //{         
        //    Task.Factory.StartNew(() => getChCodeValutesAsync());
        //}

        //private void btnGetCourseValute_Click(object sender, RoutedEventArgs e)
        //{
        //    Task.Factory.StartNew(() => getValute());
        //}

        //private void getChCodeValutesAsync()
        //{
        //    DateTime date = DateTime.Now;
        //    this.Dispatcher.BeginInvoke((Action)(() => date = dpDate.SelectedDate ?? DateTime.Now));
        //    var cbr = new CBRService();
        //    var chCodes = cbr.GetChCodeValutes(date);
        //    this.Dispatcher.BeginInvoke((Action)(() => cbChCodeValutes.ItemsSource = chCodes));
        //}

        //private void getValute()
        //{
        //    DateTime date = DateTime.Now;
        //    string currencyValute = null;
        //    this.Dispatcher.BeginInvoke((Action)(() => date = dpDate.SelectedDate ?? DateTime.Now));
        //    this.Dispatcher.BeginInvoke((Action)(() => currencyValute = cbChCodeValutes.SelectedItem.ToString()));
        //    var cbr = new CBRService();
        //    var valute = cbr.GetCourse(date, currencyValute);
        //    this.Dispatcher.BeginInvoke((Action)(() => txtCourseValute.Content = currencyValute));
        //}
    }
}
