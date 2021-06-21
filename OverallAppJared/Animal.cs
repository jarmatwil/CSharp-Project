using System;
using System.Collections.Generic;
using System.Text;

namespace OverallAppJared
{
    public class Animal
    {
        int ID;
        double waterPerDay;
        double dailyCost;
        double weight;
        int age;
        string colour;
        public Animal(int ID, double waterPerDay, double dailyCost, double weight, int ageInYears, string colour)
            {
            this.ID = ID;
            this.waterPerDay = waterPerDay;
            this.dailyCost = dailyCost;
            this.weight = weight;
            this.age = ageInYears;
            this.colour = colour;
            }

        public int getID() { return ID; }

        public double getWaterPerDay() { return waterPerDay; }

        public double getDailyCost() { return dailyCost; }

        public double getWeight() { return weight; }

        public int getAge() { return age; }

        public string getColour() { return colour; }


        public virtual Dictionary<String, Object> getAllInfo()
        {
            return null;
        }
    }
}
