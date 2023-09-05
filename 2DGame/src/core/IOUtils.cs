namespace Game
{
    static class IOUtils
    {
        public static void WriteFile(string fileName, string data)
        {
            var bytes = System.Text.Encoding.UTF8.GetBytes(data);
            for (int i = 0; i < bytes.Length; i++)
                bytes[i] ^= 0x5a;
            System.IO.File.WriteAllText(fileName, Convert.ToBase64String(bytes));
        }
        public static string ReadFile(string fileName)
        {
            var bytes = Convert.FromBase64String(System.IO.File.ReadAllText(fileName));
            for (int i = 0; i < bytes.Length; i++)
                bytes[i] ^= 0x5a;
            return System.Text.Encoding.UTF8.GetString(bytes);
        }
    }
}
