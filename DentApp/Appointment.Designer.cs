namespace DentApp
{
    partial class Appointment
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
            layout = new TableLayoutPanel();
            topPanel = new Panel();
            button1 = new Button();
            topPanel.SuspendLayout();
            SuspendLayout();
            // 
            // layout
            // 
            layout.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            layout.ColumnCount = 3;
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            layout.Location = new Point(12, 62);
            layout.Name = "layout";
            layout.RowCount = 1;
            layout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            layout.Size = new Size(776, 376);
            layout.TabIndex = 0;
            // 
            // topPanel
            // 
            topPanel.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            topPanel.Controls.Add(button1);
            topPanel.Location = new Point(12, 5);
            topPanel.Name = "topPanel";
            topPanel.Size = new Size(776, 51);
            topPanel.TabIndex = 1;
            // 
            // button1
            // 
            button1.Location = new Point(682, 0);
            button1.Name = "button1";
            button1.Size = new Size(94, 51);
            button1.TabIndex = 0;
            button1.Text = "Zapisz";
            button1.UseVisualStyleBackColor = true;
            // 
            // Appointment
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(topPanel);
            Controls.Add(layout);
            Name = "Appointment";
            Text = "Appointment";
            topPanel.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel layout;
        private Panel topPanel;
        private Button button1;
    }
}