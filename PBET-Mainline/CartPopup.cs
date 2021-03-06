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
        public string partQuantity = "";
        public string partColor = "";
        public bool partRework = false;

        public CartPopup(string prevColor)
        {
            InitializeComponent();
            partRework = reworkChk.Checked;
            txtColor.Text = prevColor;
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

        private void txtColor_TextChanged(object sender, EventArgs e)
        {
            partColor = txtColor.Text;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            partQuantity = txtQuantity.Text;
        }

        private void reworkChk_CheckedChanged(object sender, EventArgs e)
        {
            partRework = reworkChk.Checked;
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }
    }
}
