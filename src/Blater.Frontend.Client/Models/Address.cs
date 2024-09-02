using System.ComponentModel.DataAnnotations;
using Blater.Frontend.Client.Auto.AutoBuilders;
using Blater.Frontend.Client.Models.Bases;
using MudBlazor;

namespace Blater.Frontend.Client.Models;

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


    public override void Configure(AutoModelConfigurationBuilder builder)
    {
        builder.Table("TableName", configurationBuilder => { configurationBuilder.AddMember(() => ExternalReference); });

        builder.Form("FormName", configurationBuilder =>
        {
            configurationBuilder.ConfigureActions(actionConfigurationBuilder => { actionConfigurationBuilder.TypeCreateEditButton(ButtonType.Submit); });

            configurationBuilder.AddGroup(groupConfigurationBuilder =>
            {
                groupConfigurationBuilder
                   .AddMember(() => ExternalReference)
                   .IsReadOnly(true);
            });
        });

        builder.Details("DetailsName", configurationBuilder =>
        {
            configurationBuilder.AddGroup("GroupName", false, groupConfigurationBuilder =>
            {
                groupConfigurationBuilder
                   .AddMember(() => ExternalReference);
            });
        });
    }
}