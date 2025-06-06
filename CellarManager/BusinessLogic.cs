﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CellarManager.model;

namespace CellarManager
{
    internal class BusinessLogic: ILogic
    {
        private IStorage _storage;
        public List<Beverage> Beverages { get; set; }
        public BusinessLogic(IStorage storage)
        {
            _storage = storage;
            Beverages = _storage.LoadAllBeverages();
        }

        public void AddBeer(string name, double alcohol, BeerType type, string? country, int? year, int? ibu)
        {
            var beer = new Beer { Name = name, Alcohol = alcohol, Type = type };
            if (ibu != null)
            {
                beer.IBU = ibu.Value;
            }
            if (country != null)
            {
                beer.Country = country;
            }
            if (year != null)
            {
                beer.Year = year.Value;
            }
            Beverages.Add(beer);
            _storage.SaveAllBeverages(Beverages);
        }

        public void AddWine(string name, double alcohol, WineType type, string? country, int? year, string? grape)
        {
            var wine = new Wine { Name = name, Alcohol = alcohol, Type = type };
            if (grape != null)
            {
                wine.Grape = grape;
            }
            if (country != null)
            {
                wine.Country = country;
            }
            if (year != null)
            {
                wine.Year = year.Value;
            }
            Beverages.Add(wine);
            _storage.SaveAllBeverages(Beverages);
        }

        public List<Beverage> GetBeverages()
        {
            return Beverages;
        }
        public List<Beverage> GetBeverages(string? searchStr)
        {
            if (String.IsNullOrEmpty(searchStr))
            {
                return Beverages;
            }
            // const newBeverages = Beverages.filter( bev => bev.name.toLowerCase().includes(searchStr.toLowerCase()))
            // var newBeverages = Beverages.Where(b => b.Name.ToLower().Contains(searchStr.ToLower())).ToList();
            var newBeverages = Beverages.Where(bev => bev.Name.Contains(searchStr, StringComparison.OrdinalIgnoreCase)).ToList();
            return newBeverages;
        }

        public void DeleteBeverage(int index)
        {
            Beverages.RemoveAt(index);
            _storage.SaveAllBeverages(Beverages);
        }



    }
}
