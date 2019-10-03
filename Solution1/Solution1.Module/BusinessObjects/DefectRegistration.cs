using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.DC;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using Galaktika.Common.Module.BusinessObjects;
using Galaktika.MES.Core.ModelExtension;
using Galaktika.MES.Module.BusinessObjects.MainResourceManagement.Personnel;
using Galaktika.MES.Module.BusinessObjects.MainResourceManagement.WorkPlace;
using Solution1.Module.BusinessObjects;
using System;
using Xafari.SmartDesign;

namespace Galaktika.Module.BusinessObjects
{
	[XafDisplayName("Регистрация дефекта")]
	[Appearance("Red", Criteria = "[PlannedCloseDate] < Now()", BackColor = "Red", TargetItems = "*", Context = "ListView")]
	[SmartDesignStrategy(typeof(XafariSmartDesignStrategy))]
	[CreateListView(Layout = "Number;WorkPlace;DefectKind;Author;OpenDate;Personnel;Status;ProcessingDate;CloseDate;OpenedStatusDuration;ProcessingStatusDuration")]
	[CreateDetailView(Layout = "Number;WorkPlace;DefectKind;Author;OpenDate;Personnel;Status;ProcessingDate;CloseDate;OpenedStatusDuration;ProcessingStatusDuration")]
	[DefaultClassOptions]
	[FilterPanel(typeof(Filter))]
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

		private TimeSpan processingStatusDuration;
		private TimeSpan openedStatusDuration;
		private DateTime processingDate;
		private DateTime openDate;
		private DateTime closeDate;
		private WorkPlace workPlace;
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
		[RuleRequiredField]
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

		[RuleRequiredField]
		[XafDisplayName("Ответственный")]
		public Personnel Personnel
		{
			get => personnel;
			set => SetPropertyValue(nameof(Personnel), ref personnel, value);
		}

		[XafDisplayName("Дата регистрации")]
		[RuleRequiredField]
		public DateTime OpenDate
		{
			get => openDate;
			set => SetPropertyValue(nameof(OpenDate), ref openDate, value);
		}

		[XafDisplayName("Дата начала работ")]
		public DateTime ProcessingDate
		{
			get => processingDate;
			set => SetPropertyValue(nameof(ProcessingDate), ref processingDate, value);
		}

		[XafDisplayName("Дата закрытия")]
		public DateTime CloseDate
		{
			get => closeDate;
			set => SetPropertyValue(nameof(CloseDate), ref closeDate, value);
		}

		[XafDisplayName("Открыта в течение")]
		public TimeSpan OpenedStatusDuration
		{
			get => openedStatusDuration;
			set => SetPropertyValue(nameof(OpenedStatusDuration), ref openedStatusDuration, value);
		}

		[XafDisplayName("В работе в течение")]
		public TimeSpan ProcessingStatusDuration
		{
			get => processingStatusDuration;
			set => SetPropertyValue(nameof(ProcessingStatusDuration), ref processingStatusDuration, value);
		}

	}
}