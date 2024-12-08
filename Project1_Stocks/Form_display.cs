using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Project2_Stocks
{
    // Form to display the stock data in a Chart
    public partial class Form_display : Form
    {
        // Store the original unfiltered data to allow dynamic filtering
        private BindingList<SmartCandlestick> originalData;
        private string fileName;
        private List<SmartCandlestick> filteredCandlesticks;
        private string stockName;


        // Constructor initializes form and configures chart areas
        public Form_display(string filename, List<SmartCandlestick> allCandlesticks)
        {
            InitializeComponent();  // Initialize form components (UI elements)
            ConfigureChartAreas();  // Set up chart areas for displaying OHLC and volume data

            // Store the original list of smart candlestick data in a BindingList for easy data binding and filtering
            this.originalData = new BindingList<SmartCandlestick>(allCandlesticks);

            //Store the filtered candlesticks
            this.filteredCandlesticks = allCandlesticks;

            // Extract the stock name from the filename to use for display
            string stockName = Path.GetFileNameWithoutExtension(filename);

            this.Text = stockName;  // Set the form's title bar text

            // Initialize chart_Data
            if (chart_Data == null)
            {
                chart_Data = new Chart();
                this.Controls.Add(chart_Data);
            }
            PopulateComboBox();

            // Add event handler for ComboBox selection change
            comboBox1.SelectedIndexChanged += comboBox1_SelectedIndexChanged_1;
        }
    
        private void PopulateComboBox()
        {
            comboBox1.Items.Add("None");
            comboBox1.Items.Add("Bullish");
            comboBox1.Items.Add("Bearish");
            comboBox1.Items.Add("Marubozu");
            comboBox1.Items.Add("Hammer");
            comboBox1.Items.Add("Doji");
            comboBox1.Items.Add("Dragonfly Doji");
            comboBox1.Items.Add("Gravestone Doji");
            comboBox1.SelectedIndex = -1; // Set default selection
        }

        // Configures the chart areas for displaying candlestick and volume data with synchronized axes.
        private void ConfigureChartAreas()
        {
            // Ensure chart_Data is initialized
            if (chart_Data == null)
            {
                chart_Data = new Chart();
                this.Controls.Add(chart_Data);
            }

            // Clear any pre-existing chart areas
            chart_Data.ChartAreas.Clear();

            // OHLC Chart Area for candlestick data
            ChartArea chartAreaOHLC = new ChartArea("ChartArea_OHLC")
            {
                AxisX = {
                    LabelStyle = { Format = "MM/dd/yyyy" },     // Format X-axis labels as dates
                    IntervalType = DateTimeIntervalType.Days,  // Sets X-axis interval to days
                    MajorGrid = { LineDashStyle = ChartDashStyle.Dot } // Add grid lines for better readability
                },
                AxisY = { Title = "Price" } // Labels the Y-axis as "Price"
            };
            chart_Data.ChartAreas.Add(chartAreaOHLC);   // Adds the OHLC chart area to the chart

            // Configure ChartArea for Volume
            ChartArea chartAreaVolume = new ChartArea("ChartArea_Volume")
            {
                AxisX = {
                    Enabled = AxisEnabled.True,          // Enables the X-axis, synchronized with the OHLC area
                    LabelStyle = { Format = "MM/dd/yyyy" },
                    MajorGrid = { LineDashStyle = ChartDashStyle.Dot } // Add grid lines for better readability
                },
                AxisY = { Title = "Volume" }    // Labels the Y-axis as "Volume"
            };
            chart_Data.ChartAreas.Add(chartAreaVolume); // Adds the Volume chart area to the chart

            // Synchronizes the X-axis of Volume with OHLC for aligned date display
            chartAreaVolume.AlignWithChartArea = "ChartArea_OHLC";
            chartAreaVolume.AlignmentOrientation = AreaAlignmentOrientations.Vertical;

            // Remove the legend from the chart
            chart_Data.Legends.Clear();
        }

        // Configures and displays candlestick and volume data in the chart
        public void SetChartDataSource(List<SmartCandlestick> dataSource, string stockName)
        {
            // Ensure chart_Data is initialized
            if (chart_Data == null)
            {
                chart_Data = new Chart();
                this.Controls.Add(chart_Data);
            }

            chart_Data.Series.Clear();
            //chart_Data.Titles.Clear();

            // Volume data series (column style) in Volume chart area
            Series volumeSeries = new Series("Volume")
            {
                ChartType = SeriesChartType.Column,
                XValueMember = "Date",
                YValueMembers = "Volume",
                ChartArea = "ChartArea_Volume",
                XValueType = ChartValueType.DateTime
            };
            chart_Data.Series.Add(volumeSeries);

            // Candlestick series in OHLC chart area
            Series candlestickSeries = new Series("Candlestick")
            {
                ChartType = SeriesChartType.Candlestick,
                XValueMember = "Date",
                YValueMembers = "High,Low,Open,Close",
                ChartArea = "ChartArea_OHLC",
                XValueType = ChartValueType.DateTime
            };

            candlestickSeries["PriceUpColor"] = "Lime";     // Green for bullish
            candlestickSeries["PriceDownColor"] = "Red";    // Red for bearish

            // Add the configured candlestick series to the chart
            // This step ensures the series, which defines the data visualization type and its properties, is included in the chart
            chart_Data.Series.Add(candlestickSeries);

            // Set the data source for the chart to the provided dataset
            // The data source contains the candlestick data, including open, high, low, and close values
            chart_Data.DataSource = dataSource;

            // Bind the data source to the chart
            // This step connects the dataset to the chart elements, allowing the chart to render the candlestick data
            chart_Data.DataBind();


            // Apply color to each candlestick and add annotations for peaks and valleys
            for (int i = 0; i < dataSource.Count; i++)
            {
                Color candlestickColor = dataSource[i].Close > dataSource[i].Open ? Color.Lime : Color.Red;
                candlestickSeries.Points[i].Color = candlestickColor;

                // Add annotations for peaks and valleys
                if (dataSource[i].IsPeak(dataSource, i))
                {
                    AddAnnotation(chart_Data, dataSource[i].Date, (double)dataSource[i].High, "Peak", Color.Green);
                }
                if (dataSource[i].IsValley(dataSource, i))
                {
                    AddAnnotation(chart_Data, dataSource[i].Date, (double)dataSource[i].Low, "Valley", Color.Red);
                }
            }
            //Normalize the Y-axis
            double minY = dataSource.Min(c => (double)c.Low);
            double maxY = dataSource.Max(c => (double)c.High);

            //Set the Y-axis minimum to the lowest value in the dataset and
            // the Y-axis maximum to the highest value in the dataset
            chart_Data.ChartAreas["ChartArea_OHLC"].AxisY.Minimum = minY;
            chart_Data.ChartAreas["ChartArea_OHLC"].AxisY.Maximum = maxY;

            

            // Add a title to the candlestick chart with the stock name
            Title candlestickTitle = new Title
            {
                Text = stockName,
                Docking = Docking.Top,
                Font = new Font("Arial", 12, FontStyle.Bold),
                ForeColor = Color.Black
            };
            //Add the configured title to the chart
            chart_Data.Titles.Add(candlestickTitle);
        }

        // Method to add annotations to the chart
        private void AddAnnotation(Chart chart, DateTime date, double price, string label, Color color)
        {
            // Ensure the ChartArea exists before adding the annotation
            if (!chart.ChartAreas.Any(ca => ca.Name == "ChartArea_OHLC"))
            {
                throw new ArgumentException("A chart element with the name 'ChartArea_OHLC' could not be found in the 'ChartAreaCollection'.");
            }
            //Create a text annotation for the chart
            TextAnnotation annotation = new TextAnnotation
            {
                Text = label, // Set the text of the annotation
                ForeColor = color, // Set the text color
                                   // Anchor the annotation to a specific data point in the "Candlestick" series
                AnchorDataPoint = chart.Series["Candlestick"].Points.First(p => p.XValue == date.ToOADate()),
                AnchorY = price, // Set the Y-axis anchor to the specified price
                AnchorX = date.ToOADate(), // Set the X-axis anchor to the specified date
                Alignment = ContentAlignment.MiddleCenter, // Align the text to the center
                Font = new Font("Arial", 8, FontStyle.Bold) // Set the font properties for the annotation
            };
            //Add the text annotation to the chart
            chart.Annotations.Add(annotation);

            // Add horizontal line annotation
            HorizontalLineAnnotation line = new HorizontalLineAnnotation
            {
                AxisX = chart.ChartAreas["ChartArea_OHLC"].AxisX, // Set the X-axis reference to ensure the line is placed correctly on the chart
                AxisY = chart.ChartAreas["ChartArea_OHLC"].AxisY, // Set the Y-axis reference to align the line with the specified price level
                IsInfinitive = true, // Configure the line to extend infinitely across the chart area horizontally
                ClipToChartArea = "ChartArea_OHLC", // Restrict the line to be drawn only within the "ChartArea_OHLC" area
                LineColor = color, // Set the color of the line to match the annotation color for consistency
                LineWidth = 1, // Specify the thickness of the line
                AnchorY = price  // Anchor the line to the specified Y-axis price value
            };
            //Add the configured horizontal line annotation to the chart
            chart.Annotations.Add(line);
        }

        // Event handler for Update button - filters data and updates chart with selected date range.
        private void button_Update_click(object sender, EventArgs e)
        {
            DateTime startDate = dateTimePicker_startDate.Value;    // Get selected start date from date picker
            DateTime endDate = dateTimePicker_endDate.Value;        // Get selected end date from date picker

            // Filters original data based on selected date range
            List<SmartCandlestick> filteredData = originalData.Where(c => c.Date >= startDate && c.Date <= endDate).ToList();

            // Updates chart with filtered data
            SetChartDataSource(filteredData, stockName);


        }

        //private void SetChartDataSource(List<SmartCandlestick> filteredData, object stockName)
        //{
        //    throw new NotImplementedException();
        //}

       

        // This event handler is triggered whenever the text in textBox1 is changed.
        // You can add code here to handle user input in real time, such as filtering
        // the data or performing a search based on the text entered.
        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            //MessageBox.Show("ComboBox selection changed!"); // Debugging message to confirm event firing

            string selectedPattern = comboBox1.SelectedItem.ToString(); // Retrieve the selected pattern from the ComboBox

            // Check if "None" is selected, and remove any pattern annotations if true
            if (selectedPattern == "None")
            {
                // Loop through annotations in reverse order to safely remove them
                for (int i = chart_Data.Annotations.Count - 1; i >= 0; i--)
                {
                    if (chart_Data.Annotations[i].Name.StartsWith("PatternArrow"))
                    {
                        chart_Data.Annotations.RemoveAt(i); // Remove the annotation
                    }
                }
                chart_Data.Invalidate(); // Refresh the chart after removing annotations
                //MessageBox.Show("Annotations cleared!"); // Debugging message
            }
            else
            {
                //MessageBox.Show("Displaying pattern: " + selectedPattern); // Debugging message
                DisplayPattern(selectedPattern); // Display the selected pattern annotations
            }
        }


        private void DisplayPattern(string pattern)
        {
            // Ensure the smartCandlestickList is not null
            if (filteredCandlesticks == null || filteredCandlesticks.Count == 0)
            {
                MessageBox.Show("No data available or smartCandlestickList is empty.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return; // Exit if list is null or empty
            }

            // Debugging: Confirm method call
            //MessageBox.Show("Displaying pattern: " + pattern + " with " + filteredSmartCandlesticks.Count + " candlesticks.");

            // Remove all existing pattern-related annotations before adding new ones
            List<Annotation> annotationsToRemove = new List<Annotation>();
            foreach (var annotation in chart_Data.Annotations)
            {
                if (annotation.Name.StartsWith("PatternArrow"))
                {
                    annotationsToRemove.Add(annotation); // Add pattern annotations to the removal list
                }
            }
            foreach (var annotation in annotationsToRemove)
            {
                chart_Data.Annotations.Remove(annotation); // Remove each annotation
            }

            bool patternFound = false; // Initialize a flag to track whether a pattern is found
            int annotationIndex = 0; // Index each annotation

            // Iterate through the candlestick list to find and annotate the selected pattern
            foreach (var candlestick in filteredCandlesticks)
            {
                // Check if the current candlestick matches the selected pattern
                if (candlestick.IdentifyPattern() == pattern)
                {
                    patternFound = true;

                    // Find the corresponding data point for the candlestick high value
                    var point = chart_Data.Series["Candlestick"].Points.FirstOrDefault(p => p.YValues[0] == (double)candlestick.High);

                    if (point != null)
                    {
                        // Create a new arrow annotation for the pattern
                        var annotation = new ArrowAnnotation
                        {
                            Name = "PatternArrow" + annotationIndex++, // Ensure unique names for annotations
                            AnchorDataPoint = point,
                            LineColor = Color.Blue, // Set the arrow color to blue
                            ArrowSize = 5, // Set the size of the arrowhead
                            Width = 0,
                            Height = -10, // Negative height to point downwards
                            LineWidth = 2, // Set line width for annotation
                            AxisX = chart_Data.ChartAreas[0].AxisX, // Link to the chart's X-axis
                            AxisY = chart_Data.ChartAreas[0].AxisY // Link to the chart's Y-axis
                        };
                        chart_Data.Annotations.Add(annotation); // Add the annotation to the chart
                    }
                    else
                    {
                        // Debugging information if the data point for the high value is not found
                        //MessageBox.Show($"DataPoint not found for high value: {candlestick.High}", "Debug - Data Point Not Found", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }

            if (!patternFound)
            {
                // Debugging information if no matching patterns are found
                MessageBox.Show($"No candlesticks matched the pattern: {pattern}", "Debug - No Pattern Found", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            chart_Data.Invalidate(); // Refresh the chart to reflect the added annotations
        }

       
    }
}