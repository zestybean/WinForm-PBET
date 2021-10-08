
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
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.cancelBtn = new System.Windows.Forms.Button();
            this.confBtn = new System.Windows.Forms.Button();
            this.txtPartNum = new System.Windows.Forms.TextBox();
            this.subLbl1 = new System.Windows.Forms.Label();
            this.txtColor = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtQuantity = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.reworkChk = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox1.Image = global::PBET_Mainline.Properties.Resources.logo;
            this.pictureBox1.Location = new System.Drawing.Point(194, 38);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(225, 87);
            this.pictureBox1.TabIndex = 8;
            this.pictureBox1.TabStop = false;
            // 
            // cancelBtn
            // 
            this.cancelBtn.BackColor = System.Drawing.SystemColors.Highlight;
            this.cancelBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.cancelBtn.ForeColor = System.Drawing.Color.White;
            this.cancelBtn.Location = new System.Drawing.Point(322, 314);
            this.cancelBtn.Name = "cancelBtn";
            this.cancelBtn.Size = new System.Drawing.Size(97, 42);
            this.cancelBtn.TabIndex = 89;
            this.cancelBtn.Text = "Cancel";
            this.cancelBtn.UseVisualStyleBackColor = false;
            this.cancelBtn.Click += new System.EventHandler(this.cancelBtn_Click);
            // 
            // confBtn
            // 
            this.confBtn.BackColor = System.Drawing.SystemColors.Highlight;
            this.confBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.confBtn.ForeColor = System.Drawing.Color.White;
            this.confBtn.Location = new System.Drawing.Point(194, 314);
            this.confBtn.Name = "confBtn";
            this.confBtn.Size = new System.Drawing.Size(97, 42);
            this.confBtn.TabIndex = 88;
            this.confBtn.Text = "Confirm";
            this.confBtn.UseVisualStyleBackColor = false;
            this.confBtn.Click += new System.EventHandler(this.confBtn_Click);
            // 
            // txtPartNum
            // 
            this.txtPartNum.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            this.txtPartNum.Location = new System.Drawing.Point(247, 149);
            this.txtPartNum.Name = "txtPartNum";
            this.txtPartNum.Size = new System.Drawing.Size(202, 29);
            this.txtPartNum.TabIndex = 1;
            this.txtPartNum.TextChanged += new System.EventHandler(this.txtPartNum_TextChanged);
            // 
            // subLbl1
            // 
            this.subLbl1.AutoSize = true;
            this.subLbl1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold);
            this.subLbl1.Location = new System.Drawing.Point(118, 149);
            this.subLbl1.Name = "subLbl1";
            this.subLbl1.Size = new System.Drawing.Size(139, 24);
            this.subLbl1.TabIndex = 9;
            this.subLbl1.Text = "Part Number: ";
            // 
            // txtColor
            // 
            this.txtColor.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            this.txtColor.Location = new System.Drawing.Point(247, 219);
            this.txtColor.Name = "txtColor";
            this.txtColor.Size = new System.Drawing.Size(202, 29);
            this.txtColor.TabIndex = 3;
            this.txtColor.TextChanged += new System.EventHandler(this.txtColor_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold);
            this.label2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label2.Location = new System.Drawing.Point(185, 219);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 24);
            this.label2.TabIndex = 15;
            this.label2.Text = "Color: ";
            // 
            // txtQuantity
            // 
            this.txtQuantity.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            this.txtQuantity.Location = new System.Drawing.Point(247, 184);
            this.txtQuantity.Name = "txtQuantity";
            this.txtQuantity.Size = new System.Drawing.Size(202, 29);
            this.txtQuantity.TabIndex = 2;
            this.txtQuantity.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold);
            this.label3.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label3.Location = new System.Drawing.Point(159, 184);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(98, 24);
            this.label3.TabIndex = 18;
            this.label3.Text = "Quantity: ";
            // 
            // reworkChk
            // 
            this.reworkChk.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.reworkChk.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.reworkChk.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.reworkChk.Location = new System.Drawing.Point(247, 268);
            this.reworkChk.Margin = new System.Windows.Forms.Padding(1);
            this.reworkChk.Name = "reworkChk";
            this.reworkChk.Padding = new System.Windows.Forms.Padding(5, 2, 5, 0);
            this.reworkChk.Size = new System.Drawing.Size(113, 27);
            this.reworkChk.TabIndex = 4;
            this.reworkChk.Text = "Rework:";
            this.reworkChk.UseVisualStyleBackColor = false;
            this.reworkChk.CheckedChanged += new System.EventHandler(this.reworkChk_CheckedChanged);
            // 
            // CartPopup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(624, 381);
            this.Controls.Add(this.reworkChk);
            this.Controls.Add(this.txtQuantity);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtColor);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cancelBtn);
            this.Controls.Add(this.confBtn);
            this.Controls.Add(this.txtPartNum);
            this.Controls.Add(this.subLbl1);
            this.Controls.Add(this.pictureBox1);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.Name = "CartPopup";
            this.Text = "Cart Data Entry";
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
        private System.Windows.Forms.TextBox txtColor;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtQuantity;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox reworkChk;
    }
}