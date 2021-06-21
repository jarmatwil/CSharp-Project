using System;
using System.Collections.Generic;
using System.Text;

namespace OverallAppJared
{
    class Goat : Animal
    {
        double milkPerDay;
        public Goat(int ID, double waterPerDay, double dailyCost, double weight, int ageInYears, string colour, double milkPerDay)
            : base(ID, waterPerDay, dailyCost, weight, ageInYears, colour)
        {
            this.milkPerDay = milkPerDay;
        }

        public double getMilkPerDay() { return milkPerDay; }

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
            return allInfo;
        }

    }
}
