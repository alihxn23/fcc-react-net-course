using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RankingApp.Models;

namespace RankingApp.Controllers
{
    public interface IItemRepository : IDisposable
    {
        Task<IActionResult> Get(int itemType);
        Task<ActionResult<ItemModel[]>> AddItem(ItemModel item);
        Task<ActionResult<ItemModel>> UpdateItem(UpdateItemDto item);
        Task<List<ItemModel>> Delete(int itemType);
    }
}