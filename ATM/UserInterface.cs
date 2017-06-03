using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM
{
    public class UserInterface
    {
        public List<DollarBill> atmInventory = new List<DollarBill>();
        public int[] dollarValue = { 1, 5, 10, 20, 50, 100 };

        #region Menu
        public void MainMenu()
        {
            Console.WriteLine("Welcome to your ATM!");
            string interfaceChoice = Console.ReadLine();
            string[] inputData = interfaceChoice.Split('$');
            string uiChoice = inputData[0].Trim(' ');

                switch (uiChoice.ToLower())
                {
                    case "r":
                        if(inputData.Count() > 1)
                        {
                            Console.WriteLine("Failure: Invalid Command");
                            MainMenu();
                            return;
                        }
                        restockCash();
                        MainMenu();
                        return;
                    case "w":
                        int dollarsRequested;
                        string dollarAmount = inputData[1].Trim(' ');
                        bool pass = int.TryParse(inputData[1], out dollarsRequested);
                        if (pass)
                        {
                            withdrawCash(dollarsRequested);
                        }
                        else
                        {
                            Console.WriteLine("Failure: Invalid Command");
                        }
                        MainMenu();
                        return;
                    case "l":
                        for (int i = 1; i < inputData.Count(); i++)
                        {
                            int dollarRequested;
                            string newDollarRequested = inputData[i].Trim(' ');
                            bool _pass = int.TryParse(newDollarRequested, out dollarRequested);
                            if (_pass)
                            {
                                if (dollarValue.Contains(dollarRequested))
                                {
                                    checkATMInventory(dollarRequested);
                                }
                                else
                                {
                                Console.WriteLine("Failure: Invalid Command");
                                }

                            }
                            else
                            {
                                Console.WriteLine("Failure: Invalid Command");
                            }
                        
                        }
                        MainMenu();
                        return;
                    case "q":
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Failure: Invalid Command");
                        MainMenu();
                        return;
                }
        }

        #endregion Menu

        #region MenuFunctions
        public void initialCashStock()
        {
            foreach(int value in dollarValue)
            {
                DollarBill bill = new DollarBill();
                bill.billValue = value;
                for(int i =0;i < 10; i++)
                {
                    atmInventory.Add(bill);
                }
            }

        }

        public void restockCash()
        {
            foreach(int amount in dollarValue)
            {
                int billCount = atmInventory.Where(x => x.billValue == amount).Count();
                DollarBill bill = new DollarBill();
                bill.billValue = amount;
                for(int i = billCount; i < 10; i++)
                {
                    atmInventory.Add(bill);
                }
            }
            Console.WriteLine("Restocked cash!" + "\n\r" + "New Balance:");
            foreach (int amount in dollarValue)
            {
                int billCount = atmInventory.Where(x => x.billValue == amount).Count();
                Console.WriteLine("$" + amount + " - " + billCount);
            }
        }

        public void withdrawCash(int requestedAmount)
        {
            int[] decendingAmounts = { 100, 50, 20, 10, 5, 1 };
            List<DollarBill> amountToWithdraw = new List<DollarBill>();
            int requestedAmountToWithdraw = requestedAmount;
            if (requestedAmountToWithdraw <= 0)
            {
                Console.WriteLine("Failure: Invalid Command");
                return;
            }
            foreach (int amount in decendingAmounts)
            {
                while(requestedAmountToWithdraw > 0)
                {
                    if (requestedAmountToWithdraw - amount >= 0)
                    {
                        if (atmInventory.Where(x => x.billValue == amount).Count() > 0)
                        {
                            DollarBill newBill = atmInventory.Where(x => x.billValue == amount).First();
                            atmInventory.Remove(newBill);
                            amountToWithdraw.Add(newBill);
                            requestedAmountToWithdraw -= amount;
                        }
                        else
                        {
                            break;
                        }
                    }
                    else
                    {
                        break;
                    }
                }
            }
            
            if(requestedAmountToWithdraw > 0)
            {
                atmInventory.AddRange(amountToWithdraw);
                Console.WriteLine("Failure: Insufficient Funds.");
            }
            else
            {
                Console.WriteLine("Sucess: Dispenced $" + requestedAmount.ToString());
                foreach (int amount in dollarValue)
                {
                    int billCount = atmInventory.Where(x => x.billValue == amount).Count();
                    Console.WriteLine("$" + amount + " - " + billCount);
                }
            }

        }

        public void checkATMInventory(int requestedAmount)
        {
                int billCount = atmInventory.Where(x => x.billValue == requestedAmount).Count();
                Console.WriteLine("$" + requestedAmount + " - " + billCount);
        }

        #endregion Menufunctions

    }
};
