using System;
using System.Collections.Generic;
using System.Text;

namespace CityInfo1_Data.Interfaces
{
    public interface IRepositoryWrapper
    {
        ICityRepository CityRepositoryWrapper { get; }
                
        ICountryRepository CountryRepositoryWrapper { get; }
                
        void Save();

    }
}
