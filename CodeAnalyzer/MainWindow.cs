using System;
using System.IO;
using System.Windows.Forms;

namespace CodeAnalyzer
{
    public partial class MainWindow : Form
    {
        private LexicalAnalysis la;
        public MainWindow()
        {
            InitializeComponent();

            la = new LexicalAnalysis();
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {
            // GenerateAutomatonKWText.Start();

            la.LoadFiniteStateAutomatons(new string[]
            {
                "../../FiniteStateAutomatons/ID.txt",
                "../../FiniteStateAutomatons/KW.txt",
                "../../FiniteStateAutomatons/WS.txt"
            });

            la.ShowLoadAutomatons(dataGridViewFiniteStateAutomatons);

            string inputCode = "float myID = 12";
            string result = la.Start(inputCode);

            Console.WriteLine(result);
        }

        private void buttonOpenFile_Click(object sender, EventArgs e)
        {
            using(OpenFileDialog dialog = new OpenFileDialog())
            {
                dialog.Title = "Открыть файл";
                dialog.Filter = "Файлы с кодом C# (*.cs)|*.cs|Все файлы (*.*)|*.*";
                DialogResult result = dialog.ShowDialog();
                if(result == DialogResult.OK)
                {
                    StreamReader reader = new StreamReader(dialog.OpenFile());
                    richTextBoxInputCode.Text = reader.ReadToEnd();
                }
            }
        }

        private void buttonStartAnalysis_Click(object sender, EventArgs e)
        {
            richTextBoxOutput.Text = la.Start(richTextBoxInputCode.Text);
        }
    }
}
