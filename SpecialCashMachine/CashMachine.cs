using SpecialCashMachine.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public  decimal Balance =>  GetBalance().Result  ;

        public IEnumerable<ICassette> Cassettes { get; set; }

        public List<ChangeItem> WithdrawalAmounts { get; private set; }

        private  Task<decimal> GetBalance()
        {

            return Task.Run<decimal>(() => { return Cassettes.Sum(c => c.Total); } );
                

        }

        public async Task<Transaction> Dispense(decimal amount, Algorithm algo)
        {
            Transaction transaction =  new Transaction();

                
            if (amount <= Balance)
            {             
                Calculate(amount,algo);
                transaction = await GetChange();
            }
            else
            {
                transaction.TranscationStatus = Status.Error;
                transaction.ErrorMessage = $"The requested amount of {amount.ToString("C")} exceeds the available balance of {Balance.ToString("C")}";
            }
            return  transaction;
        }


        private async Task<Transaction> GetChange()
        {
           
            return new Transaction
            {
                TranscationStatus = Status.Ok,
                VendedChange = new Change
                {
                    RemainingBalance = Balance,
                    WithdrawalAmounts = WithdrawalAmounts
                }
                
            };
        }

        private string GetFormattedMessage()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Dispensing:");
            foreach (var item in WithdrawalAmounts)
            {
                if (item.Quantity > 0)
                {
                    sb.AppendLine($"{item.Denomination.ToString("C")} x {item.Quantity}");
                }
            }
            sb.AppendLine($"Remaining Balance {Balance.ToString("C")}");
            return sb.ToString();
        }


        private void Calculate(decimal amount,Algorithm favAlgorithm)
        {
            _amountRemaining = amount;
            var sortedList = OrderCassetteList(favAlgorithm);
            WithdrawalAmounts = new List<ChangeItem>();
            foreach (var cassette in sortedList)
            {
                int no = Check(cassette);
                if(no > 0)
                {
                    WithdrawalAmounts.Add(new ChangeItem { Denomination = cassette.Denomination, Quantity = no });
                }
               
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
