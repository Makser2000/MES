using DevExpress.ExpressApp.DC;
using DevExpress.Persistent.Validation;
using Galaktika.Common;
using Galaktika.Common.Module.BusinessOperations;
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
	[DisplayName("Завершить работу")]
	[ExecutionWay(ExecutionWays.Synchronous)]
	[DefaultOperationService(typeof(CompleteMaintenanceOrderService))]
	[DomainComponent]
	[CreateDetailView(Layout = "Order;Status;CompleteDate")]
	public class CompleteMaintenanceOrder : OperationBase
	{
		public CompleteMaintenanceOrder ()
		{
			CompleteDate = SysDateTime.Now;
		}

		private MaintenanceOrder maintenanceOrder;
		private DateTime completeDate; 
		
		[XafDisplayName("Наряд на ТОиР")]
		[RuleRequiredField]
		[ContextProperty(ViewType = ContextViewType.Any, ObjectsCriteria = "[Status] = 0")]
		public MaintenanceOrder Order
		{
			get => maintenanceOrder;
			set => SetPropertyValue(nameof(Order), ref maintenanceOrder, value);
		}

		[XafDisplayName("Дата завершения работ")]
		[RuleRequiredField]
		public DateTime CompleteDate
		{
			get => completeDate;
			set => SetPropertyValue(nameof(CompleteDate), ref completeDate, value);
		}

		[XafDisplayName("Текущее состояние")]
		public MaintenanceOrderStatus Status => Order.MaintenanceOrderStatus;

	}
}
