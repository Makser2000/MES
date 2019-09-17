using DevExpress.ExpressApp.DC;
using DevExpress.Persistent.Base;
using DevExpress.Xpo;
using Galaktika.Common.Module.BusinessObjects;
using Galaktika.Common.Types.Enums;
using System;
using System.Linq;
using Xafari.BC.Numerators;
using Xafari.SmartDesign;

namespace Galaktika.Module.BusinessObjects
{
	[XafDisplayName("Наряд на ТОиР")]
	[SmartDesignStrategy(typeof(XafariSmartDesignStrategy))]
	[CreateListView(Layout = "Number;DefectRegistration;MaintenanceSchedule;MaintenanceOrderStatus;Routing")]
	[CreateDetailView(Layout = "Number;DefectRegistration;MaintenanceSchedule;MaintenanceOrderStatus;Routing;Operations;Executors")]
	[DefaultClassOptions]
	public class MaintenanceOrder : BusinessObjectBase<MaintenanceOrder>
	{
		public MaintenanceOrder(Session session)
			: base(session)
		{
		}
		public override void AfterConstruction()
		{
			base.AfterConstruction();
		}

		MaintenanceOrderStatus maintenanceOrderStatus;
		private MaintenanceRouting routing = null;
		private MaintenanceSchedule maintenanceSchedule = null;
		private DefectRegistration defectRegistration = null;
		private string number;

		[XafDisplayName("Номер")]
		public string Number
		{
			get => number;
			set => SetPropertyValue(nameof(Number), ref number, value);
		}

		[XafDisplayName("Регистрация дефекта")]
		public DefectRegistration DefectRegistration
		{
			get => defectRegistration;
			set
			{
				if (defectRegistration == value)
					return;

				DefectRegistration prevdefectRegistration = defectRegistration;
				defectRegistration = value;

				if (IsLoading) return;

				if (prevdefectRegistration != null && prevdefectRegistration.MaintenanceOrder == this)
					prevdefectRegistration.MaintenanceOrder = null;

				if (defectRegistration != null)
					defectRegistration.MaintenanceOrder = this;

				OnChanged("DefectRegistration");
			}
		}

		[XafDisplayName("График ТОиР")]
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

				if (prevmaintenanceSchedule != null && prevmaintenanceSchedule.MaintenanceOrder == this)
					prevmaintenanceSchedule.MaintenanceOrder = null;
				prevmaintenanceSchedule.MaintenanceOrder = null;

				if (maintenanceSchedule != null)
					maintenanceSchedule.MaintenanceOrder = this;

				OnChanged("MaintenanceSchedule");
			}
		}

		[XafDisplayName("Состояние")]
		[ValueConverter(typeof(EnumStringValueConverter<MaintenanceOrderStatus>)), Size(1)]
		public MaintenanceOrderStatus MaintenanceOrderStatus
		{
			get => maintenanceOrderStatus;
			set => SetPropertyValue(nameof(MaintenanceOrderStatus), ref maintenanceOrderStatus, value);
		}

		[XafDisplayName("ТК")]
		public MaintenanceRouting Routing
		{
			get => routing;
			set
			{
				if (routing == value)
					return;

				MaintenanceRouting prevrouting = routing;
				routing = value;

				if (IsLoading) return;

				if (prevrouting != null && prevrouting.MaintenanceOrder == this)
					prevrouting.MaintenanceOrder = null;
				prevrouting.MaintenanceOrder = null;

				if (routing != null)
					routing.MaintenanceOrder = this;

				OnChanged("Routing");
			}
		}

		[XafDisplayName("Операции")]
		[Association]
		public XPCollection<MaintenanceOperation> Operations
		{
			get => GetCollection<MaintenanceOperation>(nameof(Operations));
		}
		
		[XafDisplayName("Исполнители")]
		[Association]
		public XPCollection<MaintenanceOrderExecutor> Executors
		{
			get => GetCollection<MaintenanceOrderExecutor>(nameof(Executors));
		}

	}

}