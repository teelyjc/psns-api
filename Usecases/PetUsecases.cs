using System.Net;
using Microsoft.AspNetCore.Mvc;
using server.Domains;

namespace server.Usecases
{
  public class PetUsecases(IPetRepository petRepository) : IPetUsecases
  {
    private readonly IPetRepository petRepository = petRepository;

    public async Task<Pet> GetPetById(string id)
    {
      Pet pet = await this.petRepository.GetPetById(id)
        ?? throw new HttpRequestException("pet was not found with this id", null, HttpStatusCode.NotFound);

      return pet;
    }

    public async Task<List<Pet>> GetPetsByUserId(string userId)
    {
      List<Pet> pets = await this.petRepository.GetPetsByUserId(userId);
      return pets;
    }

    public async Task CreatePetToUser(string userId, PetCreateInputs inputs)
    {
      var timestamp = DateTime.Now;

      await this.petRepository.CreatePetToUser(new()
      {
        Id = Guid.NewGuid().ToString(),
        UserId = userId,
        Name = inputs.Name,
        Type = inputs.Type,
        Gene = inputs.Gene,
        CreatedAt = timestamp,
        UpdatedAt = timestamp
      });
    }
  }
}
