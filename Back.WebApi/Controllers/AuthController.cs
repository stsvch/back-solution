﻿using Back.Application.Interfaces;
using Back.WebApi.DTOs.Requests;
using Back.WebApi.DTOs.Responses;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace Back.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _auth;
        private readonly IMediator _mediator;
        public AuthController(IAuthService auth, IMediator mediator)
        {
            _auth = auth;
            _mediator = mediator;
        }

        [HttpPost("register"), AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] RegisterUserRequest req)
        {
            var authRes = await _auth.RegisterAsync(req.Username, req.Email, req.Password);
            if (!authRes.Succeeded)
                return BadRequest(new AuthResponseDto
                {
                    Succeeded = false,
                    Errors = authRes.Errors
                });

            return Ok(new AuthResponseDto
            {
                Succeeded = true,
                AccessToken = authRes.AccessToken!,
                RefreshToken = authRes.RefreshToken!,
                Errors = Array.Empty<string>()
            });
        }

        [HttpPost("login"), AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] DTOs.Requests.LoginRequest req)
        {
            var res = await _auth.LoginAsync(req.Username, req.Password);

            if (!res.Succeeded)
            {
                return Unauthorized(new AuthResponseDto
                {
                    Succeeded = false,
                    Errors = res.Errors
                });
            }

            return Ok(new AuthResponseDto
            {
                Succeeded = true,
                AccessToken = res.AccessToken,
                RefreshToken = res.RefreshToken,
                Errors = Array.Empty<string>()
            });
        }

        [HttpPost("logout"), Authorize]
        public async Task<IActionResult> Logout([FromBody] DTOs.Requests.LogoutRequest req)
        {
            var result = await _auth.LogoutAsync(req.RefreshToken);
            if (!result.Succeeded)
                return BadRequest(new { result.Errors });

            return NoContent();
        }

        [HttpPost("refresh"), AllowAnonymous]
        public async Task<IActionResult> Refresh([FromBody] DTOs.Requests.RefreshRequest req)
        {
            var res = await _auth.RefreshAsync(req.RefreshToken);

            if (!res.Succeeded)
            {
                return Unauthorized(new AuthResponseDto
                {
                    Succeeded = false,
                    Errors = res.Errors
                });
            }

            return Ok(new AuthResponseDto
            {
                Succeeded = true,
                AccessToken = res.AccessToken,
                RefreshToken = res.RefreshToken,
                Errors = Array.Empty<string>()
            });
        }
    }
}
