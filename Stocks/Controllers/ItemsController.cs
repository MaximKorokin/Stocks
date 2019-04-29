using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Stocks.Models;
using Stocks.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stocks.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ItemsController : Controller
    {
        private IItemsService _itemsService;

        public ItemsController(IItemsService itemsService)
        {
            _itemsService = itemsService;
        }

        [HttpPost("add/{stockId}")]
        public IActionResult AddItem(Item item, int stockId)
        {
            var currentUserId = int.Parse(User.Identity.Name);
            var newItem = _itemsService.AddItem(item, stockId, currentUserId);
            return Ok(newItem);
        }

        [HttpGet("{stockId}")]
        public IActionResult GetStockItems(int stockId)
        {
            var currentUserId = int.Parse(User.Identity.Name);
            var items = _itemsService.GetStockItems(stockId, currentUserId);
            return Ok(items);
        }

        [HttpPost("remove/{itemId}")]
        public IActionResult RemoveItem(int itemId)
        {
            var currentUserId = int.Parse(User.Identity.Name);
            var result = _itemsService.RemoveItem(itemId, currentUserId);
            return Ok(result);
        }

        [HttpPost("move/{itemId}/{stockId}")]
        public IActionResult MoveItem(int itemId, int stockId)
        {
            var currentUserId = int.Parse(User.Identity.Name);
            var result = _itemsService.MoveItem(itemId, stockId, currentUserId);
            return Ok(result);
        }

        [HttpPost("addstate/{itemId}")]
        public IActionResult AddItemState(ItemState itemState, int itemId)
        {
            var currentUserId = int.Parse(User.Identity.Name);
            var state = _itemsService.AddItemState(itemState, itemId, currentUserId);
            return Ok(state);
        }
    }
}
