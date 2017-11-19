using MathNet.Numerics.LinearAlgebra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CSVparser
{
    public class LoadMatrix
    {
        string path;
        int skip;
        public event EventHandler PartitionComplete;
        AutoResetEvent done = new AutoResetEvent(false);
        int running = 1;

        public LoadMatrix(string path, int skip)
        {
            this.path = path;
            this.skip = skip;
        }

        public Matrix<double> loadM()
        {
            var reader = new TXTparsercs();
            var mReal1 = Matrix<double>.Build.DenseOfArray(reader.readCSV(skip, path));
            if (0 == Interlocked.Decrement(ref running))
                done.Set();
            return mReal1;
        }
        public void OnGenerateFinish()
        {
            done.WaitOne();
            OnEvent();

        }

        private void OnEvent()
        {
            if (PartitionComplete != null)
            {
                PartitionComplete(this, EventArgs.Empty);

            }
        }

    }
}
