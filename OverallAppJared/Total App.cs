using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Collections;
using System.IO;

namespace OverallAppJared
{
    public partial class Form1 : Form
    {
        Dictionary<int,Animal> allItems;
        Costs costs;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            loadDB();
            
        }
        //following method loads the database to memory
        public void loadDB()
        {
            allItems = new Dictionary<int,Animal>();
            textBox2.Clear();
            //D:\FarmInfomation.accdb
            string strDSN = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source = " + textBox1.Text;
            using (OleDbConnection connection = new OleDbConnection(strDSN))
            {
                // Open the connection and execute the select command.    
                try
                {
                    connection.Open();
                    List<double> costList = new List<double>();
                    foreach (string tableName in populateTableNames())
                    {
                        string strSQL = "SELECT * FROM " + "[" + tableName + "]";
                        OleDbCommand command = new OleDbCommand(strSQL, connection);
                        using (OleDbDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read()) 
                            {
                                    switch (tableName)
                                    {
                                        case "Cows":
                                            allItems.Add(Convert.ToInt32(reader.GetValue(0)), (new Cow(Convert.ToInt32(reader.GetValue(0)), reader.GetDouble(1),
                                                reader.GetDouble(2), reader.GetDouble(3),
                                                Convert.ToInt32(reader.GetValue(4)), reader.GetString(5), reader.GetDouble(6),reader.GetBoolean(7))));
                                            continue;
                                        case "Dogs":
                                            allItems.Add(Convert.ToInt32(reader.GetValue(0)), (new Dog(Convert.ToInt32(reader.GetValue(0)), reader.GetDouble(1),
                                                reader.GetDouble(5),reader.GetDouble(2),
                                                Convert.ToInt32(reader.GetValue(3)), reader.GetString(4))));
                                            continue;
                                        case "Goats":
                                            allItems.Add(Convert.ToInt32(reader.GetValue(0)), (new Goat(Convert.ToInt32(reader.GetValue(0)), reader.GetDouble(1),
                                                reader.GetDouble(2), reader.GetDouble(3),
                                                Convert.ToInt32(reader.GetValue(4)), reader.GetString(5), reader.GetDouble(6))));
                                            continue;
                                        case "Sheep":
                                            allItems.Add(Convert.ToInt32(reader.GetValue(0)), (new Sheep(Convert.ToInt32(reader.GetValue(0)),reader.GetDouble(1),
                                                reader.GetDouble(2), reader.GetDouble(3),
                                                Convert.ToInt32(reader.GetValue(4)), reader.GetString(5),reader.GetDouble(6))));
                                            continue;
                                        case "Commodity Prices":
                                            costList.Add(reader.GetDouble(1));
                                            continue;
                                    default:
                                        textBox2.Text = "Error.";
                                        return;
                                    }
                            }
                        }
                    }
                    //after DB load
                    costs = new Costs(costList[0], costList[1], costList[2], costList[3], costList[4], costList[5]);
                    textBox2.Text = "File has been loaded.";
                }
                catch (Exception ex)
                {
                    textBox2.Text += ex.ToString();
                    //textBox2.Text = "Error. Invalid file path.";
                }
            }
        }

        public List<string> populateTableNames()
        {
            List<string> newList = new List<string>();
            newList.Add("Cows");
            newList.Add("Dogs");
            newList.Add("Goats");
            newList.Add("Sheep");
            newList.Add("Commodity Prices");
            return newList;
        }

        public bool isFileLoaded()
        {
            if (allItems != null)
            {
                return true;
            }
            return false;
        }

        //following methods are the query logic 
        public void query1(int input)
        {
            Dictionary<string, object> allValues = allItems[input].getAllInfo();

            foreach (KeyValuePair<string, object> items in allValues)
            {
                textBox2.Text += items.Key.ToString() + ": " + items.Value;
                textBox2.Text += Environment.NewLine;
            }
        }

        public void query2()
        {
            double dailyEarnings = 0;

            foreach (KeyValuePair<int, Animal> animals in allItems)
            {

                dailyEarnings += GetItemProfit(animals.Value);

            }
            textBox2.Text = "Daily Earnings: $" + dailyEarnings.ToString("#.##");
        }
        
        public double GetItemProfit(Animal animal)
        {
            if (animal is Cow)
            {
                if (((Cow)animal).getJersy())
                {
                    return (((((Cow)animal).getMilkPerDay() * costs.getCowMilkPrice()) * (1 - (costs.getTax() + costs.getJersyCowTax()))) - animal.getDailyCost())
                    + (animal.getWaterPerDay() * costs.getWaterPrice());
                }
                else
                {
                    return ((((Cow)animal).getMilkPerDay() * costs.getCowMilkPrice()) * (1 - costs.getTax()) - animal.getDailyCost())
                    + (animal.getWaterPerDay() * costs.getWaterPrice());
                }
            }
            if (animal is Goat)
            {
                return (((((Goat)animal).getMilkPerDay() * costs.getGoatMilkPrice()) * (1 - costs.getTax()) - animal.getDailyCost()))
                + (animal.getWaterPerDay() * costs.getWaterPrice());
            }
            if (animal is Sheep)
            {
                return (((((Sheep)animal).getWoolPerYear() / 365) * costs.getSheepWoolPrice()) * (1 - costs.getTax()) - animal.getDailyCost())
                + (animal.getWaterPerDay() * costs.getWaterPrice());
            }
            if (animal is Dog)
            {
                return (animal.getWaterPerDay() * costs.getWaterPrice()) - animal.getDailyCost();
            }
            return 0;
        }

        public void query3()
        {
            double taxTotal = 0;

            foreach (KeyValuePair<int, Animal> animals in allItems)
            {
                if (animals.Value is Cow)
                {
                    taxTotal += ((((Cow)animals.Value).getMilkPerDay() * costs.getCowMilkPrice())*30) * (costs.getTax());
                }
                if (animals.Value is Goat)
                {
                    taxTotal += ((((Goat)animals.Value).getMilkPerDay() *costs.getGoatMilkPrice())*30) * costs.getTax();
                }
                if (animals.Value is Sheep)
                {
                    taxTotal += (((((Sheep)animals.Value).getWoolPerYear() / 365)*30) * costs.getSheepWoolPrice()) * costs.getTax();
                }
            }
            textBox2.Text = "Monthly Tax: $" + taxTotal.ToString("#.##");
        }

        public void query4()
        {
            double milkPerDayTotal = 0;
            foreach (KeyValuePair<int, Animal> animals in allItems)
            {
                if (animals.Value is Cow)
                {
                    milkPerDayTotal += ((Cow)animals.Value).getMilkPerDay();
                }
                if(animals.Value is Goat)
                {
                    milkPerDayTotal += ((Goat)animals.Value).getMilkPerDay();
                }
                textBox2.Text = "Milk Per Day: " + milkPerDayTotal.ToString("#.##" + "L");
            }
        }
        public void query5()
        {
            List<int> ageAvg = new List<int>();
            foreach (KeyValuePair<int, Animal> animals in allItems)
            {
                if (animals.Value is Dog)
                {
                    continue;
                }
                ageAvg.Add(animals.Value.getAge());
            }
            textBox2.Text = "Average age: " + ageAvg.Average().ToString("#.##");
        }

        public void query6()
        {
            List<double> avgCowGoatProfit = new List<double>();
            List<double> avgSheepProfit = new List<double>();
            foreach (KeyValuePair<int, Animal> animals in allItems)
            {
                if(animals.Value is Dog)
                {
                    continue;
                }
                if(animals.Value is Sheep)
                {
                    avgSheepProfit.Add(GetItemProfit(animals.Value));
                }
                if (animals.Value is Cow || animals.Value is Goat)
                {
                    avgCowGoatProfit.Add(GetItemProfit(animals.Value));
                }
            }
            textBox2.Text += "Average Cow and Goat Profit: $" + avgCowGoatProfit.Average().ToString("#.##");
            textBox2.Text += Environment.NewLine;
            textBox2.Text += "Average Sheep Profit: $" + avgSheepProfit.Average().ToString("#.##");
        }

        public void query7()
        {
            double dogCost = 0;
            double totalCost = 0;
            foreach (KeyValuePair<int, Animal> animals in allItems)
            {
                
                if (animals.Value is Dog)
                {
                    dogCost += animals.Value.getDailyCost();
                }

                totalCost += animals.Value.getDailyCost();

            }
            int gcd = GCD(Convert.ToInt32(dogCost), Convert.ToInt32(totalCost));
            textBox2.Text = "Ratio of Dog to Total Costs: " + string.Format("{0}:{1}", Convert.ToInt32(dogCost) / gcd, Convert.ToInt32(totalCost) / gcd);
        }

        public void query8(string path)
        {
            string pathEdited = path + "ID-Output.txt";
            File.CreateText(@pathEdited);
            List<string> sortedList = new List<string>();
            foreach (KeyValuePair<int, Animal> animals in allItems)
            {
                sortedList.Add(animals.Key.ToString());
            }
            File.WriteAllLines(@pathEdited, sortedList);
        }

        public void query9()
        {
            int redColour = 0;
            foreach (KeyValuePair<int, Animal> animals in allItems)
            {

                if (animals.Value.getColour() == "Red")
                {
                    ++redColour;
                }
            }
            int gcd = GCD(redColour, allItems.Count);
            textBox2.Text = "Ratio of Red Animals: " + string.Format("{0}:{1}", redColour / gcd, allItems.Count / gcd);
        }

        public void query10()
        {
            double jersyTax = 0;

            foreach (KeyValuePair<int, Animal> animals in allItems)
            {

                if (animals.Value is Cow)
                {
                    if(((Cow)animals.Value).getJersy())
                    {
                        jersyTax += (((Cow)animals.Value).getMilkPerDay()*costs.getCowMilkPrice()) * costs.getJersyCowTax();
                    }
                }
                
            }
            textBox2.Text += "Total Tax Cost for Jersy Cows per Day: $" + jersyTax.ToString("#.##");

        }

        public void query11(int ratioCheck)
        {
            List<int> aboveAge = new List<int>();
            foreach (KeyValuePair<int, Animal> animals in allItems)
            {

                if (animals.Value.getAge() > ratioCheck)
                {
                    aboveAge.Add(animals.Value.getAge());
                }
            }
            int gcd = GCD(aboveAge.Count, allItems.Count);
            textBox2.Text = "Ratio of Animals above " + ratioCheck.ToString() + " years: " + string.Format("{0}:{1}", aboveAge.Count / gcd, allItems.Count / gcd);
        }

        public void query12()
        {
            double jersyProfit = 0;

            foreach (KeyValuePair<int, Animal> animals in allItems)
            {

                if (animals.Value is Cow)
                {
                    if (((Cow)animals.Value).getJersy())
                    {
                        jersyProfit += GetItemProfit(animals.Value);
                    }
                }

            }
            textBox2.Text += "Total Profit for Jersy Cows per Day: $" + jersyProfit.ToString("#.##");
        }

        public int GCD(int a, int b)
        {
            return b == 0 ? Math.Abs(a) : GCD(b, a % b);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox2.Clear();
            if (isFileLoaded())
            {
                try
                {
                    query1(Convert.ToInt32(textBox3.Text));
                }catch(Exception ex)
                {
                    textBox2.Text = "Error. Invalid input.";
                    return;
                }
            }
            else
            {
                textBox2.Text = "Error. File not loaded.";
            }
        }


        private void button3_Click(object sender, EventArgs e)
        {
            textBox2.Clear();
            if (isFileLoaded())
            {
                query2();
            }
            else
            {
                textBox2.Text = "Error. File not loaded.";
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            textBox2.Clear();
            if (isFileLoaded())
            {
                query3();
            }
            else
            {
                textBox2.Text = "Error. File not loaded.";
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            textBox2.Clear();
            if (isFileLoaded())
            {
                query5();
            }
            else
            {
                textBox2.Text = "Error. File not loaded.";
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            textBox2.Clear();
            if (isFileLoaded())
            {
                query4();
            }
            else
            {
                textBox2.Text = "Error. File not loaded.";
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            textBox2.Clear();
            if (isFileLoaded())
            {
                query6();
            }
            else
            {
                textBox2.Text = "Error. File not loaded.";
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            textBox2.Clear();
            if (isFileLoaded())
            {
                query7();
            }
            else
            {
                textBox2.Text = "Error. File not loaded.";
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            textBox2.Clear();
            if (isFileLoaded())
            {
                query9();
            }
            else
            {
                textBox2.Text = "Error. File not loaded.";
            }
        }

        private void button12_Click(object sender, EventArgs e)
        {
            textBox2.Clear();
            if (isFileLoaded())
            {
                try
                {
                    query11(Convert.ToInt32(textBox13.Text));
                }
                catch (Exception ex)
                {
                    textBox2.Text = "Error. Invalid input.";
                    return;
                }
            }
            else
            {
                textBox2.Text = "Error. File not loaded.";
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            textBox2.Clear();
            if (isFileLoaded())
            {
                query10();
            }
            else
            {
                textBox2.Text = "Error. File not loaded.";
            }
        }

        private void button13_Click(object sender, EventArgs e)
        {
            textBox2.Clear();
            if (isFileLoaded())
            {
                query12();
            }
            else
            {
                textBox2.Text = "Error. File not loaded.";
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            textBox2.Clear();
            if (isFileLoaded())
            {
                try
                {
                    if (!File.Exists(@textBox10.Text))
                    {
                        throw new Exception();
                    }
                    query8(textBox10.Text);
                }
                catch (Exception ex)
                {
                    textBox2.Text += "Error. Invalid path.";
                    return;
                }
            }
            else
            {
                textBox2.Text = "Error. File not loaded.";
            }
        }//D:\FarmInfomation.accdb
        //D:\dims
    }
}
