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
            la.SetOutAutomatons(dataGridViewFiniteStateAutomatons);
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {
            GenerateAutomatonText.Start("../../Resources/Keywords.txt", "../../Resources/KeywordsFunc.txt");
            GenerateAutomatonText.Start("../../Resources/Operators.txt", "../../Resources/OperatorsFunc.txt");
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
            richTextBoxOutput.Text = la.Lexer(richTextBoxInputCode.Text);
        }

        private void buttonLoadFiniteStateAutomatons_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                dialog.Title = "Открыть файл";
                dialog.Filter = "Текстовый документ (*.txt)|*.txt|Все файлы (*.*)|*.*";
                dialog.Multiselect = true;
                DialogResult result = dialog.ShowDialog();
                if (result == DialogResult.OK)
                {
                    la.LoadFiniteStateAutomatons(dialog.FileNames);
                }
            }
        }

        private void buttonDeleteFiniteStateAutomaton_Click(object sender, EventArgs e)
        {
            if(dataGridViewFiniteStateAutomatons.SelectedRows.Count == 1)
            {
                try
                {
                    la.RemoveFiniteStateAutomatonByName(dataGridViewFiniteStateAutomatons.SelectedRows[0].Cells[0].Value.ToString());
                }
                catch (ArgumentException ex)
                {
                    MessageBox.Show(this, ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show(this, "Выберите строку", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
