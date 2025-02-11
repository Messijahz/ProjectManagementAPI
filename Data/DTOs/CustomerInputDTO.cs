using System.ComponentModel.DataAnnotations;

namespace Data.DTOs;

public class CustomerInputDTO
{
    [Required(ErrorMessage = "Customer name is required.")]
    [MaxLength(100, ErrorMessage = "Customer name cannot exceed 100 characters.")]
    public string CustomerName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Contact person is required.")]
    [MaxLength(100, ErrorMessage = "Contact person cannot exceed 100 characters.")]
    public string ContactPerson { get; set; } = string.Empty;
}
