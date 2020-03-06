using System;
using System.Collections.Generic;
using System.Text;

namespace WCS
{
    public class Adress
    {
        public int StreetNumber { get; private set; }
        public string StreetName { get; private set; }
        public string CityName { get; private set; }
        public string Country { get; private set; }

        public Adress(int streetNumber, string streetName, string cityName, string country)
        {
            StreetNumber = streetNumber;
            StreetName = streetName;
            CityName = cityName;
            Country = country;
        }
    }
}
