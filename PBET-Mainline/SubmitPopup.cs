using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PBET_Mainline
{
    public partial class SubmitPopup : Form
    {
        string currentUserName = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
        

        public SubmitPopup()
        {
            InitializeComponent();
        }

        private void SubmitPopup_Load(object sender, EventArgs e)
        {
            currentUserName = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
            Console.WriteLine(currentUserName);
            
        }

        private void confBtn_Click(object sender, EventArgs e)
        {
            if(txtBoxName.Text == currentUserName)
            {
                this.DialogResult = DialogResult.OK;
            } else
            {
                warningLbl.Show();
                Console.WriteLine("Invalid");
            }
        }

        private void cancelBtn_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void txtBoxName_TextChanged(object sender, EventArgs e)
        {
            warningLbl.Hide();
        }
    }
}
