using System;
using System.IO.Ports;
using System.Diagnostics;
using System.Collections.Generic;

namespace TenmaLogger
{

    class Program
    {
        static void Main(string[] args)
        {
            // I measured these in real life with the meter

            var codesAndValues = new List<Tuple<string, double>>
            {
                // voltage (V)
                Tuple.Create("00114;80:", +0.114),
                Tuple.Create("00112;80:", +0.112),
                Tuple.Create("00097;80:", +0.097),
                Tuple.Create("00014;80:", +0.014),
                Tuple.Create("00001;80:", +0.001),
                Tuple.Create("00004;808", +0.004),
                Tuple.Create("00000;<0:", -0.000),
                Tuple.Create("00000;80:", +0.000),
                Tuple.Create("10627;80:", +06.27),
                Tuple.Create("10626;<0:", -06.26),
                Tuple.Create("11643;80:", +16.43),
                Tuple.Create("40025;808", +002.5 * 1e-3),
                Tuple.Create("40034;808", +003.4 * 1e-3),

                // capacitance (nF)
                Tuple.Create("000726802", 0.072),
                Tuple.Create("011246802", 1.124),
                Tuple.Create("155366802", 55.36),
                Tuple.Create("421886802", 21.88),
                Tuple.Create("510136802", 101.3 * 1e3),

                // resistance (Ohms)
                Tuple.Create("000033802", 000.3),
                Tuple.Create("000183802", 001.8),
                Tuple.Create("015943802", 159.4),
                Tuple.Create("107843802", 0.784 * 1e3),
                Tuple.Create("107823802", 0.782 * 1e3),
                Tuple.Create("121733802", 2.173 * 1e3),
                Tuple.Create("155393802", 5.539 * 1e3),
                Tuple.Create("209493802", 09.49 * 1e3),
                Tuple.Create("210403802", 10.40 * 1e3),
                Tuple.Create("304733802", 047.3 * 1e3),
                Tuple.Create("426223802", 2.622 * 1e6),
                Tuple.Create("560003902", double.PositiveInfinity),

                // temperature (C)
                Tuple.Create("000224800", +22.0),
                Tuple.Create("000234800", +23.0),

                // low current (mA)
                Tuple.Create("00000?<0:", -0.00),
                Tuple.Create("00000?80:", +0.00),

                // high current (A)
                Tuple.Create("000009<08", -0.00),
                Tuple.Create("000009808", +0.00),

                // frequency (Hz)
                Tuple.Create("000002802", 0.0),
                Tuple.Create("000252802", 25.0),
                Tuple.Create("428702802", 28.70 * 1e6),
            };

            foreach (var codeAndValue in codesAndValues)
            {
                var reading = new TenmaReader.Reading(codeAndValue.Item1);
                double measuredValue = codeAndValue.Item2;
                double error = Math.Abs(reading.value - (float)measuredValue);
                if (error > 1e-12)
                    Console.WriteLine($"{reading.code} ERROR: {measuredValue} != {reading.value}");
                else
                    Console.WriteLine($"{reading.code} = {reading}");
            }
        }

    }
}
