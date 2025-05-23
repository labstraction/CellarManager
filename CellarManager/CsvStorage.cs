﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CellarManager.model;

namespace CellarManager
{
    internal class CsvStorage : IStorage
    {
        public List<Beverage> LoadAllBeverages()
        {
            List<Beverage> beverages = new List<Beverage>();
            try 
            {
                string[] lines = File.ReadAllLines("beverages.csv");
                for (int i = 1; i < lines.Length; i++)
                {
                    string[] parts = lines[i].Split(',');
                    if (parts[0] == "Beer")
                    {
                        var beer = new Beer
                        {
                            Name = parts[1],
                            Alcohol = double.Parse(parts[2]),
                            Country = parts[3],
                            Year = int.Parse(parts[4]),
                            Type = (BeerType)Enum.Parse(typeof(BeerType), parts[5]),
                            IBU = int.Parse(parts[6])
                        };
                        beverages.Add(beer);
                    }
                    else if (parts[0] == "Wine")
                    {
                        var wine = new Wine
                        {
                            Name = parts[1],
                            Alcohol = double.Parse(parts[2]),
                            Country = parts[3],
                            Year = int.Parse(parts[4]),
                            Type = (WineType)Enum.Parse(typeof(WineType), parts[5]),
                            Grape = parts[7]
                        };
                        beverages.Add(wine);
                    }
                }

            } catch (Exception ex)
            {
                
            }



            return beverages;
        }

        public void SaveAllBeverages(List<Beverage> beverages)
        {
            var builder = new StringBuilder();
            builder.AppendLine("Class,Name,Alcohol,Country,Year,Type,IBU,Grape");
            foreach (var bev in beverages)
            {
                builder.AppendLine(bev.CsvFormat());
            }

            try
            {
                File.WriteAllText("beverages.csv", builder.ToString());
            }
            catch (Exception ex)
            {
                // Handle exceptions (e.g., file access issues)
                Console.WriteLine($"Error saving beverages: {ex.Message}");
            }
        }
    }
}
