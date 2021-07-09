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
    public partial class PBETMainline : Form
    {
        SqlConnection connection;
        string connectionString;

        public PBETMainline()
        {
            InitializeComponent();
            connectionString = ConfigurationManager.ConnectionStrings["PBET_Mainline.Properties.Settings.PBETDBConnectionString"].ConnectionString;
        }

        private void PBETMainline_Load(object sender, EventArgs e)
        {
            
        }

        /*
        private void PopulateMainlineHeader()
        {
            //Using will close the connection for you automatically
            using (connection = new SqlConnection(connectionString))
            using (SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM MainlineHeader", connection)) 
            {
                DataTable mainlineTable = new DataTable();
                adapter.Fill(mainlineTable);

                lstMainlineHeader.DisplayMember = "Operator";
                lstMainlineHeader.ValueMember = "Id";
                lstMainlineHeader.DataSource = mainlineTable;
            }
           
        }*/
    }
}
