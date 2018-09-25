using System.Collections.Generic;

namespace ChemFinder
{
	public class Section
	{
		public string TOCHeading { get; set; }
		public string Description { get; set; }
		public List<Information1> Information { get; set; }
		public bool? HintGroupSubsectionsByReference { get; set; }

		public List<Section2> section { get; set; }
	}
}