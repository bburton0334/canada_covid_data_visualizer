/*
* FILE              : LoadDataForm.cs
* PROJECT           : PROG3070 - A04
* PROGRAMMER        : Briana Burton
* FIRST VERSION     : 2022-11-18
* DESCRIPTION       :
*           The functions in this file are used to allow the user to open
*           a dialog window to select their COVID-19 testing data file
*           and their COVID-19 case data file. The functions will error check
*           accordingly and insert the data from the files into the database
*           specified in the connection string in the app.config file.
*/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;

namespace DataVisualizer
{
    public partial class LoadDataForm : Form
    {
        // fields 
        bool isCaseDataLoaded = false;
        bool isTestingDataLoaded = false;

        public LoadDataForm()
        {
            InitializeComponent();
            
        }

        // FUNCTION         : btnTestingData_Cick
        // DESCRIPTION      :
        //      This method is called when the user clicks the button to 
        //      load in the testing data file. It allows the user to select
        //      the file with a dialog.
        // PARAMETERS       :
        //      object sender
        //      RoutedEventargs e
        // RETURNS          : none
        private void btnTestingData_Click(object sender, EventArgs e)
        {
            string filePath = string.Empty;

            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "CSV files (*.csv)|*.csv";
                openFileDialog.FilterIndex = 1;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    //Get the path of specified file
                    filePath = openFileDialog.FileName;
                    testingDataConnect(filePath);
                }
            }
        }

        // FUNCTION         : btnCaseData_Cick
        // DESCRIPTION      :
        //      This method is called when the user clicks the button to 
        //      load in the case data file. It allows the user to select
        //      the file with a dialog.
        // PARAMETERS       :
        //      object sender
        //      RoutedEventargs e
        // RETURNS          : none
        private void btnCaseData_Click(object sender, EventArgs e)
        {
            string filePath = string.Empty;

            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "CSV files (*.csv)|*.csv";
                openFileDialog.FilterIndex = 1;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    //Get the path of specified file
                    filePath = openFileDialog.FileName;
                    caseDataConnect(filePath);
                }
            }
        }

        // FUNCTION         : btnLoadContinue_Cick
        // DESCRIPTION      :
        //      This method is called when the user clicks the button to 
        //      continue. It will check to see if the files have been loaded
        //      sucessfully. If not, it will prompt a warning and not set the
        //      verification values for the firsr form to see.
        // PARAMETERS       :
        //      object sender
        //      RoutedEventargs e
        // RETURNS          : none
        private void btnLoadContinue_Click(object sender, EventArgs e)
        {
            if (isCaseDataLoaded == true && isTestingDataLoaded == true)
            {
                Verifications.caseDataLoaded = true;
                Verifications.testingDataLoaded = true;
                this.Close();
            }
            else
            {
                MessageBox.Show("Please load testing data and case data to continue.", "Please Load Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        // FUNCTION         : caseDataConnect
        // DESCRIPTION      :
        //      This method is called to connect to the database and insert
        //      the values from the case data file into the database.
        // PARAMETERS       :
        //      string filepath - path of the case data file
        // RETURNS          : none
        private void caseDataConnect(string filepath)
        {
            string connStr = ConfigurationManager.ConnectionStrings["connStr"].ToString();
            int lineNum = 0;

            try
            {
                // connecting to database
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Open();
                    var cmd = new SqlCommand();

                    // clearing database to get ready for new data
                    var sql = "DELETE FROM Cases;";
                    cmd.CommandText = sql;
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = conn;
                    cmd.ExecuteNonQuery();

                    using (StreamReader reader = new StreamReader(filepath))
                    {
                        while(!reader.EndOfStream)
                        {
                            var line = reader.ReadLine();
                            if (lineNum != 0)
                            {
                                var values = line.Split(',');
                                sql = "INSERT INTO CovidData.dbo.Cases VALUES ('" + values[0] + "', '" + values[1] + "', '" + values[2] + "', '" + values[3] + "', '" + values[4] + "', '" + values[5] + "', '" + values[6] + "', '" + values[7] + "', '" + values[8] + "', '" + values[9] + "', '" + values[10] + "', '" + values[11] + "', '" + values[12] + "', '" + values[13] + "', '" + values[14] + "', '" + values[15] + "', '" + values[16] + "', '" + values[17] + "', '" + values[18] + "', '" + values[19] + "', '" + values[20] + "', '" + values[21] + "', '" + values[22] + "');";
                        
                                cmd.CommandText = sql;
                                cmd.CommandType = CommandType.Text;
                                cmd.Connection = conn;
                                cmd.ExecuteNonQuery();
                            }
                            lineNum++;
                        }
                    }
                    conn.Close();
                }

                lblCaseLoadStatus.Text = "Status: Success!";
                lblCaseLoadStatus.ForeColor = Color.Green;
                isCaseDataLoaded = true;
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error occured. Try Again.\n\n" + ex.Message, "Error Occured", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // FUNCTION         : testingDataConnect
        // DESCRIPTION      :
        //      This method is called to connect to the database and insert
        //      the values from the testing data file into the database.
        // PARAMETERS       :
        //      string filepath - path of the case data file
        // RETURNS          : none
        private void testingDataConnect(string filepath)
        {
            string connStr = ConfigurationManager.ConnectionStrings["connStr"].ToString();
            int lineNum = 0;

            try
            {
                // connecting to database
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Open();
                    var cmd = new SqlCommand();

                    // first clearing database to get ready for new data
                    var sql = "DELETE FROM Testings;";
                    cmd.CommandText = sql;
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = conn;
                    cmd.ExecuteNonQuery();

                    using (StreamReader reader = new StreamReader(filepath))
                    {
                        while (!reader.EndOfStream)
                        {
                            var line = reader.ReadLine();
                            if (lineNum != 0)
                            {
                                var values = line.Split(',');
                                sql = "INSERT INTO CovidData.dbo.Testings VALUES ('" + values[0] + "', '" + values[1] + "', '" + values[2] + "', '" + values[3] + "', '" + values[4] + "', '" + values[5] + "', '" + values[6] + "', '" + values[7] + "', '" + values[8] + "', '" + values[9] + "');";

                                cmd.CommandText = sql;
                                cmd.CommandType = CommandType.Text;
                                cmd.Connection = conn;
                                cmd.ExecuteNonQuery();
                            }
                            lineNum++;
                        }
                    }
                    conn.Close();
                }

                lblTestingLoadStatus.Text = "Status: Success!";
                lblTestingLoadStatus.ForeColor = Color.Green;
                isTestingDataLoaded = true;
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error occured. Try Again.\n\n" + ex.Message, "Error Occured", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
