using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.DC;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using Galaktika.Common.Module.BusinessObjects;
using Galaktika.MES.Module.BusinessObjects.MainResourceManagement.Personnel;
using Galaktika.MES.Module.BusinessObjects.MainResourceManagement.WorkPlace;
using System;
using System.Linq;
using Xafari.SmartDesign;

namespace Galaktika.Module.BusinessObjects
{
	[XafDisplayName("Регистрация дефекта")]
	[Appearance("Red", Criteria = "[Status]='Обрабатывается' && DateTime.Now>[CloseDate]", BackColor = "Red", TargetItems = "DefectRegistration", Context = "ListView")]
	[SmartDesignStrategy(typeof(XafariSmartDesignStrategy))]
	[CreateListView(Layout = "Number;WorkPlace;DefectKind;Author;Personnel")]
	[CreateDetailView(Layout = "Number;WorkPlace;DefectKind;Author;Personnel")]
	[DefaultClassOptions]
	public class DefectRegistration : BusinessObjectBase<DefectRegistration>
	{
		public DefectRegistration(Session session)
			: base(session)
		{
		}
		public override void AfterConstruction()
		{
			base.AfterConstruction();
		}

		private DateTime openDate;
		private DateTime closeDate;
		private WorkPlace workPlace;
		private MaintenanceOrder maintenanceOrder = null;
		private Personnel personnel;
		private Personnel author;
		private DefectRegistrationStatus status;
		private string number;
		private DefectKind defectKind;
		[XafDisplayName("Вид дефекта")]
		[RuleRequiredField]
		[ValueConverter(typeof(EnumStringValueConverter<DefectKind>)), Size(1)]
		public DefectKind DefectKind
		{
			get => defectKind;
			set => SetPropertyValue(nameof(DefectKind), ref defectKind, value);
		}

		[RuleRequiredField]
		[XafDisplayName("Номер")]
		public string Number
		{
			get => number;
			set => SetPropertyValue(nameof(Number), ref number, value);
		}

		[RuleRequiredField]
		[XafDisplayName("Состояние")]
		[ValueConverter(typeof(EnumStringValueConverter<DefectRegistrationStatus>)), Size(1)]
		public DefectRegistrationStatus Status
		{
			get => status;
			set => SetPropertyValue(nameof(Status), ref status, value);
		}

		[XafDisplayName("Рабочее место")]
		public WorkPlace WorkPlace
		{
			get => workPlace;
			set => SetPropertyValue(nameof(WorkPlace), ref workPlace, value);
		}

		[RuleRequiredField]
		[XafDisplayName("Автор")]
		public Personnel Author
		{
			get => author;
			set => SetPropertyValue(nameof(Author), ref author, value);
		}

		[XafDisplayName("Ответственный")]
		public Personnel Personnel
		{
			get => personnel;
			set => SetPropertyValue(nameof(Personnel), ref personnel, value);
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

				if (prevmaintenanceOrder != null && prevmaintenanceOrder.DefectRegistration == this)
					prevmaintenanceOrder.DefectRegistration = null;

				if (maintenanceOrder != null)
					maintenanceOrder.DefectRegistration = this;

				OnChanged("M" +
					"aintenanceOrder");
			}
		}
		
		[XafDisplayName("Дата открытия")]
		public DateTime OpenDate
		{
			get => openDate;
			set => SetPropertyValue(nameof(OpenDate), ref openDate, value);
		}

		[XafDisplayName("Дата закрытия")]
		public DateTime CloseDate
		{
			get => closeDate;
			set => SetPropertyValue(nameof(CloseDate), ref closeDate, value);
		}
	}
}