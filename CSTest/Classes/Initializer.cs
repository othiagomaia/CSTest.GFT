using CSTest.Enums;
using CSTest.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSTest.Classes
{
    /// <summary>
    /// Singleton Class for the Project
    /// </summary>
    public class Initializer
    {
        static Initializer initializer;

        List<Category> categories;
        List<ITrade> portfolio;

        private static object locker = new object();

        protected Initializer()
        {
            categories = new List<Category>();
            portfolio = new List<ITrade>();
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
        }

        public List<ITrade> Portfolio
        {
            get
            {
                return portfolio;
            }
        }

        public void AddCategory(Category category)
        {
            categories.Add(category);
        }

        public void UpdateCategory(Category newCategory)
        {
            var oldcategory = categories.FirstOrDefault(o => o.Description == newCategory.Description);
            oldcategory.ParameterValue = newCategory.ParameterValue;
            oldcategory.Rule = newCategory.Rule;
            oldcategory.Sector = newCategory.Sector;

        }

        public void RemoveCategory(Category category)
        {
            categories.RemoveAll(o => o.Description == category.Description);
        }


        public void AddTradeToPortfolio(ITrade trade)
        {
            portfolio.Add(trade);
        }

        
    }
}
