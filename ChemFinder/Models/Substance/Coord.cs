using System.Collections.Generic;

namespace ChemFinder
{
	public class Coord
	{
		public List<int> type { get; set; }
		public List<int> aid { get; set; }
		public List<Conformer> conformers { get; set; }
	}
}