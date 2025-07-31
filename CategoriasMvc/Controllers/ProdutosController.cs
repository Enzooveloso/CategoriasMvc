using CategoriasMvc.Models;
using CategoriasMvc.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Policy;

namespace CategoriasMvc.Controllers;

public class ProdutosController : Controller
{
    private readonly IProdutoService _produtoService;
    private readonly ICategoriaService _categoriaService;
    private string token = string.Empty;
    public ProdutosController(IProdutoService produtoService, ICategoriaService categoriaService)
    {
        _produtoService = produtoService;
        _categoriaService = categoriaService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProdutoViewModel>>> Index()
    {
        var result = await _produtoService.GetProdutos(ObtemTokenJwt());
        if (result is null)
        {
            return View("Error");
        }
        return View();
    }

    [HttpGet]
    public async Task<IActionResult> CriarNovoProduto()
    {
        ViewBag.CategoriaId = new SelectList(await _categoriaService.GetCategorias(), "CategoriaId", "Nome");

        return View();
    }
    [HttpPost]
    public async Task<ActionResult<ProdutoViewModel>> CriarNovoProduto(ProdutoViewModel produtoVM)
    {
        if (ModelState.IsValid)
        {
            var result = await _produtoService.CriaProdutos(produtoVM, ObtemTokenJwt());

            if (result != null)
                return RedirectToAction(nameof(Index));
        }
        else
        {
            ViewBag.CategoriaId = new SelectList(await _categoriaService.GetCategorias(), "CategoriaId", "Nome");

        }
        return View(produtoVM);
    }

    [HttpGet]
    public async Task<IActionResult> DetalhesProduto(int id)
    {
        var result = await _produtoService.GetProdutosPorId(id, ObtemTokenJwt());
        if (result is null)
        {
            return View("Error");
        }
        return View(result);
    }

    [HttpGet]
    public async Task<ActionResult> AtualizaProduto(int id)
    {
        var result = await _produtoService.GetProdutosPorId(id, ObtemTokenJwt());

        if(result is null)
        {
            return View("Error");
        }
        ViewBag.CategoriaId = new SelectList(await _categoriaService.GetCategorias(), "CategroiasId", "Nome");
        return View(result);

    }

    [HttpGet]
    public async Task<IActionResult> DeletarProduto(int id)
    {
        var result = _produtoService.DeletaProdutos(id, ObtemTokenJwt());
        if (result is null)
        {
            return View("Error");
        }
        return View(result);
    }
    [HttpPost]
    [HttpPost(), ActionName("DeletarProduto")]
    public async Task<IActionResult> DeletaProduto(int id)
    {
        var result = await _produtoService.DeletaProdutos(id,ObtemTokenJwt());
        if (result)
        {
            return RedirectToAction(nameof(Index));
        }
        return View("Error");
    }

    private string ObtemTokenJwt()
    {
        if (HttpContext.Request.Cookies.ContainsKey("X-Access-Token"))
            token = HttpContext.Request.Cookies["X-Access-Token"].ToString();
        return token;
    }
}
