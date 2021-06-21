using System;
using System.Collections.Generic;
using System.Text;

namespace OverallAppJared
{
    class Dog : Animal
    {
        public Dog(int ID, double waterPerDay, double dailyCost, double weight, int ageInYears, string colour)
            : base(ID, waterPerDay, dailyCost, weight, ageInYears, colour)
        { }

        public override Dictionary<String, Object> getAllInfo()
        {
            Dictionary<String, Object> allInfo = new Dictionary<String, Object>();
            allInfo.Add("ID", this.getID());
            allInfo.Add("Water Per Day", this.getWaterPerDay());
            allInfo.Add("Daily Cost", this.getDailyCost());
            allInfo.Add("Weight", this.getWeight());
            allInfo.Add("Age(years)", this.getAge());
            allInfo.Add("Colour", this.getColour());
            return allInfo;

        }
    }
}
