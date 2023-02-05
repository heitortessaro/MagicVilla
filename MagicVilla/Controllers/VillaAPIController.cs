using Microsoft.AspNetCore.Mvc;
using MagicVilla_VillaAPI.Models;
using MagicVilla_VillaAPI.Models.Dto;
using MagicVilla_VillaAPI.Data;
using Microsoft.AspNetCore.JsonPatch;

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

    [HttpDelete("{id:int}", Name = "DeleteVilla")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult DeleteVilla(int id)
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
      VillaStore.villaList.Remove(villa);
      return NoContent();
    }

    [HttpPut("{id:int}", Name = "UpdateVilla")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult UpdateVilla(int id, [FromBody] VillaDTO villaDto)
    {
      if (villaDto == null || id != villaDto.Id)
      {
        return BadRequest();
      }
      var villa = VillaStore.villaList.FirstOrDefault(u => u.Id == id);
      villa.Name = villaDto.Name;
      villa.Sqft = villaDto.Sqft;
      villa.Occupancy = villaDto.Occupancy;

      return NoContent();
    }

    [HttpPatch("{id:int}", Name = "UpdatePartialVilla")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult UpdatePartialVilla(int id, JsonPatchDocument<VillaDTO> patchDTO)
    // public IActionResult UpdatePartialVilla()
    {
      if (patchDTO == null || id == 0)
      {
        return BadRequest();
      }
      var villa = VillaStore.villaList.FirstOrDefault(u => u.Id == id);
      if (villa == null)
      {
        return BadRequest();
      }
      // It applies the content to villa and update the existing fields
      // if some problem rises, it is added to the ModelState
      // the request should follow the paterns defined here (https://jsonpatch.com/)
      patchDTO.ApplyTo(villa, ModelState);
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }
      return NoContent();
    }


  }
}
