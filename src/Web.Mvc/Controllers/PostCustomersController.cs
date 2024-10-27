using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Web.Mvc.Data;
using Web.Mvc.Domain;

namespace Web.Mvc.Controllers
{
    public class PostCustomersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PostCustomersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: PostCustomers
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.PostCustomers.Include(p => p.Channel).Include(p => p.Customer).Include(p => p.User);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: PostCustomers/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var postCustomer = await _context.PostCustomers
                .Include(p => p.Channel)
                .Include(p => p.Customer)
                .Include(p => p.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (postCustomer == null)
            {
                return NotFound();
            }

            return View(postCustomer);
        }

        // GET: PostCustomers/Create
        public IActionResult Create()
        {
            var model = new PostCustomer();
            model.Cards.Add(new PostCard());

            ViewData["ChannelId"] = new SelectList(_context.Channels, "Id", "Name", null);
            ViewData["CustomerId"] = new SelectList(_context.Set<Customer>(), "Id", "Name", null);
            ViewData["CardId"] = new SelectList(_context.Set<Card>(), "Id", "Name", null);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "UserName");
            
            return View(model);
        }

        // POST: PostCustomers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Cards,Title,Content,Link,ChannelId,UserId,CustomerId,Id")] PostCustomer postCustomer)
        {
            if (ModelState.IsValid)
            {
                postCustomer.Id = Guid.NewGuid();
                _context.Add(postCustomer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ChannelId"] = new SelectList(_context.Channels, "Id", "Name", postCustomer.ChannelId);
            ViewData["CustomerId"] = new SelectList(_context.Set<Customer>(), "Id", "Name", postCustomer.CustomerId);
            ViewData["CardId"] = new SelectList(_context.Set<Customer>(), "Id", "Name", postCustomer.Cards);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "UserName", postCustomer.UserId);
            return View(postCustomer);
        }

        // GET: PostCustomers/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var postCustomer = await _context.PostCustomers.FindAsync(id);
            if (postCustomer == null)
            {
                return NotFound();
            }
            ViewData["ChannelId"] = new SelectList(_context.Channels, "Id", "Name", postCustomer.ChannelId);
            ViewData["CustomerId"] = new SelectList(_context.Set<Customer>(), "Id", "Name", postCustomer.CustomerId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "UserName", postCustomer.UserId);
            return View(postCustomer);
        }

        // POST: PostCustomers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Title,Content,Link,ChannelId,UserId,CustomerId,Id")] PostCustomer postCustomer)
        {
            if (id != postCustomer.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(postCustomer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PostCustomerExists(postCustomer.Id))
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
            ViewData["ChannelId"] = new SelectList(_context.Channels, "Id", "Name", postCustomer.ChannelId);
            ViewData["CustomerId"] = new SelectList(_context.Set<Customer>(), "Id", "Name", postCustomer.CustomerId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "UserName", postCustomer.UserId);
            return View(postCustomer);
        }

        // GET: PostCustomers/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var postCustomer = await _context.PostCustomers
                .Include(p => p.Channel)
                .Include(p => p.Customer)
                .Include(p => p.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (postCustomer == null)
            {
                return NotFound();
            }

            return View(postCustomer);
        }

        // POST: PostCustomers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var postCustomer = await _context.PostCustomers.FindAsync(id);
            if (postCustomer != null)
            {
                _context.PostCustomers.Remove(postCustomer);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PostCustomerExists(Guid id)
        {
            return _context.PostCustomers.Any(e => e.Id == id);
        }
    }
}
