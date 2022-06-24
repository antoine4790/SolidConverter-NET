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
using System.Reflection;
using System.Diagnostics;

namespace SolidConverter
{
    public partial class AboutDialog : Form
    {
        public AboutDialog()
        {
            InitializeComponent();

            checkBoxLogging.Checked = Properties.Settings.Default.EnableLogging;

            Assembly assembly = Assembly.GetExecutingAssembly();
            FileVersionInfo fileVersionInfo = FileVersionInfo.GetVersionInfo(assembly.Location);

            label1.Text = string.Format("SolidConverter-NET, Version {0}", fileVersionInfo.ProductVersion);
            label4.Text = string.Format("Solid Framework, Build: {0}", SolidFramework.SolidEnvironment.Build);
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            // If logging is changed, save the state.
            if (checkBoxLogging.Checked != Properties.Settings.Default.EnableLogging)
            {
                Properties.Settings.Default.EnableLogging = checkBoxLogging.Checked;
                Properties.Settings.Default.Save();

                if (Properties.Settings.Default.EnableLogging)
                {
                    string logPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                    logPath = Path.Combine(logPath, "SolidConverter-NET");
                    if (!Directory.Exists(logPath))
                    {
                        Directory.CreateDirectory(logPath);
                    }
                    logPath = Path.Combine(logPath, "SolidConverter-NET_Log.txt");
                    SolidFramework.Plumbing.Logging.Instance.Path = logPath;
                }
                else
                {
                    SolidFramework.Plumbing.Logging.Instance.Path = string.Empty;
                }
            }
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        private void buttonUnlock_Click(object sender, EventArgs e)
        {
            UnlockDialog dialog = new UnlockDialog();
            DialogResult result = dialog.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                this.Close();
            }
        }
    }
}
