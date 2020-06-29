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
        }
    }
}
