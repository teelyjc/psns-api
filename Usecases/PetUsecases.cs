using server.Domains;

namespace server.Usecases
{
  public class PetUsecases(IPetRepository petRepository) : IPetUsecases
  {
    private readonly IPetRepository petRepository = petRepository;
  }
}
