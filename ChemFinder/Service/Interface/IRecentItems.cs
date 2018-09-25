using System;
using System.Collections.ObjectModel;

namespace ChemFinder
{
	public interface IRecentItems
	{
		void SaveText(string filename, string text);
		ObservableCollection<string> LoadText(string filename);
		void DeleteText(string filename, string text);

	}
}
