using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RankingApp.Models;

namespace RankingApp.Controllers
{
    public class ItemRepository : IItemRepository
    {
        private ItemContext _context;
        public ItemRepository(ItemContext context)
        {
            _context = context;
        }
        public Task<ActionResult<ItemModel[]>> AddItem(ItemModel item)
        {
            throw new NotImplementedException();
        }

        public async Task<List<ItemModel>> Delete(int itemType)
        {
            var itemsToUpdate = await _context.Items.Where(f => f.ItemType == itemType).ToListAsync();
            foreach (var item in itemsToUpdate)
            {
                item.Ranking = 0;
            }
            await _context.SaveChangesAsync();
            // return itemsToUpdate.ToList<ItemModel>();
            // return Task.FromResult(itemsToUpdate);
            return itemsToUpdate;
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public Task<IActionResult> Get(int itemType)
        {
            throw new NotImplementedException();
        }

        public Task<ActionResult<ItemModel>> UpdateItem(UpdateItemDto item)
        {
            throw new NotImplementedException();
        }
    }
}