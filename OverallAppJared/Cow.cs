using System;
using System.Collections.Generic;
using System.Text;

namespace OverallAppJared
{
    class Cow : Animal
    {
        double milkPerDay;
        bool isJersy;
        public Cow(int ID, double waterPerDay, double dailyCost, double weightKG, int ageInYears, string colour, double milkPerDay, bool isJersy)
            : base(ID, waterPerDay, dailyCost, weightKG, ageInYears, colour)
        {
            this.milkPerDay = milkPerDay;
            this.isJersy = isJersy;
        }

        public double getMilkPerDay() { return milkPerDay; }

        public bool getJersy() { return isJersy; }

        public override Dictionary<String, Object> getAllInfo()
        {
            Dictionary<String, Object> allInfo = new Dictionary<String, Object>();
            allInfo.Add("ID", this.getID());
            allInfo.Add("Water Per Day", this.getWaterPerDay());
            allInfo.Add("Daily Cost", this.getDailyCost());
            allInfo.Add("Weight", this.getWeight());
            allInfo.Add("Age(years)", this.getAge());
            allInfo.Add("Colour", this.getColour());
            allInfo.Add("Milk Per Day", this.getMilkPerDay());
            allInfo.Add("Is Jersy", this.getJersy());
            return allInfo;

        }

    }
}
