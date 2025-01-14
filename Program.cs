﻿using System;
using System.Diagnostics;
using static System.Net.Mime.MediaTypeNames;

namespace WebLinks
{
    internal class Program
    {
        /// <summary>
        /// <hej>
        /// </summary>
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

            public void print()
            {
                Console.WriteLine($"Title: {title}");
                Console.WriteLine($"Url: {url}");
                Console.WriteLine($"Description: {description}");
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
                else if (command == "list")
                {
                    ShowLinks();
                }
                else if (command == "open")
                {
                    openLink();
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
            using StreamWriter sw = File.AppendText(@"links.txt");
            sw.WriteLine(title + "|" + description + "|" + url);
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
            webLinks = newList; 
        }
        public static void openLink()
        {
            Console.Write("Enter title of link: ");
            string readTitle = Console.ReadLine();
            for (int i = 0; i <webLinks.Length; i++)
            {
                if (string.Equals(webLinks[i].title, readTitle, StringComparison.OrdinalIgnoreCase))
                {

                    Process proc = new Process();
                    proc.StartInfo.UseShellExecute = true;
                    string url = webLinks[i].url;
                    if (url.StartsWith("www"))
                    {
                        url = "https://" + url;
                    }
                    if (!url.StartsWith("https://"))
                    {
                        url = "https://www." + url;
                    }
                    Console.WriteLine("opening "+url);
                    proc.StartInfo.FileName = url;
                    proc.Start();

                }
            }
        }

        private static void ShowLinks()
        { 
            string[] lines = System.IO.File.ReadAllLines(@"links.txt");
            foreach (string line in lines)
            {
                Console.WriteLine(line);
                Console.WriteLine();
            }
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
                "load  - load a new link file",
                "open  - open a specific link",
                "list  - lists all the links",
                "quit  - quit the program"
            };
            foreach (string h in hstr) Console.WriteLine(h);
        }
    }
}