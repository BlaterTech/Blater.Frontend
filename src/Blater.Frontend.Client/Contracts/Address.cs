using System.ComponentModel.DataAnnotations;
using Blater.Frontend.Client.Contracts.Bases;

namespace Blater.Frontend.Client.Contracts;

public class Address : BaseFrontendModel
{
    public Guid OwnerId { get; set; }

    [Required]
    [MinLength(6)]
    [MaxLength(100)]
    public string Street { get; set; } = string.Empty;

    [Required]
    [MinLength(1)]
    [MaxLength(60)]
    public string Number { get; set; } = string.Empty;

    [Required]
    [MinLength(6)]
    [MaxLength(50)]
    public string District { get; set; } = string.Empty;

    [Required]
    [MinLength(6)]
    [MaxLength(50)]
    public string City { get; set; } = string.Empty;

    [Required]
    public string State { get; set; } = string.Empty;

    [Required]
    [MinLength(6)]
    [MaxLength(50)]
    public string Country { get; set; } = string.Empty;

    [MinLength(1)]
    [MaxLength(60)]
    public string Complement { get; set; } = string.Empty;

    [MinLength(1)]
    [MaxLength(60)]
    public string Province { get; set; } = string.Empty;

    [Required]
    [MaxLength(8)]
    public string PostalCode { get; set; } = string.Empty;

    [Required]
    [MinLength(6)]
    [MaxLength(50)]
    public string ExternalReference { get; set; } = string.Empty;
    
}