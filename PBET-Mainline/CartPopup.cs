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
    public partial class CartPopup : Form
    {
        public string partNum = "";
        public string partLotNum = "";
        public string partQuantity = "";
        public string partColor = "";

        public CartPopup()
        {
            InitializeComponent();
        }

        private void CartPopup_Load(object sender, EventArgs e)
        {

        }

        private void confBtn_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void cancelBtn_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void txtPartNum_TextChanged(object sender, EventArgs e)
        {
            partNum = txtPartNum.Text;
            
        }

        private void txtLotNum_TextChanged(object sender, EventArgs e)
        {
            partLotNum = txtLotNum.Text;
           
        }

        private void txtColor_TextChanged(object sender, EventArgs e)
        {
            partColor = txtColor.Text;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            partQuantity = txtQuantity.Text;
        }
    }
}
