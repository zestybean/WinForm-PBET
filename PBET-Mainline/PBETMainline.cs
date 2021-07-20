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
            calcTotalHour();
            calcTotalGoal();
            calcTotalActual();
            calcVariance();
            calcTotalScrap();
            calcTotalDowntime();
            calcTotalOEE();
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

        private void sumHours()
        {
            mainHour = 0;
            foreach (Control x in this.Controls)
            {
                if (x is TextBox)
                {

                    if ((x as TextBox).Text != string.Empty || (x as TextBox).Text != "")
                    {
                        mainHour += 1;
                        reCalcTotals();
                    }
                }
            }
        }

        private void calcTotalOEE()
        {
            if (mainGoal > 0)
            {
                performance = (Convert.ToDouble(mainActual) / Convert.ToDouble(mainGoal)) * 100.0;
                
                perfLbl.Text = String.Format("{0:F2}%", performance);


                availability = (1.0 - (Convert.ToDouble(mainDowntime) / 480.0)) * 100.0;
                avaiLbl.Text = String.Format("{0:F2}%", availability);

                if (mainScrap > 0)
                {
                    quality = (1.0 - (Convert.ToDouble(mainScrap) / Convert.ToDouble(mainActual))) * 100.0;
                    quaLbl.Text = String.Format("{0:F2}%", quality);

                    oee = ((quality/100.0) * (performance/100.0) * (availability/100.0))*100.0;
                    oeeLbl.Text = String.Format("{0:F2}%", oee);
                }
            }
        }

        private void calcTotalHour()
        {
            totalHoursLbl.Text = mainHour.ToString();
        }

        private void calcTotalGoal()
        {
            //RESET GOAL
            mainGoal = 0;

            mainGoal = Decimal.ToInt32(goalTf1.Value + goalTf2.Value + goalTf3.Value + goalTf4.Value + goalTf5.Value + goalTf6.Value + goalTf7.Value + goalTf8.Value + goalTf9.Value + goalTf10.Value + goalTf11.Value + goalTf12.Value);

            totalGoalLbl.Text = mainGoal.ToString();

            bgColorChange();
            
        }

        private void calcTotalActual()
        {
            //RESET ACTUAL
            mainActual = 0;

            mainActual = Decimal.ToInt32(actTf1.Value + actTf2.Value + actTf3.Value + actTf4.Value + actTf5.Value + actTf6.Value + actTf7.Value + actTf8.Value + actTf9.Value + actTf10.Value + actTf11.Value + actTf12.Value);

            totalActualLbl.Text = mainActual.ToString();

            bgColorChange();
            
        }

        private void calcTotalScrap()
        {
            //RESET SCRAP
            mainScrap = 0;

            mainScrap = Decimal.ToInt32(scrapTf1.Value + scrapTf2.Value + scrapTf3.Value + scrapTf4.Value + scrapTf5.Value + scrapTf6.Value + scrapTf7.Value + scrapTf8.Value + scrapTf9.Value + scrapTf10.Value + scrapTf11.Value + scrapTf12.Value);

            totalScrapLbl.Text = mainScrap.ToString();

            bgColorChange();

        }

        private void calcTotalDowntime()
        {
            //RESET DOWN
            mainDowntime = 0;

            mainDowntime = Decimal.ToInt32(downTf1.Value + downTf2.Value + downTf3.Value + downTf4.Value + downTf5.Value + downTf6.Value + downTf7.Value + downTf8.Value + downTf9.Value + downTf10.Value + downTf11.Value + downTf12.Value);

            totalDtLbl.Text = mainDowntime.ToString();

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

            hourVariance = -1 * (Decimal.ToInt32(goalTf3.Value - actTf3.Value));
            varLbl3_1.Text = hourVariance.ToString();

            mainVariance += hourVariance;

            hourVariance = -1 * (Decimal.ToInt32(goalTf4.Value - actTf4.Value));
            varLbl4_1.Text = hourVariance.ToString();

            mainVariance += hourVariance;

            hourVariance = -1 * (Decimal.ToInt32(goalTf5.Value - actTf5.Value));
            varLbl5_1.Text = hourVariance.ToString();

            mainVariance += hourVariance;

            hourVariance = -1 * (Decimal.ToInt32(goalTf6.Value - actTf6.Value));
            varLbl6_1.Text = hourVariance.ToString();

            mainVariance += hourVariance;

            hourVariance = -1 * (Decimal.ToInt32(goalTf7.Value - actTf7.Value));
            varLbl7_1.Text = hourVariance.ToString();

            mainVariance += hourVariance;

            hourVariance = -1 * (Decimal.ToInt32(goalTf8.Value - actTf8.Value));
            varLbl8_1.Text = hourVariance.ToString();

            mainVariance += hourVariance;

            hourVariance = -1 * (Decimal.ToInt32(goalTf9.Value - actTf9.Value));
            varLbl9_1.Text = hourVariance.ToString();

            mainVariance += hourVariance;

            hourVariance = -1 * (Decimal.ToInt32(goalTf10.Value - actTf10.Value));
            varLbl10_1.Text = hourVariance.ToString();

            mainVariance += hourVariance;

            hourVariance = -1 * (Decimal.ToInt32(goalTf11.Value - actTf11.Value));
            varLbl11_1.Text = hourVariance.ToString();

            mainVariance += hourVariance;

            hourVariance = -1 * (Decimal.ToInt32(goalTf12.Value - actTf12.Value));
            varLbl12_1.Text = hourVariance.ToString();

            mainVariance += hourVariance;

            totalVarianceLbl.Text = mainVariance.ToString();

            bgColorChange();
        }

        

        private void hrTf1_TextChanged(object sender, EventArgs e)
        {
            sumHours();
            
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

        private void hrTf2_TextChanged(object sender, EventArgs e)
        {
            sumHours();

            //Reset label each time new input comes in
            hrLbl2.Text = "0";
            try
            {
                int result = Int32.Parse(hrTf2.Text);
                //Handle 12th hour
                if (result == 12)
                {
                    result -= 11;
                }
                else
                {
                    result += 1;
                }

                //Set label string
                hrLbl2.Text = result.ToString();
            }
            catch (FormatException)
            {
                Console.WriteLine("Bad parse.");
            }
        }

        private void hrTf3_TextChanged(object sender, EventArgs e)
        {
            sumHours();
            //Reset label each time new input comes in
            hrLbl3.Text = "0";
            try
            {
                int result = Int32.Parse(hrTf3.Text);
                //Handle 12th hour
                if (result == 12)
                {
                    result -= 11;
                }
                else
                {
                    result += 1;
                }

                //Set label string
                hrLbl3.Text = result.ToString();

            }
            catch (FormatException)
            {
                Console.WriteLine("Bad parse.");
            }
        }

        private void hrTf4_TextChanged(object sender, EventArgs e)
        {
            sumHours();
            //Reset label each time new input comes in
            hrLbl4.Text = "0";
            try
            {
                int result = Int32.Parse(hrTf4.Text);
                //Handle 12th hour
                if (result == 12)
                {
                    result -= 11;
                }
                else
                {
                    result += 1;
                }

                //Set label string
                hrLbl4.Text = result.ToString();

            }
            catch (FormatException)
            {
                Console.WriteLine("Bad parse.");
            }
        }

        private void hrTf5_TextChanged(object sender, EventArgs e)
        {
            sumHours();
            //Reset label each time new input comes in
            hrLbl5.Text = "0";
            try
            {
                int result = Int32.Parse(hrTf5.Text);
                //Handle 12th hour
                if (result == 12)
                {
                    result -= 11;
                }
                else
                {
                    result += 1;
                }

                //Set label string
                hrLbl5.Text = result.ToString();

            }
            catch (FormatException)
            {
                Console.WriteLine("Bad parse.");
            }
        }

        private void hrTf6_TextChanged(object sender, EventArgs e)
        {
            sumHours();
            //Reset label each time new input comes in
            hrLbl6.Text = "0";
            try
            {
                int result = Int32.Parse(hrTf6.Text);
                //Handle 12th hour
                if (result == 12)
                {
                    result -= 11;
                }
                else
                {
                    result += 1;
                }

                //Set label string
                hrLbl6.Text = result.ToString();

            }
            catch (FormatException)
            {
                Console.WriteLine("Bad parse.");
            }
        }

        private void hrTf7_TextChanged(object sender, EventArgs e)
        {
            sumHours();
            //Reset label each time new input comes in
            hrLbl7.Text = "0";
            try
            {
                int result = Int32.Parse(hrTf7.Text);
                //Handle 12th hour
                if (result == 12)
                {
                    result -= 11;
                }
                else
                {
                    result += 1;
                }

                //Set label string
                hrLbl7.Text = result.ToString();

            }
            catch (FormatException)
            {
                Console.WriteLine("Bad parse.");
            }
        }

        private void hrTf8_TextChanged(object sender, EventArgs e)
        {
            sumHours();
            //Reset label each time new input comes in
            hrLbl8.Text = "0";
            try
            {
                int result = Int32.Parse(hrTf8.Text);
                //Handle 12th hour
                if (result == 12)
                {
                    result -= 11;
                }
                else
                {
                    result += 1;
                }

                //Set label string
                hrLbl8.Text = result.ToString();

            }
            catch (FormatException)
            {
                Console.WriteLine("Bad parse.");
            }
        }

        private void hrTf9_TextChanged(object sender, EventArgs e)
        {
            sumHours();
            //Reset label each time new input comes in
            hrLbl9.Text = "0";
            try
            {
                int result = Int32.Parse(hrTf9.Text);
                //Handle 12th hour
                if (result == 12)
                {
                    result -= 11;
                }
                else
                {
                    result += 1;
                }

                //Set label string
                hrLbl9.Text = result.ToString();

            }
            catch (FormatException)
            {
                Console.WriteLine("Bad parse.");
            }
        }

        private void hrTf10_TextChanged(object sender, EventArgs e)
        {
            sumHours();
            //Reset label each time new input comes in
            hrLbl10.Text = "0";
            try
            {
                int result = Int32.Parse(hrTf10.Text);
                //Handle 12th hour
                if (result == 12)
                {
                    result -= 11;
                }
                else
                {
                    result += 1;
                }

                //Set label string
                hrLbl10.Text = result.ToString();

            }
            catch (FormatException)
            {
                Console.WriteLine("Bad parse.");
            }
        }

        private void hrTf11_TextChanged(object sender, EventArgs e)
        {
            sumHours();
            //Reset label each time new input comes in
            hrLbl11.Text = "0";
            try
            {
                int result = Int32.Parse(hrTf11.Text);
                //Handle 12th hour
                if (result == 12)
                {
                    result -= 11;
                }
                else
                {
                    result += 1;
                }

                //Set label string
                hrLbl11.Text = result.ToString();

            }
            catch (FormatException)
            {
                Console.WriteLine("Bad parse.");
            }
        }

        private void hrTf12_TextChanged(object sender, EventArgs e)
        {
            sumHours();
            //Reset label each time new input comes in
            hrLbl12.Text = "0";
            try
            {
                int result = Int32.Parse(hrTf12.Text);
                //Handle 12th hour
                if (result == 12)
                {
                    result -= 11;
                }
                else
                {
                    result += 1;
                }

                //Set label string
                hrLbl12.Text = result.ToString();

            }
            catch (FormatException)
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

        /// <summary>
        /// GOALLLLLL
        /// </summary>

        private void goalTf1_ValueChanged(object sender, EventArgs e)
        {
            reCalcTotals();
        }

        /// <summary>
        /// ACTUAL
        /// </summary>

        private void actTf1_ValueChanged(object sender, EventArgs e)
        {
            reCalcTotals();
        }
        
        /// <summary>
        /// SCRAP
        /// </summary>

        private void scrapTf1_ValueChanged(object sender, EventArgs e)
        {
            reCalcTotals();
        }

        /// <summary>
        /// DOWNTIME
        /// </summary>

        private void downTf1_ValueChanged(object sender, EventArgs e)
        {
            reCalcTotals();
        }

    }
}
