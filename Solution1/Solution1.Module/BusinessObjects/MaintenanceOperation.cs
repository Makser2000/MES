using System;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using DevExpress.ExpressApp;
using System.ComponentModel;
using DevExpress.ExpressApp.DC;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.Base;
using System.Collections.Generic;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using Galaktika.Common.Module.BusinessObjects;
using Xafari.SmartDesign;
using Galaktika.Common.Resolver;

namespace Galaktika.Module.BusinessObjects
{
	[XafDisplayName("Операция")]
	[SmartDesignStrategy(typeof(XafariSmartDesignStrategy))]
	[CreateListView(Layout ="Code;Name;WorkCost;TotalCost;Duration;Parent")]
	[CreateDetailView(Layout ="Code;Name;WorkCost;TotalCost;Duration;Children;Materials")]
	[DefaultClassOptions]
	public class MaintenanceOperation : TreeCatalogObjectBase<MaintenanceOperation>
	{ 
		public MaintenanceOperation(Session session)
			: base(session)
		{
		}
		public override void AfterConstruction()
		{
			base.AfterConstruction();
		}

		protected override MaintenanceOperation GetParent()
		{
			return Parent;
		}

		protected override XPCollection<MaintenanceOperation> GetChildren()
		{
			return Children;
		}

		private TimeSpan duration;
		private MaintenanceOperation parent;
		private decimal workCost;
		private decimal summaOperations;
		private decimal summaMaterials;

		[XafDisplayName("Длительность операции")]
		[Persistent]
		[PersistentCalculated(nameof(CalculateDuration))]
		public TimeSpan Duration
		{
			get => duration;
			set => SetPropertyValue(nameof(Duration), ref duration, value);
		}


		[Persistent]
		[PersistentCalculated(nameof(CalculateSummaOperations))]
		public decimal SummaOperations
		{
			get { return summaOperations; }
			set { SetPropertyValue(nameof(SummaOperations), ref summaOperations, value); }
		}

		[Persistent]
		[PersistentCalculated(nameof(CalculateSummaMaterials))]
		public decimal SummaMaterials
		{
			get { return summaMaterials; }
			set { SetPropertyValue(nameof(SummaMaterials), ref summaMaterials, value); }
		}

		[XafDisplayName("Стоимость работ")]
		public decimal WorkCost
		{
			get => workCost;
			set => SetPropertyValue(nameof(WorkCost), ref workCost, value);
		}

		[XafDisplayName("Общая стоимость")]
		public decimal TotalCost
		{
			get => WorkCost + SummaOperations + SummaMaterials;
		}

		[XafDisplayName("Главная операция")]
		[Association("MaintenanceOperation-MaintenanceOperations")]
		public MaintenanceOperation Parent
		{
			get => parent;
			set => SetPropertyValue(nameof(Parent), ref parent, value);
		}

		[XafDisplayName("Подчиненные операции")]
		[Association("MaintenanceOperation-MaintenanceOperations")]
		public XPCollection<MaintenanceOperation> Children
		{
			get => GetCollection<MaintenanceOperation>(nameof(Children));
		}

		[XafDisplayName("Потребности в материалах")]
		[Association]
		public XPCollection<MaterialRequirement> Materials
		{
			get => GetCollection<MaterialRequirement>(nameof(Materials));
		}

		[Association]
		public XPCollection<MaintenanceRouting> Maps
		{
			get => GetCollection<MaintenanceRouting>(nameof(Maps));
		}

		[Association]
		public XPCollection<MaintenanceOrder> MaintenanceOrder
		{
			get => GetCollection<MaintenanceOrder>(nameof(MaintenanceOrder));
		}

		public void CalculateSummaOperations()
		{
			decimal summa = 0m;
			foreach (MaintenanceOperation child in Children)
				summa += child.TotalCost;
			this.SummaOperations = summa;
		}

		public void CalculateSummaMaterials()
		{
			decimal summa = 0m;
			foreach (MaterialRequirement material in Materials)
				summa += material.Cost;
			this.SummaMaterials = summa;
		}

		public void CalculateDuration()
		{
			TimeSpan summa = TimeSpan.Zero;
			foreach (MaintenanceOperation child in Children)
				summa += child.Duration;
			this.Duration = summa;
		}



	}
}