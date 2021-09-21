
namespace PBET_Mainline
{
    partial class CartPopup
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CartPopup));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.cancelBtn = new System.Windows.Forms.Button();
            this.confBtn = new System.Windows.Forms.Button();
            this.txtPartNum = new System.Windows.Forms.TextBox();
            this.subLbl1 = new System.Windows.Forms.Label();
            this.txtLotNum = new System.Windows.Forms.TextBox();
            this.txtColor = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtQuantity = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox1.Image = global::PBET_Mainline.Properties.Resources.logo;
            resources.ApplyResources(this.pictureBox1, "pictureBox1");
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.TabStop = false;
            this.pictureBox1.UseWaitCursor = true;
            // 
            // cancelBtn
            // 
            this.cancelBtn.BackColor = System.Drawing.SystemColors.Highlight;
            resources.ApplyResources(this.cancelBtn, "cancelBtn");
            this.cancelBtn.ForeColor = System.Drawing.Color.White;
            this.cancelBtn.Name = "cancelBtn";
            this.cancelBtn.UseVisualStyleBackColor = false;
            this.cancelBtn.UseWaitCursor = true;
            this.cancelBtn.Click += new System.EventHandler(this.cancelBtn_Click);
            // 
            // confBtn
            // 
            this.confBtn.BackColor = System.Drawing.SystemColors.Highlight;
            resources.ApplyResources(this.confBtn, "confBtn");
            this.confBtn.ForeColor = System.Drawing.Color.White;
            this.confBtn.Name = "confBtn";
            this.confBtn.UseVisualStyleBackColor = false;
            this.confBtn.UseWaitCursor = true;
            this.confBtn.Click += new System.EventHandler(this.confBtn_Click);
            // 
            // txtPartNum
            // 
            resources.ApplyResources(this.txtPartNum, "txtPartNum");
            this.txtPartNum.Name = "txtPartNum";
            this.txtPartNum.UseWaitCursor = true;
            this.txtPartNum.TextChanged += new System.EventHandler(this.txtPartNum_TextChanged);
            // 
            // subLbl1
            // 
            resources.ApplyResources(this.subLbl1, "subLbl1");
            this.subLbl1.Name = "subLbl1";
            this.subLbl1.UseWaitCursor = true;
            // 
            // txtLotNum
            // 
            resources.ApplyResources(this.txtLotNum, "txtLotNum");
            this.txtLotNum.Name = "txtLotNum";
            this.txtLotNum.UseWaitCursor = true;
            this.txtLotNum.TextChanged += new System.EventHandler(this.txtLotNum_TextChanged);
            // 
            // txtColor
            // 
            resources.ApplyResources(this.txtColor, "txtColor");
            this.txtColor.Name = "txtColor";
            this.txtColor.UseWaitCursor = true;
            this.txtColor.TextChanged += new System.EventHandler(this.txtColor_TextChanged);
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            this.label2.UseWaitCursor = true;
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            this.label1.UseWaitCursor = true;
            // 
            // txtQuantity
            // 
            resources.ApplyResources(this.txtQuantity, "txtQuantity");
            this.txtQuantity.Name = "txtQuantity";
            this.txtQuantity.UseWaitCursor = true;
            this.txtQuantity.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            this.label3.UseWaitCursor = true;
            // 
            // CartPopup
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.txtQuantity);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtColor);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtLotNum);
            this.Controls.Add(this.cancelBtn);
            this.Controls.Add(this.confBtn);
            this.Controls.Add(this.txtPartNum);
            this.Controls.Add(this.subLbl1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.label1);
            this.Name = "CartPopup";
            this.UseWaitCursor = true;
            this.Load += new System.EventHandler(this.CartPopup_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button cancelBtn;
        private System.Windows.Forms.Button confBtn;
        private System.Windows.Forms.TextBox txtPartNum;
        private System.Windows.Forms.Label subLbl1;
        private System.Windows.Forms.TextBox txtLotNum;
        private System.Windows.Forms.TextBox txtColor;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtQuantity;
        private System.Windows.Forms.Label label3;
    }
}