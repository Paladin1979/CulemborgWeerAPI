using CulemborgWeerAPI.Data.Entities;
using System;
using System.Collections.Generic;

namespace CulemborgWeerAPI.Data
{
    public interface ICulemborgWeerRepository
    {
        IEnumerable<WeatherInformation> GetFullHistory();
        IEnumerable<WeatherInformation> GetHistoryBetweenDates(DateTime startDate, DateTime endDate);
        void StoreHistoricalData(WeatherInformation data);
    }
}