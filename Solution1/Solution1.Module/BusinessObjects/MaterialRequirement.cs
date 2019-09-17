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
	[CreateListView(Layout = "Material;Amount;Cost")]
	[CreateDetailView(Layout ="Material;Amount;Cost")]
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

		private Material material = null;
		private decimal amount;

		[XafDisplayName("Стоимость")]
		public decimal Cost
		{
			get => Material.Cost;
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
			get { return material;}
			set
			{
				if (material == value)
					return;

				Material prevMaterial = material;
				material = value;

				if (IsLoading) return;


				if (prevMaterial != null && prevMaterial.MaterialRequirement == this)
					prevMaterial.MaterialRequirement = null;

				if (material != null)
					material.MaterialRequirement = this;
				OnChanged("Material");
			}
		}
		
		[Association]
		public XPCollection<MaintenanceOperation> Operations
		{
			get => GetCollection<MaintenanceOperation>(nameof(Operations));
		}
	}
}