using SOAPClient.Model;
using SOAPClient.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;

namespace SOAPClient
{
    public class SoapClientVM : IViewModel, INotifyPropertyChanged
    {
        private string[] charCodesValuteCollection;
        private Valute sValute;
        private string selectedCharCodeValute;
        private DateTime selectedDate;
        public SoapClientVM(IView view)
        {
            View = view;
            View.ViewModel = this;
            View.Show();
            SelectedDate = DateTime.Now;
            Task.Factory.StartNew(() => getNamesValutes());

        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
        public IView View { get; set; }

        public string[] CharCodesValuteCollection
        {
            get => charCodesValuteCollection;
            set
            {
                charCodesValuteCollection = value;
                OnPropertyChanged(nameof(CharCodesValuteCollection));
            }
        }
        public Valute SValute
        {
            get => sValute;
            set
            {
                sValute = value;
                OnPropertyChanged(nameof(SValute));
            }
        }
        public string SelectedCharCodeValute
        {
            get => selectedCharCodeValute;
            set
            {
                selectedCharCodeValute = value;
                OnPropertyChanged(nameof(SelectedCharCodeValute));
            }
        }
        public DateTime SelectedDate 
        {
            get => selectedDate;
            set
            {
                selectedDate = value;
                OnPropertyChanged(nameof(SelectedDate));
            }
        }

        public Command GetValute
        {
            get => new Command(() =>
            {
                Task.Factory.StartNew(() =>
                {
                    SValute = getValute();
                });
            });
        }

        private void getNamesValutes()
        {
            try
            {
                IValuteService cbr = createService();
                CharCodesValuteCollection = cbr.GetChCodeValutes(SelectedDate);
            }
            catch (ArgumentNullException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (KeyNotFoundException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private Valute getValute()
        {
            try
            {
                IValuteService cbr = createService();
                return cbr.GetCourse(SelectedDate, SelectedCharCodeValute);
            }
            catch (ArgumentNullException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (InvalidCastException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (KeyNotFoundException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return new Valute();
        }

        private IValuteService createService()
        {
            return new CBRService();
        }
    }
}
