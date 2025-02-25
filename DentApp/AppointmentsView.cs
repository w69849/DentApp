using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
//using System.Reflection.Emit;
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
            if (Database.SaveAppointments(appointmentsGrid, rowStates))
                MessageBox.Show("Zapisano");

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
                    //return;
                }
            }
        }

        private void appointmentsGrid_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            string column = appointmentsGrid.Columns[e.ColumnIndex].Name;

            if (column == "pesel")
            {
                if (!e.FormattedValue.ToString().All(char.IsDigit))
                {
                    appointmentsGrid.Rows[e.RowIndex].ErrorText = "Pesel musi mieć 11 cyfr.";
                    e.Cancel = true; // Cancels the cell edit
                }
            }

            if (column == "date")
            {
                DateTime temp;
                string format = "dd.MM.yyyy HH:mm:ss"; // Date and Time Format

                if (!DateTime.TryParseExact(e.FormattedValue.ToString(), format,
                                            CultureInfo.InvariantCulture,
                                            DateTimeStyles.None,
                                            out temp))
                {
                    appointmentsGrid.Rows[e.RowIndex].ErrorText = "Zły format danych w kolumnie Data. Poprawny format to: dd.MM.yyyy hh:mm:ss";
                    e.Cancel = true; // Prevents leaving the cell until valid input
                }
            }

            if (column == "koszt")
            {
                decimal cost;
                NumberStyles style = NumberStyles.AllowThousands | NumberStyles.AllowDecimalPoint;
                CultureInfo culture = CultureInfo.InvariantCulture; // Adjust if needed

                if (appointmentsGrid.Rows[e.RowIndex].Cells["status"].Value == "Zrealizowana")
                {
                    if (!decimal.TryParse(e.FormattedValue.ToString(), style, culture, out cost))
                    {
                        appointmentsGrid.Rows[e.RowIndex].ErrorText = "Zły format danych w kolumnie Koszt. Poprawny format to przykładowo: 1,234.56";
                        e.Cancel = true; // Prevents leaving the cell until valid input
                    }
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
            if (e.ColumnIndex >= 0)
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
               // appointmentsGrid.Rows[e.RowIndex].Cells["cost"].ReadOnly = true;
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

        private void appointmentsGrid_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            //if (e.ColumnIndex == 1 && e.RowIndex >= 0) // Adjust column index to match your ComboBox column
            //{
            //    string selectedValue = appointmentsGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].Value?.ToString();
            //    MessageBox.Show($"Value changed in row {e.RowIndex}, column {e.ColumnIndex}: {selectedValue}");
            //}
        }

        private void openVisitButton_Click(object sender, EventArgs e)
        {
            Label label = new Label();
            label.Text = "Kliknij dwa razy na wizytę, aby do niej przejść";

            int posX = openVisitButton.Location.X + (openVisitButton.Width / 2) - (TextRenderer.MeasureText(label.Text, label.Font).Width / 2);
            int posY = openVisitButton.Location.Y + openVisitButton.Height + 10; // Place label below the button

            label.Location = new Point(posX, posY);
            label.AutoSize = true;

            // Add the label to the form
            this.Controls.Add(label);
            openVisitButton.Enabled = false;
        }

        private void appointmentsGrid_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if(!openVisitButton.Enabled)
            {
                if (e.RowIndex >= 0)
                {
                    // Get the selected row
                    DataGridViewRow row = appointmentsGrid.Rows[e.RowIndex];

                    // Example: Retrieve a value from a specific column
                    string id = row.Cells["visitNumber"].Value.ToString();

                    Appointment appointment = new Appointment();
                    appointment.Show();

                     //MessageBox.Show($"Double-clicked row: {e.RowIndex}, Value: {id}");
                }
            }
            
        }
    }
}
