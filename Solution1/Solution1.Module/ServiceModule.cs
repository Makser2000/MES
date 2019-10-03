using Galaktika.Common.Module.BusinessObjects;
using Galaktika.Common.Resolver;
using Galaktika.Module.BusinessObjects;
using Microsoft.Extensions.DependencyInjection;
using Solution1.Module.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solution1.Module
{
	class ServiceModule : ServiceModuleBase
	{
		public override void Load(IServiceCollection services)
		{
			services.AddSingleton<IBusinessLogic<MaintenanceOperation>, MaintenanceOperationBusinessLogic>();
			services.AddSingleton<IBusinessLogic<MaterialRequirement>, MaterialRequirementBusinessLogic>();
			services.AddSingleton<IBusinessLogic<DefectRegistration>, DefectRegistrationBusinessLogic>();
			services.AddSingleton<IBusinessLogic<MaintenanceOrder>,MaintenanceOrderBusinessLogic>();
		}
	}
}
