using chat.Context;
using chat.Models;
using chat.SignalR.Chat;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.Json;

namespace chat.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : Controller
    {
        public readonly ChatDbContext _chatDbContext;
        //private readonly UserManager<User> _userManager;
        //private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;

        private readonly IHubContext<ChatHub, IChatHub> _chatHubContext;

        //public UsersController(ChatDbContext chatDbContext, UserManager<User> userManager, IConfiguration configuration, RoleManager<IdentityRole> roleManager)
        public UsersController(ChatDbContext chatDbContext, IConfiguration configuration, IHubContext<ChatHub, IChatHub> chatHubContext)
        {
            this._chatDbContext = chatDbContext;
            //this._userManager = userManager;
            // this._roleManager = roleManager;
            this._configuration = configuration;
            this._chatHubContext = chatHubContext;
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<User>), StatusCodes.Status200OK)]
        public async Task<IEnumerable<User>> index()
        {
            // return  await _chatDbContext.Users.Select(p => new { p.Id }).ToListAsync();
            return await _chatDbContext.Users.ToListAsync();
        }


        [HttpPost("login")]
        [ProducesResponseType(typeof(User), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> login ([FromBody] Login login ) 
        {
            
            User user = await _chatDbContext.Users.SingleOrDefaultAsync(c => c.UserEmail == login.email);
            //User user = await _userManager.FindByEmailAsync(email);
            /*2 user for tests
            var user_ = new User();
            user_.UserCnxStatus = false ;
            user_.UserLastName = "UserLastName";
            user_.UserFirstName = "UserFirstName";
            user_.UserAdress = "22 rue chiii";
            user_.UserEmail = "UserLastName@gmail.com";
            user_.UserPassword = BCrypt.Net.BCrypt.HashPassword("P@sswor123");
            user_.UserPhone = "555";
            user_.UserImage = "/user/user/i.png";
            user_.UserBirthDate = new DateTime();
            await _chatDbContext.Users.AddAsync(user_);
            await _chatDbContext.SaveChangesAsync();

            var user_2 = new User();
            user_2.UserCnxStatus = false ;
            user_2.UserLastName = "UserLastName 2";
            user_2.UserFirstName = "UserFirstName 2";
            user_2.UserAdress = "22 rue chiii";
            user_2.UserEmail = "UserLastName2@gmail.com";
            user_2.UserPassword = BCrypt.Net.BCrypt.HashPassword("P@sswor123");
            user_2.UserPhone = "5556655";
            user_2.UserImage = "/user/user/i2.png";
            user_2.UserBirthDate = new DateTime();
            await _chatDbContext.Users.AddAsync(user_2);
            await _chatDbContext.SaveChangesAsync();
            */

            if (user == null) return NotFound();

            else{
                //bool isValidUser = await _userManager.CheckPasswordAsync(user, password);
                bool isValidUser = BCrypt.Net.BCrypt.Verify(login.password, user.UserPassword);
                
                if (!isValidUser) return NotFound();

                else{
                        var tokenHandler = new JwtSecurityTokenHandler();

                        var keyDetail = Encoding.UTF8.GetBytes(_configuration["JWT:Key"]);

                        var claims = new List<Claim>
                        {
                            new Claim(ClaimTypes.NameIdentifier, user.Id+""),
                            new Claim(ClaimTypes.Name, user.UserEmail),
                        };

                        var tokenDescriptor = new SecurityTokenDescriptor
                        {
                            Audience = _configuration["JWT:Audience"],
                            Issuer = _configuration["JWT:Issuer"],
                            Expires = DateTime.UtcNow.AddDays(5),
                            Subject = new ClaimsIdentity(claims),
                            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(keyDetail), SecurityAlgorithms.HmacSha256Signature)
                        };
                        
                        var token = tokenHandler.CreateToken(tokenDescriptor);
    
                        user.isConnected = true;
                        user.token = tokenHandler.WriteToken(token);

                        _chatDbContext.SaveChanges();

                        _chatHubContext.Clients.All.sendMessageChat(ChatMethod.NewUserConnected, JsonSerializer.Serialize(user));

                    return Ok(user);
                }

            }

        }
    }
}
