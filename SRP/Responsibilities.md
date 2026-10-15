## 1. WardBoard
- **Core Responsibilities :**
  1. Managing bed assignments and patient-to-bed allocation (`AssignPatientToBed()`).

- **SRP Violations :**
  1. **Clinical Scoring (`ScoreAcuity()`):** calc scoring of paitent
  2. **Notification Logging (`DrainPagerLog()`):** Managing Logs
  3. **Reporting and Exporting (`BuildHandoffNote()`, `ExportCensusCsv()`):** 

## 2. CheckoutBasket
- **Core Responsibilities :**
1. Managing line items collection (`_lines`, `AddLine()`).
2. Calculating the raw basket subtotal (`SubTotal()`).
3. Orchestrating checkout actions by delegating specialized domain rules to dedicated services (`DiscountService`, `GiftMessageService`, `PaymentService`).

---
- **SRP Violations :**
  1. **Coupon Parsing (, `DiscountAmount`):** Parsing discount
  2. **Packaging Policies (`_giftWrap`, ):** Handling gift-wrapping options and additional fee calculations belong in a separate fee calculator.
  3. **Marketing Copy Generation (`GiftMessageCard`):** Formatting customer-facing gift message
  4. **Payment Gateway Authorization (`AuthorizePaymentStub()`):** Simulating or executing payment transactions belongs in an external


## 3. SupportTicket
- **Core Responsibilities :**
  1. Managing ticket data and messages (`Id`, `Subject`, `Body`, `AppendCustomerMessage()`).
  2. Draft basic customer reply based on ticket context (`DraftPublicReply()`).

- **SRP Violations :**
  1. **Priority Calculation (`RecalculatePriorityFromText()`):**
  2. **Operational SLA Policies (`SlaDeadline()`, `IsBreached()`):**
  3. **Internal Logging (`InternalEscalationBlurb()`):**

## 4. LoanDesk
- **Core Responsibilities :**
  1. Holding loan data (`RequestedAmount`, `CreditScore`, `EmploymentMonths`, `HasCollateral`).

- **SRP Violations :**
  1. **Risk Scoring & Eligibility (`RiskScore()`, `IsEligible()`):** Calculating risk formulas and loan approval eligibility belong in a dedicated underwriting or risk evaluation engine.
  2. **Document Compliance (`RequiredDocuments()`):** Determining required legal and compliance documents based on loan terms belongs in a policy or compliance service.
  3. **Decision Letter Formatting (`DecisionLetter()`):** Generating customer-facing approval/rejection correspondence and prose belongs in a communication or formatting service.
  4. **Analytics Export (`UnderwriterCsvRow()`):** Generating CSV export rows for analytics and reporting violates SRP and belongs in a dedicated reporting or export service.

  ## 5. CourseEnrollmentDesk
- **Core Responsibilities :**
  1. Managing course capacity, student registration, and waitlist allocations (`Register()`, `WaitlistPosition()`, `PromoteFromWaitlist()`).

- **SRP Violations :**
  1. **Welcome Packet Generation (`WelcomePacketMarkdown()`):**  send welcome messaging
  2. **Tuition Invoicing and VAT (`TuitionInvoiceLine()`):** Calculating tuition fees, taxes, and financial invoice formats violates SRP

  ## 6. KitchenTicket
- **Core Responsibilities :**
  1. Managing kitchen ticket items collection (`_items`, `AddItem()`).

- **SRP Violations :**
  1. **Allergen Detection (`DetectAllergens()`):** Scanning ingredients for regulatory or medical allergens (dairy, gluten, nuts, shellfish) belongs in a dedicated allergen service.
  2. **Cook ETA Heuristics (`EstimatedReadyMinutes()`):** Calculating preparation time, open station workload distribution, and allergy protocol delays belongs in a dedicated time or kitchen operations service.
  3. **Printed Ticket Layout (`RenderThermalTicket()`):** Formatting hardware-specific thermal receipt layouts, widths, and string markers belongs in a printer or formatting service.
  4. **Expo Lane Routing (`ExpoLaneHint()`):** Determining expo lane routing hints (allergy, slow, fast) based on ticket rules belongs in an expo lane or routing service.

  ## 7. SubscriptionBilling
