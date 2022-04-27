
namespace OpenGlForm
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.TrackPanel = new System.Windows.Forms.Panel();
            this.TrackBarR = new System.Windows.Forms.TrackBar();
            this.TrackBarPhi = new System.Windows.Forms.TrackBar();
            this.TrackBarPsi = new System.Windows.Forms.TrackBar();
            this.TrackPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TrackBarR)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TrackBarPhi)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TrackBarPsi)).BeginInit();
            this.SuspendLayout();
            // 
            // TrackPanel
            // 
            this.TrackPanel.Controls.Add(this.TrackBarR);
            this.TrackPanel.Controls.Add(this.TrackBarPhi);
            this.TrackPanel.Controls.Add(this.TrackBarPsi);
            this.TrackPanel.Dock = System.Windows.Forms.DockStyle.Right;
            this.TrackPanel.Location = new System.Drawing.Point(600, 0);
            this.TrackPanel.Name = "TrackPanel";
            this.TrackPanel.Size = new System.Drawing.Size(200, 450);
            this.TrackPanel.TabIndex = 0;
            // 
            // TrackBarR
            // 
            this.TrackBarR.Dock = System.Windows.Forms.DockStyle.Top;
            this.TrackBarR.Location = new System.Drawing.Point(0, 90);
            this.TrackBarR.Maximum = 100;
            this.TrackBarR.Name = "TrackBarR";
            this.TrackBarR.Size = new System.Drawing.Size(200, 45);
            this.TrackBarR.TabIndex = 2;
            this.TrackBarR.Value = 10;
            this.TrackBarR.Scroll += new System.EventHandler(this.TrackBarR_Scroll);
            // 
            // TrackBarPhi
            // 
            this.TrackBarPhi.Dock = System.Windows.Forms.DockStyle.Top;
            this.TrackBarPhi.Location = new System.Drawing.Point(0, 45);
            this.TrackBarPhi.Maximum = 90;
            this.TrackBarPhi.Minimum = -90;
            this.TrackBarPhi.Name = "TrackBarPhi";
            this.TrackBarPhi.Size = new System.Drawing.Size(200, 45);
            this.TrackBarPhi.TabIndex = 1;
            this.TrackBarPhi.Scroll += new System.EventHandler(this.TrackBarPhi_Scroll);
            // 
            // TrackBarPsi
            // 
            this.TrackBarPsi.Dock = System.Windows.Forms.DockStyle.Top;
            this.TrackBarPsi.Location = new System.Drawing.Point(0, 0);
            this.TrackBarPsi.Maximum = 360;
            this.TrackBarPsi.Name = "TrackBarPsi";
            this.TrackBarPsi.Size = new System.Drawing.Size(200, 45);
            this.TrackBarPsi.TabIndex = 0;
            this.TrackBarPsi.Scroll += new System.EventHandler(this.TrackBarPsi_Scroll);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.TrackPanel);
            this.Name = "Form1";
            this.Text = "ц";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Form1_Paint);
            this.Resize += new System.EventHandler(this.Form1_Resize);
            this.TrackPanel.ResumeLayout(false);
            this.TrackPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TrackBarR)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TrackBarPhi)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TrackBarPsi)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel TrackPanel;
        private System.Windows.Forms.TrackBar TrackBarR;
        private System.Windows.Forms.TrackBar TrackBarPhi;
        private System.Windows.Forms.TrackBar TrackBarPsi;
    }
}

