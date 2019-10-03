using Galaktika.Common.Module.BusinessObjects;
using Galaktika.Module.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solution1.Module.BusinessLogic
{
	class MaintenanceOrderBusinessLogic:BusinessLogicBase<MaintenanceOrder>
	{
		public override void AfterConstruction(MaintenanceOrder obj)
		{
			base.AfterConstruction(obj);
			if (obj.IsNewObject)
			{
				obj.MaintenanceOrderStatus = MaintenanceOrderStatus.Created;
			}
		}

		public override void OnChanged(MaintenanceOrder obj, string propertyName, object oldValue, object newValue)
		{
			base.OnChanged(obj, propertyName, oldValue, newValue);
			if (obj.IsLoading) return;
			if (propertyName.Equals(nameof(MaintenanceOrder.MaintenanceSchedule)))
			{
				if (!Equals(oldValue, newValue))
				{
					ClearOperation(obj);
				}
				if (newValue is MaintenanceSchedule schedule)
				{
					obj.Routing = schedule.MaintenanceRouting;
				}
			}
			if (propertyName.Equals(nameof(MaintenanceOrder.Routing)))
			{
				ClearOperation(obj);
				if (newValue is MaintenanceRouting routing)
				{
					obj.Operations.AddRange(routing.Operations);
				}
			}
			
		}
		private void ClearOperation(MaintenanceOrder order)
		{
			while (order.Operations.Count > 0)
			{
				order.Operations.Remove(order.Operations[0]);
			}
		}
	}
}
