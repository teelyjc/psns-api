using Microsoft.AspNetCore.Mvc;

using server.Domains;

namespace server.Controllers
{
  [ApiController]
  public class PetsController(IPetUsecases petUsecases) : Controller
  {
    private readonly IPetUsecases petUsecases = petUsecases;
  }
}
