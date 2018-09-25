using System;

namespace ChemFinder
{
	class ItemsFactory
	{
		public ItemsFactory()
		{
			
		}
		public IItems CreateObj(string category) 
		{
			try
			{
				if (category == CategoryEnum.Compound.ToString())
				{
					return new CompoundFinder() as IItems;
				}
				else if(category == CategoryEnum.Substance.ToString())
				{
					return new SubstanceFinder() as IItems;
				}
				else if (category == CategoryEnum.BioAssay.ToString())
				{
					return new BioAssayFinder() as IItems;
				}
				else
				{
					return null;
				}

			}
			catch (Exception ex)
			{
				return null;
			}
		}
	}
}