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
using Galaktika.MES.Core.Common;
using DevExpress.ExpressApp.Editors;

namespace Galaktika.Module.BusinessObjects
{
	[XafDisplayName("Операция")]
	[SmartDesignStrategy(typeof(XafariSmartDesignStrategy))]
	[CreateListView(Id = "ListView", Layout = "Code;Name;WorkCost;TotalCost;Duration;Parent", IsDefaultGridListView = true, EditorAlias = EditorAliases.GridListEditor)]
	[CreateListView(Id = "TreeView", Layout = "Code;Name;WorkCost;TotalCost;Duration;Parent", IsDefaultTreeListView = true, EditorAlias = EditorAliases.TreeListEditor)]
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
		private decimal totalCost;
		private MaintenanceOperation parent;
		private decimal workCost;

		[XafDisplayName("Длительность операции")]
		[Persistent]
		[PersistentCalculated(nameof(CalculateDuration))]
		[ImmediatePostData]
		public TimeSpan Duration
		{
			get => duration;
			set => SetPropertyValue(nameof(Duration), ref duration, value);
		}


		[XafDisplayName("Стоимость работ")]
		[ImmediatePostData]
		public decimal WorkCost
		{
			get => workCost;
			set => SetPropertyValue(nameof(WorkCost), ref workCost, value);
		}

		[XafDisplayName("Общая стоимость")]
		[Persistent]
		[PersistentCalculated(nameof(CalculateCost))]
		[ImmediatePostData]
		public decimal TotalCost
		{
			get => totalCost;
			set => SetPropertyValue(nameof(TotalCost), ref totalCost, value);
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
		[Association("MaintenanceOperation-Materials")]
		public XPCollection<MaterialRequirement> Materials
		{
			get => GetCollection<MaterialRequirement>(nameof(Materials));
		}

		[Association("MaintenanceRoutings-MaintenanceOperations")]
		public XPCollection<MaintenanceRouting> Routings
		{
			get => GetCollection<MaintenanceRouting>(nameof(Routings));
		}

		[Association("MaintenanceOperations-MaintenanceOrders")]
		public XPCollection<MaintenanceOrder> Orders
		{
			get => GetCollection<MaintenanceOrder>(nameof(Orders));
		}

		public void CalculateCost()
		{
			var totalCost = WorkCost + Materials.SumOrDefault(material => material.Cost) + Children.SumOrDefault(child => child.TotalCost);
			TotalCost = totalCost;
			Parent?.CalculateCost();
		}

		public void CalculateDuration()
		{
			if (Children.Any())
			{
				var totalDuration = Children.Aggregate(TimeSpan.Zero, (current, operation) => current + operation.Duration);
				Duration = totalDuration;
			}
		}
	}
}