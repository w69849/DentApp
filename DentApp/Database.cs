using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Data;
using System.ComponentModel;
using System.Reflection;
using System.Data.Common;
using System.Data.SQLite;
using System.Windows.Forms;

namespace DentApp
{
    static class Database
    {
        private const string connectionString = "Data Source = DentDatabase.db";

        private enum AppointmentStatus
        {
            Oczekująca,
            Zrealizowana,
            Odwołana,
            [Description("Nieobecność pacjenta")]
            Nieobecność_pacjenta,
            Aktualna
        }

        public static void InitializeDatabase()
        {
            using (SqliteConnection connection = new SqliteConnection(connectionString))
            {
                connection.Open();

                var command = new SqliteCommand();
                command = connection.CreateCommand();

                command.CommandText =
                $@"
                    CREATE TABLE Patients
                    ( 
                        id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
                        name TEXT NOT NULL,
                        surname TEXT NOT NULL,
                        pesel TEXT NOT NULL
                    );

                    
                ";

                command.ExecuteNonQuery();

                //INSERT INTO Patients VALUES(1, 'Michał', 'Lewandowski', '01234567890');
                //INSERT INTO Patients VALUES(2, 'Bartłomiej', 'Bocheński', '90123456789')
                command.CommandText =
                    $@"
                        CREATE TABLE Appointments
                        (
                            id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
                            date DATETIME NOT NULL,
                            patientId INT NOT NULL,
                            status TEXT CHECK(status IN('Oczekująca', 'Aktualna', 'Zrealizowana', 'Odwołana', 'Nieobecność pacjenta')),
                            cost DECIMAL(8,2)
                            
                        )
                    ";
                //INSERT INTO Appointments VALUES(1, '2025-02-17 12:00:00', 5, 'Zrealizowana');
                //INSERT INTO Appointments VALUES(2, '2025-02-17 14:00:00', 6, 'Zrealizowana')
                //command.CommandText =
                //    $@"
                //               

                //    ";

                command.ExecuteNonQuery();
            }
        }

        public static void LoadPatients(DataGridView patientsGrid, Dictionary<int, PatientsView.RowState> rowsStates)
        {
            using (SqliteConnection conn = new SqliteConnection(connectionString))
            {
                conn.Open();

                var command = new SqliteCommand();
                command.Connection = conn;
                command.CommandText =
                    $@"
                        SELECT id AS Numer, name AS Imię, surname AS Nazwisko, pesel AS Pesel
                        FROM Patients;
                    ";

                using (SqliteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        patientsGrid.Rows.Add(reader["Numer"], reader["Imię"], reader["Nazwisko"], reader["Pesel"]);
                        int.TryParse(reader["Numer"].ToString(), out int id);
                        rowsStates[id] = PatientsView.RowState.Added;
                    }
                }
            }
        }

        public static void LoadPatients(ListBox listBox)
        {
            listBox.Items.Clear();
            using (SQLiteConnection conn = new SQLiteConnection(connectionString))
            {
                conn.Open();
                using (SQLiteCommand cmd = new SQLiteCommand("SELECT * FROM Patients", conn))
                using (SQLiteDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        listBox.Items.Add(reader["id"].ToString() + "." + reader["name"] + " " + reader["surname"] + ", " + reader["pesel"]);
                    }
                }
            }
        }

        public static void SavePatients(DataGridView patientsGrid, Dictionary<int, PatientsView.RowState> rowStates)
        {
            using (SqliteConnection conn = new SqliteConnection(connectionString))
            {
                conn.Open();

                foreach (DataGridViewRow row in patientsGrid.Rows)
                {
                    if (row.Cells["id"].Value.ToString() != null)
                    {
                        string s = row.Cells["id"].Value.ToString();
                        int.TryParse(s, out int index);

                        if (rowStates.ContainsKey(index))
                        {
                            if (rowStates[index] == PatientsView.RowState.New)
                            {
                                var command = new SqliteCommand(
                                "INSERT INTO Patients (name, surname, pesel)" +
                                "VALUES (@name, @surname, @pesel)", conn);

                                command.Parameters.AddWithValue("@name", row.Cells["name"].Value);
                                command.Parameters.AddWithValue("@surname", row.Cells["surname"].Value);
                                command.Parameters.AddWithValue("@pesel", row.Cells["pesel"].Value);

                                try
                                {
                                    command.ExecuteNonQuery();
                                    rowStates[index] = PatientsView.RowState.Added;
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show(ex.Message);
                                }
                            }
                        }             
                    }
                }
            }
        }

        public static void LoadAppointments(DataGridView gridView, Dictionary<int, AppointmentsView.RowState> rowStates)
        {
            using (SqliteConnection conn = new SqliteConnection(connectionString))
            {
                conn.Open();

                var command = new SqliteCommand();
                command.Connection = conn;
                command.CommandText =
                    $@"
                        SELECT Appointments.id AS 'Numer wizyty', date AS 'Data',
                            CONCAT(Patients.name, ' ', Patients.surname) AS 'Pacjent', Patients.pesel AS 'Pesel', status AS 'Status', cost AS 'Koszt'
                        FROM Appointments
                            INNER JOIN Patients ON Appointments.patientId = Patients.id
                    ";

                using (SqliteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        gridView.Rows.Add(reader["Numer wizyty"], reader["Data"], reader["Pacjent"], reader["Pesel"], reader["Status"], reader["Koszt"]);
                        int.TryParse(reader["Numer wizyty"].ToString(), out int id);
                        rowStates[id] = AppointmentsView.RowState.Added;
                    }
                }
            }
        }

        public static void SaveAppointments(DataGridView appointmentsGrid, Dictionary<int, AppointmentsView.RowState> rowStates)
        {
            using (SqliteConnection conn = new SqliteConnection(connectionString))
            {
                conn.Open();

                foreach (DataGridViewRow row in appointmentsGrid.Rows)
                {
                    if (row.Cells["visitNumber"].Value != null)
                    {
                        string s = row.Cells["visitNumber"].Value.ToString();
                        int.TryParse(s, out int index);

                        if (rowStates.ContainsKey(index))
                        {
                            if (rowStates[index] == AppointmentsView.RowState.New)
                            {
                                var cmd = new SqliteCommand("SELECT id FROM Patients WHERE pesel = @pesel", conn);
                                cmd.Parameters.AddWithValue("@pesel", row.Cells["Pesel"].Value);

                                string id = null;
                                try
                                {
                                    if (cmd.ExecuteScalar() != null)
                                    {
                                        id = cmd.ExecuteScalar().ToString();

                                        var command = new SqliteCommand(
                                            "INSERT INTO Appointments (date, patientId, status, cost)" +
                                            "VALUES (@date, @patientId, @status, @cost)", conn);

                                        command.Parameters.AddWithValue("@date", row.Cells["Data"].Value);
                                        command.Parameters.AddWithValue("@patientId", id);
                                        command.Parameters.AddWithValue("@status", row.Cells["Status"].Value);
                                        command.Parameters.AddWithValue("@cost", row.Cells["cost"].Value);


                                        command.ExecuteNonQuery();
                                        rowStates[index] = AppointmentsView.RowState.Added;
                                    }
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show(ex.Message);
                                }
                            }

                            //else
                            //    MessageBox.Show("Coś nie tak z wartościami");                           
                        }               
                    }
                }
            }
        }
    }
}

