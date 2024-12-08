namespace Project2_Stocks
{
    partial class Form_display
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.chart_Data = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.dataGridView_candlesticks = new System.Windows.Forms.DataGridView();
            this.dateTimePicker_startDate = new System.Windows.Forms.DateTimePicker();
            this.dateTimePicker_endDate = new System.Windows.Forms.DateTimePicker();
            this.button_Update = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.candlestickLoaderBindingSource = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.chart_Data)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_candlesticks)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.candlestickLoaderBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // chart_Data
            // 
            chartArea1.AlignmentOrientation = ((System.Windows.Forms.DataVisualization.Charting.AreaAlignmentOrientations)((System.Windows.Forms.DataVisualization.Charting.AreaAlignmentOrientations.Vertical | System.Windows.Forms.DataVisualization.Charting.AreaAlignmentOrientations.Horizontal)));
            chartArea1.AlignmentStyle = System.Windows.Forms.DataVisualization.Charting.AreaAlignmentStyles.Cursor;
            chartArea1.Name = "ChartArea_OHLC";
            chartArea2.AlignWithChartArea = "ChartArea_OHLC";
            chartArea2.Name = "Volume";
            this.chart_Data.ChartAreas.Add(chartArea1);
            this.chart_Data.ChartAreas.Add(chartArea2);
            this.chart_Data.Dock = System.Windows.Forms.DockStyle.Top;
            this.chart_Data.Location = new System.Drawing.Point(0, 0);
            this.chart_Data.Name = "chart_Data";
            series1.ChartArea = "ChartArea_OHLC";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Candlestick;
            series1.Name = "Series1";
            series1.YValuesPerPoint = 4;
            series2.ChartArea = "Volume";
            series2.Name = "Series2";
            this.chart_Data.Series.Add(series1);
            this.chart_Data.Series.Add(series2);
            this.chart_Data.Size = new System.Drawing.Size(1335, 520);
            this.chart_Data.TabIndex = 0;
            this.chart_Data.Text = "chart1";
            // 
            // dataGridView_candlesticks
            // 
            this.dataGridView_candlesticks.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_candlesticks.Location = new System.Drawing.Point(12, 362);
            this.dataGridView_candlesticks.Name = "dataGridView_candlesticks";
            this.dataGridView_candlesticks.RowHeadersWidth = 51;
            this.dataGridView_candlesticks.Size = new System.Drawing.Size(776, 200);
            this.dataGridView_candlesticks.TabIndex = 1;
            // 
            // dateTimePicker_startDate
            // 
            this.dateTimePicker_startDate.Location = new System.Drawing.Point(506, 571);
            this.dateTimePicker_startDate.Name = "dateTimePicker_startDate";
            this.dateTimePicker_startDate.Size = new System.Drawing.Size(278, 22);
            this.dateTimePicker_startDate.TabIndex = 2;
            this.dateTimePicker_startDate.Value = new System.DateTime(2022, 1, 1, 0, 0, 0, 0);
            // 
            // dateTimePicker_endDate
            // 
            this.dateTimePicker_endDate.Location = new System.Drawing.Point(506, 673);
            this.dateTimePicker_endDate.Name = "dateTimePicker_endDate";
            this.dateTimePicker_endDate.Size = new System.Drawing.Size(278, 22);
            this.dateTimePicker_endDate.TabIndex = 3;
            // 
            // button_Update
            // 
            this.button_Update.AutoSize = true;
            this.button_Update.Location = new System.Drawing.Point(593, 621);
            this.button_Update.Margin = new System.Windows.Forms.Padding(4);
            this.button_Update.Name = "button_Update";
            this.button_Update.Size = new System.Drawing.Size(75, 26);
            this.button_Update.TabIndex = 4;
            this.button_Update.Text = "Update";
            this.button_Update.UseVisualStyleBackColor = true;
            this.button_Update.Click += new System.EventHandler(this.button_Update_click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(400, 571);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 22);
            this.textBox1.TabIndex = 5;
            this.textBox1.Text = "Start Date";
            this.textBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(400, 673);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(100, 22);
            this.textBox2.TabIndex = 6;
            this.textBox2.Text = "End Date";
            this.textBox2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(811, 621);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 24);
            this.comboBox1.TabIndex = 7;
            this.comboBox1.Text = "Select Pattern"; 
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged_1);
            // 
            // candlestickLoaderBindingSource
            // 
            this.candlestickLoaderBindingSource.DataSource = typeof(Project2_Stocks.CandlestickLoader);
            // 
            // Form_display
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1335, 707);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.button_Update);
            this.Controls.Add(this.dateTimePicker_endDate);
            this.Controls.Add(this.dateTimePicker_startDate);
            this.Controls.Add(this.chart_Data);
            this.Name = "Form_display";
            this.Text = " ";
            ((System.ComponentModel.ISupportInitialize)(this.chart_Data)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_candlesticks)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.candlestickLoaderBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        public System.Windows.Forms.DataVisualization.Charting.Chart chart_Data;
        public System.Windows.Forms.DataGridView dataGridView_candlesticks;
        private System.Windows.Forms.BindingSource candlestickLoaderBindingSource;
        private System.Windows.Forms.DateTimePicker dateTimePicker_startDate;
        private System.Windows.Forms.DateTimePicker dateTimePicker_endDate;
        private System.Windows.Forms.Button button_Update;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.ComboBox comboBox1;
    }
}