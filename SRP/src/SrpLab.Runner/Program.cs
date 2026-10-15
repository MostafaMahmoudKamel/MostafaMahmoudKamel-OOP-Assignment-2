using SRP.src.SrpLab.AppointmentDesk_;
using SRP.src.SrpLab.CheckoutBasket;
using SRP.src.SrpLab.Course_EnrollmentDesk;
using SRP.src.SrpLab.Grade_Book;
using SRP.src.SrpLab.Kitchen_Ticket;
using SRP.src.SrpLab.LoanDesk_;
using SRP.src.SrpLab.SubscriptionBilling_;
using SRP.src.SrpLab.SupportTicket;
using SRP.src.SrpLab.WarehousePickList_;
using SRP.src.SrpLab.WordBoard;
using SrpLab;

Console.WriteLine("SrpLab — 10 intentional SRP violations (refactor me)");
Console.WriteLine("=================================================");
var acuityScoring = new AcuityScoring();
var logger = new WordLogger();
var wordReport = new WordReport();
var handOffNote = new HandOffNoteService();
var word = new WardBoard(acuityScoring,logger);
word.AssignBed(1, "p-88", heartRate: 130, spo2: 89);
Console.WriteLine(handOffNote.BuildHandoffNote(1, word.GetBedPatients(), word.GetVitalsScores()));
Console.WriteLine(string.Join(" | ", logger.DrainPagerLog()));

var discountService = new DiscountService();
var giftMessageService = new GiftMessageService();
var paymentService = new PaymentService();
var basket = new CheckoutBasket(discountService,paymentService,giftMessageService);
basket.AddLine("SKU-1", 40m, 2);
basket.ApplyCouponText("SAVE10");
basket.EnableGiftWrap();
Console.WriteLine($"basket total={basket.GrandTotal()} auth={basket.AuthorizePaymentStub("4242")}");

var prioritySerivce = new PriorityService();
var deadlineService = new DeadLineService();
var ticketTempleteService = new TicketTempleteService(deadlineService);
var ticket = new SupportTicket("T-1", "cannot login", "prod is down for me", DateTimeOffset.UtcNow);
Console.WriteLine(ticketTempleteService.DraftPublicReply("Nora",ticket));

var loan = new LoanDesk(60_000m, 640, 4, hasCollateral: false);
EligibleService eligibleService = new EligibleService();
RequiredDocument requiredDocument = new RequiredDocument(eligibleService);
var decisionService = new DecisionService(eligibleService,requiredDocument);
Console.WriteLine( decisionService.DecisionLetter("Omar", loan));

WelcomeService welcomeService = new();
var course = new CourseEnrollmentDesk("SEF-101", capacity: 1, tuition: 3000m);
Console.WriteLine(course.Register("a@mail.com"));
Console.WriteLine(course.Register("b@mail.com"));
Console.WriteLine(welcomeService.WelcomePacketMarkdown(course,"b@mail.com", "Bea"));

var kitchen = new KitchenTicket();
DetectErgensService detectErgensService = new DetectErgensService();
TimeService timeService = new TimeService(detectErgensService);
PrintingTicketService printingTicketService = new PrintingTicketService(detectErgensService, timeService);
kitchen.AddItem("Pasta", new[] { "wheat", "milk" }, 12);
Console.WriteLine(printingTicketService.RenderThermalTicket(kitchen,42));

ProrationService proration = new ProrationService();
InvoiceNumberingService invoiceNumberingService = new InvoiceNumberingService();
DunningEmailService dunning = new DunningEmailService(proration, invoiceNumberingService);
var sub = new SubscriptionBilling("c-9", 99m, new DateOnly(2026, 9, 1), new DateOnly(2026, 10, 1));
sub.RegisterFailedPayment();
Console.WriteLine(dunning.DunningEmail(sub,"Sara", new DateOnly(2026, 9, 20)));

var pick = new WarehousePickList();
WalkingOrderService walkingOrder = new WalkingOrderService();
AllocateService allocateService = new AllocateService();
PickerScriptService picker = new PickerScriptService(walkingOrder, allocateService);
pick.AddNeed("BOLT", "A", 3, 10, 7);
pick.AddNeed("NUT", "B", 1, 5, 5);
Console.WriteLine(picker.PickerScript(pick));

var grades = new GradeBook();
grades.Record("s1", 92);
grades.Record("s1", 88);
var calcService = new GradeCalculationService();
var transcriptService = new TranscriptService(calcService);
string transcript = transcriptService.TranscriptPlain(grades, "s1", "Ali");
Console.WriteLine(transcript);


var appt = new AppointmentDesk(new TimeOnly(9, 0), new TimeOnly(17, 0), 30);
var slotSearchService = new SlotSearchService();
var slot = slotSearchService.FindNextSlot(appt, DateTimeOffset.Parse("2026-09-21T08:00:00Z"), 48);
if (slot is null)throw new InvalidOperationException("no slot");
var isWithinHours = slotSearchService.IsWithinBusinessHours(appt, slot.Value);
appt.TryBook(slot.Value, isWithinHours);
var smsService = new SmsReminderService();
Console.WriteLine(smsService.SmsReminder(slot.Value, "0100"));



Console.WriteLine("Done. Now split responsibilities — without breaking behavior.");

