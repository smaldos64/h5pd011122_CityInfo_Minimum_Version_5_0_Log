using CityInfo1_Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CityInfo1_Data.Interfaces
{
    public interface ICountryRepository : IRepositoryBase<Country>
    {
        // Filen her er kun medtaget for at åbne op for, at man kan placere "specielle"
        // funktioner vedrørende Country funktionalitet her. Ellers kan man styre det
        // hele med de generiske funktioner erklæret i IRepositiryBase.cs og implementeret i RepositoryBase.cs.
    }
}
