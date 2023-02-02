using Microsoft.AspNetCore.Mvc;
using MagicVilla_VillaAPI.Models;
using MagicVilla_VillaAPI.Models.Dto;

namespace MagicVilla_VillaAPI.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class VillaAPICOntroller : ControllerBase
  {
    [HttpGet]
    public IEnumerable<VillaDTO> GetVillas()
    {
      return new List<VillaDTO> {
        new VillaDTO{Id=1,Name="Pool View"},
        new VillaDTO{Id=2,Name="Beach View"},
      };
    }
  }
}
