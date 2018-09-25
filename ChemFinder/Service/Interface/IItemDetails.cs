using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace ChemFinder
{
	public interface IItemDetails
	{

		Task<Tuple<ObservableCollection<ItemDetailInList>, string>> GetTopSectionData(string searchValue, string searchCategory);

		//Task<JArray> GetData(string searchValue,string searchCategory);
		

	}
}
