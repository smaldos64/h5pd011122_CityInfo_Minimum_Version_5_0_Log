using CityInfo1_Data.Context;
using CityInfo1_Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace CityInfo1_Data.DataManager
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private DatabaseContext _repoContext;

        private ICityRepository _cityRepositoryWrapper;
        private ICountryRepository _countryRepositoryWrapper;

        public RepositoryWrapper(DatabaseContext repositoryContext)
        {
            this._repoContext = repositoryContext;
        }

        public ICityRepository CityRepositoryWrapper
        {
            get
            {
                if (null == _cityRepositoryWrapper)
                {
                    _cityRepositoryWrapper = new CityRepository(_repoContext);
                }

                return (_cityRepositoryWrapper);
            }
        }
                
        public ICountryRepository CountryRepositoryWrapper
        {
            get
            {
                if (null == _countryRepositoryWrapper)
                {
                    _countryRepositoryWrapper = new CountryRepository(_repoContext);
                }

                return (_countryRepositoryWrapper);
            }
        }
                                
        public void Save()
        {
            _repoContext.SaveChanges();
        }
    }
}
