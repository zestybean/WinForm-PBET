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
using ClosedXML.Excel;
using System.Globalization;
using System.Runtime.InteropServices;


namespace PBET_Mainline
{
    public partial class mainForm : Form
    {
        int mainGoal = 0;
        int mainActual = 0;
        int mainVariance = 0;
        int mainHour = 8;
        int mainScrap = 0;
        int mainDowntime = 0;

        double quality = 0.0;
        double performance = 0.0;
        double availability = 0.0;
        double oee = 0.0;

        int hourVariance = 0;

        string[] machines = { "Spoven1", "Spoven2", "Spoven3", "Mainline" };
        string[] departments = { "Paintline", "Bonding", "Fronts", "Bumpers", "Rears", };
        string[] customers = { "PACCAR", "NAVISTAR", "PETERBILT","OTHER" };

        string prevColor = "";

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

        private int weekOfYearNum()
        {
            var date = DateTime.Now;
            CultureInfo myCI = new CultureInfo("en-US");
            Calendar myCal = myCI.Calendar;
            CalendarWeekRule myCWR = myCI.DateTimeFormat.CalendarWeekRule;
            DayOfWeek myFirstDOW = myCI.DateTimeFormat.FirstDayOfWeek;
            int weekOfYear = myCal.GetWeekOfYear(date, myCWR, myFirstDOW);
            return weekOfYear;
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

        /// <summary>
        /// HOURSSSSS
        /// </summary>

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            //sumHours();
        }

        private void goalTf12_Enter(object sender, EventArgs e)
        {
            foreach (Control x in this.Controls)
            {
                if (x is NumericUpDown)
                {
                    (x as NumericUpDown).Select(0, (x as NumericUpDown).Text.Length);
                }
            }
        }

        private void opTf_TextChanged(object sender, EventArgs e)
        {
            (sender as TextBox).BackColor = Color.White;
        }

