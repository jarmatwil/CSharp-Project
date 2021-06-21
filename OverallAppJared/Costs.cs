using System;
using System.Collections.Generic;
using System.Text;

namespace OverallAppJared
{
    class Costs
    {
        double goatMilkPrice;
        double cowMilkPrice;
        double sheepWoolPrice;
        double waterPrice;
        double tax;
        double jersyCowTax;

        public Costs(double goatMilkPrice,double sheepWoolPrice, double waterPrice, double tax, double jersyCowTax, double cowMilkPrice)
        {
            this.goatMilkPrice = goatMilkPrice;
            this.cowMilkPrice = cowMilkPrice;
            this.sheepWoolPrice = sheepWoolPrice;
            this.waterPrice = waterPrice;
            this.tax = tax;
            this.jersyCowTax = jersyCowTax;
        }

        public double getGoatMilkPrice() { return goatMilkPrice; }
        public double getCowMilkPrice() { return cowMilkPrice; }

        public double getSheepWoolPrice() { return sheepWoolPrice; }
        public double getWaterPrice() { return waterPrice; }
        public double getTax() { return tax; }
        public double getJersyCowTax() { return jersyCowTax; }
    }
}
