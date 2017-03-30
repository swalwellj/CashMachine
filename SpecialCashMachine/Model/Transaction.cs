using System.Collections.Generic;

namespace SpecialCashMachine.Model
{
    public enum Status
    {
        Ok,
        Error
    }


    public class Transaction
    {
        public Status TranscationStatus { get; set; }
        public Change VendedChange { get; set; }

        public string ErrorMessage { get; set; }
    }


    public class Change
    {
        public decimal RemainingBalance { get; set; }
        public List<ChangeItem> WithdrawalAmounts { get; set; }
    }

    public class ChangeItem
    {
        public decimal Denomination { get; set; }
        public int Quantity { get; set; }
    }


}
