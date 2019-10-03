using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.ExpressApp;
using DevExpress.Xpo;
using Galaktika.Common.Module.BusinessObjects;
using Galaktika.MES.Core.Common;
using Galaktika.Module.BusinessObjects;
using Xafari.BC.LogicControllers;

namespace Galaktika.MES.Maintenance.Module.LogicControllers
{
    public class MaintenanceOperationLogicController : LogicControllerBase<MaintenanceOperation, DetailView>
    {
        protected override void OnActivated()
        {
            base.OnActivated();

            if (View.CurrentObject is MaintenanceOperation operation)
            {
                operation.Children.CollectionChanged += ChildrenOperationChanged;
            }
        }

        private void ChildrenOperationChanged(object sender, XPCollectionChangedEventArgs e)
        {
            if (View.CurrentObject is MaintenanceOperation operation &&
                e.CollectionChangedType.OneOf(XPCollectionChangedType.AfterAdd, XPCollectionChangedType.AfterRemove))
            {
                PersistentCalculatedTool.Mark(operation, nameof(operation.TotalCost));
                PersistentCalculatedTool.Mark(operation, nameof(operation.Duration));
            }
        }

        protected override void OnDeactivated()
        {
            if (View.CurrentObject is MaintenanceOperation operation)
            {
                operation.Children.CollectionChanged -= ChildrenOperationChanged;
            }
            base.OnDeactivated();
        }
    }
}
