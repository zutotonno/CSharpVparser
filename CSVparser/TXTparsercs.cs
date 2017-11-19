
using System;
using System.Globalization;
using System.IO;
using System.Linq;

namespace CSVparser
{
    public class TXTparsercs
    {
        string[] fileFrom;
        double[,] matrix;
        int DIMR = 0;
        int DIMC = 0;

        public TXTparsercs(int i, int j)
        {
            DIMR = i;
            DIMC = j;
        }
        public TXTparsercs() { }


        public string[] readLabel(string path)
        {
            try
            {
                var csv = new StreamReader(path);
                string fields = csv.ReadLine();
                var values = fields.Split(' ');
                DIMC = values.Length;
                return values;

            }
            catch (Exception)
            {

                return null;
            }

        }
        public double[,] readCSV(int skip, string path)
        {
            var csv = new StreamReader(path);
            var csvAux = new StreamReader(path);
            DIMR -= skip;
            string aux;
            string[] count;
            while (!csvAux.EndOfStream)
            {
                DIMR++;
                aux = csvAux.ReadLine();
                count = aux.Split(',');
                DIMC = count.Count();
            }
            matrix = new double[DIMR, DIMC];

            int k = 0;
            for (int i = 0; i < skip; i++)
            {
                csv.ReadLine();
            }
            while (!csv.EndOfStream)
            {
                string fields = csv.ReadLine();
                var values = fields.Split(',');
                int j = 0;
                double[] myDoubles = new double[DIMC];
                foreach (string s in values)
                {
                    myDoubles[j] = Double.Parse(s, NumberStyles.Any, CultureInfo.InvariantCulture);
                    j++;
                }
                for (int i = 0; i < DIMC; i++)
                {
                    matrix[k, i] = myDoubles[i];
                }
                k++;
            }
            csv.Close();
            return matrix;
        }








        public void createFile(String src)
        {
            string[] lines = new string[DIMR];
            // WriteAllLines creates a file, writes a collection of strings to the file,
            // and then closes the file.  You do NOT need to call Flush() or Close().
            Random r = new Random();
            for (int i = 0; i < DIMR; i++)
            {
                string line = "";
                for (int j = 0; j < DIMC; j++)
                {
                    if (j == DIMC - 1)
                    {
                        double d = r.NextDouble();
                        line += (d.ToString());
                    }
                    else
                    {
                        double d = r.NextDouble();
                        line += (d.ToString() + " ");
                    }
                }
                lines[i] = line;
                //Console.WriteLine(line);
            }
            try
            {
                System.IO.File.WriteAllLines(@"../../matrix.txt", lines);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
