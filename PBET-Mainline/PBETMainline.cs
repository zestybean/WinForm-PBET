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
        int mainGoal = 0;
        int mainActual = 0;
        int mainVariance = 0;

        public mainForm()
        {
            InitializeComponent();
        }

        private void PBETMainline_Load(object sender, EventArgs e)
        {
            //ON LOAD

            
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

        private void calcTotalGoal()
        {
            mainGoal = 0;

            mainGoal = Decimal.ToInt32(goalTf1.Value + goalTf2.Value);

            totalGoalLbl.Text = mainGoal.ToString();
        }

        private void calcTotalActual()
        {
            mainActual = 0;

            mainActual = Decimal.ToInt32(actTf1.Value + actTf2.Value);

            totalActualLbl.Text = mainActual.ToString();

            if(mainActual < mainGoal)
            {
                //totalActualLbl.ForeColor = Color.Red;
                totalActualLbl.BackColor = Color.Red;
            } else
            {
                totalActualLbl.BackColor = Color.Green;
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

        private void goalTf1_ValueChanged(object sender, EventArgs e)
        {   
            calcTotalGoal();
        }

        private void goalTf2_ValueChanged(object sender, EventArgs e)
        {
            calcTotalGoal();
        }

        private void actTf1_ValueChanged(object sender, EventArgs e)
        {
            calcTotalActual();
        }

        private void actTf2_ValueChanged(object sender, EventArgs e)
        {
            calcTotalActual();
        }
    }
}
