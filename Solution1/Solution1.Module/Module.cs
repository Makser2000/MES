using System;
using System.Text;
using System.Linq;
using DevExpress.ExpressApp;
using System.ComponentModel;
using DevExpress.ExpressApp.DC;
using System.Collections.Generic;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Updating;
using DevExpress.ExpressApp.Model.Core;
using DevExpress.ExpressApp.Model.DomainLogics;
using DevExpress.ExpressApp.Model.NodeGenerators;
using DevExpress.ExpressApp.Xpo;
using Xafari.BC.BusinessOperations;
using Xafari;
using Xafari.BC.LogicControllers;
using Galaktika.MES.Maintenance.Module.LogicControllers;
using Galaktika.Module.BusinessObjects;
using Galaktika.Common.Module.BusinessObjects;

namespace Solution1.Module {
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppModuleBasetopic.aspx.
    public sealed partial class Solution1Module : ModuleBase,ITypesProvider<IBusinessOperation>, ITypesProvider<IOperationService>, ITypesProvider<LogicControllerBase>
	{
        public Solution1Module() {
            InitializeComponent();
			BaseObject.OidInitializationMode = OidInitializationMode.AfterConstruction;
        }
        public override IEnumerable<ModuleUpdater> GetModuleUpdaters(IObjectSpace objectSpace, Version versionFromDB) {
            ModuleUpdater updater = new DatabaseUpdate.Updater(objectSpace, versionFromDB);
            return new ModuleUpdater[] { updater };
        }
        public override void Setup(XafApplication application) {
            base.Setup(application);
            // Manage various aspects of the application UI and behavior at the module level.
        }
        public override void CustomizeTypesInfo(ITypesInfo typesInfo) {
            base.CustomizeTypesInfo(typesInfo);
            CalculatedPersistentAliasHelper.CustomizeTypesInfo(typesInfo);
			PersistentCalculatedTool.Initialize<MaintenanceOperation>();
        }

		IEnumerable<Type> ITypesProvider<IBusinessOperation>.GetTypes()
		{
			return new[]
			{
				typeof(StartMaintenanceOrder),
				typeof(CompleteMaintenanceOrder)
			};
		}

		IEnumerable<Type> ITypesProvider<LogicControllerBase>.GetTypes()
		{
			return new[]
			{
				typeof(MaintenanceOperationLogicController)
			};
		}

		IEnumerable<Type> ITypesProvider<IOperationService>.GetTypes()
		{
			return new[]
			{
				typeof(StartMaintenanceOrderService),
				typeof(CompleteMaintenanceOrderService)
			};
		}
	}
}
