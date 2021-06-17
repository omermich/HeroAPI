using System;

namespace WebAPI.Models
{

    public class Hero
    {

        public int id { get; set; }
        public string name { get; set; }
        public int ability { get; set; }
        public String startingDate { get; set; }
        public String primaryColor { get; set; }
        public String secondaryColor { get; set; }
        public int guideId { get; set; }
        public float startingPower { get; set; }
        public float currentPower { get; set; }
        public int dailyTrainCount { get; set; }
    }
}