- **Core Responsibilities :**
  1. Holding subscription billing data and tracking failed payments (`CustomerId`, `MonthlyPrice`, `PeriodStart`, `PeriodEnd`, `FailedPayments`, `RegisterFailedPayment()`)[cite: 1].

- **SRP Violations :**
  1. **Proration Math (`Prorate()`):** Calculating prorated amounts based on active calendar days belongs in a dedicated financial calculation or proration service[cite: 1].
  2. **Invoice Number Minting (`NextInvoiceNumber()`):** Generating sequential fiscal invoice numbers and sequence states belongs in a dedicated billing or numbering service[cite: 1].
  3. **Dunning Email Bodies (`DunningEmail()`):** Composing collections tone and legal boilerplate email templates belongs in a communication or email service[cite: 1].
  4. **Ledger Journal Export (`LedgerJournalLine()`):** Formatting accounting export strings for ledger entries belongs in a dedicated reporting or accounting export service[cite: 1].

## 8. WarehousePickList
- **Core Responsibilities :**
  1. Managing warehouse pick list line items collection (`_lines`, `AddModel()`, `AddNeed()`)[cite: 1].

- **SRP Violations :**
  1. **Stock Allocation (`Allocate()`):** Calculating inventory allocation and backorder policy limits belongs in a dedicated allocation service[cite: 1].
  2. **Walking Path Heuristics (`WalkingOrder()`):** Sorting and ordering item pick paths by warehouse layout layout technology belongs in a routing or warehouse path service[cite: 1].
  3. **Picker Instruction Prose (`PickerScript()`):** Formatting handheld device UX wording and shortage warnings belongs in a presentation or instruction formatting service[cite: 1].
  4. **WMS XML Batch Integration (`WmsXmlBatch()`):** Generating integration contracts and XML data structures for warehouse management systems belongs in an export or integration service[cite: 1].

  ## 9. GradeBook
- **Core Responsibilities :**
  1. Managing student scores collection and score recording (`_scores`, `Record()`).

- **SRP Violations :**
  1. **Grade Aggregation & Letter Bands (`Average()`, `Letter()`):** Calculating score averages and evaluating faculty senate academic grading policy bands belongs in a dedicated grading calculation service.
  2. **Honor Roll Evaluation (`MeetsHonorRoll()`):** Determining academic honor roll eligibility rules belongs in a policy or evaluation service.
  3. **Transcript Generation (`TranscriptPlain()`):** Formatting registrar document prose and student academic transcripts belongs in a communication or formatting service.
  4. **CSV Export (`ExportCsv()`):** Generating CSV data export rows for grade reports belongs in a reporting or data export service.

## 10. ExpenseReport
- **Core Responsibilities :**
  1. Managing expense line items collection and receipt entries (`_items`, `AddExpense()`).

- **SRP Violations :**
  1. **Policy Compliance Check (`ValidatePolicy()`):** Evaluating corporate spending limits and receipt compliance rules belongs in a dedicated policy validation service.
  2. **Currency Conversion (`ConvertCurrency()`):** Performing real-time currency exchange rate calculations and conversions belongs in a financial or currency service.
  3. **Approval Workflow Routing (`DetermineApprover()`):** Determining managerial approval chains and tiers based on total expenses belongs in an enterprise workflow service.
  4. **PDF Export & Reimbursement (`ExportPdf()`):** Formatting formal reimbursement documents and PDF layouts belongs in a document generation service.

  ## 10. AppointmentDesk
- **Core Responsibilities :**
  1. Managing slot configurations and tracking booked appointments (`_booked`, `Open`, `Close`, `SlotMinutes`, `TryBook()`).

- **SRP Violations :**
  1. **Business Hours & Slot Discovery (`IsWithinBusinessHours()`, `FindNextSlot()`):** Evaluating clinic operating hours, weekend exclusions, and scanning available time slots belong in a dedicated scheduling or slot search service.
  2. **ICS Calendar Export (`ToIcs()`):** Formatting and serializing iCalendar (.ics) event structures belong in an export or calendar integration service.
  3. **SMS Communication (`SmsReminder()`):** Formatting patient reminder copy and messaging strings belongs in a notification or messaging service.