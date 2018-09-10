using System;

namespace Assets.Scripts.Code
{
    public static class IDs
    {
        public static string Next()
        {
            return Guid.NewGuid().ToString();
        }
    }
}
