namespace SolidConverter
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
			this.buttonAbout = new System.Windows.Forms.Button();
			this.buttonConvert = new System.Windows.Forms.Button();
			this.buttonExit = new System.Windows.Forms.Button();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.buttonClear = new System.Windows.Forms.Button();
			this.buttonChoose = new System.Windows.Forms.Button();
			this.listBoxFiles = new System.Windows.Forms.ListBox();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.comboBoxOcrLanguage = new System.Windows.Forms.ComboBox();
			this.labelOCR = new System.Windows.Forms.Label();
			this.labelRecon = new System.Windows.Forms.Label();
			this.labelSave = new System.Windows.Forms.Label();
			this.comboBoxReconstructionMode = new System.Windows.Forms.ComboBox();
			this.comboBoxFileType = new System.Windows.Forms.ComboBox();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.SuspendLayout();
			// 
			// buttonAbout
			// 
			this.buttonAbout.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
			this.buttonAbout.Image = global::SolidConverter.Properties.Resources.help;
			this.buttonAbout.Location = new System.Drawing.Point(567, 22);
			this.buttonAbout.Name = "buttonAbout";
			this.buttonAbout.Size = new System.Drawing.Size(75, 75);
			this.buttonAbout.TabIndex = 2;
			this.buttonAbout.UseVisualStyleBackColor = true;
			this.buttonAbout.Click += new System.EventHandler(this.buttonAbout_Click);
			// 
			// buttonConvert
			// 
			this.buttonConvert.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
			this.buttonConvert.Image = global::SolidConverter.Properties.Resources.convert;
			this.buttonConvert.Location = new System.Drawing.Point(567, 166);
			this.buttonConvert.Name = "buttonConvert";
			this.buttonConvert.Size = new System.Drawing.Size(75, 75);
			this.buttonConvert.TabIndex = 3;
			this.buttonConvert.UseVisualStyleBackColor = true;
			this.buttonConvert.Click += new System.EventHandler(this.buttonConvert_Click);
			// 
			// buttonExit
			// 
			this.buttonExit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
			this.buttonExit.Image = global::SolidConverter.Properties.Resources.exit;
			this.buttonExit.Location = new System.Drawing.Point(567, 319);
			this.buttonExit.Name = "buttonExit";
			this.buttonExit.Size = new System.Drawing.Size(75, 75);
			this.buttonExit.TabIndex = 4;
			this.buttonExit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
			this.buttonExit.UseVisualStyleBackColor = true;
			this.buttonExit.Click += new System.EventHandler(this.buttonExit_Click);
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.buttonClear);
			this.groupBox1.Controls.Add(this.buttonChoose);
			this.groupBox1.Controls.Add(this.listBoxFiles);
			this.groupBox1.Location = new System.Drawing.Point(13, 13);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(529, 239);
			this.groupBox1.TabIndex = 0;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Files to Convert";
			// 
			// buttonClear
			// 
			this.buttonClear.Location = new System.Drawing.Point(446, 203);
			this.buttonClear.Name = "buttonClear";
			this.buttonClear.Size = new System.Drawing.Size(75, 23);
			this.buttonClear.TabIndex = 2;
			this.buttonClear.Text = "Clear files";
			this.buttonClear.UseVisualStyleBackColor = true;
			this.buttonClear.Click += new System.EventHandler(this.buttonClear_Click);
			// 
			// buttonChoose
			// 
			this.buttonChoose.Location = new System.Drawing.Point(7, 204);
			this.buttonChoose.Name = "buttonChoose";
			this.buttonChoose.Size = new System.Drawing.Size(111, 23);
			this.buttonChoose.TabIndex = 1;
			this.buttonChoose.Text = "Choose files...";
			this.buttonChoose.UseVisualStyleBackColor = true;
			this.buttonChoose.Click += new System.EventHandler(this.buttonChoose_Click);
			// 
			// listBoxFiles
			// 
			this.listBoxFiles.FormattingEnabled = true;
			this.listBoxFiles.Location = new System.Drawing.Point(7, 20);
			this.listBoxFiles.Name = "listBoxFiles";
			this.listBoxFiles.Size = new System.Drawing.Size(514, 173);
			this.listBoxFiles.TabIndex = 0;
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.comboBoxOcrLanguage);
			this.groupBox2.Controls.Add(this.labelOCR);
			this.groupBox2.Controls.Add(this.labelRecon);
			this.groupBox2.Controls.Add(this.labelSave);
			this.groupBox2.Controls.Add(this.comboBoxReconstructionMode);
			this.groupBox2.Controls.Add(this.comboBoxFileType);
			this.groupBox2.Location = new System.Drawing.Point(13, 269);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(529, 125);
			this.groupBox2.TabIndex = 1;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Conversion Settings";
			// 
			// comboBoxOcrLanguage
			// 
			this.comboBoxOcrLanguage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxOcrLanguage.FormattingEnabled = true;
			this.comboBoxOcrLanguage.Location = new System.Drawing.Point(279, 89);
			this.comboBoxOcrLanguage.Name = "comboBoxOcrLanguage";
			this.comboBoxOcrLanguage.Size = new System.Drawing.Size(242, 21);
			this.comboBoxOcrLanguage.Sorted = true;
			this.comboBoxOcrLanguage.TabIndex = 3;
			this.comboBoxOcrLanguage.Visible = false;
			// 
			// labelOCR
			// 
			this.labelOCR.AutoSize = true;
			this.labelOCR.Location = new System.Drawing.Point(279, 73);
			this.labelOCR.Name = "labelOCR";
			this.labelOCR.Size = new System.Drawing.Size(81, 13);
			this.labelOCR.TabIndex = 1;
			this.labelOCR.Text = "OCR Language";
			this.labelOCR.Visible = false;
			// 
			// labelRecon
			// 
			this.labelRecon.AutoSize = true;
			this.labelRecon.Location = new System.Drawing.Point(279, 39);
			this.labelRecon.Name = "labelRecon";
			this.labelRecon.Size = new System.Drawing.Size(112, 13);
			this.labelRecon.TabIndex = 3;
			this.labelRecon.Text = "Reconstruction Mode:";
			// 
			// labelSave
			// 
			this.labelSave.AutoSize = true;
			this.labelSave.Location = new System.Drawing.Point(10, 39);
			this.labelSave.Name = "labelSave";
			this.labelSave.Size = new System.Drawing.Size(50, 13);
			this.labelSave.TabIndex = 2;
			this.labelSave.Text = "Save As:";
			// 
			// comboBoxReconstructionMode
			// 
			this.comboBoxReconstructionMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxReconstructionMode.FormattingEnabled = true;
			this.comboBoxReconstructionMode.Items.AddRange(new object[] {
            "Flowing",
            "Continuous",
            "Exact"});
			this.comboBoxReconstructionMode.Location = new System.Drawing.Point(279, 60);
			this.comboBoxReconstructionMode.Name = "comboBoxReconstructionMode";
			this.comboBoxReconstructionMode.Size = new System.Drawing.Size(242, 21);
			this.comboBoxReconstructionMode.TabIndex = 1;
			// 
			// comboBoxFileType
			// 
			this.comboBoxFileType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxFileType.FormattingEnabled = true;
			this.comboBoxFileType.Items.AddRange(new object[] {
            "Rich Text File (*.rtf)",
            "Word Document (*.docx)",
            "Text File (*.txt)",
            "Html File (*.htm)",
            "Extract Images ( default )",
            "Excel Spreadsheet (*.xlsx)",
            "Csv Data (*.csv)",
            "PowerPoint Document (*.pptx)",
            "PDF/A Archive (*.pdf)",
            "Tagged PDF (*.pdf)"});
			this.comboBoxFileType.Location = new System.Drawing.Point(10, 60);
			this.comboBoxFileType.Name = "comboBoxFileType";
			this.comboBoxFileType.Size = new System.Drawing.Size(242, 21);
			this.comboBoxFileType.TabIndex = 0;
			this.comboBoxFileType.SelectedIndexChanged += new System.EventHandler(this.comboBoxFileType_SelectedIndexChanged);
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(671, 411);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.buttonExit);
			this.Controls.Add(this.buttonConvert);
			this.Controls.Add(this.buttonAbout);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "Form1";
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.Text = "SolidConverter";
			this.groupBox1.ResumeLayout(false);
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonAbout;
        private System.Windows.Forms.Button buttonConvert;
        private System.Windows.Forms.Button buttonExit;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button buttonClear;
        private System.Windows.Forms.Button buttonChoose;
        private System.Windows.Forms.ListBox listBoxFiles;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label labelRecon;
        private System.Windows.Forms.Label labelSave;
        private System.Windows.Forms.ComboBox comboBoxReconstructionMode;
        private System.Windows.Forms.ComboBox comboBoxFileType;
        private System.Windows.Forms.ComboBox comboBoxOcrLanguage;
        private System.Windows.Forms.Label labelOCR;
    }
}

