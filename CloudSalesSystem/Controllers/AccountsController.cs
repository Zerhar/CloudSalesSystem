using CloudSalesSystem.Data;
using CloudSalesSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CloudSalesSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly ApplicationDbContext _context; 

        public AccountsController(ApplicationDbContext context) 
        {
            _context = context;
        }

        // Get all accounts
        [HttpGet]
        public async Task<IActionResult> GetAccounts()
        {
            var accounts = await _context.Accounts
                             .Include(a => a.Softwares) 
                             .ToListAsync(); 
            return Ok(accounts);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAccount([FromBody] Account account)
        {
            DateTime currentDate = DateTime.UtcNow; 
            foreach (var software in account.Softwares)
            {
                software.IsActive = software.StartDate <= currentDate && software.ValidTo >= currentDate;
            }

            _context.Accounts.Add(account); 
            await _context.SaveChangesAsync(); 
            return CreatedAtAction(nameof(GetAccount), new { id = account.Id }, account); 
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAccount(int id)
        {
            var account = await _context.Accounts
                            .Include(a => a.Softwares)
                            .FirstOrDefaultAsync(a => a.Id == id); 
            if (account == null)
            {
                return NotFound(); 
            }

            return Ok(account);
        }
    }
}
