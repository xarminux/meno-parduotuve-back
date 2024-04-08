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
			return Ok(_itemsService.GetPrekiuKrepseliai(id));
		}

		[HttpPost]
		public IActionResult PostKrepselis(Krepselis item)
		{
			item.Id = Guid.NewGuid();
			_itemsService.AddKrepselis(item);

			return Ok(item);
		}

		[HttpDelete("{id}")]
		public IActionResult DeleteKrepselis(Guid id)
		{
			_itemsService.DeleteKrepselis(id);
			return Ok();
		}
		[HttpDelete("deleteAll/{id}")]
		public IActionResult DeleteAllKrepselis(Guid id)
		{
			_itemsService.DeleteAllKrepselis(id);
			return Ok();
		}
		[HttpPost("insertmokejimas")]
		public IActionResult InsertMokejimas(List<PrekiuKrepselis> items)
		{
			var mokejimas = _itemsService.InsertMokejimas(items);

			if (mokejimas != null)
			{
				return Ok(mokejimas);
			}
			else
			{
				return BadRequest("Nepavyko sukurti mokejimo. Patikrinkite prekių ID arba kitus duomenis.");
			}
		}
		[HttpGet("mokejimas/{id}")]
		public IActionResult GetMokejimai(Guid id)
		{
			var mokejimai = _itemsService.GetMokejimai(id);

			if (mokejimai.Count == 0)
			{
				return NotFound();
			}

			return Ok(mokejimai);
		}


		[HttpPatch("{id}")]
		public IActionResult UpdateItem(Guid id, Krepselis item)
		{
			_itemsService.UpdateKrepselis(id, item);
			return Ok();
		}
	}
}
