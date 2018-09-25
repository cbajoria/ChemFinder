using System.Collections.Generic;

namespace ChemFinder
{
	public class Section2
	{
		public string TOCHeading { get; set; }
		public string Description { get; set; }
		public List<Information2> Information { get; set; }
		public List<Section3> Section { get; set; }
		public bool? HintGroupSubsectionsByReference { get; set; }
	}
}