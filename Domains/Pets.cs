namespace server.Domains
{
  public class Pet
  {
    public string Id { get; set; } = string.Empty;
    public string UserId { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public DateTime CreatedAt;
    public DateTime UpdatedAt;
  }

  public interface IPetRepository { }
  public interface IPetUsecases { }
}
