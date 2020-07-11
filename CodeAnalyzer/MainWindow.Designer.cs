namespace CodeAnalyzer
{
    partial class MainWindow
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.buttonLoadFiniteStateAutomatons = new System.Windows.Forms.Button();
            this.buttonDeleteFiniteStateAutomaton = new System.Windows.Forms.Button();
            this.buttonOpenFile = new System.Windows.Forms.Button();
            this.richTextBoxOutput = new System.Windows.Forms.RichTextBox();
            this.buttonStartAnalysis = new System.Windows.Forms.Button();
            this.dataGridViewFiniteStateAutomatons = new System.Windows.Forms.DataGridView();
            this.richTextBoxInputCode = new System.Windows.Forms.RichTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewFiniteStateAutomatons)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonLoadFiniteStateAutomatons
            // 
            this.buttonLoadFiniteStateAutomatons.Location = new System.Drawing.Point(12, 12);
            this.buttonLoadFiniteStateAutomatons.Name = "buttonLoadFiniteStateAutomatons";
            this.buttonLoadFiniteStateAutomatons.Size = new System.Drawing.Size(175, 23);
            this.buttonLoadFiniteStateAutomatons.TabIndex = 0;
            this.buttonLoadFiniteStateAutomatons.Text = "Загрузить конечные автоматы";
            this.buttonLoadFiniteStateAutomatons.UseVisualStyleBackColor = true;
            this.buttonLoadFiniteStateAutomatons.Click += new System.EventHandler(this.buttonLoadFiniteStateAutomatons_Click);
            // 
            // buttonDeleteFiniteStateAutomaton
            // 
            this.buttonDeleteFiniteStateAutomaton.Location = new System.Drawing.Point(12, 41);
            this.buttonDeleteFiniteStateAutomaton.Name = "buttonDeleteFiniteStateAutomaton";
            this.buttonDeleteFiniteStateAutomaton.Size = new System.Drawing.Size(175, 23);
            this.buttonDeleteFiniteStateAutomaton.TabIndex = 1;
            this.buttonDeleteFiniteStateAutomaton.Text = "Удалить конечный автомат";
            this.buttonDeleteFiniteStateAutomaton.UseVisualStyleBackColor = true;
            this.buttonDeleteFiniteStateAutomaton.Click += new System.EventHandler(this.buttonDeleteFiniteStateAutomaton_Click);
            // 
            // buttonOpenFile
            // 
            this.buttonOpenFile.Location = new System.Drawing.Point(12, 70);
            this.buttonOpenFile.Name = "buttonOpenFile";
            this.buttonOpenFile.Size = new System.Drawing.Size(175, 23);
            this.buttonOpenFile.TabIndex = 3;
            this.buttonOpenFile.Text = "Открыть файл с кодом";
            this.buttonOpenFile.UseVisualStyleBackColor = true;
            this.buttonOpenFile.Click += new System.EventHandler(this.buttonOpenFile_Click);
            // 
            // richTextBoxOutput
            // 
            this.richTextBoxOutput.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.richTextBoxOutput.Location = new System.Drawing.Point(12, 519);
            this.richTextBoxOutput.Name = "richTextBoxOutput";
            this.richTextBoxOutput.Size = new System.Drawing.Size(1240, 150);
            this.richTextBoxOutput.TabIndex = 4;
            this.richTextBoxOutput.Text = "";
            // 
            // buttonStartAnalysis
            // 
            this.buttonStartAnalysis.Location = new System.Drawing.Point(12, 99);
            this.buttonStartAnalysis.Name = "buttonStartAnalysis";
            this.buttonStartAnalysis.Size = new System.Drawing.Size(175, 23);
            this.buttonStartAnalysis.TabIndex = 5;
            this.buttonStartAnalysis.Text = "Выполнить анализ";
            this.buttonStartAnalysis.UseVisualStyleBackColor = true;
            this.buttonStartAnalysis.Click += new System.EventHandler(this.buttonStartAnalysis_Click);
            // 
            // dataGridViewFiniteStateAutomatons
            // 
            this.dataGridViewFiniteStateAutomatons.AllowUserToAddRows = false;
            this.dataGridViewFiniteStateAutomatons.AllowUserToDeleteRows = false;
            this.dataGridViewFiniteStateAutomatons.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewFiniteStateAutomatons.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewFiniteStateAutomatons.Location = new System.Drawing.Point(193, 12);
            this.dataGridViewFiniteStateAutomatons.Name = "dataGridViewFiniteStateAutomatons";
            this.dataGridViewFiniteStateAutomatons.ReadOnly = true;
            this.dataGridViewFiniteStateAutomatons.Size = new System.Drawing.Size(1059, 110);
            this.dataGridViewFiniteStateAutomatons.TabIndex = 6;
            // 
            // richTextBoxInputCode
            // 
            this.richTextBoxInputCode.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.richTextBoxInputCode.Location = new System.Drawing.Point(12, 128);
            this.richTextBoxInputCode.Name = "richTextBoxInputCode";
            this.richTextBoxInputCode.Size = new System.Drawing.Size(1240, 385);
            this.richTextBoxInputCode.TabIndex = 7;
            this.richTextBoxInputCode.Text = "";
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1264, 681);
            this.Controls.Add(this.richTextBoxInputCode);
            this.Controls.Add(this.dataGridViewFiniteStateAutomatons);
            this.Controls.Add(this.buttonStartAnalysis);
            this.Controls.Add(this.richTextBoxOutput);
            this.Controls.Add(this.buttonOpenFile);
            this.Controls.Add(this.buttonDeleteFiniteStateAutomaton);
            this.Controls.Add(this.buttonLoadFiniteStateAutomatons);
            this.MinimumSize = new System.Drawing.Size(640, 480);
            this.Name = "MainWindow";
            this.Text = "Анализатор";
            this.Load += new System.EventHandler(this.MainWindow_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewFiniteStateAutomatons)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonLoadFiniteStateAutomatons;
        private System.Windows.Forms.Button buttonDeleteFiniteStateAutomaton;
        private System.Windows.Forms.Button buttonOpenFile;
        private System.Windows.Forms.RichTextBox richTextBoxOutput;
        private System.Windows.Forms.Button buttonStartAnalysis;
        private System.Windows.Forms.DataGridView dataGridViewFiniteStateAutomatons;
        private System.Windows.Forms.RichTextBox richTextBoxInputCode;
    }
}

