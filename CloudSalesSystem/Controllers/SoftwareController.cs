using CloudSalesSystem.Data;
using CloudSalesSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CloudSalesSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SoftwareController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public SoftwareController(ApplicationDbContext context)
        {
            _context = context;
        }

        // 1. Get list of accounts (based on customer names)
        [HttpGet("accounts")]
        public async Task<IActionResult> GetAccounts()
        {
            var accounts = await _context.Accounts
                             .Include(a => a.Softwares)
                             .ToListAsync();
            return Ok(accounts);
        }

        [HttpGet("available-services")]
        public ActionResult<List<string>> GetAvailableServices()
        {
            var services = new List<string> { "Microsoft Office", "Adobe Photoshop", "Google Cloud" };
            return Ok(services);
        }

        [HttpPost("order")]
        public async Task<IActionResult> OrderSoftware([FromBody] Software newSoftware)
        {
            newSoftware.IsActive = newSoftware.StartDate <= DateTime.UtcNow && newSoftware.ValidTo >= DateTime.UtcNow;

            _context.Softwares.Add(newSoftware);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetSoftwareById), new { id = newSoftware.Id }, newSoftware);
        }

        [HttpGet("licenses/{accountId}")]
        public async Task<IActionResult> GetLicensesByAccount(int accountId)
        {
            var licenses = await _context.Softwares.Where(s => s.AccountId == accountId).ToListAsync();
            return Ok(licenses);
        }

        [HttpPut("change-quantity/{id}")]
        public async Task<IActionResult> ChangeQuantity(int id, [FromBody] int newQuantity)
        {
            var software = await _context.Softwares.FindAsync(id);
            if (software == null) return NotFound();

            software.Quantity = newQuantity;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("cancel/{id}")]
        public async Task<IActionResult> CancelSoftware(int id)
        {
            var software = await _context.Softwares.FindAsync(id);
            if (software == null) return NotFound();

            _context.Softwares.Remove(software);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpPut("update-dates/{id}")]
        public async Task<IActionResult> UpdateLicenseDates(int id, [FromBody] Software updateSoftware)
        {
            var software = await _context.Softwares.FindAsync(id);
            if (software == null) return NotFound();

            if (updateSoftware.StartDate == default)
            {
                software.StartDate = DateTime.Today;
            }
            else
            {
                software.StartDate = updateSoftware.StartDate;
            }

            software.ValidTo = updateSoftware.ValidTo;

            software.IsActive = software.StartDate <= DateTime.UtcNow && software.ValidTo >= DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSoftwareById(int id)
        {
            var software = await _context.Softwares.FindAsync(id);
            if (software == null) return NotFound();

            return Ok(software);
        }
    }
}
