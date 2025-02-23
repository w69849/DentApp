using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DentApp
{
    public partial class AppointmentsView : UserControl
    {
        public enum RowState
        {
            New,
            Added,
            Deleted,
            Modified
        }

        public Dictionary<int, RowState> rowStates = new Dictionary<int, RowState>();

        public AppointmentsView()
        {
            InitializeComponent();

            FillAppointmentsGrid();
        }

        private void FillAppointmentsGrid()
        {
            Database.LoadAppointments(appointmentsGrid, rowStates);
            appointmentsGrid.Refresh();
            appointmentsGrid.Update();
        }

        private void addVisitButton_Click(object sender, EventArgs e)
        {
            appointmentsGrid.AllowUserToAddRows = true;
            appointmentsGrid.ReadOnly = false;
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            Database.SaveAppointments(appointmentsGrid, rowStates);
            appointmentsGrid.ReadOnly = true;
            editButton.Enabled = true;
            appointmentsGrid.EditMode = DataGridViewEditMode.EditProgrammatically;
        }

        private void appointmentsGrid_RowValidating(object sender, DataGridViewCellCancelEventArgs e)
        {
            foreach (DataGridViewCell cell in appointmentsGrid.Rows[e.RowIndex].Cells)
            {
                if (cell.Value == null || string.IsNullOrWhiteSpace(cell.Value.ToString()))
                {
                    appointmentsGrid.Rows[e.RowIndex].ErrorText = "Komórki nie mogą być puste.";
                    e.Cancel = true; // Prevents leaving the row
                    return;
                }
            }
        }

        private void appointmentsGrid_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (appointmentsGrid.Columns[e.ColumnIndex].Name == "pesel")
            {
                if (!e.FormattedValue.ToString().All(char.IsDigit))
                {
                    appointmentsGrid.Rows[e.RowIndex].ErrorText = "Pesel musi mieć 11 cyfr.";
                    e.Cancel = true; // Cancels the cell edit
                }
            }

            if (appointmentsGrid.Columns[e.ColumnIndex].Name == "date")
            {
                DateTime temp;
                string format = "dd.MM.yyyy HH:mm:ss"; // Date and Time Format

                if (!DateTime.TryParseExact(e.FormattedValue.ToString(), format,
                                            System.Globalization.CultureInfo.InvariantCulture,
                                            System.Globalization.DateTimeStyles.None,
                                            out temp))
                {
                    MessageBox.Show("Invalid date format! Please use dd/MM/yyyy HH:mm:ss.",
                                    "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    e.Cancel = true; // Prevents leaving the cell until valid input
                }
            }
        }

        private void appointmentsGrid_UserAddedRow(object sender, DataGridViewRowEventArgs e)
        {
            appointmentsGrid.AllowUserToAddRows = false;
        }

        private void appointmentsGrid_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            int lastRowIndex = appointmentsGrid.Rows.Count - 1;

            if (e.RowIndex != lastRowIndex)
            {
                e.Cancel = true; // Block editing for other rows
            }
        }

        private void appointmentsGrid_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            // Clear the row error in case the user presses ESC.
            appointmentsGrid.Rows[e.RowIndex].ErrorText = String.Empty;

            if (appointmentsGrid.Columns[e.ColumnIndex].Name == "visitNumber")
            {
                string s = appointmentsGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                int.TryParse(s, out int index);

                if (rowStates[index] != RowState.New)
                    rowStates[index] = RowState.Modified;
            }
        }

        private void appointmentsGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            string column = appointmentsGrid.Columns[e.ColumnIndex].Name;

            if ((column == "patient" || column == "pesel") && (e.RowIndex == appointmentsGrid.RowCount - 1))
            {
                Rectangle cellRect = appointmentsGrid.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true);
                patientsListBox.Location = new Point(appointmentsGrid.Left + cellRect.Left, appointmentsGrid.Top + cellRect.Bottom);
                patientsListBox.Width = cellRect.Width;
                patientsListBox.Visible = true;

                if (patientsListBox.Items.Count < 1)
                    Database.LoadPatients(patientsListBox);
                patientsListBox.Refresh();
                patientsListBox.Focus();
            }

            else
                patientsListBox.Visible = false;
        }

        private void patientsListBox_MouseDown(object sender, MouseEventArgs e)
        {
            int index = patientsListBox.IndexFromPoint(e.Location);

            if (index != ListBox.NoMatches)
            {
                string selectedItem = patientsListBox.Items[index].ToString();

                //var item = patientsListBox.SelectedItem.ToString();
                string withoutNumber = selectedItem.Substring(selectedItem.IndexOf('.') + 1);
                string[] parts = withoutNumber.Split(',');
                string fullName = parts[0].Trim();
                string number = parts[1].Trim();

                var currentRow = appointmentsGrid.CurrentCell.RowIndex;
                appointmentsGrid.Rows[currentRow].Cells["patient"].Value = fullName;
                appointmentsGrid.Rows[currentRow].Cells["pesel"].Value = number;
            }
            patientsListBox.Visible = false;
        }

        private void appointmentsGrid_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            string column = appointmentsGrid.Columns[appointmentsGrid.CurrentCell.ColumnIndex].Name;

            if (column == "id") // Target cell
            {
                e.Control.KeyPress -= HandleId;
                e.Control.KeyPress += HandleId;
            }
        }

        private void HandleId(object sender, KeyPressEventArgs e)
        {
            e.Handled = true; // Block any input
        }

        private void appointmentsGrid_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            if (appointmentsGrid.Rows[e.RowIndex].IsNewRow)
            {
                if (appointmentsGrid.Rows.Count > 1)
                {
                    appointmentsGrid.Rows[e.RowIndex].Cells["visitNumber"].Value = (Int64)appointmentsGrid.Rows[appointmentsGrid.Rows.Count - 2].Cells["visitNumber"].Value + 1;
                }
                else
                    appointmentsGrid.Rows[e.RowIndex].Cells["visitNumber"].Value = 1;

                if (appointmentsGrid.Rows[e.RowIndex].Cells["visitNumber"].Value != null)
                {
                    string s = appointmentsGrid.Rows[e.RowIndex].Cells["visitNumber"].Value.ToString();

                    int.TryParse(s, out int index);
                    rowStates[index] = RowState.New;
                }

                appointmentsGrid.Rows[e.RowIndex].Cells["status"].Value = "Oczekująca";
                appointmentsGrid.Rows[e.RowIndex].Cells["cost"].Value = "-";
            }
        }

        private void AppointmentsView_Load(object sender, EventArgs e)
        {

        }

        private void editButton_Click(object sender, EventArgs e)
        {
            editButton.Enabled = false;
            appointmentsGrid.ReadOnly = false;
            appointmentsGrid.EditMode = DataGridViewEditMode.EditOnKeystrokeOrF2;
        }
    }
}
