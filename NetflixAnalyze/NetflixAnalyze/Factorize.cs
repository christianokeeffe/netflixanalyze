using ILNumerics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetflixAnalyze
{
    class Factorize
    {
        public double[,] movieValues;
        public double[,] userValues;

        public static void matrix(double[,] inputMatrix)
        {
            ILInArray<double> ilArray = (ILInArray<double>)inputMatrix;
            int length = (int)Math.Sqrt(inputMatrix.Length);
            ILOutArray<double> outarray = (ILOutArray<double>)((ILArray<double>)new double[length, length]);
            ILRetArray<double> eigresult = ILMath.svd(ilArray, outarray);
        }
    }
}
