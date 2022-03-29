using System;

namespace CulemborgWeerAPI.WttrIn
{
    public class WttrInCurrentCondition
    {
        public int FeelsLikeC { get; set; }
        public int temp_C { get; set; }
        public int cloudcover { get; set; }
        public int humidity { get; set; }
        public DateTime localObsDateTime { get; set; }
        public int winddirDegree { get; set; }
        public int windspeedKmph { get; set; }
    }
}
