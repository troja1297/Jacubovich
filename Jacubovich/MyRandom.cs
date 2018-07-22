using System;

namespace Jacubovich
{
    public class MyRandom
    {
        private static Random mRandom { get; set; }

        public static Random Rand()
        {
            if (mRandom != null)
            {
                return mRandom;
            }
            else
            {
                return new Random();
            }
        }
    }
}