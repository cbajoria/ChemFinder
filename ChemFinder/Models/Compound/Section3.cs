using System.Collections.Generic;

namespace ChemFinder
{
	public class Section3
	{
		public string TOCHeading { get; set; }
		public string Description { get; set; }
		public List<Information3> Information { get; set; }
		public bool? HintEmbeddedHTML { get; set; }
		public bool? HintGroupSubsectionsByReference { get; set; }
		public List<Section4> Section { get; set; }
	}
}