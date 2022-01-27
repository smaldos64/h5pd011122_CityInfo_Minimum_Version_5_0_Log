using CityInfo1_Data.Context;
using CityInfo1_Data.DataManager;
using CityInfo1_Data.Interfaces;
using CityInfo1_Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CityInfo1_Data.DataManager
{
    public class CountryRepository : RepositoryBase<Country>, ICountryRepository
    {
        private readonly DatabaseContext _context;

        public CountryRepository(DatabaseContext context) : base(context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
    }
}
