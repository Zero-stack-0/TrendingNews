using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using Service.Dto;
using Service.Dto.Request;
using Service.Dto.Response;
using Service.Helper;

namespace Service;
public class UserService
{
    private readonly UserRepository userRepository;
    private readonly IMapper mapper;
    public UserService(UserRepository userRepository, IMapper mapper)
    {
        this.userRepository = userRepository;
        this.mapper = mapper;
    }

    public async Task<ApiResponse> Create(CreateUserRequest dto)
    {
        var userByEmailId = await userRepository.GetByEmailId(dto.EmailId);
        if (userByEmailId is not null)
        {
            return new ApiResponse(null, Constants.User.USER_WITH_SAME_EMAIL_ALREADY_EXISTS, StatusCodes.Status400BadRequest);
        }

        var userByUserName = await userRepository.GetByUserName(dto.UserName);
        if (userByUserName is not null)
        {
            return new ApiResponse(null, Constants.User.USER_WITH_SAME_USER_NAME_ALREADY_EXISTS, StatusCodes.Status400BadRequest);
        }

        var user = new Users(dto.UserName, dto.EmailId, HashPassword(dto.PassWord));

        userRepository.Add(user);

        await userRepository.SaveAsync();

        return new ApiResponse(mapper.Map<UserResponse>(user), Constants.User.USER_CREATED_SUCESSFULLY, StatusCodes.Status200OK);
    }

    public async Task<ApiResponse> Login(LoginRequest dto)
    {
        var user = await userRepository.GetByEmailOrUserName(dto.EmailOrUserName);
        if (user is null)
        {
            return new ApiResponse(null, Constants.User.INVALID_LOGIN_CREDENTIAL, StatusCodes.Status400BadRequest);
        }

        if (!BCrypt.Net.BCrypt.Verify(dto.Password, user.PassWord))
        {
            return new ApiResponse(null, Constants.User.INVALID_LOGIN_CREDENTIAL, StatusCodes.Status400BadRequest);
        }

        return new ApiResponse(GenerateJwtToken(user.UserName), Constants.User.LOGGED_IN_SUCESSFULLY, StatusCodes.Status200OK);
    }

    private static string HashPassword(string password)
    {
        return BCrypt.Net.BCrypt.HashPassword(password);
    }

    private string GenerateJwtToken(string username)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("b2tpMjN4dXltcmZpNHRnbXNtZ2xpdXU="));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, username),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.NameId, 9090.ToString())
        };

        var token = new JwtSecurityToken(
            issuer: "http://localhost:5257/api/home/login",
            audience: "http://localhost:5257",
            claims: claims,
            expires: DateTime.Now.AddMinutes(30),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}