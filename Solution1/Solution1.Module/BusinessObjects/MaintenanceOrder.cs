using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Editors;
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
	[CreateListView(Layout = "Number;DefectRegistration;MaintenanceSchedule;MaintenanceOrderStatus;Routing;StartDate;PlanCompleteDate;CompleteDate")]
	[CreateDetailView(Layout = "Number;DefectRegistration;MaintenanceSchedule;MaintenanceOrderStatus;Routing;StartDate;PlanCompleteDate;CompleteDate;Operations;Executors")]
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

		private DateTime completeDate;
		private DateTime planCompleteDate;
		private DateTime startDate;
		private DefectRegistration defectRegistration;
		private MaintenanceSchedule maintenanceSchedule;
		private MaintenanceOrderStatus maintenanceOrderStatus;
		private MaintenanceRouting maintenanceRouting;
		private string number;

		[XafDisplayName("Номер")]
		public string Number
		{
			get => number;
			set => SetPropertyValue(nameof(Number), ref number, value);
		}

		[XafDisplayName("Регистрация дефекта")]
		[ImmediatePostData]
		[Appearance("DefectVisible",Criteria ="[MaintenanceSchedule]!=NULL",Visibility =ViewItemVisibility.Hide)]
		public DefectRegistration DefectRegistration
		{
			get => defectRegistration;
			set => SetPropertyValue(nameof(DefectRegistration), ref defectRegistration, value);
		}

		[XafDisplayName("График ТОиР")]
		[ImmediatePostData]
		[Appearance("ScheduleVisible", Criteria = "[DefectRegistration]!=NULL", Visibility = ViewItemVisibility.Hide)]
		public MaintenanceSchedule MaintenanceSchedule
		{
			get => maintenanceSchedule;
			set => SetPropertyValue(nameof(MaintenanceSchedule), ref maintenanceSchedule, value);
		}


		[XafDisplayName("Состояние")]
		[ValueConverter(typeof(EnumStringValueConverter<MaintenanceOrderStatus>)), Size(1)]
		public MaintenanceOrderStatus MaintenanceOrderStatus
		{
			get => maintenanceOrderStatus;
			set => SetPropertyValue(nameof(MaintenanceOrderStatus), ref maintenanceOrderStatus, value);
		}

		[XafDisplayName("ТК")]
		[Appearance("RoutingVisible", Criteria = "[Schedule] != NULL", Visibility = ViewItemVisibility.Hide)]
		public MaintenanceRouting Routing
		{
			get => maintenanceRouting;
			set => SetPropertyValue(nameof(Routing), ref maintenanceRouting, value);
		}

		[XafDisplayName("Дата начала")]
		public DateTime StartDate
		{
			get => startDate;
			set => SetPropertyValue(nameof(StartDate), ref startDate, value);
		}

		[XafDisplayName("Плановая дата завершения")]
		public DateTime PlanCompleteDate
		{
			get => planCompleteDate;
			set => SetPropertyValue(nameof(PlanCompleteDate), ref planCompleteDate, value);
		}

		[XafDisplayName("Дата завершения")]
		public DateTime CompleteDate
		{
			get => completeDate;
			set => SetPropertyValue(nameof(CompleteDate), ref completeDate, value);
		}

		[XafDisplayName("Операции")]
		[Association("MaintenanceOperations-MaintenanceOrders")]
		[Appearance("OperationsVisible", Criteria = "[Routing] != NULL", Enabled = false)]
		public XPCollection<MaintenanceOperation> Operations
		{
			get => GetCollection<MaintenanceOperation>(nameof(Operations));
		}
		
		[XafDisplayName("Исполнители")]
		[Association("MaintenanceOrder-Executors")]
		public XPCollection<MaintenanceOrderExecutor> Executors
		{
			get => GetCollection<MaintenanceOrderExecutor>(nameof(Executors));
		}

	}

}