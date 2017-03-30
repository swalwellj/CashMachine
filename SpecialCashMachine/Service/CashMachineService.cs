using System.Collections.Generic;

namespace SpecialCashMachine.Service
{
    public  static class CashMachineService
    {

        private static CashMachine _instance;

        public static CashMachine Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new CashMachine(GetUKCassettes());
                }

                return _instance;
            }
        }

        private static List<Cassette> GetUKCassettes()
        {
            return new List<Cassette>(new Cassette[12] {
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
        }
    }
}
