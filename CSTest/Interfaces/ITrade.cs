using System;
using System.Collections.Generic;
using System.Text;

namespace CSTest.Interfaces
{
    public interface ITrade
    {
        public double Value { get; }
        public string ClientSector { get; }
    }
}
