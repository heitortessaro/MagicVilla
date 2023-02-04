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

    [HttpGet("{id:int}", Name = "GetVilla")]
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

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public ActionResult<VillaDTO> CreateVilla([FromBody] VillaDTO villaDto)
    {
      // if(!ModelState.IsValid){
      //   return BadRequest(ModelState);
      // }
      if (VillaStore.villaList.FirstOrDefault(u => u.Name.ToLower() == villaDto.Name.ToLower()) != null)
      {
        ModelState.AddModelError("CustomError", "Villa already Exists!");
        return BadRequest(ModelState);
      }
      if (villaDto == null)
      {
        return BadRequest(villaDto);
      }
      if (villaDto.Id > 0)
      {
        return StatusCode(StatusCodes.Status500InternalServerError);
      }
      // get the last id and increment
      villaDto.Id = VillaStore.villaList.OrderByDescending(u => u.Id).FirstOrDefault().Id + 1;
      VillaStore.villaList.Add(villaDto);

      return CreatedAtRoute("GetVilla", new { id = villaDto.Id }, villaDto);
    }
  }
}
