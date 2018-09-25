using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace ChemFinder
{
	public class ItemDetailInList
	{
		
		public ItemDetailInList()
		{

		}



		public string Label { get; set; }
		private string itemValue;
		public string Value
		{
			get
			{
				return itemValue;
			}
			set
			{
				if (Label == "MolecularWeight" || Label == "Molecular Weight")
				{
					itemValue = value + " g/mol";
				}
				else if (Label == "MolecularFormula" || Label == "Molecular Formula")
				{
					itemValue = FormatHelper.GetMolecularFormulaFormatted(value);
				}
				else if (Label == "Melting Point")
				{
					itemValue = value + " deg C";
				}
				else
				{
					itemValue = value;
				}

			}
		}

	}
}
