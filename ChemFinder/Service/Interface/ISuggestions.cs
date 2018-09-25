using System;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace ChemFinder
{
	public interface ISuggestions
	{
		Task<SearchSuggestions> GetData(string searchValue, string searchCategory);
	}
}
