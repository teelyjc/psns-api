namespace server.Domains
{
  public class Pet
  {
    public string Id { get; set; } = string.Empty;
    public string UserId { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public string Gene { get; set; } = string.Empty;
    public DateTime CreatedAt;
    public DateTime UpdatedAt;
  }

  public interface IPetRepository
  {
    Task<Pet> GetPetById(string id);
    Task<List<Pet>> GetPetsByUserId(string userId);
    Task CreatePetToUser(Pet pet);
  }
  public interface IPetUsecases
  {
    Task<Pet> GetPetById(string id);
    Task<List<Pet>> GetPetsByUserId(string userId);
    Task CreatePetToUser(string userId, PetCreateInputs inputs);
  }

  public class PetCreateInputs
  {
    public string Name { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public string Gene { get; set; } = string.Empty;
  }
}
