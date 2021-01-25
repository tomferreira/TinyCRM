using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using TinyCRM.Application.Interfaces;
using TinyCRM.Application.ViewModels.NaturalPerson;
using TinyCRM.Domain.Exceptions;
using TinyCRM.Web.MVC.Resources;

namespace TinyCRM.Web.MVC.Controllers
{
    [Route("[controller]/[action]")]
    [Authorize]
    public class NaturalPersonController : Controller
    {
        private INaturalPersonService _personService;
        private IAddressService _addressService;
        private readonly ISharedViewLocalizer _sharedLocalizer;
        private readonly ILogger<NaturalPersonController> _logger;

        public NaturalPersonController(
            INaturalPersonService personService, 
            IAddressService addressService,
            ISharedViewLocalizer sharedLocalizer,
            ILogger<NaturalPersonController> logger)
        {
            _personService = personService;
            _addressService = addressService;
            _sharedLocalizer = sharedLocalizer;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            var model = await _personService.AllAsync();
            return View(model);
        }

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

        // GET: /NaturalPerson/Create
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.CountryList = _addressService.GetContriesName();

            return View();
        }

        // POST: /NaturalPerson/Create
        [HttpPost]
        public IActionResult Create([FromForm] NaturalPersonViewModel model)
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

                ModelState.AddModelError(string.Empty, _sharedLocalizer["GenericErrorMessage"]);
            }

            ViewBag.CountryList = _addressService.GetContriesName();

            return View(model);
        }

        // GET: /NaturalPerson/Edit/5
        [HttpGet("{id:int}")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var model = await _personService.GetAsync(id.Value);

            if (model == null)
                return NotFound();

            ViewBag.CountryList = _addressService.GetContriesName();

            return View(model);
        }

        // POST: /NaturalPerson/Edit/5
        [HttpPost("{id:int}")]
        public async Task<ActionResult> Edit(int id, [FromForm] NaturalPersonViewModel model)
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

                ModelState.AddModelError(string.Empty, _sharedLocalizer["GenericErrorMessage"]);
            }

            ViewBag.CountryList = _addressService.GetContriesName();

            return View(model);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _personService.DeleteAsync(id);

            return RedirectToAction(nameof(Index));
        }
    }
}
