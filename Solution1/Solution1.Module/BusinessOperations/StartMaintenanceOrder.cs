using Galaktika.MES.Module.BusinessObjects.MainResourceManagement.Personnel;
using Galaktika.Module.BusinessObjects;
using System;
using System.ComponentModel;
using System.Linq;
using Xafari.BC;
using Xafari.BC.BusinessOperations;
using Xafari.BC.BusinessOperations.Attributes;

namespace Galaktika.MES.Maintenance.Module.LogicControllers
{
	[DisplayName("Начать работу")]
	[ExecutionWay(ExecutionWays.Synchronous)]
	[ContextViewType(ContextViewType.Any)]
	[BusinessOperationCategory(""), BusinessOperationCategory]
	[DefaultOperationService(typeof(StartMaintenanceOrderService))]
	public class StartMaintenanceOrder : BusinessOperationBase
	{
		public MaintenanceOrderStatus MaintenanceOrderStatus
		{
			get;
		}
		public Personnel personnel;
		public DateTime openDate;

	}
}
