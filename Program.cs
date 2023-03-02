using static System.Net.Mime.MediaTypeNames;

namespace WebLinks
{
    internal class Program
    {
        private static WebLink[] webLinks;
        class WebLink
        {
            public string title;
            public string url;
            public string description;

        public WebLink(string title, string description, string url)
            {
                this.title = title;
                this.url = url;
                this.description = description;
            }
        }
        static void Main(string[] args)
        {
            string[] lines = System.IO.File.ReadAllLines(@"links.txt");
            webLinks = new WebLink[lines.Length];
            for (int i = 0; i<lines.Length; i++)
            {
                string[] words = lines[i].Split('|');
                webLinks[i] = new WebLink(words[0], words[1], words[2]);
            }

            PrintWelcome();
            string command;
            do
            {
                Console.Write(": ");
                command = Console.ReadLine();
                if (command == "quit")
                {
                    Console.WriteLine("Good bye!");
                }
                else if (command == "help")
                {
                    WriteTheHelp();
                }
                else if (command == "load")
                {
                    NotYetImplemented(command);
                }
                else if (command == "open")
                {
                    NotYetImplemented(command);
                }
                else
                {
                    Console.WriteLine($"Unknown command '{command}'");
                }
            } while (command != "quit");
        }

        private static void NotYetImplemented(string command)
        {
            Console.WriteLine($"Sorry: '{command}' is not yet implemented");
        }

        private static void PrintWelcome()
        {
            Console.WriteLine("Hello and welcome to the ... program ...");
            Console.WriteLine("that does ... something.");
            Console.WriteLine("Write 'help' for help!");
        }

        private static void WriteTheHelp()
        {
            string[] hstr = {
                "help  - display this help",
                "load  - load all links from a file",
                "open  - open a specific link",
                "quit  - quit the program"
            };
            foreach (string h in hstr) Console.WriteLine(h);
        }
    }
}