namespace Library.Api
{
    public static class ExtensionHelp
    {
        public static string CheckNull(this object val)
        {
            if (val == null)
            {
                return String.Empty;
            }
            else
            {
                return val.ToString();
            }
        }

        public static int ConvertToInt(this object val)
        {
            if (val == null)
            {
                return 0;
            }
            else
            {
                try
                {
                    return Int32.Parse(val.ToString());
                }
                catch { return 0; }
            }
        }
    }
}
