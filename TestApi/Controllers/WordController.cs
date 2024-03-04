using BTL.DTO;
using BTL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace TestApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class WordController : ControllerBase
    {
        private DictionaryContext _dBDic { get; set; }
        private readonly ILogger<WordController> _logger;
        private readonly IConfiguration _config;

        public WordController( DictionaryContext dictionaryContext, ILogger<WordController> logger, IConfiguration configuration) { 
        this._dBDic= dictionaryContext;
            _logger = logger;
            _config = configuration;
        }
        [HttpGet]
        [Authorize(Roles = "Admin")]
        [Route("preSearchWord/{Id_Language}/{Id_Language_trans}/{sWord}")]
		public List<WordSearch> preSearchWord(int Id_Language, int Id_Language_trans, string sWord)
		{
			List<WordSearch> w = this._dBDic.preSearchWord(Id_Language, Id_Language_trans, sWord).ToList();
			return w;
		}

        [HttpPost]
        [Route("addNewWord")]
        public IActionResult addWord([FromBody] WordDTO data)
        {
            this._dBDic.addNewWord(data.IdLanguage, data.IdLanguageTrans, data.IdWordtype, data.IdUser, data.SWord, data.SExample, data.SDefinition, data.SWordTrans);
            return Ok();
        }

        [HttpGet]
        [Route("testJWTRoleAdmin")]
        public async Task<string> TestRoleAdmin()
        {
            var claims = new[]
            {
                new Claim("Tên","huy"),
                new Claim("Tuổi","22"),
                new Claim(ClaimTypes.Role,"Admin"),
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Tokens:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_config["Tokens:Issuer"],
                _config["Tokens:Issuer"],
                claims,
                expires: DateTime.Now.AddMinutes(2),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }


        [HttpGet]
        [Route("testJWTRoleUser")]
        public async Task<string> TestUser()
        {
            var claims = new[]
            {
                new Claim("Tên","huy"),
                new Claim("Tuổi","22"),
                new Claim(ClaimTypes.Role,"User"),
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Tokens:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_config["Tokens:Issuer"],
                _config["Tokens:Issuer"],
                claims,
                expires: DateTime.Now.AddMinutes(2),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
