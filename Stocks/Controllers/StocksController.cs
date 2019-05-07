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
    public class StocksController : Controller
    {
        private IStocksService _stocksService;

        public StocksController(IStocksService usersService)
        {
            _stocksService = usersService;
        }

        [HttpPost("add")]
        public IActionResult AddStock(Stock stock)
        {
            var currentUserId = int.Parse(User.Identity.Name);
            var newStock = _stocksService.AddStock(stock, currentUserId);
            return Ok(newStock);
        }

        [HttpPost("remove/{stockId}")]
        public IActionResult RemoveStock(int stockId)
        {
            var currentUserId = int.Parse(User.Identity.Name);
            return Ok(_stocksService.RemoveStock(stockId, currentUserId));
        }

        [HttpGet]
        public IActionResult GetStocks()
        {
            var currentUserId = int.Parse(User.Identity.Name);
            return Ok(_stocksService.GetStocks(currentUserId));
        }
    }
}
