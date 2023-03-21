/*
* FILE              : Form1.cs
* PROJECT           : PROG3070 - A04
* PROGRAMMER        : Briana Burton
* FIRST VERSION     : 2022-11-18
* DESCRIPTION       :
*           The functions in this file are used to allow the user to load in
*           and view COVID-19 data on two seperate graphs based on their selected
*           csv covid-19 data files. The user will be able to select a range of dates
*           and the graphs will update accordingly based on the users date and province
*           selection made. The user can also reset the application by hitting the start
*           button again.
*/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace DataVisualizer
{
    public partial class Form1 : Form
    {
        // fields holding line chart dates - done so only valid values are used
        static DateTime startLineChart = DateTime.MinValue;
        static DateTime endLineChart = DateTime.MinValue;

        public Form1()
        {
            InitializeComponent();

            // inserting the province values into combobox
            comboBoxProvinces.Items.Add("Alberta");
            comboBoxProvinces.Items.Add("British Columbia");
            comboBoxProvinces.Items.Add("Manitoba");
            comboBoxProvinces.Items.Add("New Brunswick");
            comboBoxProvinces.Items.Add("Newfoundland and Labrador");
            comboBoxProvinces.Items.Add("Northwest Territories");
            comboBoxProvinces.Items.Add("Nova Scotia");
            comboBoxProvinces.Items.Add("Nunavut");
            comboBoxProvinces.Items.Add("Ontario");
            comboBoxProvinces.Items.Add("Prince Edward Island");
            comboBoxProvinces.Items.Add("Quebec");
            comboBoxProvinces.Items.Add("Saskatchewan");
            comboBoxProvinces.Items.Add("Yukon");
            comboBoxProvinces.Items.Add("Canada");
            comboBoxProvinces.SelectedItem = "Quebec";

            // calling method to disable buttons 
            disableItems();

            chartPie.ChartAreas[0].BackColor = Color.FromArgb(37, 42, 64);
            chartPie.Series[0].LabelForeColor = Color.White;
            chartPie.ForeColor = Color.White;

            chartLine.ChartAreas[0].BackColor = Color.FromArgb(37, 42, 64);
            chartLine.Series[0].LabelForeColor = Color.White;
            chartLine.ForeColor = Color.White;
            chartLine.BackSecondaryColor = Color.White;
            chartLine.Series[0].MarkerColor = Color.White;
            chartLine.Series[1].LabelForeColor = Color.White;
            chartLine.ChartAreas[0].AxisX.LineColor = Color.LightBlue;
            chartLine.ChartAreas[0].AxisY.LineColor = Color.LightBlue;
            chartLine.ChartAreas[0].AxisX.TitleForeColor = Color.FromArgb(46, 51, 73);
            chartLine.ChartAreas[0].AxisY.TitleForeColor = Color.FromArgb(46, 51, 73);
            chartLine.Series[0].SmartLabelStyle.CalloutLineColor = Color.DarkSeaGreen;
            chartLine.Series[1].SmartLabelStyle.CalloutLineColor = Color.LightSeaGreen;

            chartLine.Series[0].ShadowColor = Color.DarkSlateGray;
            chartLine.ChartAreas[0].AxisX.LabelStyle.ForeColor = Color.LightBlue;
            chartLine.ChartAreas[0].AxisY.LabelStyle.ForeColor = Color.LightBlue;

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.AutoScroll = true;
        }



        // FUNCTION         : button1_Cick
        // DESCRIPTION      :
        //      This method is called when the user hits the start button. It first
        //      checks to see if the user has loaded in their covid-19 data, then
        //      calls the other functions to start the program if so.
        // PARAMETERS       :
        //      object sender
        //      RoutedEventargs e
        // RETURNS          : none
        private void button1_Click(object sender, EventArgs e)
        {
            if (Verifications.testingDataLoaded == true && Verifications.caseDataLoaded == true)
            {
                startProgram();
            }
            else
            {
                MessageBox.Show("Please load data first. Click 'Load Data'. ");
            }
        }

        // FUNCTION         : startProgram
        // DESCRIPTION      :
        //      This method is called in order to initizale all of the functionalities of the 
        //      two charts within the program. It loads in the data from the database and 
        //      calls the functions to insert it into the pie chart and the line chart.
        //      the file with a dialog.
        // PARAMETERS       :
        //      none
        // RETURNS          : none
        private void startProgram()
        {
            enableItems();

            string connStr = ConfigurationManager.ConnectionStrings["connStr"].ToString();
            SqlConnection conn = new SqlConnection(connStr);

            DateTime lastestDate;
            DateTime earliestDate;

            // removing title if user resets charts
            if (chartLine.Titles.Count > 0)
            {
                chartLine.Titles.RemoveAt(0);
            }

            if (chartPie.Titles.Count > 0)
            {
                chartPie.Titles.RemoveAt(0);
            }

            try
            {
                using (conn)
                {
                    conn.Open();
                    var cmd = new SqlCommand();

                    // getting the latest date from data
                    var sql = @"SELECT TOP 1 [date] FROM Cases WHERE NOT prname='Canada' AND NOT pruid='99' ORDER BY [date] DESC;";
                    cmd.CommandText = sql;
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = conn;
                    lastestDate = (DateTime)cmd.ExecuteScalar();

                    // getting the earliest date from data
                    sql = @"SELECT TOP 1 [date] FROM Cases WHERE NOT pruid='99' ORDER BY [date] ASC;";
                    cmd.CommandText = sql;
                    earliestDate = (DateTime)cmd.ExecuteScalar();

                    conn.Close();
                }

                startLineChart = earliestDate;
                endLineChart = lastestDate;

                fillPieChart(lastestDate);
                fillLineChart(earliestDate, lastestDate, "Quebec");

                pieChartDatePicker.Value = lastestDate;
                lineChartEndDatePicker.Value = lastestDate;
                lineChartStartDatePicker.Value = earliestDate;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error occured. Try Again.\n\n" + ex.Message, "Error Occured", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        // FUNCTION         : fillPieChart
        // DESCRIPTION      :
        //      This method is called in order to fill data from the database into the 
        //      pie chart of the application. It shows the total number of cases per
        //      province by date selection. This method is called whenever the chart
        //      needs to be updated.
        // PARAMETERS       :
        //      DateTime date - date to be shown in the pie chart
        // RETURNS          : none
        private void fillPieChart(DateTime date)
        {
            chartPie.Series[0].Points.Clear();

            string connStr = ConfigurationManager.ConnectionStrings["connStr"].ToString();
            SqlConnection conn = new SqlConnection(connStr);

            DataTable dt = new DataTable();

            conn.Open();
            SqlDataAdapter da = new SqlDataAdapter(@"SELECT prname, totalcases FROM cases WHERE [date]='" + date.ToShortDateString() + "' AND NOT prname='Canada' AND NOT pruid='99';", conn);
            da.Fill(dt);
            chartPie.DataSource = dt;
            conn.Close();

            chartPie.Series["Series1"].XValueMember = "prname";
            chartPie.Series["Series1"].YValueMembers = "totalcases";
            chartPie.Titles.Add("COVID Cases by Provinces & Territories as of " + date.ToShortDateString()).ForeColor = Color.White;
            chartPie.Series["Series1"].IsValueShownAsLabel = true;
            chartPie.Series["Series1"]["PieLabelStyle"] = "outside";
            chartPie.ChartAreas[0].Area3DStyle.Enable3D = true;
            chartPie.ChartAreas[0].Area3DStyle.Inclination = 10;
        }

        // FUNCTION         : pieChartDatePicker_ValueChanged
        // DESCRIPTION      :
        //      This method is called when the user selects a new date on the
        //      date picker for the pie chart. It calls the function to verify
        //      the date selection, then updates the pie chart data if valid.
        // PARAMETERS       :
        //      object sender
        //      RoutedEventargs e
        // RETURNS          : none
        private void pieChartDatePicker_ValueChanged(object sender, EventArgs e)
        {
            bool status = verifyDate(pieChartDatePicker.Value.ToShortDateString());
            if (status == false)
            {
                MessageBox.Show("Date selected has no corrosponding data. Please select a different date.");
            }
            else
            {
                chartPie.Titles.RemoveAt(0);
                fillPieChart(pieChartDatePicker.Value);
            }
        }

        // FUNCTION         : verifyDate
        // DESCRIPTION      :
        //      This method is called in order to verify the selected date inside the
        //      database. It checks if the users selected date exists within the data
        //      base.
        // PARAMETERS       :
        //      string date - date to be checked against
        // RETURNS          :
        //      bool - false if does not exist, true if does exist
        private bool verifyDate(string date)
        {
            string connStr = ConfigurationManager.ConnectionStrings["connStr"].ToString();
            SqlConnection conn = new SqlConnection(connStr);
            int rows = 0;

            using (conn)
            {
                conn.Open();
                var cmd = new SqlCommand();
                var sql = @"SELECT COUNT([date]) FROM cases WHERE [date]='" + date + "';";
                cmd.CommandText = sql;
                cmd.CommandType = CommandType.Text;
                cmd.Connection = conn;
                rows = (int)cmd.ExecuteScalar();
                conn.Close();
            }

            if (rows > 0)
            {
                return true;
            }

            return false;
        }

        // FUNCTION         : fillLineChart
        // DESCRIPTION      :
        //      This method is called in order to fill data from the database into the 
        //      line chart of the application. It shows the total number of cases based
        //      on slected province and date range that has been specified by the user.
        //      it also shows the number of tests done within that same date range.
        // PARAMETERS       :
        //      DateTime startDate - start time range
        //      DateTime endDate - end date range
        //      string province - province or canada wide for data to be shown
        // RETURNS          : none
        private void fillLineChart(DateTime startDate, DateTime endDate, string province)
        {
            chartLine.Series[0].Points.Clear();
            chartLine.Series[1].Points.Clear();

            string connStr = ConfigurationManager.ConnectionStrings["connStr"].ToString();
            SqlConnection conn = new SqlConnection(connStr);
            DataTable dt = new DataTable();

            conn.Open();
            SqlDataAdapter da = new SqlDataAdapter(@"SELECT DISTINCT cases.[date], cases.totalcases, Testings.numtests_total FROM Cases JOIN Testings ON Cases.[date] = Testings.[date] WHERE cases.prname = '" + province + "' AND Testings.prname = '" + province + "' AND Cases.[date] <= '" + endDate.ToShortDateString() + "' AND Cases.[date] >= '" + startDate.ToShortDateString() + "';", conn);
            
            da.Fill(dt);
            chartLine.DataSource = dt;
            conn.Close();

            chartLine.Series[0].XValueMember = "date";
            chartLine.Series[0].YValueMembers = "totalcases";
            chartLine.Series[1].XValueMember = "date";
            chartLine.Series[1].YValueMembers = "numtests_total";

            chartLine.Titles.Add("COVID Cases and Testing in " + province + " Between " + startDate.ToShortDateString() + " - " + endDate.ToShortDateString()).ForeColor = Color.White;
            chartLine.Series[0].IsValueShownAsLabel = true;
            chartLine.Series[0]["LineLabelStyle"] = "outside";
            chartLine.Series[1].IsValueShownAsLabel = true;
            chartLine.Series[1]["LineLabelStyle"] = "outside";

   

            chartLine.Series[0].XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.DateTime;
            chartLine.Series[0].BorderWidth = 5;
            chartLine.Series[1].BorderWidth = 5;
        }

        // FUNCTION         : disableItems
        // DESCRIPTION      :
        //      disables all of the buttons / clickable elements within
        //      this main form of the application.
        // PARAMETERS       : none
        // RETURNS          : none
        private void disableItems()
        {
            comboBoxProvinces.Enabled = false;
            pieChartDatePicker.Enabled = false;
            lineChartEndDatePicker.Enabled = false;
            lineChartStartDatePicker.Enabled = false;
        }

        // FUNCTION         : enableItems
        // DESCRIPTION      :
        //      enables all of the buttons / clickable elements within
        //      this main form of the application.
        // PARAMETERS       : none
        // RETURNS          : none
        private void enableItems()
        {
            comboBoxProvinces.Enabled = true;
            pieChartDatePicker.Enabled = true;
            lineChartStartDatePicker.Enabled = true;
            lineChartEndDatePicker.Enabled = true;
        }

        // FUNCTION         : lineChartStartDatePicker_ValueChanged
        // DESCRIPTION      :
        //      This method is called when the user selects a new date on the
        //      start date picker for the line chart. It calls the function to verify
        //      the date selection, then updates the line chart data if valid.
        // PARAMETERS       :
        //      object sender
        //      RoutedEventargs e
        // RETURNS          : none
        private void lineChartStartDatePicker_ValueChanged(object sender, EventArgs e)
        {
            bool status = verifyDate(lineChartStartDatePicker.Value.ToShortDateString());
            if (status == false)
            {
                MessageBox.Show("Date selected has no corrosponding data. Please select a different date.");
            }
            else
            {

                startLineChart = lineChartStartDatePicker.Value;

                chartLine.Titles.RemoveAt(0);
                fillLineChart(startLineChart, endLineChart, comboBoxProvinces.Text);
            }
        }

        // FUNCTION         : lineChartEndDatePicker_ValueChanged
        // DESCRIPTION      :
        //      This method is called when the user selects a new date on the
        //      end date picker for the line chart. It calls the function to verify
        //      the date selection, then updates the line chart data if valid.
        // PARAMETERS       :
        //      object sender
        //      RoutedEventargs e
        // RETURNS          : none
        private void lineChartEndDatePicker_ValueChanged(object sender, EventArgs e)
        {
            bool status = verifyDate(lineChartEndDatePicker.Value.ToShortDateString());
            if (status == false)
            {
                MessageBox.Show("Date selected has no corrosponding data. Please select a different date.");
            }
            else
            {
                endLineChart = lineChartEndDatePicker.Value;

                chartLine.Titles.RemoveAt(0);
                fillLineChart(startLineChart, endLineChart, comboBoxProvinces.Text);
            }
        }

        // FUNCTION         : comboBoxProvinces_SelectionChangeCommitted
        // DESCRIPTION      :
        //      This method is called when the user selects a different element
        //      in the province combo box for the line chart. It calls the function
        //      to update the line chart data with the selected value.
        // PARAMETERS       :
        //      object sender
        //      RoutedEventargs e
        // RETURNS          : none
        private void comboBoxProvinces_SelectionChangeCommitted(object sender, EventArgs e)
        {
            chartLine.Titles.RemoveAt(0);
            fillLineChart(startLineChart, endLineChart, comboBoxProvinces.Text);
        }

        // FUNCTION         : btnLoadData_Click
        // DESCRIPTION      :
        //      This method is called when the user selects the load data
        //      button in the menu. It opens the other form for data selection.
        // PARAMETERS       :
        //      object sender
        //      RoutedEventargs e
        // RETURNS          : none
        private void btnLoadData_Click(object sender, EventArgs e)
        {
            LoadDataForm dataForm = new LoadDataForm();
            dataForm.Show();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }

    //
    // class name : Verifications
    // attributes: bool caseDataLoaded - whether or not case data is loaded
    //             bool testingDataLoaded - whether or not testing data is loaded
    //
    //  description: the purpose of this class is to transfer data from
    //              the LoadDataForm form to this Form1, so that this
    //              form can make decisions based on the actions in other
    //              forms.
    //
    public static class Verifications
    {
        public static bool caseDataLoaded { get; set; }
        public static bool testingDataLoaded { get; set; }
    }
}
