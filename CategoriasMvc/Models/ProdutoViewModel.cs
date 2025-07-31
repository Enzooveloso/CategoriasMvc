using System.ComponentModel.DataAnnotations;

namespace CategoriasMvc.Models;

public class ProdutoViewModel
{
    public int ProdutoId { get; set; }
    [Required(ErrorMessage = "O nome do produto é obrigatorio")]
    public string? Nome{ get; set; }
    [Required(ErrorMessage ="A descrição doo prooduto é obrigatoria")]
    public string? Descricao { get; set; }
    [Required(ErrorMessage ="Informe o precoo do produto")]
    public decimal Precoo { get; set; }
    [Required(ErrorMessage ="Informe o caminho da imagem do produto")]
    [Display(Name = "Caminho da imagem")]
    public string? ImagemUrl {  get; set; }
    [Display(Name = "Categoria")]
    public int CategoriaId {  get; set; }
}
