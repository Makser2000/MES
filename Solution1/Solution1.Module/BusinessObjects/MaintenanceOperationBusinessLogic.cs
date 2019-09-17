using Galaktika.Common.Module.BusinessObjects;
using System;
using System.Linq;

namespace Galaktika.Module.BusinessObjects
{
	public class MaintenanceOperationBusinessLogic : BusinessLogicBase<MaintenanceOperation>
	{
		public override void OnChanged(MaintenanceOperation obj,string propertyName, object oldValue, object newValue)
		{
			base.OnChanged(obj,propertyName, oldValue, newValue);
			if (obj.IsLoading) return;
			if (propertyName.Equals(nameof(MaintenanceOperation.Duration)))
			{ 
				if (obj.Parent != null)
				{
					PersistentCalculatedTool.MarkAndCalculate(obj.Parent, nameof(MaintenanceOperation.Duration));
				}
			}
			if (propertyName.Equals(nameof(MaintenanceOperation.WorkCost)))
			{
				if (obj.Parent != null)
				{
					PersistentCalculatedTool.MarkAndCalculate(obj.Parent, nameof(MaintenanceOperation.WorkCost));
				}
			}
		}

	}
}