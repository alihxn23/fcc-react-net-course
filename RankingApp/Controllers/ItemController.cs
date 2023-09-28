using AutoMapper;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RankingApp.Models;

namespace RankingApp.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class ItemController : ControllerBase
    {
        private readonly ItemContext _context;
        private readonly IMapper _mapper;
        private IItemRepository _itemRepository;

        public ItemController(ItemContext context, IMapper mapper, IItemRepository itemRepository)
        {
            _context = context;
            _mapper = mapper;
            _itemRepository = itemRepository;
        }

        // public ItemController(IMapper mapper)
        // {
        //     _mapper = mapper;
        // }

        // private static readonly IEnumerable<ItemModel> Items = new[]
        // {
        //     new ItemModel{Id =1, Title = "The Godfather", ImageId=1, Ranking=0,ItemType=1 },
        //     new ItemModel{Id =2, Title = "Highlander", ImageId=2, Ranking=0,ItemType=1 },
        //     new ItemModel{Id =3, Title = "Highlander II", ImageId=3, Ranking=0,ItemType=1 },
        //     new ItemModel{Id =4, Title = "The Last of the Mohicans", ImageId=4, Ranking=0,ItemType=1 },
        //     new ItemModel{Id =5, Title = "Police Academy 6", ImageId=5, Ranking=0,ItemType=1 },
        //     new ItemModel{Id =6, Title = "Rear Window", ImageId=6, Ranking=0,ItemType=1 },
        //     new ItemModel{Id =7, Title = "Road House", ImageId=7, Ranking=0,ItemType=1 },
        //     new ItemModel{Id =8, Title = "The Shawshank Redemption", ImageId=8, Ranking=0,ItemType=1 },
        //     new ItemModel{Id =9, Title = "Star Treck IV", ImageId=9, Ranking=0,ItemType=1 },
        //     new ItemModel{Id =10, Title = "Superman 4", ImageId=10, Ranking=0,ItemType=1 },
        //     new ItemModel{Id = 11, Title = "Abbey Road", ImageId=11, Ranking=0,ItemType=2 },
        //     new ItemModel{Id = 12, Title = "Adrenalize", ImageId=12, Ranking=0,ItemType=2 },
        //     new ItemModel{Id = 13, Title = "Back in Black", ImageId=13, Ranking=0,ItemType=2 },
        //     new ItemModel{Id = 14, Title = "Enjoy the Silence", ImageId=14, Ranking=0,ItemType=2 },
        //     new ItemModel{Id = 15, Title = "Parachutes", ImageId=15, Ranking=0,ItemType=2 },
        //     new ItemModel{Id = 16, Title = "Ride the Lightning", ImageId=16, Ranking=0,ItemType=2 },
        //     new ItemModel{Id = 17, Title = "Rock or Bust", ImageId=17, Ranking=0,ItemType=2 },
        //     new ItemModel{Id = 18, Title = "Rust in Peace", ImageId=18, Ranking=0,ItemType=2 },
        //     new ItemModel{Id = 19, Title = "St. Anger", ImageId=19, Ranking=0,ItemType=2 },
        //     new ItemModel{Id = 20, Title = "The Final Countdown", ImageId=20, Ranking=0,ItemType=2 }

        // };

        [HttpGet("{itemType:int}")]
        public async Task<IActionResult> Get(int itemType)
        {
            //ItemModel[] items = Items.Where(i => i.ItemType == itemType).ToArray();
            //Thread.Sleep(2000);
            var items = await _context.Items.Where(x => x.ItemType == itemType).ToListAsync();
            if (items.Count == 0)
            {
                return BadRequest("no data found");
            }
            // return Ok(_mapper.Map<UpdateItemDto[]>(items));
            return Ok(items);
        }

        [HttpPost]
        public async Task<ActionResult<ItemModel[]>> AddItem(ItemModel item)
        {
            _context.Items.Add(item);
            await _context.SaveChangesAsync();

            return Ok(await _context.Items.ToListAsync());
        }

        [HttpPut]
        public async Task<ActionResult<ItemModel>> UpdateItem(UpdateItemDto item)
        {
            var i = await _context.Items.FirstOrDefaultAsync(c => c.Id == item.Id);
            if (i == null)
            {
                return BadRequest("item not found");
            }
            // i.Ranking = item.Ranking;
            _mapper.Map<UpdateItemDto, ItemModel>(item, i);
            var validator = new ItemControllerValidator();
            ValidationResult result = validator.Validate(i);
            Console.WriteLine(i.Ranking);
            // return Ok(result);
            if (!result.IsValid)
            {
                return BadRequest(result.Errors);
            }
            _context.Entry(i).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return Ok(i);
            // var a = _mapper.Map<ItemModel>(item);
            // return Ok(a);
            // return _mapper.Map<ItemModel>(item);

            //if(i == null)
            //{
            //    return BadRequest("data not found");
            //}

            //i.ImageId = item.ImageId;
            //i.Ranking = item.Ranking;
            //i.ItemType = item.ItemType;

            //_context.Items.Update(i);
            //await _context.SaveChangesAsync();

            //var changedItem = await _context.Items.FindAsync(i.Id);
            //return Ok(changedItem);


            // if (id != item.Id)
            // {
            //     return BadRequest();
            // }

            // _context.Entry(item).State = EntityState.Modified;

            // try
            // {
            //     await _context.SaveChangesAsync();
            // }
            // catch (DbUpdateConcurrencyException)
            // {
            //     if (!(_context.Items?.Any(e => e.Id == id)).GetValueOrDefault())
            //     {
            //         return NotFound();
            //     }
            //     else
            //     {
            //         throw;
            //     }
            // }

            // return item;
        }

        [HttpDelete("{itemType:int}")]
        public async Task<ActionResult<ItemModel[]>> Delete(int itemType)
        {
            // var itemsToUpdate = await _context.Items.Where(f => f.ItemType == itemType).ToListAsync();
            // foreach (var item in itemsToUpdate)
            // {
            //     item.Ranking = 0;
            // }
            // await _context.SaveChangesAsync();
            // return Ok(itemsToUpdate);
            var result = await _itemRepository.Delete(itemType);
            return Ok(result);
            // return Ok(_itemRepository.Delete(itemType));
            // return Ok(_itemRepository.Delete(itemType));

            // var items = await _context.Items.Where(x => x.ItemType == itemType).ToListAsync();
            // if (items.Count == 0)
            // {
            //     return BadRequest("no data found");
            // }
            // return Ok(items);
        }

    }
}

