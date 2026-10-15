using Inheritance;


 //Person plainPerson = new Person("P1", "Plain Person", "01000000000");//can't compile abstract class
 //Member plainMember = new Member("M1", "Plain Member", "01000000001", 3, 0m);
// Staff plainStaff = new Staff("S1", "Staff", "01000000002", DateTime.Today, 10000m);
// LibraryItem plainItem = new LibraryItem("I1", "Item", 5, 2m, 1m);
// student.FullName = "Changed Name"; //get only method not have set;[readonly]
// student.Loans.Add(someLoan);//can't add IReadonlySet
// book1.IsOnLoan = true;

var student = new StudentMember("M-001", "Sara Student", "01011111111");
var premium = new PremiumMember("M-002", "Omar Premium", "01022222222", 20m);

var book1 = new Book("B-001", "C# Fundamentals", 2m);
var book2 = new Book("B-002", "Clean Code", 2m);
var book3 = new Book("B-003", "Algorithms", 2m);
var book4 = new Book("B-004", "Design Patterns", 2m);
var dvd = new DVD("D-001", "Learning C#", 3m);
var magazine = new Magazine("MZ-001", "Tech Monthly", 1m);
var withdrawnBook = new Book("B-005", "Archived Book", 2m);
var head = new HeadLibrarian("S-001", "Mona Head", "01033333333", new DateTime(2020, 1, 1), 20000m);

head.WithdrawItem(withdrawnBook);
try
{
    premium.BorrowItem("L-001", withdrawnBook, new DateTime(2026, 10, 1));
}
catch (Exception ex)
{
    Console.WriteLine($"Rejected as expected (borrow withdrawn item): {ex.Message}");
}

var firstLoan = premium.BorrowItem("L-002", dvd, new DateTime(2026, 10, 1));
try
{
    student.BorrowItem("L-003", dvd, new DateTime(2026, 10, 2));
}
catch (Exception ex)
{
    Console.WriteLine($"Rejected as expected (borrow item already on loan): {ex.Message}");
}

student.BorrowItem("L-004", book1, new DateTime(2026, 10, 1));
student.BorrowItem("L-005", book2, new DateTime(2026, 10, 1));
student.BorrowItem("L-006", book3, new DateTime(2026, 10, 1));
try
{
    student.BorrowItem("L-007", book4, new DateTime(2026, 10, 1));
}
catch (Exception ex)
{
    Console.WriteLine($"Rejected as expected (student's fourth loan): {ex.Message}");
}

var shelver = new Shelver("S-002", "Ali Shelver", "01044444444", new DateTime(2022, 5, 1), 9000m, "Fiction");
var librarian = new Librarian("S-003", "Nour Librarian", "01055555555", new DateTime(2021, 3, 1), 12000m);

Console.WriteLine("\n=== Monthly pay (List<Staff>) ===");
List<Staff> staffMembers = new List<Staff> { librarian, head, shelver };
foreach (Staff staff in staffMembers)
    Console.WriteLine($"{staff.FullName}: {staff.MonthlyPay:C}");

Console.WriteLine("\n=== Loan periods and daily late fees (List<LibraryItem>) ===");
List<LibraryItem> items = new List<LibraryItem> { book1, dvd, magazine };
foreach (LibraryItem item in items)
    Console.WriteLine($"{item.Title}: {item.LoanPeriodDays} days, daily fee {item.GetDailyLateFee():C}");

Console.WriteLine("\n=== Premium member returns DVD five days late ===");
// Borrow date 2026-10-01; DVD due date 2026-10-08; return on 2026-10-13 = 5 days late.
librarian.ProcessReturn(firstLoan, new DateTime(2026, 10, 13));
Console.WriteLine($"Due date: {firstLoan.DueDate:yyyy-MM-dd}");
Console.WriteLine($"Late fee before discount: {5 * dvd.GetDailyLateFee():C}");
Console.WriteLine($"Late fee after {premium.DiscountPercentage}% discount: {firstLoan.LateFee:C}");
Console.WriteLine($"Premium reading points: {premium.ReadingPoints}");

try
{
    firstLoan.Return(new DateTime(2026, 10, 14));
}
catch (Exception ex)
{
    Console.WriteLine($"Rejected as expected (return twice): {ex.Message}");
}

try
{
    librarian.MarkItemLost(firstLoan);
}
catch (Exception ex)
{
    Console.WriteLine($"Rejected as expected (mark returned loan lost): {ex.Message}");
}

try
{
    librarian.GiveRaise(0m);
}
catch (Exception ex)
{
    Console.WriteLine($"Rejected as expected (zero raise): {ex.Message}");
}

try
{
    head.ChangeLateFee(book1, -1m);
}
catch (Exception ex)
{
    Console.WriteLine($"Rejected as expected (negative late fee): {ex.Message}");
}

try
{
    Book dateTestBook = new Book("B-099", "Date Test", 2m);
    Loan dateLoan = student.BorrowItem("L-099", dateTestBook, new DateTime(2026, 10, 10));
    dateLoan.Return(new DateTime(2026, 10, 9));
}
catch (Exception ex)
{
    Console.WriteLine($"Rejected as expected (return before borrow date): {ex.Message}");
}

Console.WriteLine("\n=== Additional actions ===");
shelver.Reassign("Children");
librarian.GiveRaise(5m);
Console.WriteLine($"{shelver.FullName} reassigned to {shelver.Section}.");
Console.WriteLine($"{librarian.FullName}'s new salary: {librarian.MonthlySalary:C}");
Console.WriteLine($"Book daily fee before/after pricing can be changed by Head Librarian: {book1.GetDailyLateFee():C}");
