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
        void AddItem(ItemModel item);
        void UpdateItem(ItemModel item);
        Task Delete(int itemType);
    }
}