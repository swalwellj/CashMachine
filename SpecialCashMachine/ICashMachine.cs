
using System.Collections.Generic;

namespace SpecialCashMachine
{
    public interface ICashMachine
    {
         decimal Balance { get;  }

        IEnumerable<ICassette> Cassettes { get; set; }
    }
}
