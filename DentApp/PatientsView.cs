using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DentApp
{
    public partial class PatientsView : UserControl
    {
        public enum RowState
        {
            New,
            Added,
            Deleted,
            Modified
        }

        public Dictionary<int, RowState> rowStates = new Dictionary<int, RowState>();

        public PatientsView()
        {
            InitializeComponent();

            FillPatientsGrid();
        }

        private void FillPatientsGrid()
        {

            Database.LoadPatients(patientsGrid, rowStates);
            patientsGrid.Refresh();
            //patientsGrid.Update();
        }

        private void addPatientButton_Click(object sender, EventArgs e)
        {
            // patientsGrid.Rows[patientsGrid.Rows.Count - 1].ReadOnly = false;
            patientsGrid.AllowUserToAddRows = true;
            patientsGrid.ReadOnly = false;
        }

        private void patientsGrid_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            if (patientsGrid.Rows[e.RowIndex].IsNewRow)
            {
                if (patientsGrid.Rows.Count > 1)
                {
                    patientsGrid.Rows[e.RowIndex].Cells["id"].Value = (Int64)patientsGrid.Rows[patientsGrid.Rows.Count - 2].Cells["id"].Value + 1;
                }
                else
                    patientsGrid.Rows[e.RowIndex].Cells["id"].Value = 1;

                if (patientsGrid.Rows[e.RowIndex].Cells["id"].Value != null)
                {
                    string s = patientsGrid.Rows[e.RowIndex].Cells["id"].Value.ToString();

                    int.TryParse(s, out int index);
                    rowStates[index] = RowState.New;
                }
            }
        }

        private void patientsGrid_UserAddedRow(object sender, DataGridViewRowEventArgs e)
        {
            patientsGrid.AllowUserToAddRows = false;
        }

        private void patientsGrid_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            int lastRowIndex = patientsGrid.Rows.Count - 1;

            if (e.RowIndex != lastRowIndex && patientsGrid.ReadOnly == true)
            {
                e.Cancel = true; // Block editing for other rows
            }
        }

        private void patientsGrid_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            string column = patientsGrid.Columns[patientsGrid.CurrentCell.ColumnIndex].Name;

            if (column == "id") // Target cell
            {
                e.Control.KeyPress -= HandlePesel;
                e.Control.KeyPress -= HandleLetters;
                e.Control.KeyPress += HandleId;
            }

            if (column == "pesel")
            {
                e.Control.KeyPress -= HandleId;
                e.Control.KeyPress -= HandleLetters;
                e.Control.KeyPress -= HandlePesel;
                e.Control.KeyPress += HandlePesel;
            }

            if (column == "name" || column == "surname")
            {
                e.Control.KeyPress -= HandlePesel;
                e.Control.KeyPress -= HandleId;
                e.Control.KeyPress += HandleLetters;
            }
        }

        private void HandlePesel(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true; // Cancels the cell edit
            }

            //if (patientsGrid.Rows[])
        }

        private void HandleId(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void HandleLetters(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true; // Cancels the cell edit
            }
        }

        private void patientsGrid_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            string column = patientsGrid.Columns[e.ColumnIndex].Name;

            if (column == "pesel")
            {
                if (e.FormattedValue.ToString().Length < 11 || e.FormattedValue.ToString().Length > 11)
                {
                    patientsGrid.Rows[e.RowIndex].ErrorText = "Pesel musi mieć 11 cyfr.";
                    e.Cancel = true; // Cancels the cell edit
                }
            }
        }

        private void patientsGrid_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            // Clear the row error in case the user presses ESC.
            patientsGrid.Rows[e.RowIndex].ErrorText = String.Empty;

            string column = patientsGrid.Columns[e.ColumnIndex].Name;

            if (column == "name" || column == "surname")
            {
                if(patientsGrid.Rows[e.RowIndex].Cells[column].Value.ToString() != null)
                {
                    string text = patientsGrid.Rows[e.RowIndex].Cells[column].Value.ToString();

                    if (text.Length > 0)
                    {
                        string updated = char.ToUpper(text[0]) + text.Substring(1);
                        patientsGrid.Rows[e.RowIndex].Cells[column].Value = updated;
                    }
                }              
            }

            if (patientsGrid.Rows[e.RowIndex].Cells["id"].Value != null)
            {
                string s = patientsGrid.Rows[e.RowIndex].Cells["id"].Value.ToString();

                int.TryParse(s, out int index);



                if (rowStates[index] != RowState.New)
                {
                    
                    rowStates[index] = RowState.Modified;
                }
            }
        }

        private void patientsGrid_RowValidating(object sender, DataGridViewCellCancelEventArgs e)
        {
            foreach (DataGridViewCell cell in patientsGrid.Rows[e.RowIndex].Cells)
            {
                if (cell.Value == null || string.IsNullOrWhiteSpace(cell.Value.ToString()))
                {
                    patientsGrid.Rows[e.RowIndex].ErrorText = "Komórki nie mogą być puste.";
                    e.Cancel = true; // Prevents leaving the row
                    return;
                }
            }
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            Database.SavePatients(patientsGrid, rowStates);
            patientsGrid.ReadOnly = true;
            editButton.Enabled = true;
            patientsGrid.EditMode = DataGridViewEditMode.EditProgrammatically;
        }

        private void editButton_Click(object sender, EventArgs e)
        {
            editButton.Enabled = false;
            patientsGrid.ReadOnly = false;
            patientsGrid.EditMode = DataGridViewEditMode.EditOnKeystrokeOrF2;
        }
    }
}
