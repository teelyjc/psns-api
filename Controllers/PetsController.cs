using Microsoft.AspNetCore.Mvc;
using server.Usecases;

namespace server.Controllers {
  [ApiController]
  [Route("pets")]
  public class PetsController {
    private readonly PetsUsecases petUsecases;
    public PetsController(PetsUsecases petUsecases) {
      this.petUsecases = petUsecases;
    }
  }
}
