/*
 Created by : Moni
 Created Date : 16/02/2017
 Purpose : To form the url for displaying detail of selected record.
 */
using System;
namespace ChemFinder
{
	public static class UrlHelperForItemDetail
	{

		// Form the url like this BaseUrl + ForCompounds(or ForSubstances or ForAssays)+ CID + Output

		public static string BaseUrl = "https://pubchem.ncbi.nlm.nih.gov/rest/pug_view/data/";
		public static string ForCompounds = "compound/";
		public static string ForSubstances = "substance/";
		public static string ForAssays = "assay/";
		public static string CID = "";
		public static string Output = "JSON";
		public static string ImageBaseUrl = "https://pubchem.ncbi.nlm.nih.gov/image/imgsrv.fcgi?";


	}
}
