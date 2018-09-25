using System;
namespace ChemFinder
{
	public class PubSearchInput
	{
		public string Domain { get; set;}
		public string CompoundDomain { get; set; }
		public string SubstanceDomain { get; set; }
		public string StructureSearch { get; set; }
		public string FastSearch { get; set; }
		public string AssayDomain { get; set; }
		public string AssayType { get; set; }
		public string AssayTarget { get; set; }
		public string Identifiers { get; set; }
		public string OtherInputs { get; set; }

	}
}
