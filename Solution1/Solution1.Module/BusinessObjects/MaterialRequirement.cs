using DevExpress.ExpressApp.DC;
using DevExpress.Persistent.Base;
using DevExpress.Xpo;
using Galaktika.Common.Module.BusinessObjects;
using System;
using System.Linq;
using Xafari.SmartDesign;

namespace Galaktika.Module.BusinessObjects
{
	[XafDisplayName("Потребность в материале")]
	[SmartDesignStrategy(typeof(XafariSmartDesignStrategy))]
	[CreateListView(Layout = "Operation;Material;Amount;Cost")]
	[CreateDetailView(Layout ="Operation;Material;Amount;Cost")]
	[DefaultClassOptions]
	public class MaterialRequirement : BusinessObjectBase<MaterialRequirement>
	{
		public MaterialRequirement(Session session)
			: base(session)
		{
		}
		public override void AfterConstruction()
		{
			base.AfterConstruction();
		}

		private MaintenanceOperation operation;
		private Material material;
		private decimal amount;
		private decimal cost;

		[XafDisplayName("Стоимость")]
		public decimal Cost
		{
			get => cost;
			set => SetPropertyValue(nameof(Cost), ref cost, value);
		}

		[XafDisplayName("Количество")]
		public decimal Amount
		{
			get => amount;
			set => SetPropertyValue(nameof(Amount), ref amount, value);
		}

		[XafDisplayName("Материал")]
		public Material Material
		{
			get => material;
			set => SetPropertyValue(nameof(Material), ref material, value);
		}
		
		[XafDisplayName("Операция")]
		[Association("MaintenanceOperation-Materials")]
		public MaintenanceOperation Operation
		{
			get => operation;
			set => SetPropertyValue(nameof(Operation), ref operation, value);
		}

	}
}