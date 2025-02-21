namespace DentApp
{
    partial class PatientsView
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
            patientsGrid = new DataGridView();
            id = new DataGridViewTextBoxColumn();
            name = new DataGridViewTextBoxColumn();
            surname = new DataGridViewTextBoxColumn();
            pesel = new DataGridViewTextBoxColumn();
            addPatientButton = new Button();
            saveButton = new Button();
            editButton = new Button();
            ((System.ComponentModel.ISupportInitialize)patientsGrid).BeginInit();
            SuspendLayout();
            // 
            // patientsGrid
            // 
            patientsGrid.AllowUserToAddRows = false;
            patientsGrid.AllowUserToDeleteRows = false;
            patientsGrid.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            patientsGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            patientsGrid.BackgroundColor = SystemColors.Control;
            patientsGrid.BorderStyle = BorderStyle.None;
            patientsGrid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            patientsGrid.Columns.AddRange(new DataGridViewColumn[] { id, name, surname, pesel });
            patientsGrid.GridColor = SystemColors.ControlDarkDark;
            patientsGrid.Location = new Point(10, 73);
            patientsGrid.Name = "patientsGrid";
            patientsGrid.ReadOnly = true;
            patientsGrid.RowHeadersWidth = 51;
            patientsGrid.Size = new Size(564, 244);
            patientsGrid.TabIndex = 3;
            patientsGrid.CellBeginEdit += patientsGrid_CellBeginEdit;
            patientsGrid.CellEndEdit += patientsGrid_CellEndEdit;
            patientsGrid.CellValidating += patientsGrid_CellValidating;
            patientsGrid.EditingControlShowing += patientsGrid_EditingControlShowing;
            patientsGrid.RowsAdded += patientsGrid_RowsAdded;
            patientsGrid.RowValidating += patientsGrid_RowValidating;
            patientsGrid.UserAddedRow += patientsGrid_UserAddedRow;
            // 
            // id
            // 
            id.HeaderText = "Numer";
            id.MinimumWidth = 6;
            id.Name = "id";
            id.ReadOnly = true;
            // 
            // name
            // 
            name.HeaderText = "Imię";
            name.MinimumWidth = 6;
            name.Name = "name";
            name.ReadOnly = true;
            // 
            // surname
            // 
            surname.HeaderText = "Nazwisko";
            surname.MinimumWidth = 6;
            surname.Name = "surname";
            surname.ReadOnly = true;
            // 
            // pesel
            // 
            pesel.HeaderText = "Pesel";
            pesel.MinimumWidth = 6;
            pesel.Name = "pesel";
            pesel.ReadOnly = true;
            // 
            // addPatientButton
            // 
            addPatientButton.Location = new Point(20, 10);
            addPatientButton.Name = "addPatientButton";
            addPatientButton.Size = new Size(77, 49);
            addPatientButton.TabIndex = 4;
            addPatientButton.Text = "Dodaj\r\npacjenta";
            addPatientButton.UseVisualStyleBackColor = true;
            addPatientButton.Click += addPatientButton_Click;
            // 
            // saveButton
            // 
            saveButton.Location = new Point(207, 10);
            saveButton.Name = "saveButton";
            saveButton.Size = new Size(94, 49);
            saveButton.TabIndex = 5;
            saveButton.Text = "Zapisz\r\nzmiany";
            saveButton.UseVisualStyleBackColor = true;
            saveButton.Click += saveButton_Click;
            // 
            // editButton
            // 
            editButton.Location = new Point(103, 10);
            editButton.Name = "editButton";
            editButton.Size = new Size(98, 49);
            editButton.TabIndex = 6;
            editButton.Text = "Edytuj";
            editButton.UseVisualStyleBackColor = true;
            editButton.Click += editButton_Click;
            // 
            // PatientsView
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(editButton);
            Controls.Add(saveButton);
            Controls.Add(addPatientButton);
            Controls.Add(patientsGrid);
            Name = "PatientsView";
            Size = new Size(584, 333);
            ((System.ComponentModel.ISupportInitialize)patientsGrid).EndInit();
            ResumeLayout(false);
        }

        #endregion

        public DataGridView patientsGrid;
        private DataGridViewTextBoxColumn id;
        private DataGridViewTextBoxColumn name;
        private DataGridViewTextBoxColumn surname;
        private DataGridViewTextBoxColumn pesel;
        private Button addPatientButton;
        private Button saveButton;
        private Button editButton;
    }
}
