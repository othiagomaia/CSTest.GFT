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

            InitCategories(init);
            InitPortfolio(init);

            List<string> tradeCategories = categorizer.GenerateCategories(init.Portfolio, init.Categories);

            Console.WriteLine("tradeCategories = " + categorizer.StringPortfolio(tradeCategories));
        }

        /// <summary>
        /// Initializes the Categories collection according to the example
        /// </summary>
        private static void InitCategories(Initializer init)
        {
            init.AddCategory(new Category { Description = "LOWRISK", Rule = ComparisonRule.LessThan, ParameterValue = 1000000, Sector = ClientSector.Public });
            init.AddCategory(new Category { Description = "MEDIUMRISK", Rule = ComparisonRule.GreaterThan, ParameterValue = 1000000, Sector = ClientSector.Public });
            init.AddCategory(new Category { Description = "HIGHRISK", Rule = ComparisonRule.GreaterThan, ParameterValue = 1000000, Sector = ClientSector.Private });
        }

        /// <summary>
        /// Initializes the Trade Portfolio collection according to the example
        /// </summary>
        private static void InitPortfolio(Initializer init)
        {
            init.AddTradeToPortfolio(new Trade(2000000, ClientSector.Private));
            init.AddTradeToPortfolio(new Trade(400000, ClientSector.Public));
            init.AddTradeToPortfolio(new Trade(500000, ClientSector.Public));
            init.AddTradeToPortfolio(new Trade(3000000, ClientSector.Public));
        }






    }
}
