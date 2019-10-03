using Galaktika.Common;
using Galaktika.Common.Module.BusinessObjects;
using Galaktika.Module.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solution1.Module.BusinessLogic
{
	class DefectRegistrationBusinessLogic:BusinessLogicBase<DefectRegistration>
	{
		public override void AfterConstruction(DefectRegistration obj)
		{
			base.AfterConstruction(obj);
			if (obj.IsNewObject)
			{
				obj.OpenDate = SysDateTime.Now;
				obj.Status = DefectRegistrationStatus.Opened;
			}
		}
	}
}
