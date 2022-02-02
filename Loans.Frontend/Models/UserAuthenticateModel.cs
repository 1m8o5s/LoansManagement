using System.ComponentModel.DataAnnotations;

namespace Loans.Frontend.Models;

public class UserAuthenticateModel
{
    [Required]
    public string Name { get; set; }
    [Required]
    [MinLength(8)]
    public string Password { get; set; }
}