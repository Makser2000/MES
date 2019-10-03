using Galaktika.Common.Module.BusinessObjects;
using Galaktika.MES.Core.Common;
using System;
using System.Linq;

namespace Galaktika.Module.BusinessObjects
{
	public class MaterialRequirementBusinessLogic : BusinessLogicBase<MaterialRequirement>
	{
		public override void OnChanged(MaterialRequirement obj, string propertyName, object oldValue, object newValue)
		{
			base.OnChanged(obj, propertyName, oldValue, newValue);
			if (obj.IsLoading) return;
			if (propertyName.OneOf(nameof(MaterialRequirement.Material), nameof(MaterialRequirement.Amount)))
			{
				obj.Cost = CalculateCost(obj);
			}
			if (propertyName.Equals(nameof(MaterialRequirement.Cost)))
			{
				PersistentCalculatedTool.MarkAndCalculate(obj.Operation, nameof(MaintenanceOperation.TotalCost));
			}
		}

		public override void AfterConstruction(MaterialRequirement obj)
		{
			base.AfterConstruction(obj);

			if (obj.IsNewObject)
			{
				obj.Amount = 1;
			}
		}

		public override void BeforeDeleting(MaterialRequirement obj)
		{
			base.BeforeDeleting(obj);
			PersistentCalculatedTool.MarkAndCalculate(obj.Operation, nameof(MaintenanceOperation.TotalCost));
		}

		private decimal CalculateCost(MaterialRequirement requirement)
		{
			var materialCost = requirement.Material!=null ? requirement.Material.Cost:0m;
			return materialCost * requirement.Amount;
		}

	}
}