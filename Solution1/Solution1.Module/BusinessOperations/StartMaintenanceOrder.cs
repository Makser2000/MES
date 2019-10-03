using DevExpress.ExpressApp.DC;
using DevExpress.Persistent.Validation;
using Galaktika.Common;
using Galaktika.Common.Module.BusinessOperations;
using Galaktika.MES.Module.BusinessObjects.MainResourceManagement.Personnel;
using Galaktika.Module.BusinessObjects;
using System;
using System.ComponentModel;
using System.Linq;
using Xafari.BC;
using Xafari.BC.BusinessOperations;
using Xafari.BC.BusinessOperations.Attributes;
using Xafari.SmartDesign;

namespace Galaktika.MES.Maintenance.Module.LogicControllers
{
	[DisplayName("Начать работу")]
	[ExecutionWay(ExecutionWays.Synchronous)]
	[DefaultOperationService(typeof(StartMaintenanceOrderService))]
	[DomainComponent]
	[CreateDetailView(Layout = "Order;Status;StartDate;Responsible")]
	public class StartMaintenanceOrder : OperationBase
	{
		public StartMaintenanceOrder()
		{
			StartDate = SysDateTime.Now;
		}

		private MaintenanceOrder order;
		private DateTime startDate;
		private Personnel responsible;

		[XafDisplayName("Наряд на ТОиР")]
		[RuleRequiredField]
		[ContextProperty(ViewType = ContextViewType.Any, ObjectsCriteria = "[Status] = 0")]
		public MaintenanceOrder Order
		{
			get => order;
			set => SetPropertyValue(nameof(Order), ref order, value);
		}

		[XafDisplayName("Текущее состояние")]
		public MaintenanceOrderStatus Status => Order.MaintenanceOrderStatus;

		[XafDisplayName("Дата начала работ")]
		[RuleRequiredField]
		public DateTime StartDate
		{
			get => startDate;
			set => SetPropertyValue(nameof(StartDate), ref startDate, value);
		}

		[XafDisplayName("Ответственный")]
		[RuleRequiredField]
		public Personnel Responsible
		{
			get => responsible;
			set => SetPropertyValue(nameof(Responsible), ref responsible, value);
		}
	}
}
