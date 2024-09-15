using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace IoStreamFileReplaceIpMac
{
	internal class Program
	{
		static void Main(string[] args)
		{
			string inputFilePath = "C:\\Users\\rls\\source\\repos\\IoStreamFileReplaceIpMac\\IoStreamFileReplaceIpMac\\201 RAW.txt";
			string outputFilePath1 = "C:\\Users\\rls\\source\\repos\\IoStreamFileReplaceIpMac\\IoStreamFileReplaceIpMac\\201 ready.txt";
			string outputFilePath2 = "C:\\Users\\rls\\source\\repos\\IoStreamFileReplaceIpMac\\IoStreamFileReplaceIpMac\\201.dhcpd";

			using (StreamReader inFromFile = new StreamReader(inputFilePath))
			using (StreamWriter outInFile = new StreamWriter(outputFilePath1,true))
			{
				string line;
				while ((line = inFromFile.ReadLine()) != null)
				{
					string[] parts = Regex.Split(line.Trim(), @"\s+");
					if (parts.Length >= 2)
					{
						string ip = parts[0];
						string mac = parts[1];
						outInFile.WriteLine($"{ReplaceChar(mac, '-', ':')}\t{ip}");
					}
				}
			}

			using (StreamReader inFromFile = new StreamReader(inputFilePath))
			using (StreamWriter outInFile = new StreamWriter(outputFilePath2, append: true))
			{
				int countLine = 1;
				string line;
				while ((line = inFromFile.ReadLine()) != null)
				{
					if (string.IsNullOrWhiteSpace(line)) // если пустая или из пробелов
					{
						continue;
					}

					string[] parts = Regex.Split(line.Trim(), @"\s+"); //line.Split(' ');
					if (parts.Length >= 2)
					{
						string ip = parts[0];
						string mac = parts[1];

						outInFile.WriteLine($"host-{countLine}");
						outInFile.WriteLine("{");
						outInFile.WriteLine($"\thardware ethernet\t{ReplaceChar(mac, '-', ':')};");
						outInFile.WriteLine($"\tfixed-address\t\t{ip};");
						outInFile.WriteLine("}");
						outInFile.WriteLine();
//						outInFile.WriteLine($"host-{0}\n{{\n\thardware ethernet\t{1};" +
//											$"\n\tfixed-address\t\t{2};\n}}\n\n",
//											countLine, Regex.Replace(parts[0], @"-", ":"), ip);

						countLine++;
					}
				}
			}
		}
		static string ReplaceChar(string str, char oldChar, char newChar)
		{
			return str.Replace(oldChar, newChar);
		}

		static string ToUpperChar(string str)
		{
			return str.ToUpper();
		}
	}
}
		
	

