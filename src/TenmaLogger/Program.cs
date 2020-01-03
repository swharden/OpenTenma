using System;
using System.IO.Ports;
using System.Diagnostics;

namespace TenmaLogger
{
    class Program
    {
        static void Main(string[] args)
        {
            var ser = new SerialPort()
            {
                PortName = "COM2",
                BaudRate = 19230,
                Parity = Parity.Odd,
                DataBits = 7,
                ReadTimeout = 1500,
                DtrEnable = true,
                RtsEnable = false
            };

            ser.Open();

            for (int i = 0; i < 10; i++)
            {
                string line = ReadValue(ser);
                Console.WriteLine($"{i}: {line}");
            }
        }

        static string ReadValue(SerialPort ser)
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
