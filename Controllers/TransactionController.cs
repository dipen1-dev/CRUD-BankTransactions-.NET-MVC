#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CRUD_BankTransactions.Models;

namespace CRUD_BankTransactions.Controllers
{
    public class TransactionController : Controller
    {
        private readonly TransactionDbContext _context;

        public TransactionController(TransactionDbContext context)
        {
            _context = context;
        }

        // GET: Transaction
        public async Task<IActionResult> Index()
        {
            return View(await _context.Transactions.ToListAsync());
        }

        /*async and await are used for asynchronous method*/
        // GET: Transaction/AddOrEdit
        public IActionResult AddOrEdit(int id = 0)//by default id=0 
        {
            if (id == 0) //on get request if id is not given that is add and if id is given that is edit mode
                return View(new Transaction());// for add we transfer simply object
            else
                return View(_context.Transactions.Find(id));// for edit we transfer object with corresponding id to be edit 
        }

        // POST: Transaction/AddOrEdit
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken] //this is for the validation
        public async Task<IActionResult> AddOrEdit([Bind("TransactionId,AccountNumber,BeneficiaryName,BankName,SWIFTCode,Amount,Date")] Transaction transaction)
        {
            //Bind is used to bind the given field to the object transaction
            if (ModelState.IsValid) //it checks the validation on the server side 
            {
                if (transaction.TransactionId == 0) //this is to assign current date when the mode is add
                {
                    transaction.Date = DateTime.Now;
                    _context.Add(transaction);//this makes ready to insert the entity to the database
                }
                else //this is for update
                    _context.Update(transaction); //this is not final edit to the database
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));//it redirect to the index
            }
            return View(transaction);//if not valid it will remain in addoredit 
        }


        // POST: Transaction/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id) //for delete we should pass id 
        {
            var transaction = await _context.Transactions.FindAsync(id);
            _context.Transactions.Remove(transaction);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
