using System;
namespace ChemFinder
{
	public static class FormatHelper
	{
		

		public static string GetMolecularFormulaFormatted(string molFormula)
		{
			var returnValue = string.Empty;

			for (int i = 0; i < molFormula.Length; i++)
			{
				if (Char.IsNumber(molFormula[i]))
				{
					returnValue = returnValue + GetSubscript(molFormula[i].ToString());
				}
				else
				{
					returnValue = returnValue + molFormula[i];
				}
			}
			return returnValue;
		}

		public static string GetSubscript(string value)
		{
			string returnValue = string.Empty;
			foreach (char digit in value)
				returnValue = returnValue + ((char)(digit - '0' + '₀'));
			return returnValue;
		}
	}
}