        private void shiftTf_ValueChanged(object sender, EventArgs e)
        {
            (sender as NumericUpDown).BackColor = Color.White;
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

        /// <summary>
        /// SUBMIT
        /// </summary>

        private void subButton_Click(object sender, EventArgs e)
        {
            opTf.Text = opTf.Text.Trim();
            machineTf.Text = machineTf.Text.Trim();
            deptTf.Text = deptTf.Text.Trim();
            custTf.Text = custTf.Text.Trim();

            

            if(opTf.Text == string.Empty)
            {
                opTf.BackColor = Color.LightCoral;
                
            } else if (machineTf.Text == string.Empty)
            {
                machineTf.BackColor = Color.LightCoral;
               
            } else if (deptTf.Text == string.Empty)
            {
                deptTf.BackColor = Color.LightCoral;
                
            } else if (custTf.Text == string.Empty)
            {
                custTf.BackColor = Color.LightCoral;
               
            } else
            {
                if (shiftTf.Value == 0 || shiftTf.Value > 3)
                {
                    shiftTf.BackColor = Color.LightCoral;
                    return;
                }

                SubmitPopup popSubmitForm = new SubmitPopup();

                // Show testDialog as a modal dialog and determine if DialogResult = OK.
                if (popSubmitForm.ShowDialog(this) == DialogResult.OK)
                {
                    saveDataToExcel();
                    ClearTextBoxes();
                }
                else
                {
                    //Cancel
                }
                popSubmitForm.Dispose();
            }
        }

        private void ClearTextBoxes()
        {
            Action<Control.ControlCollection> func = null;

            mainGoal = 0;
            mainActual = 0;
            mainVariance = 0;
            mainHour = 8;
            mainScrap = 0;
            mainDowntime = 0;

            quality = 0.0;
            performance = 0.0;
            availability = 0.0;
            oee = 0.0;

            hourVariance = 0;

            totalHoursLbl.Text = "8";
            totalVarianceLbl.Text = "0";
            totalScrapLbl.Text = "0";
            totalDtLbl.Text = "0";
            totalGoalLbl.Text = "0";
            totalActualLbl.Text = "0";

            oeeLbl.Text = "-";
            perfLbl.Text = "-";
            avaiLbl.Text = "-";
            quaLbl.Text = "-";
            //NEEDS WORK!!!! THIS CLEARS ALL BOXES
            func = (controls) =>
            {
                foreach (Control control in controls)
                    if (control is TextBox)
                        (control as TextBox).Text = " ";
                    else
                        func(control.Controls);

                foreach (Control control in controls)
                    if (control is NumericUpDown)
                        (control as NumericUpDown).Value = 0;
                    else
                        func(control.Controls);
            };

            func(Controls);
        }

        private void saveDataToExcel()
        {
            //EXPORT TO EXCEL STARTS HERE
            DataTable dt = new DataTable();

            var date = DateTime.Now;
            string path = $@"\\hail\Shared\Pace Board\PaceboardData\Week-{weekOfYearNum() - 1}\{date.DayOfWeek}\Shift-{shiftTf.Value}\{machineTf.Text}-{date.ToString(@"MM-dd-yy")}.xlsx";

            var workbook = new XLWorkbook();

            //HRxHR
            foreach (DataGridViewColumn col in dataGridView1.Columns)
            {
                dt.Columns.Add(col.HeaderText);
            }
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                dt.Rows.Add();
                foreach (DataGridViewCell cell in row.Cells)
                {
                    dt.Rows[dt.Rows.Count - 1][cell.ColumnIndex] = cell.Value;
                }
            }

         
                workbook.Worksheets.Add(dt, "HRxHR Parts");
                workbook.SaveAs(path);
        

                //SUBMIT TO EXCEL
                //PACEBOARD
                var worksheet = workbook.Worksheets.Add("Paceboard Summary");
                //Title
                worksheet.Cell("A1").Value = "Paceboard Data";
                //Headings
                worksheet.Cell("A2").Value = "Operator";
                worksheet.Cell("B2").Value = "Shift";
                worksheet.Cell("C2").Value = "Machine";
                worksheet.Cell("D2").Value = "Department";
                worksheet.Cell("E2").Value = "Customer";
                worksheet.Cell("F2").Value = "Date";
                //Headings Data
                worksheet.Cell("A3").Value = opTf.Text;
                worksheet.Cell("B3").Value = shiftTf.Value;
                worksheet.Cell("C3").Value = machineTf.Text;
                worksheet.Cell("D3").Value = deptTf.Text;
                worksheet.Cell("E3").Value = custTf.Text;
                worksheet.Cell("F3").Value = dtPicker.Value;

                worksheet.Cell("A5").Value = mainHour;
                worksheet.Cell("B5").Value = mainGoal;
                worksheet.Cell("C5").Value = mainActual;
                worksheet.Cell("D5").Value = mainVariance;
                worksheet.Cell("E5").Value = mainScrap;
                worksheet.Cell("F5").Value = mainDowntime;

                workbook.SaveAs(path);
                
            }

        private void mainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            var window = MessageBox.Show("Close the window?","Are you sure?",MessageBoxButtons.YesNo);
            if(window == DialogResult.Yes)
            {

            }
            e.Cancel = (window == DialogResult.No);
        }

        /// <summary>
        /// CLEAR CARTS
        /// </summary>
        private void addBlankBtn_Click(object sender, EventArgs e)
        {
            //Clear the color
            prevColor = "";
            this.dataGridView1.Rows.Add(DateTime.Now.ToString("HH:mm:ss tt"), "Clear", "Clear", "Clear", "Clear", false);
        }


