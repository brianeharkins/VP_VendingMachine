using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VP_VendingMachine
{
    public class Coin
    {
        public decimal Size { get; set; }
        public decimal Weight { get; set; }

        public Coin(string strCoin)
        {
            decimal Size;
            decimal Weight;

            bool validCoin = true;

            if (!string.IsNullOrEmpty(strCoin))
            {
                string[] coinDetails = strCoin.Split('/');

                if (!decimal.TryParse(coinDetails[0], out Size))
                {
                    validCoin = false;
                }

                if (!decimal.TryParse(coinDetails[1], out Weight))
                {
                    validCoin = false;
                }

                if (validCoin)
                {
                    Initialise(Size, Weight);
                }
            }
            else
            {
                Initialise(0, 0);
            }
        }

        public Coin(decimal size, decimal weight)
        {
            Initialise(size, weight);
        }

        public void Initialise(decimal size, decimal weight)
        {
            Size = size;
            Weight = weight;
        }
    }
}
