using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BudgetApi.Models;

namespace BudgetApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BudgetItemsController : ControllerBase
    {
        private readonly BudgetContext _context;

        public BudgetItemsController(BudgetContext context)
        {
            _context = context;
        }

        // GET: api/BudgetItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BudgetItemDTO>>> GetBudgetItems()
        {
            return await _context.BudgetItems
             .Select(x => BudgetToDTO(x))
             .ToListAsync();
        }

        // GET: api/BudgetItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BudgetItemDTO>> GetBudgetItem(long id)
        {
            var budgetItem = await _context.BudgetItems.FindAsync(id);

            if (budgetItem == null)
            {
                return NotFound();
            }

            return BudgetToDTO(budgetItem);
        }

        // PUT: api/BudgetItems/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBudgetItem(long id, BudgetItemDTO budgetDTO)
        {

            if (id != budgetDTO.Id)
            {
                return BadRequest();
            }

            var budgetItem = await _context.BudgetItems.FindAsync(id);
            if (budgetItem == null)
            {
                return NotFound();
            }

            budgetItem.CategoryName = budgetDTO.CategoryName;
            budgetItem.Amount = budgetDTO.Amount;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) when (!BudgetItemExists(id))
            {
                return NotFound();
            }

            return NoContent();
        }

        // POST: api/BudgetItems
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<BudgetItem>> PostBudgetItem(BudgetItemDTO budgetDTO)
        {
            var budgetItem = new BudgetItem
            {
                Amount = budgetDTO.Amount,
                CategoryName = budgetDTO.CategoryName
            };

            _context.BudgetItems.Add(budgetItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetBudgetItem),
                new { id = budgetItem.Id },
                BudgetToDTO(budgetItem));
        }

        // DELETE: api/BudgetItems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBudgetItem(long id)
        {
            var budgetItem = await _context.BudgetItems.FindAsync(id);
            if (budgetItem == null)
            {
                return NotFound();
            }

            _context.BudgetItems.Remove(budgetItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BudgetItemExists(long id)
        {
            return _context.BudgetItems.Any(e => e.Id == id);
        }

        private static BudgetItemDTO BudgetToDTO(BudgetItem budgetItem) =>
           new BudgetItemDTO
           {
               Id = budgetItem.Id,
               CategoryName = budgetItem.CategoryName,
               Amount = budgetItem.Amount
           };
    }
}
