using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace SpecialCashMachine
{
    public class Cassette : ICassette
    {
      

        public Cassette(decimal denomination = 0.00m,int quantity =0, string currency = "GBP")
        {
            Denomination = denomination;
            Quantity = quantity;
            Currency = currency;

        }

        public decimal Denomination { get; set; }

        public int Quantity { get; set; }

        public string Currency { get; set; }

        public decimal Total => (Denomination*Quantity);
    }
}
