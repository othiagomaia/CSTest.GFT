using CSTest.Enums;
using CSTest.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace CSTest.Classes
{
    /// <summary>
    /// Facade Class to Categorize the Trades
    /// </summary>
    public class TradeCategorizer
    {
        /// <summary>
        /// Generates Categories for Trades Collection
        /// </summary>
        /// <param name="trades">Trades Collection</param>
        /// <param name="categories">Categories Collection</param>
        /// <returns></returns>
        public List<string> GenerateCategories(List<ITrade> trades, List<Category> categories)
        {
            List<string> result = new List<string>();

            foreach (Trade trade in trades)
            {
                GenerateTradeCategory(trade, categories);
                result.Add(trade.Category.Description);
            }

            return result;
        }

        /// <summary>
        /// Show Trade Category List Formatted in Console App
        /// </summary>
        /// <param name="tradeCategories">List of Trade Categories</param>
        /// <returns></returns>
        public string StringPortfolio(List<string> tradeCategories)
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
        /// Associates Category on the Trades
        /// </summary>
        /// <param name="trade"></param>
        /// <param name="categories"></param>
        private void GenerateTradeCategory(Trade trade, List<Category> categories)
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
