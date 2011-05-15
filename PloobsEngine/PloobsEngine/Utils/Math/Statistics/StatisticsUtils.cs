﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PloobsEngine.Utils
{
    public class StatisticsUtils
    {
        /// <summary>
        /// Get average
        /// </summary>
        public static double GetAverage(double[] data)
        {
            int len = data.Length;

            if (len == 0)
                throw new Exception("No data");

            double sum = 0;

            for (int i = 0; i < data.Length; i++)
                sum += data[i];

            return sum / len;
        }


        /// <summary>
        /// Get variance
        /// </summary>
        public static double GetVariance(double[] data)
        {
            int len = data.Length;

            // Get average
            double avg = GetAverage(data);

            double sum = 0;

            for (int i = 0; i < data.Length; i++)
                sum += System.Math.Pow((data[i] - avg), 2);

            return sum / len;
        }

        /// <summary>
        /// Get standard deviation
        /// </summary>
        public static double GetStdev(double[] data)
        {
            return Math.Sqrt(GetVariance(data));
        }

        /// <summary>
        /// Get the correlation between X and Y
        /// </summary>
        /// <param name="x">X</param>
        /// <param name="y">Y</param>
        /// <param name="covXY">Covariance X,Y</param>
        /// <param name="pearson">Pearson X,Y</param>
        /// <param name="MultiplierX">Multiplier of X ( COVXY / VARX )</param>
        /// /// <param name="MultiplierY">Multiplier of Y ( COVXY / VARY )</param>
        public static void GetCorrelation(double[] x, double[] y, ref double covXY, ref double pearson, ref double multiplierX, ref double multiplierY)
        {
            if (x.Length != y.Length)
                throw new Exception("Length of sources is different");

            double avgX = GetAverage(x);
            double stdevX = GetStdev(x);
            double avgY = GetAverage(y);
            double stdevY = GetStdev(y);

            int len = x.Length;



            for (int i = 0; i < len; i++)
                covXY += (x[i] - avgX) * (y[i] - avgY);

            covXY /= len;

            pearson = covXY / (stdevX * stdevY);

            multiplierX = covXY / Math.Pow(stdevX, 2);
            multiplierY = covXY / Math.Pow(stdevY, 2);
        }

    }
}
