using System.Net;
using System.Numerics;
using System.Security.Cryptography;
using System.Text;
using server.Domains;

namespace server.Usecases
{
  public class SessionUsecases(ISessionRepository sessionRepository) : ISessionUsecases
  {
    private readonly ISessionRepository sessionRepository = sessionRepository;
    private readonly string SESSION_SECRET = "@Secure-Password@12345";
    private readonly TimeSpan SESSION_EXPIRES_TIME_SPAN = new TimeSpan(30, 0, 0, 0);

    public async Task<Session> GetSessionById(string id)
    {
      Session? session = await this.sessionRepository.GetSessionById(id);
      if (session.ExpiredAt < DateTime.Now)
      {
        throw new HttpRequestException("session was expired", null, HttpStatusCode.Unauthorized);
      }

      return session ?? throw new HttpRequestException("session was not found with this id", null, HttpStatusCode.NotFound);
    }

    public async Task<Session> ValidateSession(string id)
    {
      string ssid = this.UnSign(id);
      Session? session = await this.GetSessionById(ssid);

      return session ?? throw new HttpRequestException("session was not found with this ssid", null, HttpStatusCode.Unauthorized);
    }

    public async Task<string> CreateSession(SessionCreateInputs inputs)
    {
      string sessionId = Guid.NewGuid().ToString();

      await this.sessionRepository.CreateSession(
        new()
        {
          Id = sessionId,
          UserId = inputs.UserId,
          UserAgent = inputs.UserAgent,
          IpAddress = inputs.IpAddress,
          CreatedAt = DateTime.Now,
          ExpiredAt = DateTime.Now.Add(this.SESSION_EXPIRES_TIME_SPAN)
        });

      return this.Sign(sessionId);
    }

    public async Task DestroySessionById(string id)
    {
      await this.sessionRepository.DestroySessionById(id);
      return;
    }

    public string Sign(string input)
    {
      using (var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(this.SESSION_SECRET)))
      {
        var hashCode = hmac.ComputeHash(Encoding.UTF8.GetBytes(input));
        var digest = Convert.ToBase64String(hashCode);

        return $"{input}.{digest}";
      };
    }

    public string UnSign(string input)
    {
      var parts = input.Split('.');
      if (parts.Length != 2)
      {
        throw new HttpRequestException("signature does not match from system", null, HttpStatusCode.BadRequest);
      }

      string digest = this.Sign(parts[0]).Split('.')[1];
      if (!CryptographicOperations.FixedTimeEquals(
        Encoding.UTF8.GetBytes(parts[1]),
        Encoding.UTF8.GetBytes(digest)
      ))
      {
        throw new HttpRequestException("signature does not match from origin", null, HttpStatusCode.BadRequest);
      }

      return parts[0];
    }
  }
}
