using SOAPClient.Model;
using System;

namespace SOAPClient.Services
{
    public interface IValuteService
    {
        Valute GetCourse(DateTime date, string currencyCode);
        string[] GetChCodeValutes(DateTime date);
    }
}
