using System;
using System.Diagnostics;
using System.Net;
using System.Numerics;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace WebLinks_kopia
{
    internal class Program
    {

        static WebLink[] webLinks = new WebLink[99]; //Array av WebLink, kan lagra max 99 länkadresser 

        class WebLink //Klass WebLink, enligt uppgift
        {
            string title, url, description;
        public WebLink(string title, string description, string url) //Klass WebLink, standard konstruktor
            {
                this.title = title;
                this.url = url;
                this.description = description;
            }
            public void Print() //Klass WebLink, printfunktion
            {
                Console.WriteLine($"Name: {title}");
                Console.WriteLine($"Url: {url}");
                Console.WriteLine($"Description: {description}");
            }
        }
     
        
        public static void Main(string[] args)
        {
            string[] lines = File.ReadAllLines(@"links.txt"); 
            for (int i = 0; i<lines.Length; i++)                //Upggift 1 laddar weblänkar från en fil till en array webLinks
            {
                string[] words = lines[i].Split('|');
                WebLink temp = new WebLink(words[0], words[1], words[2]);   
                webLinks[i] = temp;
            }

            PrintWelcome();
            string command;
            do
            {
                Console.Write(": ");
                command = Console.ReadLine();
                if (command == "q")
                {
                    Console.WriteLine("Good bye!");
                }
                else if (command == "list")
                {
                    WriteTheList();
                }
                else if (command == "help")
                {
                    WriteTheHelp();
                }
                else if (command == "add")
                {
                    AddTheList();
                }
                else if (command == "open")
                {
                    openNewWebLink();
                }
                else
                {
                    Console.WriteLine($"Unknown command '{command}'");
                }
            } while (command != "q");
        }

        static void openNewWebLink()
        {
            Console.Write($"enter URL: ");
            string url = Console.ReadLine();
            Process proc = new Process();
            proc.StartInfo.UseShellExecute = true;
            string open = "https://" + url;
            proc.StartInfo.FileName = open;
            proc.Start();
        }


        private static void PrintWelcome()
        {
            Console.WriteLine("Hello and welcome to the ... program ...");
            Console.WriteLine("that does ... something.");
            Console.WriteLine("Write 'help' for help!");
        }

        private static void WriteTheList()
        {
            int i = 0;
            while (webLinks[i] != null) {
                webLinks[i].Print();
                i++; }
            Console.Write($"If you want to open a link from the list write the Name:");
            string title = Console.ReadLine();
            string[] lines = File.ReadAllLines(@"links.txt");
            for (int j = 0; j < lines.Length; j++)
            {
                string[] words = lines[j].Split('|');
                if (words[0] == title) {
                    Process proc = new Process();
                    proc.StartInfo.UseShellExecute = true;
                    string open = words[2];
                    proc.StartInfo.FileName = open;
                    proc.Start();
                }
            }
        }

        private static void AddTheList()
        {
            Console.Write($"enter title: ");
            string title = Console.ReadLine();
            Console.Write($"enter description: ");
            string description = Console.ReadLine();
            Console.Write($"enter URL: ");
            string url = Console.ReadLine();

            StreamWriter utfil = new StreamWriter(@"links.txt", true);
            utfil.WriteLine($"{title}|{description}|{url}");
            utfil.Close();
            Console.WriteLine($"Done adding new weblink");
        }

        private static void WriteTheHelp()
        {
            string[] hstr = {
                "help  - display this help",
                "list  - list all links from a file",
                "add  - add a link to a file",
                "open  - open a specific link",
                "q  - quit the program"
            };
            foreach (string h in hstr) Console.WriteLine(h);
        }
    }
}