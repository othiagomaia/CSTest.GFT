using CSTest.Enums;
using CSTest.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace CSTest.Classes
{
    /// <summary>
    /// Singleton Class for the Project
    /// </summary>
    public class Initializer
    {
        static Initializer initializer;

        List<Category> categories = new List<Category>();
        List<ITrade> portfolio = new List<ITrade>();

        private static object locker = new object();

        protected Initializer()
        {
            InitCategories();
            InitPortfolio();
        }

        public static Initializer GetInitializer()
        {
            if (initializer == null)
            {
                lock (locker)
                {
                    if (initializer == null)
                    {
                        initializer = new Initializer();
                    }
                }
            }

            return initializer;
        }

        public List<Category> Categories
        {
            get
            {
                return categories;
            }
            set
            {
                categories = value;
            }
        }

        public List<ITrade> Portfolio
        {
            get
            {
                return portfolio;
            }
            set
            {
                portfolio = value;
            }
        }

        /// <summary>
        /// Initializes the Categories collection according to the example
        /// </summary>
        private void InitCategories()
        {
            categories = new List<Category>();
            categories.Add(new Category { Description = "LOWRISK", Rule = ComparisonRule.LessThan, ParameterValue = 1000000, Sector = ClientSector.Public });
            categories.Add(new Category { Description = "MEDIUMRISK", Rule = ComparisonRule.GreaterThan, ParameterValue = 1000000, Sector = ClientSector.Public });
            categories.Add(new Category { Description = "HIGHRISK", Rule = ComparisonRule.GreaterThan, ParameterValue = 1000000, Sector = ClientSector.Private });
        }

        /// <summary>
        /// Initializes the Trade Portfolio collection according to the example
        /// </summary>
        private void InitPortfolio()
        {
            portfolio = new List<ITrade>();
            portfolio.Add(new Trade(2000000, ClientSector.Private));
            portfolio.Add(new Trade(400000, ClientSector.Public));
            portfolio.Add(new Trade(500000, ClientSector.Public));
            portfolio.Add(new Trade(3000000, ClientSector.Public));
        }
    }
}
