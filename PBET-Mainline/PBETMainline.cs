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
        int mainHour = 0;
        int mainScrap = 0;
        int mainDowntime = 0;

        double quality = 0.0;
        double performance = 0.0;
        double availability = 0.0;
        double oee = 0.0;

        int hourVariance = 0;

        string[] machines = { "Spoven1", "Spoven2", "Spoven3", "Mainline" };
        string[] departments = { "Paintline", "Bonding", "Fronts", "Bumpers", "Rears", };
        string[] customers = { "PACCAR", "NAVISTAR", "OTHER" };

        public mainForm()
        {
            InitializeComponent();
        }

        private void PBETMainline_Load(object sender, EventArgs e)
        {
            //ON LOAD
            AutoCompleteStringCollection sugMachine = new AutoCompleteStringCollection();
            AutoCompleteStringCollection sugDept = new AutoCompleteStringCollection();
            AutoCompleteStringCollection sugCust = new AutoCompleteStringCollection();

            foreach (string machine in machines)
            {
                sugMachine.Add(machine);
            }
            machineTf.AutoCompleteCustomSource = sugMachine;
            foreach (string department in departments)
            {
                sugDept.Add(department);
            }
            deptTf.AutoCompleteCustomSource = sugDept;
            foreach (string customer in customers)
            {
                sugCust.Add(customer);
            }
            custTf.AutoCompleteCustomSource = sugCust;


        }

        ///HELPER METHODS///
        ///
        private void reCalcTotals()
        {
            
            calcTotalGoal();
            calcTotalActual();
            calcVariance();
            calcTotalScrap();
            calcTotalDowntime();
        }

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

        private void bgColorChange() {
            if (mainActual < mainGoal)
            {
                totalActualLbl.BackColor = Color.Red;
            }
            else
            {
                totalActualLbl.BackColor = Color.Green;
            }

            if (mainVariance < 0)
            {
                totalVarianceLbl.BackColor = Color.Red;
            }
            else
            {
                totalVarianceLbl.BackColor = Color.Green;
            }

            if (mainScrap > 0)
            {
                totalScrapLbl.BackColor = Color.Red;
            }

            if (mainDowntime > 0)
            {
                totalDtLbl.BackColor = Color.Red;
            }
        }

        private void calcTotalGoal()
        {
            mainGoal = 0;

            mainGoal = Decimal.ToInt32(goalTf1.Value + goalTf2.Value);

            totalGoalLbl.Text = mainGoal.ToString();

            bgColorChange();
            
        }

        private void calcTotalActual()
        {
            mainActual = 0;

            mainActual = Decimal.ToInt32(actTf1.Value + actTf2.Value);

            totalActualLbl.Text = mainActual.ToString();

            bgColorChange();
            
        }


        private void calcVariance()
        {
            //RESET VARIANCE
            mainVariance = 0;

            //TOP HOURLY VARIANCE
            hourVariance = -1 * (Decimal.ToInt32(goalTf1.Value - actTf1.Value));
            varLbl1_1.Text = hourVariance.ToString();

            mainVariance += hourVariance;

            hourVariance = -1 * (Decimal.ToInt32(goalTf2.Value - actTf2.Value));
            varLbl2_1.Text = hourVariance.ToString();

            mainVariance += hourVariance;

            totalVarianceLbl.Text = mainVariance.ToString();

            bgColorChange();
        }

        private void calcTotalScrap()
        {
            //RESET SCRAP
            mainScrap = 0;

            mainScrap = Decimal.ToInt32(scrapTf1.Value + scrapTf2.Value);

            totalScrapLbl.Text = mainScrap.ToString();

            bgColorChange();

        }

        private void calcTotalDowntime()
        {
            //RESET DOWN
            mainDowntime = 0;

            mainDowntime = Decimal.ToInt32(downTf1.Value + downTf2.Value);

            totalDtLbl.Text = mainDowntime.ToString();

            bgColorChange();
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
            reCalcTotals();
        }

        private void goalTf2_ValueChanged(object sender, EventArgs e)
        {
            reCalcTotals();
        }

        private void actTf1_ValueChanged(object sender, EventArgs e)
        {
            reCalcTotals();
        }

        private void actTf2_ValueChanged(object sender, EventArgs e)
        {
            reCalcTotals();
        }

        private void scrapTf1_ValueChanged(object sender, EventArgs e)
        {
            reCalcTotals();
        }

        private void scrapTf2_ValueChanged(object sender, EventArgs e)
        {
            reCalcTotals();
        }

        private void downTf1_ValueChanged(object sender, EventArgs e)
        {
            reCalcTotals();
        }

        private void downTf2_ValueChanged(object sender, EventArgs e)
        {
            reCalcTotals();
        }
    }
}
