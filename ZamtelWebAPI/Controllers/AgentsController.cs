using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ZamtelWebAPI.Models;
using ZamtelWebAPI.Validators;
using ZamtelWebAPI.ViewModels;

namespace ZamtelWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AgentsController : ControllerBase
    {
        private readonly ZamtelContext _context;

        public AgentsController(ZamtelContext context)
        {
            _context = context;
        }

        // GET: api/Agents
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AgentViewModel>>> GetAgents()
        {
            var agents = _context.Agents.ToList();

            var agentsViewModel = new List<AgentViewModel>();

            foreach (var agent in agents)
            {
                var nextOfKin = _context.NextOfKins.Where(x => x.Id == agent.NextOfKinId).FirstOrDefault();

                var agentViewModel = new AgentViewModel()
                {
                    Firstname = agent.Firstname,
                    Middlename = agent.Middlename,
                    Lastname = agent.Lastname,
                    DeviceOwnership = agent.DeviceOwnership,
                    SupervisorId = agent.SupervisorId,
                    Password = agent.Password,
                    Gender = agent.Gender,
                    Area = agent.Area,
                    TownId = agent.TownId,
                    ProvinceId = agent.ProvinceId,
                    NationalityId = agent.NationalityId,
                    IdNumber = agent.IdNumber,
                    MobileNumber = agent.MobileNumber,
                    AlternativeMobileNumber = agent.AlternativeMobileNumber,
                    AgentCode = agent.AgentCode,
                    PotrailUrl = agent.PotrailUrl,
                    NationalIdFrontUrl = agent.NationalIdFrontUrl,
                    NationalIdBackUrl = agent.NationalIdBackUrl,
                    SignatureUrl = agent.SignatureUrl,
                    AgentContractFormUrl = agent.AgentContractFormUrl,
                    IsVerified = agent.IsVerified,
                    NextOfKin = _context.NextOfKins.Where(x => x.Id == agent.NextOfKinId).FirstOrDefault(),
                    DateTimeCreated = agent.DateTimeCreated,
                    CreatedByUserId = agent.CreatedByUserId,
                    ModifiedByUserId = agent.ModifiedByUserId
                };

                agentsViewModel.Add(agentViewModel);
            }

            return agentsViewModel;
        }

        // GET: api/Agents/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AgentViewModel>> GetAgent(int id)
        {
            var agent = _context.Agents.Where(x => x.Id == id).FirstOrDefault();

            if (agent == null)
            {
                return NotFound();
            }

            var agentViewModel = new AgentViewModel()
            {
                Id = agent.Id,
                Firstname = agent.Firstname,
                Middlename = agent.Middlename,
                Lastname = agent.Lastname,
                DeviceOwnership = agent.DeviceOwnership,
                SupervisorId = agent.SupervisorId,
                Password = agent.Password,
                Gender = agent.Gender,
                Area = agent.Area,
                TownId = agent.TownId,
                ProvinceId = agent.ProvinceId,
                NationalityId = agent.NationalityId,
                IdNumber = agent.IdNumber,
                MobileNumber = agent.MobileNumber,
                AlternativeMobileNumber = agent.AlternativeMobileNumber,
                AgentCode = agent.AgentCode,
                IsVerified = agent.IsVerified,
                NextOfKinId = agent.NextOfKinId,
                CreatedByUserId = agent.CreatedByUserId,
                NextOfKin = _context.NextOfKins.Where(x => x.Id == agent.NextOfKinId).FirstOrDefault(),
                DateTimeCreated = agent.DateTimeCreated,
                DateTimeModified = agent.DateTimeModified,
                ModifiedByUserId = agent.ModifiedByUserId
            };

            return agentViewModel;
        }

        // PUT: api/Agents/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAgent(int id, AgentViewModel agentViewModel)
        {
            if (id != agentViewModel.Id)
            {
                ModelState.AddModelError("Error", "Agent cannot be updated. Id sent does not match Agent's Id");

                return BadRequest(ModelState);
            }

            AgentValidators agentValidators = new AgentValidators(_context);

            var doesAgentExist = agentValidators.IsAgentMobileNumberExist(agentViewModel.MobileNumber);

            if (!doesAgentExist)
            {
                ModelState.AddModelError("Error", "Agent cannot be updated. Agent mobile number " + agentViewModel.MobileNumber + " does not exist.");

                return BadRequest(ModelState);
            }

            var isMobileNumber = PhoneNumber.IsPhoneNbr(agentViewModel.NextOfKin.MobileNumber);

            if (!isMobileNumber)
            {
                ModelState.AddModelError("Error", "Agent cannot be updated. Next of kin mobile number is not in correct format");

                return BadRequest(ModelState);
            }

            var nextOfKin = _context.NextOfKins.Where(x => x.Id == agentViewModel.NextOfKinId).FirstOrDefault();

            if (nextOfKin == null)
            {
                ModelState.AddModelError("Error", "Agent cannot be updated. NextOfKinId cannot be '0'");

                return BadRequest(ModelState);
            }

            nextOfKin.Firstname = agentViewModel.NextOfKin.Firstname;
            nextOfKin.Middlename = agentViewModel.NextOfKin.Middlename;
            nextOfKin.Lastname = agentViewModel.NextOfKin.Lastname;
            nextOfKin.MobileNumber = agentViewModel.NextOfKin.MobileNumber;
            nextOfKin.DateTimeModified = DateTime.Now;
            nextOfKin.ModifiedByUserId = agentViewModel.ModifiedByUserId;

            if (nextOfKin.ModifiedByUserId == null)
            {
                nextOfKin.ModifiedByUserId = 3;
            }

            var agentToBeUpdated = _context.Agents.Where(x => x.Id == id || x.Id == agentViewModel.Id).FirstOrDefault();
                       
            agentToBeUpdated.Firstname = agentViewModel.Firstname;
            agentToBeUpdated.Middlename = agentViewModel.Middlename;
            agentToBeUpdated.Lastname = agentViewModel.Lastname;
            agentToBeUpdated.DeviceOwnership = agentViewModel.DeviceOwnership;
            agentToBeUpdated.SupervisorId = agentViewModel.SupervisorId;
            agentToBeUpdated.Password = agentViewModel.Password;
            agentToBeUpdated.Gender = agentViewModel.Gender;
            agentToBeUpdated.Area = agentViewModel.Area;
            agentToBeUpdated.TownId = agentViewModel.TownId;
            agentToBeUpdated.ProvinceId = agentViewModel.ProvinceId;
            agentToBeUpdated.NationalityId = agentViewModel.NationalityId;
            agentToBeUpdated.IdNumber = agentViewModel.IdNumber;
            agentToBeUpdated.MobileNumber = agentViewModel.MobileNumber;
            agentToBeUpdated.AlternativeMobileNumber = agentViewModel.AlternativeMobileNumber;
            agentToBeUpdated.AgentCode = agentViewModel.AgentCode;
            agentToBeUpdated.PotrailUrl = agentViewModel.PotrailUrl;
            agentToBeUpdated.NationalIdFrontUrl = agentViewModel.NationalIdFrontUrl;
            agentToBeUpdated.NationalIdBackUrl = agentViewModel.NationalIdBackUrl;
            agentToBeUpdated.SignatureUrl = agentViewModel.SignatureUrl;
            agentToBeUpdated.AgentContractFormUrl = agentViewModel.AgentContractFormUrl;
            agentToBeUpdated.IsVerified = agentViewModel.IsVerified;
            agentToBeUpdated.NextOfKinId = agentViewModel.NextOfKin.Id;
            agentToBeUpdated.DateTimeCreated = DateTime.Now;
            agentToBeUpdated.ModifiedByUserId = agentViewModel.ModifiedByUserId;

            _context.Entry(nextOfKin).State = EntityState.Modified;
            _context.Entry(agentToBeUpdated).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AgentExists(id))
                {
                    ModelState.AddModelError("Error", "Agent cannot be updated. Agent Id does not exist");

                    return NotFound(ModelState);
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Agents
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Agent>> PostAgent(AgentViewModel agentViewModel)
        {
            AgentValidators agentValidators = new AgentValidators(_context);

            var doesAgentMobileNumberExist = agentValidators.IsAgentMobileNumberExist(agentViewModel.MobileNumber);

            if (doesAgentMobileNumberExist)
            {
                ModelState.AddModelError("Error", "Agent cannot be created. Agent mobile number " + agentViewModel.MobileNumber + " already exists.");

                return BadRequest(ModelState);
            }

            var doesAgentIdNumberExist = agentValidators.IsAgentIdNumberExist(agentViewModel.IdNumber);

            if (doesAgentIdNumberExist)
            {
                ModelState.AddModelError("Error", "Agent cannot be created. Agent national id number " + agentViewModel.IdNumber + " already exists.");

                return BadRequest(ModelState);
            }

            var doesNextOfKinExist = agentValidators.IsNextOfKinMobileNumberExist(agentViewModel.MobileNumber);

            if (doesNextOfKinExist)
            {
                ModelState.AddModelError("Error", "Agent cannot be created. Next of kin mobile number " + agentViewModel.MobileNumber + " already exists.");

                return BadRequest(ModelState);
            }

            NextOfKin nextOfKin = new NextOfKin()
            {
                Firstname = agentViewModel.NextOfKin.Firstname,
                Middlename = agentViewModel.NextOfKin.Middlename,
                Lastname = agentViewModel.NextOfKin.Lastname,
                MobileNumber = agentViewModel.NextOfKin.MobileNumber,
                CreatedByUserId = agentViewModel.CreatedByUserId,
                DateTimeCreated = DateTime.Now
            };

            _context.NextOfKins.Add(nextOfKin);
            await _context.SaveChangesAsync();

            var lastAddedNextOfKin = _context.NextOfKins.OrderByDescending(x => x.Id).FirstOrDefault();

            agentViewModel.CreatedByUserId = 3;

            if (lastAddedNextOfKin != null)
            {
                Agent agent = new Agent()
                {
                    Firstname = agentViewModel.Firstname,
                    Middlename = agentViewModel.Middlename,
                    Lastname = agentViewModel.Lastname,
                    DeviceOwnership = agentViewModel.DeviceOwnership,
                    SupervisorId = agentViewModel.SupervisorId,
                    Password = agentViewModel.Password,
                    Gender = agentViewModel.Gender,
                    Area = agentViewModel.Area,
                    TownId = agentViewModel.TownId,
                    ProvinceId = agentViewModel.ProvinceId,
                    NationalityId = agentViewModel.NationalityId,
                    IdNumber = agentViewModel.IdNumber,
                    MobileNumber = agentViewModel.MobileNumber,
                    AlternativeMobileNumber = agentViewModel.AlternativeMobileNumber,
                    AgentCode = agentViewModel.AgentCode,
                    PotrailUrl = agentViewModel.PotrailUrl,
                    NationalIdFrontUrl = agentViewModel.NationalIdFrontUrl,
                    NationalIdBackUrl = agentViewModel.NationalIdBackUrl,
                    SignatureUrl = agentViewModel.SignatureUrl,
                    AgentContractFormUrl = agentViewModel.AgentContractFormUrl,
                    IsVerified = agentViewModel.IsVerified,
                    NextOfKinId = lastAddedNextOfKin.Id,
                    DateTimeCreated = DateTime.Now,
                    CreatedByUserId = agentViewModel.CreatedByUserId,
                    ModifiedByUserId = agentViewModel.ModifiedByUserId
                };

                _context.Agents.Add(agent);
                await _context.SaveChangesAsync();

                var lastAddedAgent = _context.Agents.OrderByDescending(x => x.Id).FirstOrDefault();

                var agentViewModel1 = new AgentViewModel()
                {
                    Id = lastAddedAgent.Id,
                    Firstname = lastAddedAgent.Firstname,
                    Middlename = lastAddedAgent.Middlename,
                    Lastname = lastAddedAgent.Lastname,
                    DeviceOwnership = lastAddedAgent.DeviceOwnership,
                    SupervisorId = lastAddedAgent.SupervisorId,
                    Password = lastAddedAgent.Password,
                    Gender = lastAddedAgent.Gender,
                    Area = lastAddedAgent.Area,
                    TownId = lastAddedAgent.TownId,
                    ProvinceId = lastAddedAgent.ProvinceId,
                    NationalityId = lastAddedAgent.NationalityId,
                    IdNumber = lastAddedAgent.IdNumber,
                    MobileNumber = lastAddedAgent.MobileNumber,
                    AlternativeMobileNumber = lastAddedAgent.AlternativeMobileNumber,
                    AgentCode = lastAddedAgent.AgentCode,
                    IsVerified = lastAddedAgent.IsVerified,
                    NextOfKin = _context.NextOfKins.Where(x => x.Id == lastAddedAgent.NextOfKinId).FirstOrDefault(),
                    DateTimeCreated = lastAddedAgent.DateTimeCreated,
                    CreatedByUserId = lastAddedAgent.CreatedByUserId,
                    ModifiedByUserId = lastAddedAgent.ModifiedByUserId
                };

                return CreatedAtAction("GetAgent", new { id = lastAddedAgent.Id }, agentViewModel1);
            }
            else
            {
                ModelState.AddModelError("Error", "There has been a fatal error in saving your Next of Kin Details");

                return BadRequest(ModelState);
            }
        }

        // DELETE: api/Agents/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<AgentViewModel>> DeleteAgent(int id)
        {
            var agent = _context.Agents.Where(x => x.Id == id).FirstOrDefault();

            if (agent == null)
            {
                ModelState.AddModelError("Error", "Agent cannot be deleted. Agent Id does not exist");

                return NotFound(ModelState);
            }

            var nextOfKin = _context.NextOfKins.Where(x => x.Id == agent.NextOfKinId).FirstOrDefault();

            if (nextOfKin == null)
            {
                ModelState.AddModelError("Error", "Agent cannot be deleted. Agent Id does not exist");

                return NotFound(ModelState);
            }

            _context.NextOfKins.Remove(nextOfKin);
            _context.Agents.Remove(agent);
            await _context.SaveChangesAsync();

            var agentViewModel = new AgentViewModel()
            {
                Firstname = agent.Firstname,
                Middlename = agent.Middlename,
                Lastname = agent.Lastname,
                DeviceOwnership = agent.DeviceOwnership,
                SupervisorId = agent.SupervisorId,
                Password = agent.Password,
                Gender = agent.Gender,
                Area = agent.Area,
                TownId = agent.TownId,
                ProvinceId = agent.ProvinceId,
                NationalityId = agent.NationalityId,
                IdNumber = agent.IdNumber,
                MobileNumber = agent.MobileNumber,
                AlternativeMobileNumber = agent.AlternativeMobileNumber,
                AgentCode = agent.AgentCode,
                IsVerified = agent.IsVerified,
                NextOfKinId = agent.NextOfKinId,
                NextOfKin = _context.NextOfKins.Where(x => x.Id == agent.NextOfKinId).FirstOrDefault(),
                DateTimeCreated = agent.DateTimeCreated,
                DateTimeModified = agent.DateTimeModified,
                CreatedByUserId = agent.CreatedByUserId,
                ModifiedByUserId = agent.ModifiedByUserId
            };

            return agentViewModel;
        }

        private bool AgentExists(int id)
        {
            return _context.Agents.Any(e => e.Id == id);
        }
    }
}
