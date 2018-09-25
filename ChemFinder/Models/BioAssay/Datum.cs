using System.Collections.Generic;

namespace ChemFinder
{
	public class Datum
	{
		public int sid { get; set; }
		public int version { get; set; }
		public int outcome { get; set; }
		public List<Datum2> data { get; set; }
		public Date date { get; set; }
	}
}