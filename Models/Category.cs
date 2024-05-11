namespace WebApiWeb.Models;
using System.ComponentModel.DataAnnotations;

public class Category
{
    //[DatabaseGenerated(DatabaseGeneratedOption.None)]
    //[Key]
    [Display(Name="ID")]
    public int Id { get; set; }
    [Display(Name="Название")]
    public string? Name { get; set; }
    [Display(Name="Посты")]
    public ICollection<Post> Posts { get; set; } = new List<Post>();
}