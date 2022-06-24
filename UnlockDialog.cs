using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SolidConverter
{
    public partial class UnlockDialog : Form
    {
        public UnlockDialog()
        {
            InitializeComponent();

            textBoxUserName.Text = Properties.Settings.Default.UserName;
            textBoxUserEmail.Text = Properties.Settings.Default.UserEmail;
            textBoxUserOrg.Text = Properties.Settings.Default.UserOrg;
            textBoxUserUnlock.Text = Properties.Settings.Default.UserCode;
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            if (textBoxUserOrg.Text == null)
                textBoxUserOrg.Text = string.Empty;

            if (string.IsNullOrEmpty(textBoxUserName.Text) ||
                string.IsNullOrEmpty(textBoxUserEmail.Text) ||
                string.IsNullOrEmpty(textBoxUserUnlock.Text))
            {
                ShowInvalidMessage(this);
                return;
            }

            SolidFramework.License.Clear();

            try
            {
                SolidFramework.License.Import(textBoxUserName.Text,
                    textBoxUserEmail.Text,
                    textBoxUserOrg.Text,
                    textBoxUserUnlock.Text);
            }
            catch (SolidFramework.InvalidLicenseException)
            {
                ShowInvalidMessage(this);
                textBoxUserName.Focus();
                return;
            }


            // Save user info
            Properties.Settings.Default.UserName = textBoxUserName.Text;
            Properties.Settings.Default.UserEmail = textBoxUserEmail.Text;
            Properties.Settings.Default.UserOrg = textBoxUserOrg.Text;
            Properties.Settings.Default.UserCode = textBoxUserUnlock.Text;
            Properties.Settings.Default.Save();

            ShowSuccessMessage(this);

            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }


        public static void ShowSuccessMessage(Form parent)
        {
            SolidFramework.Forms.SolidMessageBox msgDialog = new SolidFramework.Forms.SolidMessageBox(parent);
            msgDialog.MessageIcon = MessageBoxIcon.Information;
            msgDialog.Buttons = MessageBoxButtons.OK;
            msgDialog.Content = "SolidConverter-Net successfully unlocked.";
            msgDialog.Text = "Unlock";
            msgDialog.Execute();
        }

        public static void ShowInvalidMessage(Form parent)
        {
            SolidFramework.Forms.SolidMessageBox msgDialog = new SolidFramework.Forms.SolidMessageBox(parent);
            msgDialog.MessageIcon = MessageBoxIcon.Error;
            msgDialog.Buttons = MessageBoxButtons.OK;
            msgDialog.Content = "Please enter valid unlock information.";
            msgDialog.Text = "Invalid Unlock";
            msgDialog.Execute();
        }
    }
}
