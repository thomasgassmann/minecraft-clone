namespace System
{
    public static class ObjectExtensions
    {
        public static void CheckArgumentIsNotNull(this object obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException();
            }
        }
    }
}
