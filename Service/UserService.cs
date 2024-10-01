using AutoMapper;
using Entities;
using Microsoft.AspNetCore.Http;
using Service.Dto;
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

    private static string HashPassword(string password)
    {
        return BCrypt.Net.BCrypt.HashPassword(password);
    }
}