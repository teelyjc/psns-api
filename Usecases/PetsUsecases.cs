using server.Repositories;

namespace server.Usecases
{
  public class PetsUsecases
  {
    private readonly PetsRepository petsRepository;
    public PetsUsecases(PetsRepository petsRepository)
    {
      this.petsRepository = petsRepository;
    }
  }
}
