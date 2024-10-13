using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using server.Domains;
using server.Middlewares;

namespace server.Controllers
{
  [ApiController]
  [Route("pets")]
  public class PetController(IPetUsecases petUsecases) : Controller
  {
    private readonly IPetUsecases petUsecases = petUsecases;

    [HttpGet]
    [ServiceFilter(typeof(AuthMiddleware))]
    public async Task<ActionResult<Response<List<Pet>>>> GetUserPets()
    {
      User user = Domains.User.GetUserFromContext(this.HttpContext);
      List<Pet> pets = await this.petUsecases.GetPetsByUserId(user.Id);

      return new Response<List<Pet>>()
      {
        Success = true,
        Data = pets
      };
    }

    [HttpPost]
    [ServiceFilter(typeof(AuthMiddleware))]
    public async Task<ActionResult<Response>> CreatePetToUser(PetCreateInputs inputs)
    {
      User user = Domains.User.GetUserFromContext(this.HttpContext);
      await this.petUsecases.CreatePetToUser(user.Id, inputs);

      return new Response()
      {
        Success = true,
        Data = new Success() { Message = "pet was added to user successfully" }
      };
    }
  }
}
