using Back.Application.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Back.Application.Interfaces
{
    public interface IJwtTokenService
    {
        Task<JwtAuthenticationResult> GenerateTokensAsync(string userId);
        Task<JwtAuthenticationResult> RefreshTokensAsync(string refreshToken);
    }
}
