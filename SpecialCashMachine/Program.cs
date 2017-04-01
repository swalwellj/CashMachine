using System;
using System.Collections.Generic;
using System.Text;
using SpecialCashMachine.Model;
using static System.Decimal;

namespace SpecialCashMachine
{
    class Program
    {
        private static List<Cassette> _cassettes;
        private static decimal _withdraw = 0;
        private static int _selected = 0;
        private static CashMachine _cashMachine;
        static void Main(string[] args)
        {
            _cassettes = GetUKCassettes();
            _cashMachine = new CashMachine(_cassettes);
            try
            {
                NextTransaction();

            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);
            }

        }

        private static void GetWithdraw()
        {
            if (_withdraw > 0)
            {
                var transaction = _cashMachine.Dispense(_withdraw, (Algorithm)_selected).Result;
                string formattedMsg;
                if (transaction.TranscationStatus == Status.Ok)
                {
                  formattedMsg  = GetFormattedMessage(transaction);
                }
                else
                {
                    formattedMsg = transaction.ErrorMessage;
                }

                Console.WriteLine(formattedMsg);
            }
        }

        private static string GetFormattedMessage(Transaction transaction)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Dispensing:");
            foreach (var item in transaction.VendedChange.WithdrawalAmounts)
            {
                if (item.Quantity > 0)
                {
                    sb.AppendLine($"{item.Denomination.ToString("C")} x {item.Quantity}");
                }
            }
            sb.AppendLine($"Remaining Balance {transaction.VendedChange.RemainingBalance.ToString("C")}");
            return sb.ToString();
        }

        private static void NextTransaction()
        {
            Console.Clear();
            GetAmount();
            GetSelection();
            GetWithdraw();
            Console.WriteLine("Press any key to make another withdrawal");
            Console.ReadLine();
            NextTransaction();
        }

        private static void GetSelection()
        {
            _selected = 0;

            Console.WriteLine("Select the following");
            Console.WriteLine("Enter 1 to receive the least amount of notes and coins");
            Console.WriteLine("Enter 2 to receive the most £20 notes");

            var choice = Console.ReadLine();

            int selected = 0;
            int.TryParse(choice, out _selected);


            if (_selected != 1 && _selected != 2)
            {
                _selected = 1;
            }
        }



        private static void GetAmount()
        {
            _withdraw = 0;
            Console.WriteLine("Please enter the amount you wish to withdraw");

            var amount = Console.ReadLine();


            TryParse(amount, out _withdraw);

            if (_withdraw <= 0)
            {
                Console.WriteLine("Please enter a valid amount e.g. 20.00");
                Console.WriteLine("Press any key to continue");
                Console.ReadLine();
                NextTransaction();
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