        /// <summary>
        /// CART AREA
        /// </summary>
        private void button1_Click(object sender, EventArgs e)
        {
            CartPopup cartDataEntry = new CartPopup(prevColor: prevColor);

            if (cartDataEntry.ShowDialog(this) == DialogResult.OK)
            {
                prevColor = cartDataEntry.partColor;
                this.dataGridView1.Rows.Add(DateTime.Now.ToString("HH:mm:ss tt"), "HZ Bumper/Fender", cartDataEntry.partNum, cartDataEntry.partQuantity, cartDataEntry.partColor, cartDataEntry.partRework);
            }
            else
            {
                //Cancel
            }
            cartDataEntry.Dispose();
            
        }

        private void hzBumberBtn_Click(object sender, EventArgs e)
        {
            CartPopup cartDataEntry = new CartPopup(prevColor: prevColor);

            if (cartDataEntry.ShowDialog(this) == DialogResult.OK)
            {
                prevColor = cartDataEntry.partColor;
                this.dataGridView1.Rows.Add(DateTime.Now.ToString("HH:mm:ss tt"), "HZ Fronts", cartDataEntry.partNum, cartDataEntry.partQuantity, cartDataEntry.partColor, cartDataEntry.partRework);
            }
            else
            {
                //Cancel
            }
            cartDataEntry.Dispose();
        }

        private void hzSkirtBtn_Click(object sender, EventArgs e)
        {
            CartPopup cartDataEntry = new CartPopup(prevColor: prevColor);

            if (cartDataEntry.ShowDialog(this) == DialogResult.OK)
            {
                prevColor = cartDataEntry.partColor;
                this.dataGridView1.Rows.Add(DateTime.Now.ToString("HH:mm:ss tt"), "HZ Rears", cartDataEntry.partNum, cartDataEntry.partQuantity, cartDataEntry.partColor, cartDataEntry.partRework);
            }
            else
            {
                //Cancel
            }
            cartDataEntry.Dispose();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            CartPopup cartDataEntry = new CartPopup(prevColor: prevColor);

            if (cartDataEntry.ShowDialog(this) == DialogResult.OK)
            {
                prevColor = cartDataEntry.partColor;
                this.dataGridView1.Rows.Add(DateTime.Now.ToString("HH:mm:ss tt"), "HZ Bumper", cartDataEntry.partNum, cartDataEntry.partQuantity, cartDataEntry.partColor, cartDataEntry.partRework);
            }
            else
            {
                //Cancel
            }
            cartDataEntry.Dispose();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            CartPopup cartDataEntry = new CartPopup(prevColor: prevColor);

            if (cartDataEntry.ShowDialog(this) == DialogResult.OK)
            {
                prevColor = cartDataEntry.partColor;
                this.dataGridView1.Rows.Add(DateTime.Now.ToString("HH:mm:ss tt"), "HZ Fenders", cartDataEntry.partNum, cartDataEntry.partQuantity, cartDataEntry.partColor, cartDataEntry.partRework);
            }
            else
            {
                //Cancel
            }
            cartDataEntry.Dispose();
        }


        /// <summary>
        /// ROBOT CARTS
        /// </summary>
        private void rbtHzBumFenBtn_Click(object sender, EventArgs e)
        {
            CartPopup cartDataEntry = new CartPopup(prevColor: prevColor);

            if (cartDataEntry.ShowDialog(this) == DialogResult.OK)
            {
                prevColor = cartDataEntry.partColor;
                this.dataGridView1.Rows.Add(DateTime.Now.ToString("HH:mm:ss tt"), "HZ Bumper/Fender ROBOT", cartDataEntry.partNum, "10", cartDataEntry.partColor, cartDataEntry.partRework);
            }
            else
            {
                //Cancel
            }
            cartDataEntry.Dispose();
        }

        private void rbtHzBumperBtn_Click(object sender, EventArgs e)
        {
            CartPopup cartDataEntry = new CartPopup(prevColor: prevColor);

            if (cartDataEntry.ShowDialog(this) == DialogResult.OK)
            {
                prevColor = cartDataEntry.partColor;
                this.dataGridView1.Rows.Add(DateTime.Now.ToString("HH:mm:ss tt"), "HZ Bumper ROBOT", cartDataEntry.partNum, "6", cartDataEntry.partColor, cartDataEntry.partRework);
            }
            else
            {
                //Cancel
            }
            cartDataEntry.Dispose();
        }

