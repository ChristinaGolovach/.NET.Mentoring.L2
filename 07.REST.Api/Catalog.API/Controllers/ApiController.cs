using Catalog.API.Constants;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.API.Controllers
{
	[Route("api/[controller]")]
	[Produces(ContentTypeName.ApplicationJson)]
	[Consumes(ContentTypeName.ApplicationJson)]
	[ApiController]
	public class ApiController : ControllerBase
	{
		[ApiExplorerSettings(IgnoreApi = true)]
		public IActionResult ReturnResultOrNotFound(object data)
		{
			if (data != null)
			{
				return Ok(data);
			}

			return NotFound();
		}
	}
}
