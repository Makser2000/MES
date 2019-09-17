using DevExpress.ExpressApp.DC;
using DevExpress.Persistent.Base;
using DevExpress.Xpo;
using Galaktika.Common.Module.BusinessObjects;
using Galaktika.MES.Module.BusinessObjects.MainResourceManagement.WorkPlace;
using System;
using System.Linq;
using Xafari.SmartDesign;

namespace Galaktika.Module.BusinessObjects
{
	[XafDisplayName("График ТОиР")]
	[SmartDesignStrategy(typeof(XafariSmartDesignStrategy))]
	[CreateListView(Layout = "Code;Name;WorkPlace;MaintenanceKind;WorkPlace1;ScheduleDate;MaintenanceRouting")]
	[CreateDetailView(Layout = "Code;Name;WorkPlace;MaintenanceKind;WorkPlace1;ScheduleDate;MaintenanceRouting")]
	[DefaultClassOptions]
	public class MaintenanceSchedule : CatalogObjectBase<MaintenanceSchedule>
	{
		public MaintenanceSchedule(Session session)
			: base(session)
		{
		}
		public override void AfterConstruction()
		{
			base.AfterConstruction();
		}

		private MaintenanceOrder maintenanceOrder=null;
		private WorkPlace workPlace;
		private MaintenanceRouting maintenanceRouting = null;
		private DateTime scheduleDate;
		private MaintenanceKind maintenanceKind;

		[XafDisplayName("Дата графика")]
		public DateTime ScheduleDate
		{
			get => scheduleDate;
			set => SetPropertyValue(nameof(ScheduleDate), ref scheduleDate, value);
		}

		[XafDisplayName("Рабочее место")]
		public WorkPlace WorkPlace
		{
			get => workPlace;
			set => SetPropertyValue(nameof(WorkPlace), ref workPlace, value);
		}

		[XafDisplayName("Рабочее место")]
		public WorkPlace WorkPlace1
		{
			get => MaintenanceRouting.WorkPlace;
		}

		[XafDisplayName("Вид обслуживания/ремонта")]
		[ValueConverter(typeof(EnumStringValueConverter<MaintenanceKind>)), Size(1)]
		public MaintenanceKind MaintenanceKind
		{
			get => maintenanceKind;
			set => SetPropertyValue(nameof(MaintenanceKind), ref maintenanceKind, value);
		}

		[XafDisplayName("ТК")]
		public MaintenanceRouting MaintenanceRouting
		{
			get => maintenanceRouting;
			set
			{
				if (maintenanceRouting == value)
					return;

				MaintenanceRouting prevmaintenanceRouting = maintenanceRouting;
				maintenanceRouting = value;

				if (IsLoading) return;

				if (prevmaintenanceRouting != null && prevmaintenanceRouting.MaintenanceSchedule == this)
					prevmaintenanceRouting.MaintenanceSchedule = null;

				if (maintenanceRouting != null)
					maintenanceRouting.MaintenanceSchedule = this;

				OnChanged("MaintenanceRouting");
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
				maintenanceOrder= value;

				if (IsLoading) return;

				if (prevmaintenanceOrder != null && prevmaintenanceOrder.MaintenanceSchedule == this)
					prevmaintenanceOrder.MaintenanceSchedule = null;

				if (maintenanceOrder != null)
					maintenanceOrder.MaintenanceSchedule = this;

				OnChanged("MaintenanceOrder");
			}
		}
	}
}