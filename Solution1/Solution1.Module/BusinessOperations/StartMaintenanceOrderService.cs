using Galaktika.Module.BusinessObjects;
using System;
using System.Linq;
using Xafari.BC.BusinessOperations;
using Xafari.BC.BusinessOperations.Attributes;

namespace Galaktika.MES.Maintenance.Module.LogicControllers
{
	[BusinessOperation(typeof(StartMaintenanceOrder))]
	public class StartMaintenanceOrderService : OperationServiceBase
	{
		public override void Execute(IBusinessOperation businessOperation)
		{
			if (businessOperation is StartMaintenanceOrder startMaintenanceOrder)
			{
				ExecuteInternal(startMaintenanceOrder);
			}
			else
			{
				throw new ArgumentException("Неверный тип бизнес-операции!");
			}
		}
		private void ExecuteInternal(StartMaintenanceOrder startMaintenanceOrder)
		{
			var order = startMaintenanceOrder.Order;
			TimeSpan plannedDuration = order.Operations.Aggregate(TimeSpan.Zero, (current, operation) => current.Add(operation.Duration));
			order.MaintenanceOrderStatus = MaintenanceOrderStatus.Performed;
			order.StartDate = startMaintenanceOrder.StartDate;
			order.PlanCompleteDate = order.StartDate.Add(plannedDuration);
			if (order.DefectRegistration != null)
			{
				var registration = order.DefectRegistration;
				registration.Status = DefectRegistrationStatus.Processing;
				registration.ProcessingDate = order.StartDate;
				registration.PlannedCloseDate = order.PlanCompleteDate;
				registration.OpenedStatusDuration = registration.ProcessingDate - registration.OpenDate;
				registration.Personnel = startMaintenanceOrder.Responsible;
				registration.Save();
			}
			order.Save();
		}
	}
}
