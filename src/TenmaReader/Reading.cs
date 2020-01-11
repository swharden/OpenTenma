using System;
using System.Collections.Generic;
using System.Text;

namespace TenmaReader
{
    public class Reading
    {
        public readonly float value;
        public readonly string units;
        public readonly string code;

        public Reading(string line)
        {
            code = line.Trim();

            if (code.Length != 9)
                throw new ArgumentException("line is expected to contain 9 characters");

            int multiplierIndex = int.Parse(code.Substring(0, 1));
            int digits = int.Parse(code.Substring(1, 4));
            string mode = code.Substring(5, 1);

            double[] multipliers;
            if (mode == ";")
            {
                multipliers = new double[] { .001, .01, double.NaN, double.NaN, .0001, double.NaN };
                units = "V";
            }
            else if (mode == "6")
            {
                multipliers = new double[] { .001, .01, double.NaN, double.NaN, .01, 100 };
                value = (float)(digits * multipliers[multiplierIndex]);
                units = "nF";
            }
            else if (mode == "3")
            {
                multipliers = new double[] { .1, 1, 10, 100, 1000, double.PositiveInfinity };
                value = (float)(digits * multipliers[multiplierIndex]);
                units = "Ohm";
            }
            else if (mode == "2")
            {
                multipliers = new double[] { 1, 10, 100, 1_000, 10_000, 100_000 };
                units = "Hz";
            }
            else if (mode == "?")
            {
                multipliers = new double[] { 1, double.NaN, double.NaN, double.NaN, double.NaN, double.NaN };
                units = "mA";
            }
            else if (mode == "9")
            {
                multipliers = new double[] { 1, double.NaN, double.NaN, double.NaN, double.NaN, double.NaN };
                units = "A";
            }
            else if (mode == "4")
            {
                multipliers = new double[] { 1, double.NaN, double.NaN, double.NaN, double.NaN, double.NaN };
                units = "C";
            }
            else
            {
                throw new NotImplementedException("unknown mode");
            }

            value = (float)(digits * multipliers[multiplierIndex]);

            if (code.Substring(6, 1) == "<")
                value *= -1;
        }

        private string Abbreviate(double value, string units)
        {
            if (value > 1_000_000)
            {
                value /= 1_000_000;
                units = "M" + units;
            }
            else if (value > 1_000)
            {
                value /= 1_000;
                units = "k" + units;
            }

            return string.Format("{0:0.000} {1}", value, units);
        }

        public override string ToString()
        {
            return Abbreviate(value, units);
        }
    }
}
