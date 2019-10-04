using System;
using System.Collections.Generic;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Xpo;
using Galaktika.MES.Core.BusinessObjects.FloatingPanels;
using Galaktika.MES.Module.BusinessObjects.Filters;
using Galaktika.MES.Module.BusinessObjects.MainResourceManagement.WorkPlace;
using Xafari.SmartDesign;

namespace Solution1.Module.BusinessObjects
{
	[DomainComponent, NonPersistent]
	[SmartDesignStrategy(typeof(XafariSmartDesignStrategy))]
	[CreateDetailView(Layout = "WorkPlace")]
	[ModelDefault("Caption", "Фильтр")]
	public class Filter : ResourceFilter
	{

		public Filter(Session session) : base(session)
		{
		}

		private WorkPlace workPlace;

		/// <summary>
		/// Рабочее место
		/// </summary>
		[XafDisplayName("Рабочее место")]
		public WorkPlace WorkPlace
		{
			get => workPlace;
			set => SetPropertyValue(nameof(WorkPlace), ref workPlace, value);
		}

		public override CriteriaOperator BuildCriteria()
		{
			return CriteriaOperator.Parse("WorkPlace.Oid=?", WorkPlace?.Oid ?? Guid.Empty);
		}
	}
}
