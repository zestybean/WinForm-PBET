using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.Data.SqlClient;


namespace PBET_Mainline
{
    public partial class mainForm : Form
    {
        public mainForm()
        {
            InitializeComponent();
        }

        private void PBETMainline_Load(object sender, EventArgs e)
        {
            

            
        }

        ///HELPER METHODS///
        private void validateInput(object sender, KeyPressEventArgs e)
        {
     
            // Verify that the pressed key isn't CTRL or any non-numeric digit
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // If you want, you can allow decimal (float) numbers
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void hrTf1_KeyPress(object sender, KeyPressEventArgs e)
        {
            
            validateInput(sender, e);
        }

        private void hrTf1_TextChanged(object sender, EventArgs e)
        {
            //Reset label each time new input comes in
            hrLbl1.Text = "0";
            try
            {
                int result = Int32.Parse(hrTf1.Text);
                //Handle 12th hour
                if(result == 12)
                {
                    result -= 11;
                } else
                {
                    result += 1;
                }
                
                //Set label string
                hrLbl1.Text = result.ToString();

            } catch (FormatException)
            {
                Console.WriteLine("Bad parse.");
            }
        }

        private void subButton_Click(object sender, EventArgs e)
        {
            var user = System.Security.Principal.WindowsIdentity.GetCurrent();

            
            //USER CHECK

         
            //SUBMIT TO EXCEL
            
            
        }
    }
}
