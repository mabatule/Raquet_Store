using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using RaquetShop.Models.Security;
using System.IdentityModel.Tokens.Jwt;
using RaquetShop.Models;

namespace RaquetShop.Services.Security
{
    public class UserService:IUserService
    {
        private UserManager<IdentityUser> userManager;
        private RoleManager<IdentityRole> roleManager;
        private IConfiguration configuration;

        public UserService(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.configuration = configuration;
        }


        public async Task<UserManagerResponse> LoginUserAsync(LoginViewModel model)
        {
            var user = await userManager.FindByEmailAsync(model.Email);

            if (user == null)
            {
                return new UserManagerResponse
                {
                    Token = "No existe un usuario que tenga ese Email.",
                    IsSuccess = false,
                };
            }

            var result = await userManager.CheckPasswordAsync(user, model.Password);

            if (!result)
                return new UserManagerResponse
                {
                    Token = "La contraseña es invalida.",
                    IsSuccess = false,
                };

            var roles = await userManager.GetRolesAsync(user);

            var claims = new List<Claim>()
            {
                new Claim("Email", model.Email),
                new Claim(ClaimTypes.NameIdentifier, user.Id),
            };

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }


            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["AuthSettings:Key"]));

            var token = new JwtSecurityToken(
                issuer: configuration["AuthSettings:Issuer"],
                audience: configuration["AuthSettings:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(2),
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256));

            string tokenAsString = new JwtSecurityTokenHandler().WriteToken(token);

            return new UserManagerResponse
            {
                Token = tokenAsString,
                IsSuccess = true,
                ExpireDate = token.ValidTo
            };
        }

        public async Task<UserManagerResponse> RegisterUserAsync(RegisterViewModel model)
        {
            if (model == null)
            {
                throw new NullReferenceException("el registro esta vacio.");
            }

            if (model.Password != model.ConfirmPassword)
                return new UserManagerResponse
                {
                    Token = "Las contraseñas no coinciden.",
                    IsSuccess = false,
                };

            var identityUser = new IdentityUser
            {
                Email = model.Email,
                UserName = model.Email
            };

            var result = await userManager.CreateAsync(identityUser, model.Password);

            if (result.Succeeded)
            {
                return new UserManagerResponse
                {
                    Token = "El usuario fue creado exitosamente.",
                    IsSuccess = true,
                };
            }

            return new UserManagerResponse
            {
                Token = "Usuario no fue creado.",
                IsSuccess = false,
                Errors = result.Errors.Select(e => e.Description)
            };
        }

        public async Task<UserManagerResponse> CreateRoleAsync(CreateRoleViewModel model)
        {

            var identityRole = new IdentityRole()
            {
                Name = model.Name,
                NormalizedName = model.NormalizedName
            };

            var result = await roleManager.CreateAsync(identityRole);

            if (result.Succeeded)
            {
                return new UserManagerResponse
                {
                    Token = "Rol creado exitosamente.",
                    IsSuccess = true,
                };
            }

            return new UserManagerResponse
            {
                Token = "El rol no fue creado.",
                IsSuccess = false,
                Errors = result.Errors.Select(e => e.Description)
            };
        }


        public async Task<UserManagerResponse> CreateUserRoleAsync(CreateUserRoleViewModel model)
        {
            var role = await roleManager.FindByIdAsync(model.RoleId);
            if (role == null)
            {
                return new UserManagerResponse
                {
                    Token = "El rol no existe.",
                    IsSuccess = false
                };
            }

            var user = await userManager.FindByIdAsync(model.UserId);
            if (role == null)
            {
                return new UserManagerResponse
                {
                    Token = "El usuario no existe.",
                    IsSuccess = false
                };
            }

            if (await userManager.IsInRoleAsync(user, role.Name))
            {
                return new UserManagerResponse
                {
                    Token = "El usuario ya posee ese rol.",
                    IsSuccess = false
                };
            }

            var result = await userManager.AddToRoleAsync(user, role.Name);

            if (result.Succeeded)
            {
                return new UserManagerResponse
                {
                    Token = "El rol fue asignado.",
                    IsSuccess = true
                };
            }

            return new UserManagerResponse
            {
                Token = "Algo fallo.",
                IsSuccess = false
            };
        }

        public async Task<UserManagerResponse> CreateUserRolenSimpleAsync(User model)
        {
            var user = await userManager.FindByEmailAsync(model.Email);

            if (user == null)
            {
                return new UserManagerResponse
                {
                    Token = "No existe un usuario que tenga ese Email.",
                    IsSuccess = false,
                };
            }
            var normalizade = model.role.ToUpper();

            var identityRole = new IdentityRole()
            {
                Name = model.role,
                NormalizedName = normalizade
            };
            
            var role = await roleManager.FindByNameAsync(model.role);
            

            if (role == null)
            {
                return new UserManagerResponse
                {
                    Token = "El rol no existe.",
                    IsSuccess = false
                };
            }

            if (await userManager.IsInRoleAsync(user, role.Name))
            {
                return new UserManagerResponse
                {
                    Token = "El usuario ya posee ese rol.",
                    IsSuccess = false
                };
            }

            var result = await userManager.AddToRoleAsync(user, role.Name);

            if (result.Succeeded)
            {
                return new UserManagerResponse
                {
                    Token = "El rol fue asignado.",
                    IsSuccess = true
                };
            }


            return new UserManagerResponse
            {
                Token = "Algo fallo.",
                IsSuccess = false
            };
        }
    }
}
