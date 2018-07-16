using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ecommerce.Controllers;

namespace ecommerce.Models.Handlers
{
    public class FavAnimalHandler : AuthorizationHandler<FavAnimalRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, FavAnimalRequirement requirement)
        {
            if(context.User.HasClaim(c => c.Type == "FavAnimal"))
            {
               context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }

    }
}
