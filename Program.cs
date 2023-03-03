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
            webLinks = new WebLink[0];
            string[] lines = System.IO.File.ReadAllLines(@"links.txt");
            for (int i = 0; i<lines.Length; i++)
            {
                string[] words = lines[i].Split('|');
                addWebLink(words[0], words[1], words[2]);
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
                    loadNewWebLink();
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
        private static void loadNewWebLink()
        {
            Console.Write($"enter title: ");
            string title = Console.ReadLine();
            Console.Write($"enter description: ");
            string description = Console.ReadLine();
            Console.Write($"enter URL: ");
            string url = Console.ReadLine();
            addWebLink(title, description, url);
            Console.WriteLine($"Done adding new weblink");
        }

        private static void addWebLink(string title, string description, string url)
        {
            WebLink newWebLink = new WebLink(title, description, url);

            WebLink[] newList = new WebLink[webLinks.Length+1];

            for (int i = 0; i< webLinks.Length; i++)
            {
                newList[i] = webLinks[i];
            }
            newList[newList.Length-1] = newWebLink;
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