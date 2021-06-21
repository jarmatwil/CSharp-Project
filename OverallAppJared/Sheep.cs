using System;
using System.Collections.Generic;
using System.Text;

namespace OverallAppJared
{
    class Sheep : Animal
    {
        double woolPerYear;
        public Sheep(int ID, double waterPerDay, double dailyCost, double weight, int ageInYears, string colour, double woolPerYear)
            : base(ID, waterPerDay, dailyCost, weight, ageInYears, colour)
        {
            this.woolPerYear = woolPerYear;
        }

        public double getWoolPerYear() { return woolPerYear; }
        public override Dictionary<String, Object> getAllInfo()
        {
            Dictionary<String, Object> allInfo = new Dictionary<String, Object>();
            allInfo.Add("ID", this.getID());
            allInfo.Add("Water Per Day", this.getWaterPerDay());
            allInfo.Add("Daily Cost", this.getDailyCost());
            allInfo.Add("Weight", this.getWeight());
            allInfo.Add("Age(years)", this.getAge());
            allInfo.Add("Colour", this.getColour());
            allInfo.Add("Wool Per Year", this.getWoolPerYear());
            return allInfo;
        }
    }
}
