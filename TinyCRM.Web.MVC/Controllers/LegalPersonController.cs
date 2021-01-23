using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using TinyCRM.Application.Interfaces;
using TinyCRM.Application.ViewModels.LegalPerson;
using TinyCRM.Domain.Exceptions;

namespace TinyCRM.Web.MVC.Controllers
{
    [Route("[controller]/[action]")]
    [Authorize]
    public class LegalPersonController : Controller
    {
        private ILegalPersonService _personService;
        private readonly ILogger<LegalPersonController> _logger;

        public LegalPersonController(ILegalPersonService personService, ILogger<LegalPersonController> logger)
        {
            _personService = personService;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            var model = await _personService.AllAsync();
            return View(model);
        }

        // GET: /LegalPerson/Show/1
        [HttpGet("{id:int}")]
        public async Task<IActionResult> Show(int? id)
        {
            if (id == null)
                return NotFound();

            var model = await _personService.GetAsync(id.Value);

            if (model == null)
                return NotFound();

            return View(model);
        }

        // GET: /LegalPerson/Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // POST: /LegalPerson/Create
        [HttpPost]
        public IActionResult Create([FromForm] LegalPersonViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _personService.Add(model);

                    return RedirectToAction(nameof(Index));
                }
            }
            catch (BusinessRuleException ex)
            {
                ModelState.AddModelError(ex.Key, ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

                ModelState.AddModelError(string.Empty,
                    "Unable to save changes. Try again, and if the problem persists contact your system administrator.");
            }

            return View(model);
        }

        // GET: /LegalPerson/Edit/5
        [HttpGet("{id:int}")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var model = await _personService.GetAsync(id.Value);

            if (model == null)
                return NotFound();

            return View(model);
        }

        // PUT: /LegalPerson/Edit/5
        [HttpPost("{id:int}")]
        public async Task<ActionResult> Edit(int id, [FromForm] LegalPersonViewModel model)
        {
            if (id != model.Id)
                return NotFound();

            try
            {
                if (ModelState.IsValid)
                {
                    await _personService.UpdateAsync(model);

                    return RedirectToAction(nameof(Index));
                }
            }
            catch (BusinessRuleException ex)
            {
                ModelState.AddModelError(ex.Key, ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

                ModelState.AddModelError(string.Empty,
                    "Unable to save changes. Try again, and if the problem persists contact your system administrator.");
            }

            return View(model);
        }
    }
}
