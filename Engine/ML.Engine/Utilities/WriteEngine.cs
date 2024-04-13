namespace ML.Engine.Utilities
{
    public static class WriteEngine
    {
        public static bool WriteXml(
            string content,
            string path,
            bool overwrite = false)
        {
            try
            {
                if (File.Exists(path) && overwrite == false)
                {
                    return false;
                }

                using (StreamWriter sw = File.CreateText(path))
                {
                    sw.Write(content);
                }

                return true;
            }
            catch (IOException e)
            {
                Console.WriteLine("The file could not be created:");
                Console.WriteLine(e.Message);
            }

            return false;
        }
    }
}
