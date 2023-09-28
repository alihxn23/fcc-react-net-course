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
        Task<List<ItemModel>> Get(int itemType);

        Task<ItemModel> GetObjectById(int objectId);
        Task<List<ItemModel>> AddItem(ItemModel item);
        Task<ItemModel> UpdateItem(ItemModel item);
        Task<List<ItemModel>> Delete(int itemType);
    }
}