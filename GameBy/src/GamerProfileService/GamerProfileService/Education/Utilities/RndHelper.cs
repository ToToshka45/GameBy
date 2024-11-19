using System;

namespace Education.Middlewares
{
    public static class RndHelper
    {
        public static bool NextBoolean(this Random random)
        {
            return random.Next() > (Int32.MaxValue / 2);
        }
    }
}