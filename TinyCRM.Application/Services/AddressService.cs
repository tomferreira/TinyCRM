using System.Collections.Generic;
using System.Globalization;
using TinyCRM.Application.Interfaces;

namespace TinyCRM.Application.Services
{
    public class AddressService : IAddressService
    {
        public List<string> GetContriesName()
        {
            List<string> countryList = new List<string>();

            CultureInfo[] CInfoList = CultureInfo.GetCultures(CultureTypes.SpecificCultures);
            foreach (CultureInfo CInfo in CInfoList)
            {
                RegionInfo R = new RegionInfo(CInfo.LCID);

                if (!(countryList.Contains(R.EnglishName)))
                    countryList.Add(R.EnglishName);
            }

            countryList.Sort();

            return countryList;
        }
    }
}
