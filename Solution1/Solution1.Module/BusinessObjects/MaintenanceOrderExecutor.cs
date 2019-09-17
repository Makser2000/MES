using DevExpress.ExpressApp.DC;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using Galaktika.Common.Module.BusinessObjects;
using Galaktika.MES.Module.BusinessObjects.MainResourceManagement.Personnel;
using System;
using System.Linq;
using Xafari.SmartDesign;

namespace Galaktika.Module.BusinessObjects
{
	[XafDisplayName("Исполнитель наряда")]
	[DefaultClassOptions]
	public class MaintenanceOrderExecutor : BusinessObjectBase<MaintenanceOrderExecutor>
	{
		public MaintenanceOrderExecutor(Session session)
			: base(session)
		{
		}
		public override void AfterConstruction()
		{
			base.AfterConstruction();
		}


		private Personnel personnel;
		private MaintenanceOrder order;

		[RuleRequiredField]
		[Association]
		[XafDisplayName("Наряд")]
		public MaintenanceOrder Order
		{
			get => order;
			set => SetPropertyValue(nameof(Order), ref order, value);
		}

		[XafDisplayName("Сотрудник")]
		[RuleRequiredField]
		public Personnel Personnel
		{
			get => personnel;
			set => SetPropertyValue(nameof(Personnel), ref personnel, value);
		}
		
	}
}