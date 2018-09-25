using System;
namespace ChemFinder
{
	public static class RecentSearchFileFinder
	{

		public static string GetFileName(string category)
		{
			string fileName = "";

			if (category == CategoryEnum.Compound.ToString())
			{
				fileName= "CompoundRecentItems.txt";
			}
			else if (category == CategoryEnum.Substance.ToString())
			{
				fileName = "SubstanceRecentItems.txt";
			}
			else if(category == CategoryEnum.BioAssay.ToString())
			{
				fileName = "BioAssayRecentItems.txt";

			}
			return fileName;
		}
	}
}
