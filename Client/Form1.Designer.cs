namespace Client
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
            this.comboBoxHourSleep = new System.Windows.Forms.ComboBox();
            this.comboBoxMinSleep = new System.Windows.Forms.ComboBox();
            this.buttonSend = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // comboBoxHourSleep
            // 
            this.comboBoxHourSleep.FormattingEnabled = true;
            this.comboBoxHourSleep.Items.AddRange(new object[] {
            "00",
            "01",
            "02",
            "03",
            "04",
            "05",
            "06",
            "07",
            "08",
            "09",
            "10",
            "11",
            "12",
            "13",
            "14",
            "15",
            "16",
            "17",
            "18",
            "19",
            "20",
            "21",
            "21",
            "23"});
            this.comboBoxHourSleep.Location = new System.Drawing.Point(129, 84);
            this.comboBoxHourSleep.Name = "comboBoxHourSleep";
            this.comboBoxHourSleep.Size = new System.Drawing.Size(83, 23);
            this.comboBoxHourSleep.TabIndex = 1;
            this.comboBoxHourSleep.SelectedIndexChanged += new System.EventHandler(this.comboBoxHourSleep_SelectedIndexChanged);
            // 
            // comboBoxMinSleep
            // 
            this.comboBoxMinSleep.FormattingEnabled = true;
            this.comboBoxMinSleep.Items.AddRange(new object[] {
            "00",
            "05",
            "10",
            "15",
            "20",
            "25",
            "30",
            "35",
            "40",
            "45",
            "50",
            "55"});
            this.comboBoxMinSleep.Location = new System.Drawing.Point(226, 84);
            this.comboBoxMinSleep.Name = "comboBoxMinSleep";
            this.comboBoxMinSleep.Size = new System.Drawing.Size(83, 23);
            this.comboBoxMinSleep.TabIndex = 1;
            // 
            // buttonSend
            // 
            this.buttonSend.Location = new System.Drawing.Point(129, 113);
            this.buttonSend.Name = "buttonSend";
            this.buttonSend.Size = new System.Drawing.Size(180, 35);
            this.buttonSend.TabIndex = 2;
            this.buttonSend.Text = "Enter";
            this.buttonSend.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.buttonSend);
            this.Controls.Add(this.comboBoxMinSleep);
            this.Controls.Add(this.comboBoxHourSleep);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBoxHourSleep;
        private System.Windows.Forms.ComboBox comboBoxMinSleep;
        private System.Windows.Forms.Button buttonSend;
    }
}

