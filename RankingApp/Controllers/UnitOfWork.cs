using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace RankingApp.Controllers
{
    public class UnitOfWork
    {
        private ItemContext _context;
        private ItemRepository _itemRepository;

        public UnitOfWork(DbContextOptions<ItemContext> options)
        {
            _context = new ItemContext(options);
        }

        public ItemRepository ItemRepository
        {
            get
            {
                if (_itemRepository == null)
                {
                    _itemRepository = new ItemRepository(_context);
                }
                return _itemRepository;
            }
        }

        // private bool disposed = false;
        // public void Dispose()
        // {
        //     // throw new NotImplementedException();
        // }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

    }
}