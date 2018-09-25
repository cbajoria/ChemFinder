/*
 Created by : Moni
 Created Date : 16/02/2017
 Purpose : To form the url for displaying suggestions when user types the search string.
 */
using System;
namespace ChemFinder
{
	public static class UrlHelperForSuggestion
	{
		// Form the url like this BaseUrl + ForCompounds(or ForSubstances or ForAssays)+ SearchString

		public static string BaseUrl = "https://pubchem.ncbi.nlm.nih.gov/pcautocp/pcautocp.cgi?callback=JSON&dict=";
		public static string ForCompounds = "pc_compoundnames&n=20&q=";
		public static string ForSubstances = "pc_compoundnames&n=20&q=";
		public static string ForAssays = "pc_assaynames&n=20&q=";
		public static string SearchString = "";

	}
}
