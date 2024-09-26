namespace SubstringCounter
{
    public class Program
    {
        public static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("Usage: SubstringCounter <filename>");
                return;
            }
            Run(args[0]);
        }

        private static void Run(string path)
        {
            var f = File.Open(path, FileMode.Open);
            int pos = path.IndexOf('.');
            string name = path.Substring(0, pos);
            System.IO.StreamReader file = new System.IO.StreamReader(f);
            string line;
            int counter = 0;
            while (true)
            {
                line = file.ReadLine();
                if (line == null) break;
                if (line.Contains(name))
                    counter++;
            }
            Console.WriteLine("Found " + counter);
        }
    }
}
