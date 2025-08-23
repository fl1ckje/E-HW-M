using OpenHardwareMonitor.Hardware;
using System;
using System.Drawing;
using System.IO;
using System.IO.Ports;
using System.Threading;
using System.Windows.Forms;

namespace E_HW_M
{
	public partial class MainForm : Form
	{
		private SerialPort serialPort;
		private bool isConnected;
		private Thread monitorThread;
		private readonly object lockObject = new object();
		private readonly Computer computer;
		private UpdateVisitor updateVisitor;

		private readonly string systemPath = Path.GetPathRoot(Environment.SystemDirectory);
		private readonly double bytes2gbytes = Math.Pow(1024.0, 3);

		public MainForm()
		{
			InitializeComponent();
			UpdateComPortsList();

			updateVisitor = new UpdateVisitor();
			computer = new Computer
			{
				CPUEnabled = true,
				GPUEnabled = true,
				RAMEnabled = true,
			};
			computer.Open();
		}

		private void UpdateComPortsList()
		{
			if(comComboBox.InvokeRequired)
			{
				comComboBox.Invoke((MethodInvoker)delegate
				{
					UpdateComPortsList();
				});
				return;
			}

			comComboBox.Items.Clear();
			foreach(string portName in SerialPort.GetPortNames())
			{
				comComboBox.Items.Add(portName);
			}

			if(comComboBox.Items.Count > 0)
			{
				comComboBox.Enabled = true;
				connectButton.Enabled = true;
			}
			else
			{
				comComboBox.Items.Add("");
				comComboBox.Enabled = false;
				connectButton.Enabled = false;
			}
			comComboBox.SelectedIndex = 0;
		}

		private void InitializeSerialPort(string portName)
		{
			serialPort?.Dispose(); // Clean up existing port if any

			serialPort = new SerialPort(portName)
			{
				BaudRate = 9600,
				ReadTimeout = 500,
				WriteTimeout = 500
			};
		}

		private void MonitorStats()
		{
			while(isConnected)
			{
				string statsData = GetFormattedData();

				lock(lockObject)
				{
					if(serialPort.IsOpen)
					{
						txDataTextBox.Text = statsData; // TODO: debug for thread interaction
						serialPort.WriteLine(statsData);
					}
				}

				Thread.Sleep(2100); // Update every second
			}
			Disconnect();
		}


		private string GetFormattedData()
		{
			updateVisitor = new UpdateVisitor();
			computer.Accept(updateVisitor);

			int? cpuLoad = null,
				cpuTemp = null,
				gpuLoad = null,
				gpuTemp = null,
				usedRam = null,
				totalRam = null;
			int usedSpace, totalSpace;

			// Collect hardware data except drive info
			foreach(IHardware hw in computer.Hardware)
			{
				switch(hw.HardwareType)
				{
					// CPU data collection
					case HardwareType.CPU:
						foreach(ISensor sensor in hw.Sensors)
						{
							if(sensor.SensorType == SensorType.Load && sensor.Name.Contains("Total"))
								cpuLoad = NearestInt(sensor.Value);

							if(sensor.SensorType == SensorType.Temperature)
								cpuTemp = NearestInt(sensor.Value);
						}
						break;

					// GPU data collection
					case HardwareType.GpuNvidia:
					case HardwareType.GpuAti:
						foreach(ISensor sensor in hw.Sensors)
						{
							if(sensor.SensorType == SensorType.Load)
								gpuLoad = NearestInt(sensor.Value);

							if(sensor.SensorType == SensorType.Temperature)
								gpuTemp = NearestInt(sensor.Value);
						}
						break;

					// RAM data collection
					case HardwareType.RAM:
						foreach(ISensor sensor in hw.Sensors)
						{
							if(sensor.SensorType == SensorType.Data)
							{
								if(sensor.Name.Contains("Used"))
									usedRam = NearestInt(sensor.Value);
								if(sensor.Name.Contains("Available"))
									totalRam = usedRam + NearestInt(sensor.Value);
							}
						}
						break;
					default:
						break;
				}
			}

			DriveInfo drive = new DriveInfo(systemPath);
			usedSpace = (int)(drive.AvailableFreeSpace / bytes2gbytes);
			totalSpace = (int)(drive.TotalSize / bytes2gbytes);


			// Format the string according to requirements
			return $"{cpuTemp?.ToString() ?? "-1"};" +
				   $"{cpuLoad?.ToString() ?? "-1"};" +
				   $"{gpuTemp?.ToString() ?? "-1"};" +
				   $"{gpuLoad?.ToString() ?? "-1"};" +
				   $"{usedRam?.ToString() ?? "-1"};" +
				   $"{totalRam?.ToString() ?? "-1"};" +
				   $"{usedSpace};" +
				   $"{totalSpace}";
		}

		protected override void OnFormClosing(FormClosingEventArgs e)
		{
			Disconnect();
			computer.Close();
			base.OnFormClosing(e);
		}

		private void UpdateComButtonClick(object sender, EventArgs e) => UpdateComPortsList();

		private void ConnectButtonClick(object sender, EventArgs e)
		{
			if(!isConnected)
			{
				try
				{
					string selectedPort = comComboBox.SelectedItem?.ToString();
					InitializeSerialPort(selectedPort);
					serialPort.Open();
					isConnected = true;
					connectButton.Text = "Disconnect";
					connectionStatusLabel.ForeColor = Color.Green;
					connectionStatusLabel.Text = "Connected";

					// Start monitoring thread
					monitorThread = new Thread(MonitorStats);
					monitorThread.Start();
				}
				catch(Exception ex)
				{
					MessageBox.Show($"Error connecting to board: {ex.Message}");
				}
			}
			else
			{
				Disconnect();
			}
		}

		private void Disconnect()
		{
			isConnected = false;
			if(monitorThread != null && monitorThread.IsAlive)
			{
				monitorThread.Join(1000);
			}

			if(serialPort != null && serialPort.IsOpen)
			{
				serialPort?.Close();
				serialPort?.Dispose();
			}

			connectButton.Text = "Connect";
			connectionStatusLabel.ForeColor = Color.Red;
			connectionStatusLabel.Text = "Disconnected";

			UpdateComPortsList();  // Refresh ports after disconnection
		}

		private int? NearestInt(float? val) => (int?)Math.Round((double)val, 0);
	}
}

public class UpdateVisitor : IVisitor
{
	public void VisitComputer(IComputer computer)
	{
		computer.Traverse(this);
	}

	public void VisitHardware(IHardware hardware)
	{
		hardware.Update();
		foreach(IHardware subHardware in hardware.SubHardware) subHardware.Accept(this);
	}

	public void VisitSensor(ISensor sensor) { }
	public void VisitParameter(IParameter parameter) { }
}