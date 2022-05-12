using CSTest.Enums;
using CSTest.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace CSTest.Classes
{
    public class Trade : ITrade
    {
        public Trade(double value, ClientSector clientSector)
        {
            Value = value;
            ClientSector = clientSector.ToString();
            Sector = clientSector;
        }
        public double Value { get; }

        public string ClientSector { get; }

        public ClientSector Sector { get; set; }

        public Category Category { get; set; }

    }
}
