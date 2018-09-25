using System.Collections.Generic;

namespace ChemFinder
{
	public class Information4
	{
		public int ReferenceNumber { get; set; }
		public string Name { get; set; }
		public List<double> NumValueList { get; set; }
		public List<string> StringValueList { get; set; }
		public List<string> ExternalDataURLList { get; set; }
		public string ExternalDataMimeType { get; set; }
	}
}