namespace WindowsFormsApp1
{
    partial class Startproject
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
            this.components = new System.ComponentModel.Container();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.Star = new Bunifu.Framework.UI.BunifuProgressBar();
            this.prec = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Century Gothic", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.label1.Location = new System.Drawing.Point(62, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(513, 42);
            this.label1.TabIndex = 0;
            this.label1.Text = "Magazine information system";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Century Gothic", 15.25F);
            this.label2.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.label2.Location = new System.Drawing.Point(233, 229);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(218, 24);
            this.label2.TabIndex = 1;
            this.label2.Text = "Made By A.A.A.L.Y.M";
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // Star
            // 
            this.Star.BackColor = System.Drawing.Color.Crimson;
            this.Star.BorderRadius = 5;
            this.Star.ForeColor = System.Drawing.Color.Black;
            this.Star.Location = new System.Drawing.Point(12, 267);
            this.Star.MaximumValue = 100;
            this.Star.Name = "Star";
            this.Star.ProgressColor = System.Drawing.Color.White;
            this.Star.Size = new System.Drawing.Size(632, 10);
            this.Star.TabIndex = 2;
            this.Star.Value = 0;
            // 
            // prec
            // 
            this.prec.AutoSize = true;
            this.prec.Font = new System.Drawing.Font("Century Gothic", 20.25F);
            this.prec.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.prec.Location = new System.Drawing.Point(292, 121);
            this.prec.Name = "prec";
            this.prec.Size = new System.Drawing.Size(36, 33);
            this.prec.TabIndex = 4;
            this.prec.Text = "%";
            // 
            // Startproject
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Crimson;
            this.ClientSize = new System.Drawing.Size(656, 279);
            this.Controls.Add(this.prec);
            this.Controls.Add(this.Star);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Startproject";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Startproject_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Timer timer1;
        private Bunifu.Framework.UI.BunifuProgressBar Star;
        private System.Windows.Forms.Label prec;
        // private Bunifu.UI.WinForms.BunifuButton.BunifuButton bunifuButton1;
    }
}

