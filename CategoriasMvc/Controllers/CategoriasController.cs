using Microsoft.AspNetCore.Mvc;
using CategoriasMvc.Services;
using CategoriasMvc.Models;

namespace CategoriasMvc.Controllers;

public class CategoriasController : Controller
{
    private readonly ICategoriaService _catagoriaService;

    public CategoriasController(ICategoriaService catagoriaService)
    {
        _catagoriaService = catagoriaService;
    }

    public async Task<ActionResult<IEnumerable<CategoriaViewModel>>> Index()
    {
        var result = await _catagoriaService.GetCategorias();

        if (result is null)
            return View("Error");

        return View(result);
    }
    [HttpGet]
    public IActionResult CriarNovaCategoria()
    {
        return View();
    }
    [HttpPost]
    public async Task<ActionResult<CategoriaViewModel>> CriarNovaCategoria(CategoriaViewModel categoriaVM)
    {
        if (ModelState.IsValid)
        {
            var result = await _catagoriaService.CriaCategoria(categoriaVM);

            if (result != null)
                return RedirectToAction(nameof(Index));
        }
        ViewBag.Erro = "Erro ao criar Categoria";
        return View(categoriaVM);

    }

    [HttpGet]
    public async Task<IActionResult> AtualizarCategoria(int id)
    {
        var result = await _catagoriaService.GetCategoriaPorId(id);

        if( result is null)
        {
            return View("Error");
        }
        return View(result);
    }
    [HttpPost]
    public async Task<ActionResult<CategoriaViewModel>>AtualizarCategoria(int id, CategoriaViewModel categoriaVM)
    {
        if (ModelState.IsValid)
        {
            var result = await _catagoriaService.AtualizaCategoria(id, categoriaVM);

            if (result)
            {
                return RedirectToAction(nameof(Index));
            }
        }
        return View(categoriaVM);
    }
    [HttpGet]
    public async Task<IActionResult> DeletarCategoria(int id)
    {
        var result = _catagoriaService.DeletaCategoria(id);
        if(result is null)
        {
            return View("Error");
        }
        return View(result);
    }

    [HttpPost(), ActionName("DeletarCategoria")]
    public async Task<IActionResult> DeletaCategoria(int id)
    {
        var result = await _catagoriaService.DeletaCategoria(id);
        if(result)
        {
            return RedirectToAction(nameof(Index));
        }
        return View("Error");
    }
}

