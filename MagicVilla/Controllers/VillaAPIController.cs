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
    public IEnumerable<VillaDTO> GetVillas()
    {
      return VillaStore.villaList;
    }

    [HttpGet("{id:int}")]
    public VillaDTO GetVilla(int id)
    {
      return VillaStore.villaList.FirstOrDefault(u => u.Id == id);
    }
  }
}
