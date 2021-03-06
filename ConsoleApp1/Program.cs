﻿using System;
//using System.Collections.Generic;
using System.Linq;
using System.Reflection;
//using System.Text;
//using System.Threading.Tasks;
using System.Net;

namespace ConsoleApp1
{
	class Program
	{
		// this is the main method for our program
		static void Main(string[] args = null)
		{
			// clear output
			Console.Clear();

			// set foreground/background
			Console.BackgroundColor = ConsoleColor.Black;
			Console.ForegroundColor = ConsoleColor.White;

			// allows skipping the main menu on startup and go directly to your choice if called via the cmd line
			string choice = "";
			if (args == null || args.Length < 1)
			{
				choice = MainMenu();
			}
			else
			{
				// capture the users choice
				choice = args[0];
			}
			
			// do the selected choice
			switch (choice)
			{
				case "1":
					OutputRandomNumber();
					break;

				case "2":
					OutputNCharsNTimes();
					break;

				case "3":
					Kit();
					break;

				case "4":
					IPFromWeb();
					break;

				default:
					// if an invalid option is input, dump the user back at the main menu
					Main();
					break;
			}

			// repeat or not
			if (Again())
			{
				Main();
			}
		}

		// asks the user if they want to do it all over again, returns bool
		static bool Again()
		{
			// output question, read input
			Console.WriteLine("Would you like to do it again?");
			string choice = Console.ReadLine();

			if (!choice.IsNullOrEmpty() && choice.ToLower().Substring(0, 1) == "n")
			{
				return false;
			}
			return true;
		}

		// outputs the main menu and returns the users choice
		static string MainMenu()
		{
			// output question
			Console.WriteLine("Please make a selection:");

			// array of choices
			string[] selections = {
				"Output a Random Number",
				"Output n Characters n Times",
				"Kit",
				"IP From Web",
			};

			// output choices
			int i = 1;
			foreach (string selection in selections)
			{
				Console.WriteLine(i + ": " + selection);
				i++;
			}

			// capture user choice
			string choice = Console.ReadLine();
			// return choice
			return choice;
		}

		// outputs a random number
		static void OutputRandomNumber()
		{
			Console.Clear();

			try
			{
				// instantiate random class
				Random rand = new Random();

				// output random number
				Console.WriteLine("Random Number: " + rand.Next());
			}
			catch (Exception ex)
			{
				Oops(ex.Message, null);
			}
		}

		// outputs N characters, N times
		static void OutputNCharsNTimes()
		{
			Console.Clear();

			// ask user to enter number, capture
			Console.WriteLine("Enter a number:");
			string n = Console.ReadLine();

			// convert to int
			int num = Int32.Parse(n);

			// array of characters to pick from
			const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
			// instantiate random class
			Random rand = new Random();

			// loop n times and output string
			for (int i = 0; i < num; i++)
			{
				// i found this here: https://stackoverflow.com/questions/1344221/how-can-i-generate-random-alphanumeric-strings-in-c
				string result = new string(Enumerable.Repeat(chars, num)
					.Select(s => s[rand.Next(s.Length)]).ToArray());

				Console.WriteLine(result);
			}
		}

		// outputs the LED thing from Knight Rider
		static void Kit()
		{
			int cycles = 5;
			Console.ForegroundColor = ConsoleColor.Red;

			// todo: make this array populate programatically, using maths
			string[] progression = {
				"=#             ",
				"-=#            ",
				" -=#           ",
				"  -=#          ",
				"   -=#         ",
				"    -=#        ",
				"     -=#       ",
				"      -=#      ",
				"       -=#     ",
				"        -=#    ",
				"         -=#   ",
				"          -=#  ",
				"           -=# ",
				"            -=#",
				"             #=",
				"            #=-",
				"           #=- ",
				"          #=-  ",
				"         #=-   ",
				"        #=-    ",
				"       #=-     ",
				"      #=-      ",
				"     #=-       ",
				"    #=-        ",
				"   #=-         ",
				"  #=-          ",
				" #=-           ",
				"#=-            ",
			};

			for (int i = 0; i < cycles; i++)
			{
				foreach (string line in progression)
				{
					Console.Clear();
					Console.WriteLine(line);
					System.Threading.Thread.Sleep(20);
				}
			}
			Console.Clear();
			Console.ForegroundColor = ConsoleColor.White;
			Console.WriteLine("Hoff!");
		}

		// outputs the response from a web request
		private static void IPFromWeb()
		{
			try
			{
				Console.Clear();
				WebClient client = new WebClient();
				string downloadString = client.DownloadString("http://samplebeer.com");
				Console.WriteLine(downloadString);
			}
			catch (Exception ex)
			{
				Oops(ex.Message, null);
			}
		}
		
		private static void Oops(string msg = null, string method = null)
		{
			Console.ForegroundColor = ConsoleColor.Red;
			Console.WriteLine("Ya Big Dummeh!");
			if (msg != null)
			{
				Console.ForegroundColor = ConsoleColor.Yellow;
				Console.WriteLine(msg);
				Console.ForegroundColor = ConsoleColor.Red;
			}
			if (method != null)
			{
				var mi = typeof(Program).GetMethod(method);
				if (mi != null)
				{
					mi.Invoke(null, null);
				}
			}

			Console.ForegroundColor = ConsoleColor.White;
		}
	}
}