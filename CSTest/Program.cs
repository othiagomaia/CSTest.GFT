using CSTest.Classes;
using CSTest.Enums;
using CSTest.Interfaces;
using System;
using System.Collections.Generic;

namespace CSTest
{
    class Program
    {
        static void Main(string[] args)
        {
            //Singleton Instance
            Initializer init = Initializer.GetInitializer();
            
            //Facade containing the methods
            TradeCategorizer categorizer = new TradeCategorizer();
            
            List<string> tradeCategories = categorizer.GenerateCategories(init.Portfolio, init.Categories);

            Console.WriteLine("tradeCategories = " + categorizer.StringPortfolio(tradeCategories));
        }

        
        
    }
}
