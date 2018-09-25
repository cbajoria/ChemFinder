using System;
using System.Collections.Generic;

namespace ChemFinder
{
	public static class PropertyListHelper
	{

		public static List<string> Properties = new List<string>();
		public static string IdName = "";

		public static List<string> GetPropertyList(string Category)
		{
			Properties = new List<string>();

			IdName = "";
			if (Category == CategoryEnum.Compound.ToString())
			{
				Properties.Add("CID");
				Properties.Add("MolecularFormula");
				Properties.Add("MolecularWeight");
				Properties.Add("IUPACName");
				Properties.Add("InChI");
				Properties.Add("InChIKey");
				Properties.Add("CanonicalSMILES");
				IdName = "CID";
				return Properties;
			}
			else if (Category == CategoryEnum.Substance.ToString())
			{
				//TODO : Need to change the property according to Substance
				Properties.Add("id");
				Properties.Add("name");
				Properties.Add("str");
				Properties.Add("IUPACName");
				IdName = "SID";
				return Properties;
			}
			else if (Category == CategoryEnum.BioAssay.ToString())
			{
				//TODO : Need to change the property according to BioAssay
				Properties.Add("AID");
				Properties.Add("MolecularFormula");
				Properties.Add("MolecularWeight");
				Properties.Add("IUPACName");
				IdName = "AID";
				return Properties;
			}
			else
			{
				return null;
			}
		}
	}
}
