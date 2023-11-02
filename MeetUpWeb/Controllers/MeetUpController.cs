using MeetUpCore.Entities;
using MeetUpCore.Models;
using MeetUpCore.Roles;
using MeetUpCore.ServiceCore.MeetUps;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Security.Claims;

namespace MeetUpWeb.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class MeetUpController : ControllerBase
    {
        private readonly IMeetUpService _meetUpService;
        private long _userID => long.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

        public MeetUpController(IMeetUpService meetUpService)
        {
            _meetUpService = meetUpService;
        }
        [Authorize]
        [Route("[action]")]
        [HttpPost]
        public async Task<ActionResult> CreateMeetUp(MeetUpCreationModel model)
        {
            await _meetUpService.CreateMeetUpAsync(model, _userID);
            return new OkResult();
        }
        [Authorize]
        [Route("[action]")]
        [HttpDelete]
        public async Task<ActionResult> DeleteMeetUp(long ID)
        {
            await _meetUpService.DeleteMeetUpAsync(ID, _userID);
            return new OkResult();
        }
        [Authorize]
        [Route("[action]")]
        [HttpPut]
        public async Task<ActionResult> UpdateMeetUp(MeetUpUpdateModel model)
        {
            await _meetUpService.UpdateMeetUpAsync(model, _userID);
            return new OkResult();
        }

        [Authorize]
        [Route("[action]")]
        [HttpGet]
        public async Task<ActionResult<MeetUpReturningModel>> GetMeetUpByID(long ID)
        {
            return new OkObjectResult(await _meetUpService.SearchMetUpAsync(ID));
        }

        [Authorize]
        [Route("[action]")]
        [HttpGet]
        public async Task<ActionResult<List<MeetUpReturningModel>>> GetAllMeetUp([FromQuery] PaginationSettingsModel model) 
        {
            return new OkObjectResult(await _meetUpService.GetAllAsync(model));
        }
    }
}
