namespace DentApp
{
    partial class AppointmentsView
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            appointmentsGrid = new DataGridView();
            visitNumber = new DataGridViewTextBoxColumn();
            date = new DataGridViewTextBoxColumn();
            patient = new DataGridViewTextBoxColumn();
            pesel = new DataGridViewTextBoxColumn();
            status = new DataGridViewComboBoxColumn();
            cost = new DataGridViewTextBoxColumn();
            saveButton = new Button();
            addVisitButton = new Button();
            patientsListBox = new ListBox();
            editButton = new Button();
            openVisitButton = new Button();
            ((System.ComponentModel.ISupportInitialize)appointmentsGrid).BeginInit();
            SuspendLayout();
            // 
            // appointmentsGrid
            // 
            appointmentsGrid.AllowUserToAddRows = false;
            appointmentsGrid.AllowUserToDeleteRows = false;
            appointmentsGrid.AllowUserToOrderColumns = true;
            appointmentsGrid.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            appointmentsGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            appointmentsGrid.BackgroundColor = SystemColors.Control;
            appointmentsGrid.BorderStyle = BorderStyle.None;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = SystemColors.Control;
            dataGridViewCellStyle1.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle1.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.False;
            appointmentsGrid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            appointmentsGrid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            appointmentsGrid.Columns.AddRange(new DataGridViewColumn[] { visitNumber, date, patient, pesel, status, cost });
            appointmentsGrid.Location = new Point(18, 99);
            appointmentsGrid.Name = "appointmentsGrid";
            appointmentsGrid.ReadOnly = true;
            appointmentsGrid.RowHeadersWidth = 51;
            appointmentsGrid.Size = new Size(879, 386);
            appointmentsGrid.TabIndex = 0;
            appointmentsGrid.CellBeginEdit += appointmentsGrid_CellBeginEdit;
            appointmentsGrid.CellClick += appointmentsGrid_CellClick;
            appointmentsGrid.CellEndEdit += appointmentsGrid_CellEndEdit;
            appointmentsGrid.CellValidating += appointmentsGrid_CellValidating;
            appointmentsGrid.EditingControlShowing += appointmentsGrid_EditingControlShowing;
            appointmentsGrid.RowsAdded += appointmentsGrid_RowsAdded;
            appointmentsGrid.RowValidating += appointmentsGrid_RowValidating;
            appointmentsGrid.UserAddedRow += appointmentsGrid_UserAddedRow;
            appointmentsGrid.CellDoubleClick += appointmentsGrid_CellDoubleClick;
            // 
            // visitNumber
            // 
            visitNumber.AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
            visitNumber.HeaderText = "Numer wizyty";
            visitNumber.MinimumWidth = 6;
            visitNumber.Name = "visitNumber";
            visitNumber.ReadOnly = true;
            visitNumber.Width = 128;
            // 
            // date
            // 
            dataGridViewCellStyle2.Format = "G";
            dataGridViewCellStyle2.NullValue = null;
            date.DefaultCellStyle = dataGridViewCellStyle2;
            date.HeaderText = "Data";
            date.MinimumWidth = 6;
            date.Name = "date";
            date.ReadOnly = true;
            // 
            // patient
            // 
            patient.HeaderText = "Pacjent";
            patient.MinimumWidth = 6;
            patient.Name = "patient";
            patient.ReadOnly = true;
            // 
            // pesel
            // 
            pesel.HeaderText = "Pesel";
            pesel.MinimumWidth = 6;
            pesel.Name = "pesel";
            pesel.ReadOnly = true;
            // 
            // status
            // 
            status.HeaderText = "Status";
            status.Items.AddRange(new object[] { "Oczekująca", "Aktualna", "Odwołana", "Zrealizowana", "Nieobecność pacjenta" });
            status.MinimumWidth = 6;
            status.Name = "status";
            status.ReadOnly = true;
            status.Resizable = DataGridViewTriState.True;
            status.SortMode = DataGridViewColumnSortMode.Automatic;
            // 
            // cost
            // 
            cost.HeaderText = "Koszt";
            cost.MinimumWidth = 6;
            cost.Name = "cost";
            cost.ReadOnly = true;
            // 
            // saveButton
            // 
            saveButton.Location = new Point(218, 13);
            saveButton.Name = "saveButton";
            saveButton.Size = new Size(94, 54);
            saveButton.TabIndex = 1;
            saveButton.Text = "Zapisz zmiany";
            saveButton.UseVisualStyleBackColor = true;
            saveButton.Click += saveButton_Click;
            // 
            // addVisitButton
            // 
            addVisitButton.Location = new Point(18, 13);
            addVisitButton.Name = "addVisitButton";
            addVisitButton.Size = new Size(94, 54);
            addVisitButton.TabIndex = 2;
            addVisitButton.Text = "Dodaj\r\nwizytę";
            addVisitButton.UseVisualStyleBackColor = true;
            addVisitButton.Click += addVisitButton_Click;
            // 
            // patientsListBox
            // 
            patientsListBox.BackColor = SystemColors.AppWorkspace;
            patientsListBox.FormattingEnabled = true;
            patientsListBox.Location = new Point(438, 151);
            patientsListBox.Name = "patientsListBox";
            patientsListBox.Size = new Size(150, 104);
            patientsListBox.TabIndex = 3;
            patientsListBox.Visible = false;
            patientsListBox.MouseDown += patientsListBox_MouseDown;
            // 
            // editButton
            // 
            editButton.Location = new Point(118, 13);
            editButton.Name = "editButton";
            editButton.Size = new Size(94, 54);
            editButton.TabIndex = 4;
            editButton.Text = "Edytuj";
            editButton.UseVisualStyleBackColor = true;
            editButton.Click += editButton_Click;
            // 
            // openVisitButton
            // 
            openVisitButton.Anchor = AnchorStyles.Top;
            openVisitButton.Location = new Point(438, 13);
            openVisitButton.Name = "openVisitButton";
            openVisitButton.Size = new Size(94, 54);
            openVisitButton.TabIndex = 5;
            openVisitButton.Text = "Przejdź do wizyty";
            openVisitButton.UseVisualStyleBackColor = true;
            openVisitButton.Click += openVisitButton_Click;
            // 
            // AppointmentsView
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(openVisitButton);
            Controls.Add(editButton);
            Controls.Add(patientsListBox);
            Controls.Add(addVisitButton);
            Controls.Add(saveButton);
            Controls.Add(appointmentsGrid);
            Name = "AppointmentsView";
            Size = new Size(920, 500);
            Load += AppointmentsView_Load;
            ((System.ComponentModel.ISupportInitialize)appointmentsGrid).EndInit();
            ResumeLayout(false);
        }

        #endregion

        public DataGridView appointmentsGrid;
        private Button saveButton;
        private Button addVisitButton;
        private ListBox patientsListBox;
        private Button editButton;
        private DataGridViewTextBoxColumn visitNumber;
        private DataGridViewTextBoxColumn date;
        private DataGridViewTextBoxColumn patient;
        private DataGridViewTextBoxColumn pesel;
        private DataGridViewComboBoxColumn status;
        private DataGridViewTextBoxColumn cost;
        private Button openVisitButton;
    }
}
