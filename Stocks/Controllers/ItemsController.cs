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

        [HttpGet("get/{itemId}")]
        public IActionResult GetItem(int itemId)
        {
            var currentUserId = int.Parse(User.Identity.Name);
            var item = _itemsService.GetItem(itemId, currentUserId);
            if (item == null)
                return NotFound();
            return Ok(item);
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
            var state = _itemsService.AddItemState(itemState, itemId, currentUserId, true);
            return Ok(state);
        }

        [HttpPost("writestate/{itemId}")]
        public IActionResult WriteItemState(ItemState itemState, int itemId)
        {
            var currentUserId = int.Parse(User.Identity.Name);
            var state = _itemsService.AddItemState(itemState, itemId, currentUserId, false);
            return Ok(state);
        }

        [AllowAnonymous]
        [HttpPost("writestatewithdevice")]
        public IActionResult WriteDeviceState(ItemState itemState)
        {
            var state = _itemsService.WriteItemStateWithDevice(itemState);
            return Ok(state);
        }

        [HttpGet("history/{itemId}")]
        public IActionResult GetItemHistory(int itemId)
        {
            var currentUserId = int.Parse(User.Identity.Name);
            var itemHistory = _itemsService.GetItemHistory(itemId, currentUserId);
            if (itemHistory == null)
                return BadRequest();
            return Ok(itemHistory);
        }

        [HttpGet("getstate/{stateId}")]
        public IActionResult GetItemState(int stateId)
        {
            var currentUserId = int.Parse(User.Identity.Name);
            var state = _itemsService.GetItemState(stateId, currentUserId);
            return Ok(state);
        }
    }
}
