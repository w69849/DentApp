using System.Net.NetworkInformation;

namespace DentApp
{
    public partial class Form1 : Form
    {
        private enum View
        {
            Patients,
            Appointments
        }

        private View currentView;

        private PatientsView patientsView;
        private AppointmentsView appointmentsView;

        public Form1()
        {
            if (!File.Exists("test41.db"))
                Database.InitializeDatabase();
            
            InitializeComponent();           

            patientsView = new PatientsView();
            appointmentsView = new AppointmentsView();

            patientsView.Dock = DockStyle.Fill;
            appointmentsView.Dock = DockStyle.Fill;

            navigationPanel.Controls.Clear();
            navigationPanel.Controls.Add(patientsView);

            currentView = View.Patients;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = false;
        }

        private void appointmentsButton_Click(object sender, EventArgs e)
        {
            if (currentView == View.Patients)
                Database.SavePatients(patientsView.patientsGrid, patientsView.rowStates);
            
            navigationPanel.Controls.Clear();
            navigationPanel.Controls.Add(appointmentsView);
            currentView = View.Appointments;
        }

        private void patientsListButton_Click(object sender, EventArgs e)
        {
            if(currentView == View.Appointments)
                Database.SaveAppointments(appointmentsView.appointmentsGrid, appointmentsView.rowStates);
           
            navigationPanel.Controls.Clear();
            navigationPanel.Controls.Add(patientsView);
            currentView = View.Patients;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
