using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ecommerce.Models.Handlers
{
    public class FavAnimalRequirement : IAuthorizationRequirement
    {
        public string FavAnimal { get; set; }

        public FavAnimalRequirement(string animal)
        {
            FavAnimal = animal;
        }
    }
}
