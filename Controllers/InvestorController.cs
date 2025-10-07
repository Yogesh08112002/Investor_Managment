using Microsoft.AspNetCore.Mvc;
using InvestorManagementAPI.Data;
using InvestorManagementAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InvestorManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvestorController : ControllerBase
    {
        private readonly AppDbContext _context;

        public InvestorController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/investor
        // GET all investors with portfolios and transactions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Investor>>> GetInvestors()
        {
            return await _context.Investors
                .Include(i => i.InvestmentPortfolios)
                    .ThenInclude(p => p.Transactions)
                .ToListAsync();
        }

        // GET single investor
        [HttpGet("{id}")]
        public async Task<ActionResult<Investor>> GetInvestor(int id)
        {
            var investor = await _context.Investors
                .Include(i => i.InvestmentPortfolios)
                    .ThenInclude(p => p.Transactions)
                .FirstOrDefaultAsync(i => i.Id == id);

            if (investor == null) return NotFound();

            return investor;
        }

        // POST new investor - consider using a DTO here!
        [HttpPost]
        public async Task<ActionResult<Investor>> PostInvestor(Investor investor)
        {
            _context.Investors.Add(investor);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetInvestor), new { id = investor.Id }, investor);
        }

        // PUT update investor
        [HttpPut("{id}")]
        public async Task<IActionResult> PutInvestor(int id, Investor investor)
        {
            if (id != investor.Id) return BadRequest();

            _context.Entry(investor).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InvestorExists(id))
                    return NotFound();
                else
                    throw;
            }

            return NoContent();
        }

        // DELETE an investor
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInvestor(int id)
        {
            var investor = await _context.Investors.FindAsync(id);
            if (investor == null) return NotFound();

            _context.Investors.Remove(investor);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool InvestorExists(int id) => _context.Investors.Any(e => e.Id == id);

    }

}
