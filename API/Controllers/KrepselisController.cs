using API.Models;
using API.Services;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;

namespace API.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class KrepselisController : ControllerBase
	{
		private KrepselisService _itemsService;
		private readonly IMapper _mapper;

		public KrepselisController(KrepselisService itemsService, IMapper mapper)
		{
			_itemsService = itemsService;
			_mapper = mapper;
		}

		[HttpGet("{id}")]
		public IActionResult GetPrekiuKrepseliai(Guid id)
		{
			return Ok(_itemsService.GetKopijosByVartotojoId(id));
		}

		[HttpPost]
		public IActionResult PostKrepselis(Guid preke_id, Guid vartotojo_id)
		{
			Kopija item = new Kopija();
			item.Id = Guid.NewGuid();
            Console.WriteLine(item.Id);
            Console.WriteLine(preke_id);
            Console.WriteLine(vartotojo_id);
            item.Preke_Id = preke_id;
			_itemsService.AddKrepselis(item, vartotojo_id);

			return Ok(item);
		}

		[HttpDelete("{id}")]
		public IActionResult DeleteKrepselis(Guid id)
		{
			_itemsService.RemovePrekeFromKrepselis(id);
			return Ok();
		}
	}
}
