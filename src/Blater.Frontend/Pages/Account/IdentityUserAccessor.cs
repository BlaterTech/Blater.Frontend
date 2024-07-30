using Blater.Interfaces;
using Blater.Models.User;
using Microsoft.AspNetCore.Http;

namespace Blater.Frontend.Pages.Account;

public sealed class IdentityUserAccessor(
    IBlaterDatabaseRepository<BlaterUser> repository,
    IdentityRedirectManager redirectManager)
{
    public async Task<BlaterUser> GetRequiredUserAsync(HttpContext context)
    {
        var claim = context.User.Claims.FirstOrDefault(x => x.Type == "UserId");
        if (claim == null)
        {
            redirectManager.RedirectToWithStatus("Account/InvalidUser", $"Error: Unable to load user.", context);
            return new BlaterUser();
        }

        var user = await repository.FindOne(x => x.Id == claim.Value);

        if (user is null)
        {
            redirectManager.RedirectToWithStatus("Account/InvalidUser", $"Error: Unable to load user.", context);
        }

        return user;
    }
}