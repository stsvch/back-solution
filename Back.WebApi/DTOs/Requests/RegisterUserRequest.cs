﻿namespace Back.WebApi.DTOs.Requests
{
    public class RegisterUserRequest
    {
        public string Username { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public DateTimeOffset DateOfBirth { get; set; }
    }
}
