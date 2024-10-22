﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

using Web.Mvc.Data;
using Web.Mvc.Domain;

namespace Web.Mvc.Controllers;

[Authorize]
public class PostTagsController : Controller
{
    private readonly ApplicationDbContext _context;

    public PostTagsController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: PostTags
    public async Task<IActionResult> Index()
    {
        var applicationDbContext = _context.PostTags.Include(p => p.Post).Include(p => p.Tag);
        return View(await applicationDbContext.ToListAsync());
    }

    // GET: PostTags/Details/5
    public async Task<IActionResult> Details(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var postTag = await _context.PostTags
            .Include(p => p.Post)
            .Include(p => p.Tag)
            .FirstOrDefaultAsync(m => m.PostId == id);
        if (postTag == null)
        {
            return NotFound();
        }

        return View(postTag);
    }

    // GET: PostTags/Create
    public IActionResult Create()
    {
        ViewData["PostId"] = new SelectList(_context.Posts, "Id", "Content");
        ViewData["TagId"] = new SelectList(_context.Tags, "Id", "Name");
        return View();
    }

    // POST: PostTags/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("PostId,TagId")] PostTag postTag)
    {
        if (ModelState.IsValid)
        {
            _context.Add(postTag);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        ViewData["PostId"] = new SelectList(_context.Posts, "Id", "Content", postTag.PostId);
        ViewData["TagId"] = new SelectList(_context.Tags, "Id", "Name", postTag.TagId);
        return View(postTag);
    }

    // GET: PostTags/Edit/5
    public async Task<IActionResult> Edit(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var postTag = await _context.PostTags.FindAsync(id);
        if (postTag == null)
        {
            return NotFound();
        }
        ViewData["PostId"] = new SelectList(_context.Posts, "Id", "Content", postTag.PostId);
        ViewData["TagId"] = new SelectList(_context.Tags, "Id", "Name", postTag.TagId);
        return View(postTag);
    }

    // POST: PostTags/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Guid id, [Bind("PostId,TagId")] PostTag postTag)
    {
        if (id != postTag.PostId)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(postTag);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PostTagExists(postTag.PostId))
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
        ViewData["PostId"] = new SelectList(_context.Posts, "Id", "Content", postTag.PostId);
        ViewData["TagId"] = new SelectList(_context.Tags, "Id", "Name", postTag.TagId);
        return View(postTag);
    }

    // GET: PostTags/Delete/5
    public async Task<IActionResult> Delete(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var postTag = await _context.PostTags
            .Include(p => p.Post)
            .Include(p => p.Tag)
            .FirstOrDefaultAsync(m => m.PostId == id);
        if (postTag == null)
        {
            return NotFound();
        }

        return View(postTag);
    }

    // POST: PostTags/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(Guid id)
    {
        var postTag = await _context.PostTags.FindAsync(id);
        if (postTag != null)
        {
            _context.PostTags.Remove(postTag);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool PostTagExists(Guid id)
    {
        return _context.PostTags.Any(e => e.PostId == id);
    }
}
