using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM
{
    class Program
    {
        public static void Main(string[] args)
        {
            UserInterface interFace = new UserInterface();
            interFace.initialCashStock();
            interFace.MainMenu();
        }
    }
}
