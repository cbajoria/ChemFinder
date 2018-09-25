using System.Collections.Generic;

namespace ChemFinder
{
	public class PCSubstance
	{
		public Sid sid { get; set; }
		public Source source { get; set; }
		public List<string> synonyms { get; set; }
		public List<Xref> xref { get; set; }
		public List<Compound> compound { get; set; }
		public List<string> comment { get; set; }
	}
}