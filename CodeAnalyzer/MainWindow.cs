using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CodeAnalyzer
{
    public partial class MainWindow : Form
    {
        private List<FiniteStateAutomaton> FiniteStateAutomatons;


        public MainWindow()
        {
            InitializeComponent();

            FiniteStateAutomatons = new List<FiniteStateAutomaton>();
        }


        private void MainWindow_Load(object sender, EventArgs e)
        {
            FiniteStateAutomatons.Add(new FiniteStateAutomaton("../../FiniteStateAutomatons/ID.txt"));
            FiniteStateAutomatons.Add(new FiniteStateAutomaton("../../FiniteStateAutomatons/KW.txt"));

            FiniteStateAutomatons[0].maxString("myID = 12", 0);
            FiniteStateAutomatons[1].maxString("int myID = 12", 0);
            FiniteStateAutomatons[1].maxString("float myID = 12", 0);

            string inputCode = "float myID = 12";

            int k = 0;
            while(k < inputCode.Length)
            {
                bool Result = false;
                int M = 0;
                int Priority = -1;
                string FiniteStateAutomatonName = "";

                FiniteStateAutomatons.ForEach(item =>
                {
                    Tuple<bool, int> maxStringResult = item.maxString(inputCode, k);
                    if(M < maxStringResult.Item2 || (M == maxStringResult.Item2 && Priority > item.Priority))
                    {
                        FiniteStateAutomatonName = item.Name;
                        Priority = item.Priority;
                        M = maxStringResult.Item2;
                        Result = maxStringResult.Item1;
                    }
                });
                if(!Result)
                {
                    Console.WriteLine($"<\"Err\",{k}>");
                    k++;
                }
                else
                {
                    Console.WriteLine($"<\"{FiniteStateAutomatonName}\",{inputCode.Substring(k, M)}>");
                    k += M;
                }
            }
        }
    }
}
