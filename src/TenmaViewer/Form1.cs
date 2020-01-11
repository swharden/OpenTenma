using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;
using System.Diagnostics;
using System.Threading;

namespace TenmaViewer
{
    public partial class Form1 : Form
    {
        private Thread readerThread = null;

        int nextValueIndex = 0;
        double[] values;

        public Form1()
        {
            InitializeComponent();

            values = new double[60 * 60 * 24]; // 1 day of data
            formsPlot1.plt.PlotSignal(values);

            readerThread = new Thread(new ThreadStart(ReadContinuously));
            readerThread.Start();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ScanPorts();

            if (Debugger.IsAttached)
            {
                PortCombo.SelectedIndex = PortCombo.Items.Count - 1;
                ConnectButton_Click(null, null);
            }
        }

        private void ClearButton_Click(object sender, EventArgs e)
        {
            nextValueIndex = 0;
            values = new double[60 * 60 * 24]; // 1 day of data
            formsPlot1.Render();
        }

        private void ScanPorts()
        {
            PortCombo.Items.Clear();
            PortCombo.Items.AddRange(SerialPort.GetPortNames());
            if (PortCombo.Items.Count > 0)
                PortCombo.SelectedIndex = 0;
        }

        TenmaReader.SerialReader reader = null;
        private void ConnectButton_Click(object sender, EventArgs e)
        {
            if (reader is null)
            {
                try
                {
                    string comPort = PortCombo.Text;
                    reader = new TenmaReader.SerialReader(comPort);
                    ConnectButton.Text = "Disconnect";
                }
                catch
                {
                    ConnectButton.Enabled = false;
                    ConnectButton.Text = "ERROR";
                    Application.DoEvents();
                    System.Threading.Thread.Sleep(2000);
                    ConnectButton.Text = "Connect";
                    ConnectButton.Enabled = true;
                }
            }
            else
            {
                reader.Dispose();
                ConnectButton.Text = "Connect";
            }
        }

        private void ReadContinuously()
        {
            while (true)
            {
                if (reader is null)
                {
                    Thread.Sleep(100);
                }
                else
                {
                    var reading = reader.Read();
                    values[nextValueIndex++] = reading.value;
                    ValueLabel.BeginInvoke((MethodInvoker)delegate () { ValueLabel.Text = reading.ToString(); });
                    StatusLabel.Text = string.Format("Read #{0}: {1}", reader.readCount, reading);
                }
            }
        }

        int lastPlottedIndex = 0;
        private void PlotTimer_Tick(object sender, EventArgs e)
        {
            if (nextValueIndex == lastPlottedIndex)
                return;
            formsPlot1.Render();
        }
    }
}
