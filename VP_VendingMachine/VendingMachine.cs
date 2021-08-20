using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VP_VendingMachine
{
    public class VendingMachine
    {
        public decimal AmountDeposited { get; set; }
        public decimal AmountReturned { get; set; }

        public Dictionary<string, decimal> ValidCoins = new Dictionary<string, decimal>()
        {
            // Size(mm), Weight(g)
            {"17/2.3", 0.05M }, //dime
            {"19/2.5", 0.10M }, //cent
            {"24/5.7", 0.25M }, //quarter
        };

        public Dictionary<int, KeyValuePair<string, decimal>> Products = new Dictionary<int, KeyValuePair<string, decimal>>()
        {
            { 1, new KeyValuePair<string, decimal> ("Cola",      1.00M) },
            { 2, new KeyValuePair<string, decimal> ("Crisps",    0.50M) },
            { 3, new KeyValuePair<string, decimal> ("Chocolate", 0.65M) },
        };

        public VendingMachine()
        {
            AmountDeposited = 0;
            AmountReturned = 0;
        }

        static void Main(string[] args)
        {
            VendingMachine vend = new VendingMachine();

            Console.WriteLine("VENDING MACHINE SIMLUATOR");
            Console.WriteLine("=========================\n");
            Console.WriteLine(" Options:");
            Console.WriteLine("   r                Return coins.");
            Console.WriteLine("   l                List all products.");
            Console.WriteLine("   p <Id>           Purchase item (if funds allow).");
            Console.WriteLine("   i <Size/Weight>  Insert coin, e.g. 'i 17/2.3' will enter a Dime, 'i 24/5.7' will enter a quarter.");
            Console.WriteLine();

            while (true)
            {
                Console.WriteLine("AMOUNT DEPOSITED: $" + vend.AmountDeposited);
                Console.WriteLine("\nINSERT COIN");

                string[] input = Console.ReadLine().Split(' ');

                if (input[0] == "r")
                {
                    Console.WriteLine("COINS RETURNED. AMOUNT: $" + vend.ReturnCoins());
                }
                else if (input[0] == "l") 
                {
                    Console.WriteLine("AVAILABLE PRODUCTS");
                    Console.WriteLine("==================\n");
                    for (int i = 1; i <= vend.Products.Count; i++)
                    {                        
                        Console.WriteLine(i + " - " + vend.Products[i].Key + ": $" + Math.Round(vend.Products[i].Value, 2));
                    }
                    Console.WriteLine();
                }
                else if (input[0] == "p")
                {
                    try
                    {
                        int selectedProductId = int.Parse(input[1]);

                        if (Enumerable.Range(1, vend.Products.Count).Contains(selectedProductId))
                        {
                            vend.Purchase(selectedProductId);
                        }
                    }
                    catch 
                    {
                        Console.WriteLine("ENTER A VALID PRODUCT ID");
                    }
                    
                }
                else if (input[0] == "i")
                {
                    if (vend.IsCoinValid(new Coin(input[1])))
                    {
                        Console.WriteLine("COIN ACCEPTED");
                    }
                }
                else
                {
                    Console.WriteLine("PLEASE CHECK COIN RETURN");
                }

            }
        }

        public bool IsCoinValid(Coin coin)
        {
            if (ValidCoins.ContainsKey(coin.Size + "/" + coin.Weight))
            {
                AddAmount(ValidCoins[coin.Size + "/" + coin.Weight]);
                return true;
            }
            return false;
        }

        public decimal ReturnCoins()
        {
            AmountReturned = AmountDeposited;
            AmountDeposited = 0;

            return AmountReturned;
        }

        public bool Purchase(int Id)
        {
            if (Products.ContainsKey(Id) && AmountDeposited >= Products[Id].Value)
            {
                SubtractAmount(Products[Id].Value);                
                Console.WriteLine("THANK YOU\n");
                return true;
            }
            else if (Products.ContainsKey(Id) && AmountDeposited < Products[Id].Value)
            {
                Console.WriteLine("PRICE: $" + Products[Id].Value + "\n");
                return false;
            }
            else
            {
                Console.WriteLine("NOT RECOGNISED!" + "\n");
                return false;
            }
        }

        private void AddAmount(decimal amount)
        {
            AmountDeposited += amount;
        }

        private void SubtractAmount(decimal amount)
        {
            AmountDeposited -= amount;
        }


    }
}
