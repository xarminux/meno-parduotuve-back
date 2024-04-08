using API.Models;
using API.Services;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ItemsController : ControllerBase
    {
        private ItemsService _itemsService;
        private FileService _fileService;
        private readonly IMapper _mapper;

        public ItemsController(ItemsService itemsService, FileService fileService, IMapper mapper)
        {
            _itemsService = itemsService;
            _fileService = fileService;
            _mapper = mapper;
        }

        [HttpGet("all")]
        public IActionResult GetAllItems()
        {
            return Ok(_itemsService.GetItems());
        }

        [HttpGet("{id}")]
        public IActionResult GetItem(Guid id)
        {
            return Ok(_itemsService.GetItem(id));
        }

        [HttpPost]
        public IActionResult PostItem(Preke item)
        {
            item.Id = Guid.NewGuid();
            _itemsService.AddItem(item);

            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteItem(Guid id)
        {
            var item = _itemsService.GetItem(id);
            _itemsService.DeleteItem(id);
            return Ok();
        }

        [HttpPatch("{id}")]
        public IActionResult UpdateItem(Guid id, Preke item) 
        {
            _itemsService.UpdateItem(id, item);
            return Ok();
        }
    }
}
