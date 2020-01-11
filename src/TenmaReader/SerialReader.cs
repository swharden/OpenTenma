using System;
using System.Collections.Generic;
using System.Text;
using System.IO.Ports;
using System.Diagnostics;

namespace TenmaReader
{
    public class SerialReader : IDisposable
    {
        readonly SerialPort ser;

        public SerialReader(string portName = "COM1")
        {
            ser = new SerialPort()
            {
                PortName = portName,
                BaudRate = 19230,
                Parity = Parity.Odd,
                DataBits = 7,
                ReadTimeout = 2500,
                DtrEnable = true,
                RtsEnable = false
            };
            ser.Open();
        }

        public void Dispose()
        {
            ser.Close();
        }

        public Reading Read()
        {
            string line = ReadLine();
            if (line is null)
                return null;
            else
                return new Reading(line);
        }

        public string ReadLine()
        {
            // a valid line:
            //   * happens twice in a row identically
            //   * has < 100 ms between them
            //   * has 10 characters per line (+CR, +LF)

            Stopwatch stopwatch = new Stopwatch();
            string line1 = ser.ReadLine();
            stopwatch.Restart();
            string line2 = ser.ReadLine();
            stopwatch.Stop();

            // lines were separated for than expected
            if (stopwatch.ElapsedMilliseconds > 100)
                line1 = ser.ReadLine();

            if (string.Compare(line1, line2) != 0)
                return null; // strings dont match

            return line1;
        }
    }
}
