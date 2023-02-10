using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using secretSanta.Models;
using secretSanta.Services;


namespace secretSanta.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SecretSantaController : ControllerBase
    {
        private readonly ISecretSantaService _SecretSantaService;

        public SecretSantaController(ISecretSantaService SecretSantaService)
        {
            _SecretSantaService = SecretSantaService;

        }
       
        [HttpPost]
        [Route("SendMail")]
        public async Task<ActionResult<ResponseObject>> SendMail(List<playerDetails> playerList)
        {
                ResponseObject result =await _SecretSantaService.Randomizer(playerList);
                _SecretSantaService.sendEmailToList(playerList, result.Value);

              return StatusCode(result.StatusCode, result);
        }
    }
}
