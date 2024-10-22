using System.Security.Claims;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

using Web.Mvc.Data;
using Web.Mvc.Domain;
using Web.Mvc.Domain.Base;

namespace Web.Mvc.Controllers;


[Authorize]
public class PostsController : BaseController
{
    private readonly ApplicationDbContext _context;
    private readonly SignInManager<User> _signInManager;
    private readonly ILogger<PostsController> _logger;

    public PostsController(
        ApplicationDbContext context,
        SignInManager<User> signInManager,
        ILogger<PostsController> logger,
        IAppIdentityUser user) : base(user)
    {
        _context = context;
        _signInManager = signInManager;
        _logger = logger;
    }

    // GET: Posts
    public async Task<IActionResult> Index()
    {
        var applicationDbContext = _context.Posts
            .Include(p => p.Category)
            .Include(p => p.User)
            .Select(p => new Post
            {
                Id = p.Id,
                Title = p.Title,
                Content = p.Content,
                CreatedAt = p.CreatedAt.ToLocalTime(),
                UpdatedAt = p.UpdatedAt.ToLocalTime(),
                Category = p.Category,
                User = p.User
            });

        return View(await applicationDbContext.ToListAsync());
    }

    // GET: Posts/Details/5
    public async Task<IActionResult> Details(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var post = await _context.Posts
            .Include(p => p.Category)
            .Include(p => p.User)
            .FirstOrDefaultAsync(m => m.Id == id);
        if (post == null)
        {
            return NotFound();
        }

        return View(post);
    }

    // GET: Posts/Create
    public IActionResult Create()
    {
        ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name");
        ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");
        return View();
    }

    // POST: Posts/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Title,Content,CategoryId")] Post post)
    {
        if (!UserIsAuthenticated) throw new InvalidOperationException("Usuário não autenticado");

        post.UserId = UserId;

        if (ModelState.IsValid)
        {
            post.Id = Guid.NewGuid();

            _context.Add(post);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", post.CategoryId);
        ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", post.UserId);
        return View(post);
    }

    // GET: Posts/Edit/5
    public async Task<IActionResult> Edit(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var post = await _context.Posts.FindAsync(id);
        if (post == null)
        {
            return NotFound();
        }
        ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", post.CategoryId);
        ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", post.UserId);
        return View(post);
    }

    // POST: Posts/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Guid id, [Bind("Id,Title,Content,CreatedAt,UpdatedAt,UserId,CategoryId")] Post post)
    {
        if (id != post.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(post);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PostExists(post.Id))
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
        ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", post.CategoryId);
        ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", post.UserId);
        return View(post);
    }

    // GET: Posts/Delete/5
    public async Task<IActionResult> Delete(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var post = await _context.Posts
            .Include(p => p.Category)
            .Include(p => p.User)
            .FirstOrDefaultAsync(m => m.Id == id);
        if (post == null)
        {
            return NotFound();
        }

        return View(post);
    }

    // POST: Posts/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(Guid id)
    {
        var post = await _context.Posts.FindAsync(id);
        if (post != null)
        {
            _context.Posts.Remove(post);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool PostExists(Guid id)
    {
        return _context.Posts.Any(e => e.Id == id);
    }
}
