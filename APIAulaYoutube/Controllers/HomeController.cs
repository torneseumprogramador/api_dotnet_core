using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APIAulaYoutube.Controllers
{
    [Route("")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        public Apresentacao Index()
        {
            return new Apresentacao();
        }

        [Route("login")]
        public Validacao Login(string login, string senha)
        {
            return new Apresentacao().ValidarLogin(login, senha);
        }
    }
}