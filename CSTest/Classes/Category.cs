using CSTest.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace CSTest.Classes
{
    public class Category
    {
        public double ParameterValue { get; set; }
        public ClientSector Sector { get; set; }
        public ComparisonRule Rule { get; set; }
        public string Description { get; set; }

        
    }
}
