using CSTest.Classes;
using CSTest.Enums;
using CSTest.Interfaces;
using System;
using System.Collections.Generic;

namespace CSTest
{
    class Program
    {
        public static List<Category> categories;
        public static List<ITrade> portfolio;

        /// <summary>
        /// Fill Category List with Initial Data
        /// </summary>
        private static void InitCategories()
        {
            categories = new List<Category>();
            categories.Add(new Category { Description = "LOWRISK", Rule = ComparisonRule.LessThan, ParameterValue = 1000000, Sector = ClientSector.Public });
            categories.Add(new Category { Description = "MEDIUMRISK", Rule = ComparisonRule.GreaterThan, ParameterValue = 1000000, Sector = ClientSector.Public });
            categories.Add(new Category { Description = "HIGHRISK", Rule = ComparisonRule.GreaterThan, ParameterValue = 1000000, Sector = ClientSector.Private });
        }

        /// <summary>
        /// Fill Trades portfolio with Initial Data
        /// </summary>
        private static void InitPortfolio()
        {
            portfolio = new List<ITrade>();
            portfolio.Add(new Trade(2000000, ClientSector.Private));
            portfolio.Add(new Trade(400000, ClientSector.Public));
            portfolio.Add(new Trade(500000, ClientSector.Public));
            portfolio.Add(new Trade(3000000, ClientSector.Public));
        }

        /// <summary>
        /// Generate List of Category Strings 
        /// </summary>
        /// <param name="trades">List of Existing Trades</param>
        /// <returns></returns>
        private static List<string> GenerateCategories(List<ITrade> trades)
        {
            List<string> result = new List<string>();

            foreach (Trade trade in trades)
            {
                GenerateTradeCategory(trade);
                result.Add(trade.Category.Description);
            }

            return result;
        }

        static void Main(string[] args)
        {
            InitCategories();

            InitPortfolio();
            

            List<string> tradeCategories = GenerateCategories(portfolio);

            Console.WriteLine("tradeCategories = " + StringPortfolio(tradeCategories));
        }

        /// <summary>
        /// Show Trade Category List Formatted in Console App
        /// </summary>
        /// <param name="tradeCategories">List of Trade Categories</param>
        /// <returns></returns>
        private static string StringPortfolio(List<string> tradeCategories)
        {
            string output = "{";

            for (int i = 0; i < tradeCategories.Count; i++)
            {
                output += "\"" + tradeCategories[i] + "\"";
                if (i < tradeCategories.Count - 1)
                    output += ", ";   
            }

            output += "}";

            return output;
        }

        /// <summary>
        /// Fill Category information into Trade
        /// </summary>
        /// <param name="trade">Current Trade</param>
        private static void GenerateTradeCategory(Trade trade)
        {
            string tc = string.Empty;

            foreach (Category category in categories)
            {
                if (category.Sector == trade.Sector)
                {
                    switch (category.Rule)
                    {
                        case ComparisonRule.GreaterThan:
                            if (trade.Value > category.ParameterValue)
                            {
                                trade.Category = category;
                                return;
                            }
                            break;
                        case ComparisonRule.LessThan:
                            if (trade.Value < category.ParameterValue)
                            { 
                                trade.Category = category;
                                return;
                            }
                            break;
                        case ComparisonRule.Equals:
                            if (trade.Value == category.ParameterValue)
                            {
                                trade.Category = category;
                                return;
                            }
                            break;
                        default:
                            break;
                    }
                }
            }
        }
    }
}
