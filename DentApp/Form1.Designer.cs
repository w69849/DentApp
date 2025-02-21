namespace DentApp
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
            patientsListButton = new Button();
            navigationPanel = new Panel();
            appointmentsButton = new Button();
            panel1 = new Panel();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // patientsListButton
            // 
            patientsListButton.Location = new Point(0, 0);
            patientsListButton.Name = "patientsListButton";
            patientsListButton.Size = new Size(151, 29);
            patientsListButton.TabIndex = 0;
            patientsListButton.Text = "Lista pacjentów";
            patientsListButton.UseVisualStyleBackColor = true;
            patientsListButton.Click += patientsListButton_Click;
            // 
            // navigationPanel
            // 
            navigationPanel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            navigationPanel.Location = new Point(0, 65);
            navigationPanel.Name = "navigationPanel";
            navigationPanel.Size = new Size(800, 385);
            navigationPanel.TabIndex = 3;
            // 
            // appointmentsButton
            // 
            appointmentsButton.Location = new Point(157, 0);
            appointmentsButton.Name = "appointmentsButton";
            appointmentsButton.Size = new Size(102, 29);
            appointmentsButton.TabIndex = 1;
            appointmentsButton.Text = "Wizyty";
            appointmentsButton.UseVisualStyleBackColor = true;
            appointmentsButton.Click += appointmentsButton_Click;
            // 
            // panel1
            // 
            panel1.Controls.Add(appointmentsButton);
            panel1.Controls.Add(patientsListButton);
            panel1.Location = new Point(12, 12);
            panel1.Name = "panel1";
            panel1.Size = new Size(261, 34);
            panel1.TabIndex = 4;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(panel1);
            Controls.Add(navigationPanel);
            Name = "Form1";
            Text = "DentApp";
            WindowState = FormWindowState.Maximized;
            FormClosing += Form1_FormClosing;
            Load += Form1_Load;
            panel1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Button patientsListButton;
        private Panel navigationPanel;
        private Button appointmentsButton;
        private Panel panel1;
    }
}
