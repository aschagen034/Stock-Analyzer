using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Windows.Forms.DataVisualization.Charting;

namespace Project2_Stocks
{
    public partial class Form_input : Form
    {
        // List to store all candlestick data
        List<Candlestick> allCandlesticks = new List<Candlestick>();

        // Constructor that initializes the Form_input and its components
        public Form_input()
        {
            InitializeComponent();

        }

        // Event handler for the form load event
        public void Form_input_Load(object sender, EventArgs e)
        {
        }

        // Event handler for the button click event to load a file
        public void button_load_Click(object sender, EventArgs e)
        {
            // Opens a file dialog to allow the user to select a stock data file
            openFileDialog1_load.Multiselect = true; // Allow multiple file selection
            openFileDialog1_load.ShowDialog();

        }

        // Event handler for when a file is selected and the dialog is confirmed
        public void openFileDialog1_load_FileOk(object sender, CancelEventArgs e)
        {
            foreach (var FileName in openFileDialog1_load.FileNames)
            {
                // Load stock data from the selected CSV file and reverse the order for display
                allCandlesticks = CandlestickLoader.LoadFromCsv(FileName);

                // Check if the data is in reverse chronological order
                if (allCandlesticks.Count >= 2 && allCandlesticks[0].Date > allCandlesticks[1].Date)
                {
                    // If the first date is later than the second, reverse the list
                    allCandlesticks.Reverse();
                }

                // Convert Candlestick objects to SmartCandlestick
                List<SmartCandlestick> smartCandlesticks = allCandlesticks.Select(c => new SmartCandlestick(c)).ToList();

                // Get the start and end dates from the DateTimePickers
                DateTime startDate = dateTimePicker_startDate.Value;
                DateTime endDate = dateTimePicker_endDate.Value;

                // Filter the smart candlesticks based on the selected date range
                List<SmartCandlestick> filteredCandlesticks = FilterCandlesticks(smartCandlesticks, startDate, endDate);

                // Extract the stock name from the filename to use for the chart title
                string stockName = Path.GetFileNameWithoutExtension(FileName);

                BindingList<SmartCandlestick> boundlist = new BindingList<SmartCandlestick>(filteredCandlesticks);

                // Create and initialize a Form_display instance to show the data
                Form_display displayForm = new Form_display(FileName, filteredCandlesticks);

                // Set up the chart in Form_display with the filtered candlesticks
                displayForm.SetChartDataSource(filteredCandlesticks, stockName);

                // Show the display form as a dialog
                displayForm.Show();
            }
        }

        // Method to filter the list of smart candlesticks based on the start and end date
        public List<SmartCandlestick> FilterCandlesticks(List<SmartCandlestick> candlesticks, DateTime startDate, DateTime endDate)
        {
            // Use LINQ to filter candlesticks within the specified date range
            return candlesticks.Where(c => c.Date >= startDate && c.Date <= endDate).ToList();
        }


       
      
        
    }
}
