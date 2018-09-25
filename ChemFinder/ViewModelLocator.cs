using System;
using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;

namespace ChemFinder
{
	public class ViewModelLocator
	{
		static ViewModelLocator()
		{
			ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);
			SimpleIoc.Default.Register<SearchViewModel>();
			SimpleIoc.Default.Register<SubstanceDetailViewModel>();
			SimpleIoc.Default.Register<CompoundDetailViewModel>();
			SimpleIoc.Default.Register<BioAssayDetailViewModel>();
		}

		public SearchViewModel Search
		{
			get
			{
				return ServiceLocator.Current.GetInstance<SearchViewModel>();
			}
		}
		public SubstanceDetailViewModel Substance
		{
			get
			{
				return ServiceLocator.Current.GetInstance<SubstanceDetailViewModel>();
			}
		}
		public CompoundDetailViewModel Compound
		{
			get
			{
				return ServiceLocator.Current.GetInstance<CompoundDetailViewModel>();
			}
		}
		public BioAssayDetailViewModel BioAssay
		{
			get
			{
				return ServiceLocator.Current.GetInstance<BioAssayDetailViewModel>();
			}
		}
	}
}
