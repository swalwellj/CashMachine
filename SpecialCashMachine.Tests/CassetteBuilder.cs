using System;
using System.Collections.Generic;
using Castle.Components.DictionaryAdapter;

namespace SpecialCashMachine.Tests
{
    public class CassetteBuilder
    {
        private List<Cassette> _cassettes = new List<Cassette>();

        public CassetteBuilder WithDefaultCurrency()
        {
            _cassettes.AddRange(new Cassette[12] {
                new Cassette { Denomination = 50.00m, Quantity = 50 },
                new Cassette { Denomination = 20.00m, Quantity = 50 },
                new Cassette { Denomination = 10.00m, Quantity = 50 },
                new Cassette { Denomination = 5.00m, Quantity = 50 },
                new Cassette { Denomination = 2.00m, Quantity = 100 },
                new Cassette { Denomination = 1.00m, Quantity = 100 },
                new Cassette { Denomination = 0.50m, Quantity = 100 },
                new Cassette { Denomination = 00.20m, Quantity = 100 },
                new Cassette { Denomination = 00.10m, Quantity = 100 },
                new Cassette { Denomination = 00.05m, Quantity = 100 },
                new Cassette { Denomination = 00.02m, Quantity = 100 },
                new Cassette { Denomination = 00.01m, Quantity = 100 }
            });


            return this;
        }

        public List<Cassette> Build()
        {
            return _cassettes;
        }
    }

}
