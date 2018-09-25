using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using ChemFinder.iOS;
using Xamarin.Forms;
using System.Linq;

[assembly: Dependency(typeof(RecentItems))]
namespace ChemFinder.iOS
{
	public class RecentItems: IRecentItems
	{

		ObservableCollection<string> recentItems;
		Stack<string> stack = new Stack<string>();
		public void SaveText(string filename, string text)
		{
			try
			{
				var documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
				var filePath = Path.Combine(documentsPath, filename);
				if (File.Exists(filePath))
				{
					string[] lines = File.ReadAllLines(filePath);
					if (lines.Contains(text))
					{
						var newLines = lines.Where(line => !line.Contains(text));
						File.WriteAllLines(filePath, newLines);

					}
					if (lines.Count() > 10)
					{
						var linesReversed = lines.Reverse().Take(10).ToArray();
						File.WriteAllLines(filePath, linesReversed.Reverse());

					}
				}

				using (StreamWriter outputFile = new StreamWriter(filePath, true))
				{
					outputFile.WriteLine(text);
				}
			}
			catch (Exception ex)
			{
			}
			//string[] lines = new HashSet<string>(s).ToArray();
		
			
		}
		public ObservableCollection<string>  LoadText(string filename)
		{
			try
			{
				recentItems = new ObservableCollection<string>();
				var documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
				var filePath = Path.Combine(documentsPath, filename);
				string[] s = File.ReadAllLines(filePath);
				string[] s1 = new HashSet<string>(s).ToArray();
				foreach (string item in s1)
				{
					recentItems.Add(item);
				}
				recentItems=new ObservableCollection<string>(recentItems.Reverse().ToList<string>());
				return recentItems;
			}
			catch (Exception e)
			{
				return new ObservableCollection<string>(); 

			}

		}

		public void DeleteText(string filename, string text)
		{
			var documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
			var filePath = Path.Combine(documentsPath, filename);
			var oldLines = File.ReadAllLines(filePath);
			var newLines = oldLines.Where(line => !line.Contains(text));
			File.WriteAllLines(filePath, newLines);
		}
	}
}
