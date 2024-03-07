namespace VirtualControllerGDI
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.pbTank = new System.Windows.Forms.PictureBox();
            this.VCL = new VirtualControllerGDI.VirtualController2();
            ((System.ComponentModel.ISupportInitialize)(this.pbTank)).BeginInit();
            this.SuspendLayout();
            // 
            // pbTank
            // 
            this.pbTank.BackColor = System.Drawing.Color.Transparent;
            this.pbTank.Image = ((System.Drawing.Image)(resources.GetObject("pbTank.Image")));
            this.pbTank.Location = new System.Drawing.Point(565, 299);
            this.pbTank.Name = "pbTank";
            this.pbTank.Size = new System.Drawing.Size(65, 71);
            this.pbTank.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbTank.TabIndex = 1;
            this.pbTank.TabStop = false;
            // 
            // VCL
            // 
            this.VCL.Location = new System.Drawing.Point(0, 327);
            this.VCL.Name = "VCL";
            this.VCL.Size = new System.Drawing.Size(285, 285);
            this.VCL.TabIndex = 2;
            this.VCL.FG_Pressed += new VirtualControllerGDI.VirtualController2.PressedDelegate(this.virtualController1_FG_Pressed);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1039, 612);
            this.Controls.Add(this.pbTank);
            this.Controls.Add(this.VCL);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "GTAVI";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pbTank)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pbTank;
        private VirtualController2 VCL;
    }
}

