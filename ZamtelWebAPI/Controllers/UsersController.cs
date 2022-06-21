using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ZamtelWebAPI.Models;

namespace ZamtelWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ZamtelContext _context;
        private readonly JWTSettings _jwtsettings;

        public UsersController(ZamtelContext context, IOptions<JWTSettings> jwtsettings)
        {
            _context = context;
            _jwtsettings = jwtsettings.Value;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        //// GET: api/Users/5
        //[HttpPost("Login")]
        //public async Task<ActionResult<UserWithToken>> Login([FromBody] User user)
        //{

        //    var currentUser =  _context.Users.Where(x => x.Email == user.Email).FirstOrDefault();

        //    if (currentUser == null)
        //    {
        //        return NotFound();
        //    }

        //    UserWithToken userWithToken = new UserWithToken(user);

        //    if (userWithToken == null)
        //    {
        //        return NotFound();
        //    }

        //    var tokenHandler = new JwtSecurityTokenHandler();
        //    var key = Encoding.ASCII.GetBytes(_jwtsettings.SecretKey);
        //    var tokenDescriptor = new SecurityTokenDescriptor
        //    {
        //        Subject = new ClaimsIdentity(new Claim[]
        //        {
        //            new Claim(ClaimTypes.Name, user.Email)
        //        }),
        //        Expires = DateTime.Now.AddMonths(1),
        //        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
        //        SecurityAlgorithms.HmacSha256Signature)
        //    };

        //    var token = tokenHandler.CreateToken(tokenDescriptor);
        //    userWithToken.Token = tokenHandler.WriteToken(token);

        //    return userWithToken;
        //}

        // GET: api/Users/5
        [HttpPost, Route("Login")]
        public async Task<ActionResult<string>> Login()
        {
            var user = HttpContext.User.Identity.Name;

            var currentUser = await _context.Users.Where(x => x.Email == user).FirstOrDefaultAsync();

            if (currentUser == null)
            {
                return NotFound();
            }

            currentUser.Password = null;

            return "Authenticated";
        }

        // PUT: api/Users/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, User user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Users
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUser", new { id = user.Id }, user);
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<User>> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return user;
        }

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
    }
}
