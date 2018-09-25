using System.Collections.Generic;

namespace ChemFinder
{
	public class Record
	{
		public string RecordType { get; set; }
		public int RecordNumber { get; set; }
		public List<Section> Section { get; set; }
		public List<Reference> Reference { get; set; }
	}
}