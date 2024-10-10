using Microsoft.AspNetCore.Mvc;

using server.Domains;

namespace server.Controllers
{
  [ApiController]
  public class PetController(IPetUsecases petUsecases) : Controller
  {
    private readonly IPetUsecases petUsecases = petUsecases;
  }
}
