using CulemborgWeerAPI.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CulemborgWeerAPI.Data
{
    public class CulemborgWeerRepository : ICulemborgWeerRepository
    {
        private CulemborgWeerContext _context;

        public CulemborgWeerRepository(CulemborgWeerContext context)
        {
            _context = context;
        }

        public IEnumerable<WeatherInformation> GetFullHistory()
        {
            return _context.WeatherInformation
                .OrderBy(w => w.Date)
                .ToList();
        }

        public IEnumerable<WeatherInformation> GetHistoryBetweenDates(DateTime startDate, DateTime endDate)
        {
            return _context.WeatherInformation
                .Where(w => w.Date >= startDate && w.Date <= endDate)
                .OrderBy(w => w.Date)
                .ToList();
        }

        public void StoreHistoricalData(WeatherInformation data)
        {
            if (_context.WeatherInformation.Any(w => w.Date == data.Date))
            {
                _context.WeatherInformation.Update(data);
            }
            else
            {
                _context.WeatherInformation.Add(data);
            }
            _context.SaveChanges();
        }
    }
}
