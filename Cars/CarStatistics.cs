using System;

namespace Cars
{
    public class CarStatistics
    {

        public CarStatistics()
        {
            Max = int.MaxValue;
            Min = int.MinValue;
        }

        public int Max { get; set; }
        public int Min { get; set; }
        public double Average { get; set; }
        public int Total { get; set; }
        public int Count { get; set; }

        public CarStatistics Compute()
        {
            Average = Total / Count;
            return this;
        }


        public CarStatistics Accumulate(Car car)
        {
            Count += 1;
            Total += car.Combined;
            Max = Math.Max(Max, car.Combined);
            Max = Math.Min(Min, car.Combined);

            return this;

        }
    }
}