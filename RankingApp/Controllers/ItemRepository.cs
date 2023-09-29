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
        public void AddItem(ItemModel item)
        {
            _context.Items.Add(item);
            // await _context.SaveChangesAsync();
            // return await _context.Items.ToListAsync();
        }

        public async Task Delete(int itemType)
        {
            var itemsToUpdate = await _context.Items.Where(f => f.ItemType == itemType).ToListAsync();
            foreach (var item in itemsToUpdate)
            {
                item.Ranking = 0;
            }
            // await _context.SaveChangesAsync();
            // return itemsToUpdate;
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public async Task<List<ItemModel>> Get(int itemType)
        {
            var items = await _context.Items.Where(x => x.ItemType == itemType).ToListAsync();
            return items;
        }

        public async Task<ItemModel> GetObjectById(int objectId)
        {
            var i = await _context.Items.FirstOrDefaultAsync(c => c.Id == objectId);
            return i;
        }

        public void UpdateItem(ItemModel item)
        {
            // throw new NotImplementedException();
            // var i = await _context.Items.FirstOrDefaultAsync(c => c.Id == item.Id);
            // if(i == null){
            //     return null;
            // }
            // // i.Ranking = item.Ranking;
            // _mapper.Map<UpdateItemDto, ItemModel>(item, i);
            // var validator = new ItemControllerValidator();
            // ValidationResult result = validator.Validate(i);
            // Console.WriteLine(i.Ranking);
            // // return Ok(result);
            // if (!result.IsValid)
            // {
            //     return BadRequest(result.Errors);
            // }
            _context.Entry(item).State = EntityState.Modified;
            // await _context.SaveChangesAsync();
            // return item;
            // return Ok(i);
        }
    }
}