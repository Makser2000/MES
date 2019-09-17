using DevExpress.ExpressApp.DC;
using DevExpress.Persistent.Base;
using DevExpress.Xpo;
using Galaktika.Common.Module.BusinessObjects;
using Galaktika.Common.Types.Enums;
using Galaktika.MES.Module.BusinessObjects.MainResourceManagement.WorkPlace;
using System;
using System.Linq;
using Xafari.SmartDesign;

namespace Galaktika.Module.BusinessObjects
{
	[XafDisplayName("Технологическая карта ТОиР ")]
	[SmartDesignStrategy(typeof(XafariSmartDesignStrategy))]
	[CreateListView(Layout = "Сode;Name;MaintenanceKind;WorkPlace")]
	[CreateDetailView(Layout = "Сode;Name;MaintenanceKind;WorkPlace;Operations")]
	[DefaultClassOptions]
	public class MaintenanceRouting : CatalogObjectBase<MaintenanceRouting>
	{
		public MaintenanceRouting(Session session)
			: base(session)
		{
		}
		public override void AfterConstruction()
		{
			base.AfterConstruction();
		}

		private MaintenanceOrder maintenanceOrder =null;
		private MaintenanceSchedule maintenanceSchedule = null;
		private WorkPlace workPlace;
		private MaintenanceKind maintenanceKind;

		[XafDisplayName("Вид обслуживания/ремонта")]
		[ValueConverter(typeof(EnumStringValueConverter<MaintenanceKind>)), Size(1)]
		public MaintenanceKind MaintenanceKind
		{
			get => maintenanceKind;
			set => SetPropertyValue(nameof(MaintenanceKind), ref maintenanceKind, value);
		}

		[XafDisplayName("Рабочее место")]
		public WorkPlace WorkPlace
		{
			get => workPlace;
			set => SetPropertyValue(nameof(WorkPlace), ref workPlace, value);
		}

		[XafDisplayName("Операции")]
		[Association]
		public XPCollection<MaintenanceOperation> Operations
		{
			get => GetCollection<MaintenanceOperation>(nameof(Operations));
		}

		public MaintenanceSchedule MaintenanceSchedule
		{
			get => maintenanceSchedule;
			set
			{
				if (maintenanceSchedule == value)
					return;

				MaintenanceSchedule prevmaintenanceSchedule = maintenanceSchedule;
				maintenanceSchedule = value;

				if (IsLoading) return;

				if (prevmaintenanceSchedule != null && prevmaintenanceSchedule.MaintenanceRouting == this)
					prevmaintenanceSchedule.MaintenanceRouting = null;

				if (maintenanceSchedule != null)
					maintenanceSchedule.MaintenanceRouting = this;

				OnChanged("MaintenanceSchedule");
			}
		}

		public MaintenanceOrder MaintenanceOrder
		{
			get => maintenanceOrder;
			set
			{
				if (maintenanceOrder == value)
					return;

				MaintenanceOrder prevmaintenanceOrder = maintenanceOrder;
				maintenanceOrder = value;

				if (IsLoading) return;

				if (prevmaintenanceOrder != null && prevmaintenanceOrder.Routing== this)
					prevmaintenanceOrder.Routing = null;
				prevmaintenanceOrder.Routing= null;

				if (maintenanceOrder != null)
					maintenanceOrder.Routing = this;

				OnChanged("MaintenanceOrder");
			}
		}

	}

	public enum MaintenanceKind
	{
		[XafDisplayName("Обслуживание"), StringValue("M")]
		Maintenance = 0,
		[XafDisplayName("Средний ремонт"), StringValue("MR")]
		MediumRepair = 1,
		[XafDisplayName("Капитальный ремонт"), StringValue("B")]
		BigRepair = 2
	}

	public enum DefectRegistrationStatus
	{
		[XafDisplayName("Открыта"), StringValue("O")] 
		Opened = 0,
		[XafDisplayName("Обрабатывается"), StringValue("P")]
		Processing = 1,
		[XafDisplayName("Закрыта"), StringValue("C")]
		Closed = 2
	}

	public enum DefectKind
	{
		[XafDisplayName("Повреждение"), StringValue("D")]
		Damage = 0,
		[XafDisplayName("Отказ"), StringValue("R")]
		Failure = 1,
		[XafDisplayName("Неисправимый"), StringValue("F")]
		Fatal = 2
	}

	public enum MaintenanceOrderStatus
	{
		[XafDisplayName("Создан"), StringValue("C")]
		Created = 0,
		[XafDisplayName("Выполняется"), StringValue("P")]
		Performed = 1,
		[XafDisplayName("Завершен"), StringValue("C")]
		Completed = 2
	}

}