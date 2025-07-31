using CategoriasMvc.Models;

namespace CategoriasMvc.Services;

public interface IProdutoService
{
    Task<IEnumerable<ProdutoViewModel>> GetProdutos(string token);
    Task<ProdutoViewModel> GetProdutosPorId(int id, string token);
    Task<ProdutoViewModel> CriaProdutos(ProdutoViewModel ProdutosVM, string token);
    Task<bool> AtualizaProdutos(int id, ProdutoViewModel ProdutosVM, string token);
    Task<bool> DeletaProdutos(int id, string token);
}
