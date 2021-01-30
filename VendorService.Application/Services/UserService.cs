using AutoMapper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VendorService.Application.Mappers;
using VendorService.Application.Models;
using VendorService.Application.Validators;
using VendorService.Domain.Entities;
using VendorService.Domain.Enums;
using VendorService.Domain.Extensions;
using VendorService.Domain.Repositories;

namespace VendorService.Application.Services.Interfaces
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly ITokenService _tokenService;
        private readonly IUserRepository _userRepository;
        private readonly LoginModelValidator _loginModelValidator;
        private readonly UserRegisterModelValidator _userRegisterModelValidator;

        public UserService(IMapper mapper, ITokenService tokenService, IUserRepository userRepository, 
            LoginModelValidator loginModelValidator,
            UserRegisterModelValidator userRegisterModelValidator)
        {
            _mapper = mapper;
            _tokenService = tokenService;
            _userRepository = userRepository;
            _loginModelValidator = loginModelValidator;
            _userRegisterModelValidator = userRegisterModelValidator;
        }

       
        public async Task<BaseModel<UserModel>> Authenticate(LoginModel loginModel)
        {

            var validationResult = _loginModelValidator.Validate(loginModel);
            if (!validationResult.IsValid)
            {
                return new BaseModel<UserModel>(false, validationResult.Errors);
            }

            var result = await _userRepository.Authenticate(loginModel.Email, loginModel.Password);

            if (result == default)
            {
                return new BaseModel<UserModel>(false, EMessages.Success);
            }

            var map = _mapper.Map<UserModel>(result);
            map.Token = _tokenService.GenerateToken(map);

            var model = new BaseModel<UserModel>(true, EMessages.Success, map);
            return model;
        }

        public async Task<BaseModel<UserRegisterModel>> Create(UserRegisterModel userRegisterModel)
        {
            var validationResult = _userRegisterModelValidator.Validate(userRegisterModel);
            if (!validationResult.IsValid)
            {
                return new BaseModel<UserRegisterModel>(false, validationResult.Errors);
            }

            var user = _mapper.Map<User>(userRegisterModel);
            var result = _mapper.Map<UserRegisterModel>(await _userRepository.Create(user));
            return new BaseModel<UserRegisterModel>(true, EMessages.Success, result);
        }


        public async Task<BaseModel<UserRegisterModel>> Update(UserRegisterModel userRegisterModel)
        {
            var validationResult = _userRegisterModelValidator.Validate(userRegisterModel);
            if (!validationResult.IsValid)
            {
                return new BaseModel<UserRegisterModel>(false, validationResult.Errors);
            }

            var user = _mapper.Map<User>(userRegisterModel);
            var result = _mapper.Map<UserRegisterModel>(await _userRepository.Update(user));

            return new BaseModel<UserRegisterModel>(true, EMessages.Success, result);
        }

       
        public async Task<BaseModel<List<UserModel>>> List()
        {
            var result = _mapper.Map<List<UserModel>>(await _userRepository.List());
            return new BaseModel<List<UserModel>>(true, EMessages.Success, result);
        }

        public async Task<BaseModel<UserModel>> GetById(Guid id)
        {
            var result = _mapper.Map<UserModel>(await _userRepository.GetById(id));
            return new BaseModel<UserModel>(true, EMessages.Success, result);
        }

        public async Task<BaseModel<UserModel>> Delete(Guid id)
        {
            await _userRepository.Delete(id);
            return new BaseModel<UserModel>(true, EMessages.Success);
        }

        public BaseModel<List<EnumModel>> ListProfiles()
        {
            var profiles = Enum.GetValues(typeof(EProfiles));
            var result = new List<EnumModel>();
            foreach (var profile in profiles)
            {
                result.Add(new EnumModel()
                {
                    Id = ((EProfiles)profile).GetEnumValue(),
                    Name = ((EProfiles)profile).GetEnumName(),
                    Description = ((EProfiles)profile).GetEnumDescription()
                });
            }
            return new BaseModel<List<EnumModel>>(true, EMessages.Success, result);
        }
    }
}

