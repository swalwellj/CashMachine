using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace SpecialCashMachine
{
    public enum Algorithm
    {
        LeastItems,
        FavouredDenomination
    }

    public class CashMachine : ICashMachine
    {
        private decimal _amountRemaining;

        public CashMachine(IEnumerable<ICassette> cassettes)
        {
            if (cassettes == null) throw new ArgumentException("Object of type ICassette must be supplied");
            Cassettes = cassettes;
        }

        public decimal Balance => GetBalance();

        public IEnumerable<ICassette> Cassettes { get; set; }

        public Dictionary<decimal, int> WithdrawalAmounts { get; private set; }

        private decimal GetBalance()
        {
            return Cassettes.Sum(c => c.Total);
        }

        public string Dispense(decimal amount, Algorithm algo)
        {
            string msg = string.Empty;
            WithdrawalAmounts = new Dictionary<decimal, int>();           
            if (amount <= GetBalance())
            {             
                Calculate(amount,algo);
                msg = GetFormattedMessage();
            }
            else
            {
                msg = $"The requested amount of {amount.ToString("C")} exceeds the available balance of {GetBalance().ToString("C")}";
            }
            return msg;
        }

        private string GetFormattedMessage()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Dispensing:");
            foreach (var item in WithdrawalAmounts)
            {
                if (item.Value > 0)
                {
                    sb.AppendLine($"{item.Key.ToString("C")} x {item.Value}");
                }
            }
            sb.AppendLine($"Remaining Balance {GetBalance().ToString("C")}");
            return sb.ToString();
        }


        private void Calculate(decimal amount,Algorithm favAlgorithm)
        {
            _amountRemaining = amount;
            var sortedList = OrderCassetteList(favAlgorithm);
            WithdrawalAmounts = new Dictionary<decimal, int>();
            foreach (var cassette in sortedList)
            {
                int no = Check(cassette);
                WithdrawalAmounts.Add(cassette.Denomination, no);
            }
        }

        /// <summary>
        /// Can be extended so denomination can be passed in
        /// </summary>
        /// <returns></returns>
        private decimal GetFavouredDenomination()
        {
            return 20.00m;
        }

        private IEnumerable<ICassette> OrderCassetteList(Algorithm algorithm)
        {
            IEnumerable<ICassette> result = Cassettes.OrderByDescending(c => c.Denomination);

            if (algorithm == Algorithm.FavouredDenomination)
            {
                var reorder = result.ToList();
                var cassette = reorder.Find(x => x.Denomination == GetFavouredDenomination());
                if (cassette != null)
                {
                    reorder.Remove(cassette);
                    reorder[0] = cassette;
                }

                result = reorder.ToList() ;

            }

            return result;
        }


        private int Check(ICassette cassette)
        {

            if (cassette.Quantity == 0 || _amountRemaining == 0) return 0;

            int count = 0;

            while (cassette.Quantity >= count && _amountRemaining >= cassette.Denomination)
            {

                _amountRemaining -= cassette.Denomination;
                count++;
            }

            cassette.Quantity -= count;


            return count;
        }
    }
}
