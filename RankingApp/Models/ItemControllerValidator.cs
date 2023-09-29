using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using RankingApp.Models;

namespace RankingApp.Controllers
{
    public class ItemControllerValidator : AbstractValidator<ItemModel>
    {

        public ItemControllerValidator()
        {
            RuleFor(model => model.Ranking).LessThanOrEqualTo(16);
        }

    }
}