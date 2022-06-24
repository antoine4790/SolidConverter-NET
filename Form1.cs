using SolidFramework.Plumbing;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Runtime.InteropServices;

namespace SolidConverter
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            comboBoxFileType.SelectedIndex = 1;
            comboBoxReconstructionMode.SelectedIndex = 0;

            buttonConvert.Enabled = false;

            listBoxFiles.Items.Clear();

            // The License needs to be Imported before a Converter is created.
            // Note that if you use a free trial license then some letters in the generated file 
            // will be deliberately substitued with other letters e.g. "e" replaced with "o".
            // This substitution does not occur if a full developer license is used.
            if (!string.IsNullOrEmpty(Properties.Settings.Default.UserEmail))
            {
                try
                {
                    SolidFramework.License.Import(Properties.Settings.Default.UserName,
                        Properties.Settings.Default.UserEmail,
                        Properties.Settings.Default.UserOrg,
                        Properties.Settings.Default.UserCode);
                }
                catch (SolidFramework.InvalidLicenseException)
                {
                    // license was invalid
                }
            }

            if (Properties.Settings.Default.EnableLogging)
            {
                string logPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                logPath = Path.Combine(logPath, "SolidConverter");
                if (!Directory.Exists(logPath))
                {
                    Directory.CreateDirectory(logPath);
                }
                logPath = Path.Combine(logPath, "SolidConverter_Log.txt");
                SolidFramework.Plumbing.Logging.Instance.Path = logPath;
            }

            // Check if Iris is installed.
            if (OCRInit.IrisInstalled() != false)
            {
                EnableOCR();
            }
        }

        private void EnableOCR()
        {
            labelOCR.Visible = true;
            comboBoxOcrLanguage.Visible = true;

            labelSave.Top = labelSave.Top - 25;
            comboBoxFileType.Top = comboBoxFileType.Top - 25;
            labelRecon.Top = labelRecon.Top - 25;
            comboBoxReconstructionMode.Top = comboBoxReconstructionMode.Top - 25;

            // Add the ocr languages.
            comboBoxOcrLanguage.Items.Clear();
            foreach (string lang in OCRInit.GetOCRLanguages())
            {
                if (lang == "au")
                {
                    comboBoxOcrLanguage.Items.Add("Automatic");
                }
                else
                {
                    comboBoxOcrLanguage.Items.Add(lang);
                }
            }

            comboBoxOcrLanguage.SelectedIndex = 0;
        }

        private void buttonAbout_Click(object sender, EventArgs e)
        {
            AboutDialog dialog = new AboutDialog();
            dialog.ShowDialog();
        }

        private void buttonConvert_Click(object sender, EventArgs e)
        {
            List<string> files = new List<string>();
            foreach(string filename in listBoxFiles.Items)
            {
                files.Add(filename);
            }

            SolidFramework.Converters.Plumbing.ReconstructionMode mode;

            switch(comboBoxReconstructionMode.SelectedIndex)
            {
                case 1:
                    mode = SolidFramework.Converters.Plumbing.ReconstructionMode.Continuous;
                    break;
                case 2:
                    mode = SolidFramework.Converters.Plumbing.ReconstructionMode.Exact;
                    break;
                default:
                    mode = SolidFramework.Converters.Plumbing.ReconstructionMode.Flowing;
                    break;
            }

            ProgressDialog convertDialog;
            if (OCRInit.IrisInstalled() != false)
            { 
                convertDialog = new ProgressDialog(ref files, comboBoxFileType.SelectedIndex, mode, comboBoxOcrLanguage.Text);
            }
            else
            {
                convertDialog = new ProgressDialog(ref files, comboBoxFileType.SelectedIndex, mode, "Automatic");
            }
            convertDialog.ShowDialog(this);

            buttonClear_Click(null, null);
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void buttonChoose_Click(object sender, EventArgs e)
        {
            SolidFramework.Forms.SolidOpenFileDialog addDialog = new SolidFramework.Forms.SolidOpenFileDialog(this);
            addDialog.Multiselect = true;
            addDialog.Filter = "PDF Files|*.pdf";
            addDialog.FilterIndex = 0;

            if (addDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                foreach (string filePath in addDialog.FileNames)
                {
                    listBoxFiles.Items.Add(filePath);
                }
            }

            if (listBoxFiles.Items.Count > 0)
            {
                buttonConvert.Enabled = true;
            }
            else
            {
                buttonConvert.Enabled = false;
            }
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            listBoxFiles.Items.Clear();
            buttonConvert.Enabled = false;
            listBoxFiles.Focus();
        }

        private void comboBoxFileType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxFileType.SelectedIndex > 1)
            {
                comboBoxReconstructionMode.Enabled = false;
            }
            else
            {
                comboBoxReconstructionMode.Enabled = true;
            }

            if (comboBoxFileType.SelectedIndex == 4 ||
                comboBoxFileType.SelectedIndex == 8)
            {
                comboBoxOcrLanguage.Enabled = false;
            }
            else
            {
                comboBoxOcrLanguage.Enabled = true;
            }
        }
    }
}
