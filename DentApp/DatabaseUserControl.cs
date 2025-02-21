using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentApp
{
    internal class DatabaseUserControl : UserControl
    {
        public enum RowState
        {
            New,
            Added,
            Deleted,
            Modified
        }

        public Dictionary<int, RowState> rowStates = new Dictionary<int, RowState>();

        public DatabaseUserControl() : base()
        {
            InitializeComponent();
            FillGrid();
        }

        protected virtual void InitializeComponent()
        {           
        }

        protected virtual void FillGrid()
        {
        }

        protected virtual void saveButton_Click(object sender, EventArgs e)
        {
        }

        protected virtual void Grid_RowValidating(object sender, DataGridViewCellCancelEventArgs e)
        {

        }
    }
}
