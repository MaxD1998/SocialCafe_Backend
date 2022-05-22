using System.Net;

namespace ApplicationCore.Dtos.RefreshToken
{
    public class RefreshTokenInputDto
    {
        public DateTime CreationDate { get; set; }

        public DateTime ExpireDate { get; set; }

        public IPAddress RemoteAddress { get; set; }

        public Guid Token { get; set; }
    }
}