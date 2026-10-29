using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ZombieParty.Utility;
using Microsoft.EntityFrameworkCore;
using ZombieParty.Models;
using ZombieParty.Models.Data;

namespace ZombieParty.Controllers
{
    public class ZombieController : Controller
    {
        private ZombiePartyDbContext context;
        private IImageFileManager _imageFileManager;

        public ZombieController(ZombiePartyDbContext context, IImageFileManager imageFileManager)
        {
            this.context = context;
            _imageFileManager = imageFileManager;
        }

        public async Task<IActionResult> Index()
        {
            this.ViewBag.MaListe = await this.context.Zombie.ToListAsync();

            return View();
        }

        //GET UPSERT
        public async Task<IActionResult> Upsert(int zombieId = 0)
        {
            if (zombieId == 0)
            {
                ViewBag.ZombieTypes = new SelectList(await this.context.ZombieType.ToListAsync(), "Id", "TypeName", null);
                return View(new Zombie());

            }
            else
            {
                var zombie = await this.context.Zombie.FindAsync(zombieId);
                ZombieType selectedZombieType = await this.context.ZombieType.FirstOrDefaultAsync(x => x.Id == zombie.ZombieTypeId);

                ViewBag.ZombieTypes = new SelectList(await this.context.ZombieType.ToListAsync(), "Id", "TypeName", selectedZombieType);
                return View(zombie);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Upsert(Zombie zombie)
        {
            if (ModelState.IsValid)
            {
                IFormCollection form = HttpContext.Request.Form;
                bool edit = zombie.Id != 0;
                string? oldImagePath = zombie.Image;

                // Début du code qui doit être déplacer dans ImageFileManager
                zombie.Image = _imageFileManager.UploadImage(form, edit, oldImagePath);
                // Fin du code qui doit être déplacer dans ImageFileManager

                if (zombie.Id == 0)
                    this.context.Zombie.Add(zombie);
                else
                    this.context.Update(zombie);

                await this.context.SaveChangesAsync();
                return this.RedirectToAction("Index");
            }

            //Je vais chercher le type sélectionné. 
            ZombieType selectedZombieType = await this.context.ZombieType.FirstAsync(x => x.Id == zombie.ZombieTypeId);
            ViewBag.ZombieTypes = new SelectList(await this.context.ZombieType.ToListAsync(), "Id", "TypeName", selectedZombieType);
            return this.View(zombie);
        }
    }
}
