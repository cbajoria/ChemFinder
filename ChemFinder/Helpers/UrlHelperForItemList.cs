using System;
namespace ChemFinder
{
	public class UrlHelperForItemList
	{
		public UrlHelperForItemList()
		{
		}

		// Form the url like this BaseUrl + ForCompounds(or ForSubstances or ForAssays)+ Identifier + searchInput +property+propertyValues+output + nameType
		//https://pubchem.ncbi.nlm.nih.gov/rest/pug/compound/name/acacetin/json?name_type=word
		//https://pubchem.ncbi.nlm.nih.gov/rest/pug/compound/name/albendazole/property/MolecularFormula,IUPACName,MolecularWeight/json?name_type=word
		public static string BaseUrl = "https://pubchem.ncbi.nlm.nih.gov/rest/pug/";
		public static string ForCompounds = "compound/";
		public static string ForSubstances = "substance/";
		public static string ForAssays = "assay/";
		public static string Identifier = "name/";
		public static string searchInput = "";
		public static string property = "/property";
		public static string propertyValues = "/MolecularFormula,IUPACName,MolecularWeight";
		public static string output = "/json?";
		public static string nameType = "name_type=word";

	}
}
