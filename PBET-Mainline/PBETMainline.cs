﻿using System;
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
        string[] customers = { "PACCAR", "NAVISTAR", "PETERBILT","OTHER" };

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

        private void sumHours()
        {
            mainHour = 0;
            foreach (Control x in this.Controls)
            {
                if (x is NumericUpDown)
                {
                    if ((x as NumericUpDown).Value != 0 || (x as NumericUpDown).Text != "")
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

        /// <summary>
        /// HOURSSSSS
        /// </summary>

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            sumHours();
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

            //NEEDS WORK!!!!
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
            var date = DateTime.Now;
            string path = $@"\\hail\Shared\Pace Board\PaceboardData\Week-{weekOfYearNum() - 1}\{machineTf.Text}-{date.ToString(@"MM-dd-yy")}.xlsx";

            //SUBMIT TO EXCEL
            var workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add("Summary Report");
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

        
    }
}
