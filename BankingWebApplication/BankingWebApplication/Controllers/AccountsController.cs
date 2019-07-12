using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DAL;
using DAL.Entities;
using BusinessLayer;
using Microsoft.AspNetCore.Http;

namespace BankingWebApplication.Controllers
{
    public class AccountsController : Controller
    {
        static IAccount ca;
        static IAccount ba;
        static IAccount la;
        static IAccount td;
        private readonly ApplicationDbContext _context;

       AccountBL accountbl = new AccountBL();

        public AccountsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Accounts
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Account.Include(a => a.customer);
            if (HttpContext.Session.GetString("CustomerId") != null)
            {
                return View(await applicationDbContext.ToListAsync());
            }
            else
            {
                return RedirectToAction("Login", "Customers");
            }
            
        }

        // GET: Accounts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var account = await _context.Account
                .Include(a => a.customer)
                .FirstOrDefaultAsync(m => m.Accountno == id);
            if (account == null)
            {
                return NotFound();
            }

            return View(account);
        }

        // GET: Accounts/Create
        public IActionResult OpenAccount()
        {
            ViewData["CustomerId"] = new SelectList(_context.Customer, "CustomerId", "UserName");
            return View();
        }

        // POST: Accounts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult OpenAccount([Bind("Balance,Accountno,AccountType,CustomerId,AccountStatus,MaturityDateTime,Interestrate")] Account account)
        {

            if (ModelState.IsValid)
            {
                account.Accountno = accountbl.GenerateAccountno();

                if (account.AccountType == "Checking")
                {

                    ca = new CheckingAccount()
                    {
                        Accountno = accountbl.GenerateAccountno(),
                      
                        CustomerId = account.CustomerId,
                        Balance = account.Balance,
                        AccountStatus = accountbl.GetAccountStatus(),
                        MaturityDateTime = account.MaturityDateTime,
                        AccountType = account.AccountType,
                       
                    };
                 
                    ca.Interestrate = ca.GetIntrestrate();
                    accountbl.OpenAccount(ca, _context);

                }
                else if (account.AccountType == "Business")
                {

                    ba = new BusinessAccount()
                    {
                        Accountno = accountbl.GenerateAccountno(),

                        CustomerId = account.CustomerId,
                        Balance = account.Balance,
                        AccountStatus = accountbl.GetAccountStatus(),
                        MaturityDateTime = account.MaturityDateTime,
                        AccountType = account.AccountType,

                    };

                    ba.Interestrate = ba.GetIntrestrate();
                    accountbl.OpenAccount(ba, _context);

                }
                else if (account.AccountType == "Loan")
                {

                    la = new Loan()
                    {
                        Accountno = accountbl.GenerateAccountno(),

                        CustomerId = account.CustomerId,
                        Balance = account.Balance,
                        AccountStatus = accountbl.GetAccountStatus(),
                        MaturityDateTime = account.MaturityDateTime,
                        AccountType = account.AccountType,

                    };

                    la.Interestrate = la.GetIntrestrate();
                    accountbl.OpenAccount(la, _context);

                }
                else if (account.AccountType == "Term Deposit")
                {

                    td = new TermDeposit(0)
                    {
                        Accountno = accountbl.GenerateAccountno(),

                        CustomerId = account.CustomerId,
                        Balance = account.Balance,
                        AccountStatus = accountbl.GetAccountStatus(),
                        MaturityDateTime = account.MaturityDateTime,
                        AccountType = account.AccountType,

                    };

                    td.Interestrate = td.GetIntrestrate();
                    accountbl.OpenAccount(td, _context);

                }

             

                return RedirectToAction(nameof(Index));
            
            }
            //ViewData["CustomerId"] = new SelectList(_context.Customer, "CustomerId", "Email", account.CustomerId);
            return View(account);
        }

        // GET: Accounts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var account = await _context.Account.FindAsync(id);
            if (account == null)
            {
                return NotFound();
            }
            ViewData["CustomerId"] = new SelectList(_context.Customer, "CustomerId", "Email", account.CustomerId);
            return View(account);
        }

        // POST: Accounts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Balance,Accountno,AccountType,CustomerId,AccountStatus,MaturityDateTime,Interestrate")] Account account)
        {
            if (id != account.Accountno)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(account);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AccountExists(account.Accountno))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CustomerId"] = new SelectList(_context.Customer, "CustomerId", "Email", account.CustomerId);
            return View(account);
        }

        // GET: Accounts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var account = await _context.Account
                .Include(a => a.customer)
                .FirstOrDefaultAsync(m => m.Accountno == id);
            if (account == null)
            {
                return NotFound();
            }

            return View(account);
        }

        // POST: Accounts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var account = await _context.Account.FindAsync(id);

            account.AccountStatus = false;
            if(account.Balance >= 0)
            {
                _context.Account.Update(account);
                await _context.SaveChangesAsync();
            }
            else
            {
                //unable to close account, balance less than 0
            }

            return RedirectToAction(nameof(Index));
        }

        private bool AccountExists(int id)
        {
            return _context.Account.Any(e => e.Accountno == id);
        }

        public IActionResult Deposit(int? id)
        {
            IAccount acc = _context.Account.Find(id);


       
            return View(acc);
        }

        [HttpPost]
        
        public IActionResult Deposit(int? id, decimal amount)
        {

            if (id == null)
            {
                return NotFound();
            }

            IAccount acc = _context.Account.FirstOrDefault(a => a.Accountno == id) ;// get account info 

            accountbl.Deposit(acc, amount, _context); // calling businesslayer


            return RedirectToAction("Index", "Accounts"); // redirect link to index
    
        }


        public IActionResult Withdraw(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            else
            {
                IAccount acc = _context.Account.Find(id);
                return View(acc);

            }

        }

        [HttpPost]
        public ActionResult Withdraw(int? id, decimal amount)
        {
            if (id == null)
            {
                return NotFound();
            }
            else
            {

                IAccount acc = _context.Account.Find(id); // get account info 

                accountbl.Withdraw(acc, amount, _context); // calling businesslayer

                return RedirectToAction("Index", "Accounts"); // redirect link to index

            }

        }

        public IActionResult Transfer(int? id, decimal amount)
        {

            if (id == null)
            {
                return NotFound();
            }
            else
            {
                Account acc = _context.Account.Find(id); // get account info 



                var AccList =
                    _context.Account
                    .Where(a => a.CustomerId == acc.CustomerId)
                    .Where(a => a.Accountno != acc.Accountno).ToList();

                TransferAccount ta = new TransferAccount()
                {
                    account = acc,
                    accounts = AccList

                };



                return View(ta);
            }//end
        }

        [HttpPost]
        public IActionResult Transfer(int? id, int ToAccountno, decimal amount)
        {
            if (id == null)
            {
                return NotFound();
            }
            else
            {

                Account fromAccount = _context.Account.Find(id); // find transfer account from
                Account toAccount = _context.Account.Find(ToAccountno); /// get to acc

                accountbl.Transfer(fromAccount, toAccount, amount, _context); // calling businesslayer


                return RedirectToAction("Index", "Accounts"); // redirect link to index
            }//end else
        }
        public IActionResult PayLoan(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            else
            {
                Account acc = _context.Account.Find(id); // get account info 



                var AccList =
                    _context.Account
                    .Where(a => a.CustomerId == acc.CustomerId)
                    .Where(a => a.Accountno != acc.Accountno).ToList();

                TransferAccount ta = new TransferAccount()
                {
                    account = acc,
                    accounts = AccList

                };

                return View(ta);
            }
        }

        [HttpPost]
            public ActionResult PayLoan(int? id, int FromAccountno, decimal amount)
        {
            if (id == null)
            {
                return NotFound();
            }
            else
            {
                Account toAccount = _context.Account.Find(id); // find transfer account from

                Account fromAccount = _context.Account.Find(FromAccountno); /// select account to pay loan

                accountbl.PayLoan(toAccount, fromAccount, amount, _context); // calling businesslayer

               
                return RedirectToAction("Index", "Accounts"); // redirect link to index
            }
        }

        
        public  IActionResult TransactionList(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            else
            {
                var account = _context.Account.Where(x => x.Accountno == id).FirstOrDefault();

                var list = _context.Transaction.Where(x => x.Accountno == account.Accountno && x.CustomerId == account.CustomerId).Take(10).OrderByDescending(t => t.Time);

                return View(list);
            }
        }

       
        public IActionResult TransactionRange(int? id)
        {

            var acc = _context.Account.Find(id);
       
                return View(acc);
            

        }

        [HttpPost]
        public IActionResult TransactionView(int id, DateTime startTime, DateTime endTime)
        {
            

            var account = _context.Account.Where(x => x.Accountno == id).FirstOrDefault();

            var list = _context.Transaction
                .Where(x => x.Accountno == account.Accountno && x.CustomerId == account.CustomerId &&  x.Time > startTime && x.Time <= endTime);
           

                return View(list);
            
        

        }



    }
}
