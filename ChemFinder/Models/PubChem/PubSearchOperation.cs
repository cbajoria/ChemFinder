using System;
namespace ChemFinder
{
	public class PubSearchOperation
	{
		public string CompoundDomain { get; set; }
		public string CompoundProperty { get
			{ return "MolecularFormula,IUPACName,MolecularWeight"; }  
		}
		public string CompoundDetailProperty
		{
			get
			{ return "MolecularFormula,IUPACName,MolecularWeight,InChI,InchiKey,CanonicalSMILES"; }
		}
		public string SubstanceDomain { get; set; }
		public string AssayDomain { get; set; }
		public string TargetType { get; set; }

	}
}
