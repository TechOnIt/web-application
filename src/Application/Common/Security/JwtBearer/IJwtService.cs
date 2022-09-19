﻿using iot.Application.Common.DTOs.Users.Authentication;
using System.Security.Claims;

namespace iot.Application.Common.Security.JwtBearer;

public interface IJwtService
{
    string GenerateTokenWithClaims(List<Claim> claims, DateTime? expireDateTime = null);
    Task<AccessToken> GenerateAccessToken(User user, CancellationToken stoppingToken = default);
}