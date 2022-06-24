namespace SolidConverter
{
    partial class ProgressDialog
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
            this.labelFile = new System.Windows.Forms.Label();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.labelMode = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // labelFile
            // 
            this.labelFile.AutoEllipsis = true;
            this.labelFile.Location = new System.Drawing.Point(8, 20);
            this.labelFile.Name = "labelFile";
            this.labelFile.Size = new System.Drawing.Size(305, 26);
            this.labelFile.TabIndex = 0;
            this.labelFile.Text = "Filename";
            // 
            // buttonCancel
            // 
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(123, 103);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 1;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(9, 49);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(303, 23);
            this.progressBar1.TabIndex = 2;
            // 
            // labelMode
            // 
            this.labelMode.Location = new System.Drawing.Point(8, 79);
            this.labelMode.Name = "labelMode";
            this.labelMode.Size = new System.Drawing.Size(305, 21);
            this.labelMode.TabIndex = 3;
            this.labelMode.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ProgressDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(321, 138);
            this.ControlBox = false;
            this.Controls.Add(this.labelMode);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.labelFile);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ProgressDialog";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Converting";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label labelFile;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label labelMode;
    }
}