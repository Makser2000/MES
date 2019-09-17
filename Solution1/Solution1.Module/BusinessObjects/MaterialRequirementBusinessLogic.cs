using Galaktika.Common.Module.BusinessObjects;
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
			if (propertyName.Equals(nameof(MaterialRequirement.Material)))
			{
				if (obj.Material != null)
				{
					PersistentCalculatedTool.MarkAndCalculate(obj, nameof(MaterialRequirement.Cost));
				}
			}
		}
	}
}