using Galaktika.Module.BusinessObjects;
using System;
using System.Linq;
using Xafari.BC.BusinessOperations;

namespace Galaktika.MES.Maintenance.Module.LogicControllers
{
	public class CompleteMaintenanceOrderService : OperationServiceBase
	{
		public override void Execute(IBusinessOperation businessOperation)
		{
			if (businessOperation is CompleteMaintenanceOrder completeMaintenanceOrder)
			{
				ExecuteInternal(completeMaintenanceOrder);
			}
			else
			{
				throw new ArgumentException("Неверный тип бизнес-операции!");
			}
		}

		private void ExecuteInternal(CompleteMaintenanceOrder completeMaintenanceOrder)
		{
			var order = completeMaintenanceOrder.Order;
			order.MaintenanceOrderStatus = MaintenanceOrderStatus.Completed;
			order.CompleteDate = completeMaintenanceOrder.CompleteDate;
			if (order.DefectRegistration != null)
			{
				var registration = order.DefectRegistration;
				registration.Status = DefectRegistrationStatus.Closed;
				registration.CloseDate = order.CompleteDate;
				registration.ProcessingStatusDuration = registration.CloseDate - registration.ProcessingDate;
				registration.Save();
			}
			order.Save();
		}
	}
}
