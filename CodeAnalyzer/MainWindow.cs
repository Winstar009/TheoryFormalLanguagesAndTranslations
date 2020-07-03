using System;
using System.Windows.Forms;

namespace CodeAnalyzer
{
    public partial class MainWindow : Form
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {
            //GenerateAutomatonKWText.Start();

            LexicalAnalysis la = new LexicalAnalysis();
            la.LoadFiniteStateAutomatons(new string[]
            {
                "../../FiniteStateAutomatons/ID.txt",
                "../../FiniteStateAutomatons/KW.txt"
            });

            string inputCode = "float myID = 12";
            string result = la.Start(inputCode);

            Console.WriteLine(result);
        }
    }
}
