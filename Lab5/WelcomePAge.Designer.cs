namespace Lab5
{
    partial class WelcomePage
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
            this.notifyLabel = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // notifyLabel
            // 
            this.notifyLabel.AutoSize = true;
            this.notifyLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.notifyLabel.Location = new System.Drawing.Point(15, 31);
            this.notifyLabel.Name = "notifyLabel";
            this.notifyLabel.Size = new System.Drawing.Size(674, 39);
            this.notifyLabel.TabIndex = 0;
            this.notifyLabel.Text = "Welcome you have signed in successfully :)";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.notifyLabel);
            this.panel1.Location = new System.Drawing.Point(191, 56);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(698, 241);
            this.panel1.TabIndex = 1;
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(227, 96);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(219, 107);
            this.button1.TabIndex = 1;
            this.button1.Text = "Sign Out";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // WelcomePage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1094, 506);
            this.Controls.Add(this.panel1);
            this.MaximizeBox = false;
            this.Name = "WelcomePage";
            this.Text = "Welcome Page";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.WelcomePage_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label notifyLabel;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button button1;
    }
}