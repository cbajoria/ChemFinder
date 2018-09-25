namespace ChemFinder
{
	public class ItemDetailsFactory
	{
		public ItemDetailsFactory()
		{
			
		}
		public IItemDetails CreateObj(string category)
		{
			if (category == CategoryEnum.Compound.ToString())
			{
				return (IItemDetails)new CompoundDetails();
			}
			else if (category == CategoryEnum.Substance.ToString())
			{
				return (IItemDetails)new SubstanceDetails();
			}
			else if (category == CategoryEnum.BioAssay.ToString())
			{
				return (IItemDetails)new BioAssayDetails();
			}
			else
			{
				return null;
			}
		}

	}
}
