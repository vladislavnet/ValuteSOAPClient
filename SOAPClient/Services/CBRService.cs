using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using SOAPClient.Model;
using SOAPClient.ru.cbr.www;

namespace SOAPClient.Services
{
    public class CBRService : IValuteService
    {
        private DailyInfo _cbrClient;

        public DailyInfo Cbr
        {
            get
            {
                if(_cbrClient == null)
                {
                    _cbrClient = new DailyInfo();
                }   
                
                return _cbrClient;
            }
        }

        public Valute GetCourse(DateTime date, string currencyCode)
        {
            DataSet ds = Cbr.GetCursOnDate(date);
            if (ds == null)
                throw new ArgumentNullException("ds", "Сервис вернул null");
            if(string.IsNullOrEmpty(currencyCode))
                 throw new ArgumentNullException("currencyCode", "currencyCode не может быть null");
            DataTable dt = ds.Tables["ValuteCursOnDate"];

            DataRow[] rows = dt.Select(string.Format("VchCode=\'{0}\'", currencyCode));

            if (rows.Length > 0)
            {
                var valute = new Valute();
                if (decimal.TryParse(rows[0]["Vcurs"].ToString(), out decimal resultCourse) 
                    && (int.TryParse(rows[0]["Vnom"].ToString(), out int resultNominal)))
                {
                    valute.Curse = resultCourse;
                    valute.Nominal = resultNominal;
                    valute.Name = rows[0]["Vname"].ToString();
                    return valute;
                }
                throw new InvalidCastException("От службы ожидалось значение курса валют.");               
            }

            throw new KeyNotFoundException("Для заданной валюты не найден курс.");
        }

        public string[] GetChCodeValutes(DateTime date)
        {
            DataSet ds = Cbr.GetCursOnDate(date);
            if (ds == null)
                throw new ArgumentNullException("ds", "Сервис вернул null");
            
            DataTable dt = ds.Tables["ValuteCursOnDate"];

            DataRow[] rows = dt.Select();

            if (rows.Length > 0)
            {
                var chCodeVal = new List<string>();
                foreach(var row in rows)
                {
                    chCodeVal.Add(row["VchCode"].ToString());
                }
                return chCodeVal.ToArray();
            }

            throw new KeyNotFoundException("Не удалось подгрузить список кодов валют");
        }

        
    }
}
