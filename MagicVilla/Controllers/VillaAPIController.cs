using Microsoft.AspNetCore.Mvc;
using MagicVilla_VillaAPI.Models;
using MagicVilla_VillaAPI.Models.Dto;
using MagicVilla_VillaAPI.Data;

namespace MagicVilla_VillaAPI.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class VillaAPICOntroller : ControllerBase
  {
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public ActionResult<IEnumerable<VillaDTO>> GetVillas()
    {
      return Ok(VillaStore.villaList);
    }

    [HttpGet("{id:int}")]
    // [ProducesResponseType(200, Type = typeof(VillaDTO))]
    // [ProducesResponseType(404)]
    // [ProducesResponseType(400)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<VillaDTO> GetVilla(int id)
    {
      if (id == 0)
      {
        return BadRequest();
      }
      var villa = VillaStore.villaList.FirstOrDefault(u => u.Id == id);
      if (villa == null)
      {
        return NotFound();
      }
      return Ok(villa);
    }
  }
}
