using System;
using Microsoft.EntityFrameworkCore;
using RankingApp.Models;

namespace RankingApp
{
	public class ItemContext : DbContext
	{
		public ItemContext(DbContextOptions<ItemContext> options) : base(options)
		{

		}

		public DbSet<ItemModel> Items { get; set; }
	}
}

