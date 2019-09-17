using DevExpress.ExpressApp.DC;
using DevExpress.Persistent.Base;
using DevExpress.Xpo;
using Galaktika.Common.Module.BusinessObjects;
using Galaktika.MES.Module.BusinessObjects.Catalogs;
using System;
using System.Linq;
using Xafari.SmartDesign;

namespace Galaktika.Module.BusinessObjects
{
	[XafDisplayName("Материал")]
	[SmartDesignStrategy(typeof(XafariSmartDesignStrategy))]
	[CreateListView(Layout = "Code;Name;Unit;Cost")]
	[CreateDetailView(Layout = "Code;Name;Unit;Cost")]
	[DefaultClassOptions]
	public class Material : CatalogObjectBase<Material>
	{
		public Material(Session session)
			: base(session)
		{
		}
		public override void AfterConstruction()
		{
			base.AfterConstruction();
		}

		private MaterialRequirement materialRequirement=null;
		private Unit unit;
		private decimal cost;

		[XafDisplayName("Единица измерения")]
		public Unit Unit
		{
			get => unit;
			set => SetPropertyValue(nameof(Unit), ref unit, value);
		}

		[XafDisplayName("Стоимость")]
		public decimal Cost
		{
			get => cost;
			set => SetPropertyValue(nameof(Cost), ref cost, value);
		}
		
		public MaterialRequirement MaterialRequirement
		{
			get { return materialRequirement; }
			set
			{
				if (materialRequirement == value)
					return;

				MaterialRequirement prevmaterialRequirement =materialRequirement;
				materialRequirement = value;

				if (IsLoading) return;

				if (prevmaterialRequirement != null && prevmaterialRequirement.Material == this)
					prevmaterialRequirement.Material = null;

				if (materialRequirement != null)
					materialRequirement.Material = this;

				OnChanged("MaterialRequirement");
			}
		}
	}
}