        private void rbtHzSkirtBtn_Click(object sender, EventArgs e)
        {
            CartPopup cartDataEntry = new CartPopup(prevColor: prevColor);

            if (cartDataEntry.ShowDialog(this) == DialogResult.OK)
            {
                prevColor = cartDataEntry.partColor;
                this.dataGridView1.Rows.Add(DateTime.Now.ToString("HH:mm:ss tt"), "HZ Skirt ROBOT", cartDataEntry.partNum, "5", cartDataEntry.partColor, cartDataEntry.partRework);
            }
            else
            {
                //Cancel
            }
            cartDataEntry.Dispose();
        }
        
        private void rbtMluFairingsBtn_Click(object sender, EventArgs e)
        {
            CartPopup cartDataEntry = new CartPopup(prevColor: prevColor);

            if (cartDataEntry.ShowDialog(this) == DialogResult.OK)
            {
                prevColor = cartDataEntry.partColor;
                this.dataGridView1.Rows.Add(DateTime.Now.ToString("HH:mm:ss tt"), "MLU Fairings ROBOT", cartDataEntry.partNum, "4", cartDataEntry.partColor, cartDataEntry.partRework);
            }
            else
            {
                //Cancel
            }
            cartDataEntry.Dispose();
        }

        private void rbtMluBumpersBtn_Click(object sender, EventArgs e)
        {
            CartPopup cartDataEntry = new CartPopup(prevColor: prevColor);

            if (cartDataEntry.ShowDialog(this) == DialogResult.OK)
            {
                prevColor = cartDataEntry.partColor;
                this.dataGridView1.Rows.Add(DateTime.Now.ToString("HH:mm:ss tt"), "MLU Bumpers ROBOT", cartDataEntry.partNum, "2", cartDataEntry.partColor, cartDataEntry.partRework);
            }
            else
            {
                //Cancel
            }
            cartDataEntry.Dispose();
        }

        /// <summary>
        /// MLU MANUAL
        /// </summary>

        private void button4_Click(object sender, EventArgs e)
        {
            CartPopup cartDataEntry = new CartPopup(prevColor: prevColor);

            if (cartDataEntry.ShowDialog(this) == DialogResult.OK)
            {
                prevColor = cartDataEntry.partColor;
                this.dataGridView1.Rows.Add(DateTime.Now.ToString("HH:mm:ss tt"), "MLU Fwd Fairings", cartDataEntry.partNum, cartDataEntry.partQuantity, cartDataEntry.partColor, cartDataEntry.partRework);
            }
            else
            {
                //Cancel
            }
            cartDataEntry.Dispose();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            CartPopup cartDataEntry = new CartPopup(prevColor: prevColor);

            if (cartDataEntry.ShowDialog(this) == DialogResult.OK)
            {
                prevColor = cartDataEntry.partColor;
                this.dataGridView1.Rows.Add(DateTime.Now.ToString("HH:mm:ss tt"), "MLU Ctr Fairings", cartDataEntry.partNum, cartDataEntry.partQuantity, cartDataEntry.partColor, cartDataEntry.partRework);
            }
            else
            {
                //Cancel
            }
            cartDataEntry.Dispose();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            CartPopup cartDataEntry = new CartPopup(prevColor: prevColor);

            if (cartDataEntry.ShowDialog(this) == DialogResult.OK)
            {
                prevColor = cartDataEntry.partColor;
                this.dataGridView1.Rows.Add(DateTime.Now.ToString("HH:mm:ss tt"), "MLU Bumper", cartDataEntry.partNum, cartDataEntry.partQuantity, cartDataEntry.partColor, cartDataEntry.partRework);
            }
            else
            {
                //Cancel
            }
            cartDataEntry.Dispose();
        }
    }
}
