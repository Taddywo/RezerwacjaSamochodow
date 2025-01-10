using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RezerwacjaSamochodow.Models;
using System.Linq;
using System.Threading.Tasks;

[Authorize]
public class ReservationsController : Controller
{
    private readonly ApplicationDbContext _context;

    public ReservationsController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: Reservations
    public async Task<IActionResult> Index()
    {
        var reservations = _context.Reservations
            .Include(r => r.Car)
            .Include(r => r.User);

        if(HttpContext.User.Identities.First().Claims.Any(x => x.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/role" && x.Value == "Admin"))
        {
            return View(await reservations.ToListAsync());
        }
        string userId = HttpContext.User.Identities.First().Claims
            .First(x => x.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier").Value;
        var filteredReservations = reservations
            .Where(r => r.User.Id == userId);

        return View(await filteredReservations.ToListAsync());
    }

    // GET: Reservations/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var reservation = await _context.Reservations
            .Include(r => r.Car)
            .Include(r => r.User)
            .FirstOrDefaultAsync(m => m.Id == id);

        if (reservation == null)
        {
            return NotFound();
        }

        return View(reservation);
    }

    // GET: Reservations/Create
    public IActionResult Create()
    {
        // No cars shown initially, user needs to select a date
        ViewData["CarId"] = new SelectList(Enumerable.Empty<Car>(), "Id", "Brand");
        return View();
    }

    // POST: Reservations/Create - Step 1: Filter available cars
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult CreateFilter([Bind("StartDate,EndDate")] ReservationDto reservationDto)
    {
        if (reservationDto.StartDate >= reservationDto.EndDate)
        {
            ModelState.AddModelError("", "End date must be after start date.");
        }

        if (ModelState.IsValid)
        {
            // Find cars that are not reserved in the specified date range
            //DateTime StartDate = new DateTime(reservationDto.StartDate.Ticks, DateTimeKind.Utc);
            //DateTime EndDate = new DateTime(reservationDto.EndDate.Ticks, DateTimeKind.Utc);
            var reservedCarIds = _context.Reservations
                .Where(r => r.StartDate < reservationDto.EndDate && r.EndDate > reservationDto.StartDate)
                .Select(r => r.CarId)
                .Distinct()
                .ToList();

            var availableCars = _context.Cars
                .Where(car => !reservedCarIds.Contains(car.Id))
                .ToList();

            ViewData["CarId"] = new SelectList(availableCars, "Id", "Brand");
            ViewBag.StartDate = reservationDto.StartDate;
            ViewBag.EndDate = reservationDto.EndDate;

            return View("Create", reservationDto);
        }

        ViewData["CarId"] = new SelectList(Enumerable.Empty<Car>(), "Id", "Brand");
        return View("Create", reservationDto);
    }

    // POST: Reservations/Create - Step 2: Finalize reservation
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("CarId,StartDate,EndDate")] ReservationDto reservationDto)
    {
        if (ModelState.IsValid)
        {
            var reservation = new Reservation
            {
                StartDate = reservationDto.StartDate,
                EndDate = reservationDto.EndDate,
                CarId = reservationDto.CarId
            };

            string userId = HttpContext.User.Identities.First().Claims
                .First(x => x.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier").Value;

            reservation.User = await _context.Users.SingleAsync(x => x.Id == userId);

            _context.Add(reservation);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        ViewData["CarId"] = new SelectList(Enumerable.Empty<Car>(), "Id", "Brand");
        return View(reservationDto);
    }

    // GET: Reservations/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var reservation = await _context.Reservations.FindAsync(id);
        if (reservation == null)
        {
            return NotFound();
        }

        ViewData["CarId"] = new SelectList(_context.Cars, "Id", "Brand", reservation.CarId);
        return View(reservation);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("StartDate,EndDate")] ReservationDto reservationDto)
    {
        if (!ModelState.IsValid)
        {
            ModelState.AddModelError("", "Invalid data provided.");
            return View(reservationDto);
        }

        // Fetch the reservation from the database
        var reservation = await _context.Reservations.Include(r => r.Car).FirstOrDefaultAsync(r => r.Id == id);
        if (reservation == null)
        {
            return NotFound();
        }

        // Validate dates
        if (reservationDto.StartDate >= reservationDto.EndDate)
        {
            ModelState.AddModelError("", "End date must be after the start date.");
            return View(reservationDto);
        }

        try
        {
            // Update reservation dates and save
            reservation.StartDate = DateTime.SpecifyKind(reservationDto.StartDate, DateTimeKind.Utc);
            reservation.EndDate = DateTime.SpecifyKind(reservationDto.EndDate, DateTimeKind.Utc);

            _context.Update(reservation);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!ReservationExists(id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }
    }


    // GET: Reservations/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var reservation = await _context.Reservations
            .Include(r => r.Car)
            .Include(r => r.User)
            .FirstOrDefaultAsync(m => m.Id == id);

        if (reservation == null)
        {
            return NotFound();
        }

        return View(reservation);
    }

    // POST: Reservations/Delete/5
    [HttpPost, ActionName("DeleteConfirmed")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var reservation = await _context.Reservations.FindAsync(id);
        _context.Reservations.Remove(reservation);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool ReservationExists(int id)
    {
        return _context.Reservations.Any(e => e.Id == id);
    }
}
