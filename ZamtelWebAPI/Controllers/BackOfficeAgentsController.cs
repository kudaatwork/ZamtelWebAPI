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
    public class BackOfficeAgentsController : ControllerBase
    {
        private readonly ZamtelContext _context;

        public BackOfficeAgentsController(ZamtelContext context)
        {
            _context = context;
        }

        // GET: api/BackOfficeAgents
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BackOfficeAgentViewModel>>> GetBackOfficeAgents()
        {
            var backOfficeAgents = _context.BackOfficeAgents.ToList();

            var backOfficeAgentsViewModels = new List<BackOfficeAgentViewModel>();

            foreach (var backOfficeAgent in backOfficeAgents)
            {
                var backOfficeAgentViewModel = new BackOfficeAgentViewModel()
                {
                    Id = backOfficeAgent.Id,
                    Firstname = backOfficeAgent.Firstname,
                    Middlename = backOfficeAgent.Middlename,
                    Lastname = backOfficeAgent.Lastname,
                    SupervisorId = backOfficeAgent.SupervisorId,
                    Password = backOfficeAgent.Password,
                    Gender = backOfficeAgent.Gender,
                    Area = backOfficeAgent.Area,
                    TownId = backOfficeAgent.TownId,
                    ProvinceId = backOfficeAgent.ProvinceId,
                    NationalityId = backOfficeAgent.NationalityId,
                    NationalIdNumber = backOfficeAgent.NationalIdNumber,
                    MobileNumber = backOfficeAgent.MobileNumber,
                    AlternativeMobileNumber = backOfficeAgent.AlternativeMobileNumber,
                    PotrailUrl = backOfficeAgent.PotrailUrl,
                    NationalIdFrontUrl = backOfficeAgent.NationalIdFrontUrl,
                    NationalIdBackUrl = backOfficeAgent.NationalIdBackUrl,
                    SignatureUrl = backOfficeAgent.SignatureUrl,
                    AgentContractFormUrl = backOfficeAgent.AgentContractFormUrl,
                    IsVerified = backOfficeAgent.IsVerified,
                    NextOfKin = _context.NextOfKins.Where(x => x.Id == backOfficeAgent.NextOfKinId).FirstOrDefault(),
                    DateTimeCreated = backOfficeAgent.DateTimeCreated,
                    RoleId = backOfficeAgent.RoleId,
                    CreatedByUserId = backOfficeAgent.CreatedByUserId,
                    ModifiedByUserId = backOfficeAgent.ModifiedByUserId
                };

                backOfficeAgentsViewModels.Add(backOfficeAgentViewModel);
            }

            return backOfficeAgentsViewModels;
        }

        // GET: api/BackOfficeAgents/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BackOfficeAgentViewModel>> GetBackOfficeAgent(int id)
        {
            var backOfficeAgent = _context.BackOfficeAgents.Where(x => x.Id == id).FirstOrDefault();

            if (backOfficeAgent == null)
            {
                return NotFound();
            }

            var backOfficeAgentViewModel = new BackOfficeAgentViewModel()
            {
                Id = backOfficeAgent.Id,
                Firstname = backOfficeAgent.Firstname,
                Middlename = backOfficeAgent.Middlename,
                Lastname = backOfficeAgent.Lastname,
                SupervisorId = backOfficeAgent.SupervisorId,
                Password = backOfficeAgent.Password,
                Gender = backOfficeAgent.Gender,
                Area = backOfficeAgent.Area,
                TownId = backOfficeAgent.TownId,
                ProvinceId = backOfficeAgent.ProvinceId,
                NationalityId = backOfficeAgent.NationalityId,
                NationalIdNumber = backOfficeAgent.NationalIdNumber,
                MobileNumber = backOfficeAgent.MobileNumber,
                AlternativeMobileNumber = backOfficeAgent.AlternativeMobileNumber,
                IsVerified = backOfficeAgent.IsVerified,
                NextOfKinId = backOfficeAgent.NextOfKinId,
                CreatedByUserId = backOfficeAgent.CreatedByUserId,
                ModifiedByUserId = backOfficeAgent.ModifiedByUserId,
                NextOfKin = _context.NextOfKins.Where(x => x.Id == backOfficeAgent.NextOfKinId).FirstOrDefault(),
                DateTimeCreated = backOfficeAgent.DateTimeCreated,
                DateTimeModified = backOfficeAgent.DateTimeModified,
                RoleId = backOfficeAgent.RoleId
            };

            return backOfficeAgentViewModel;
        }

        // PUT: api/BackOfficeAgents/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBackOfficeAgent(int id, BackOfficeAgentViewModel backOfficeAgentViewModel)
        {
            if (id != backOfficeAgentViewModel.Id)
            {
                ModelState.AddModelError("Error", "Back office agent cannot be updated. Id sent does not match back office agent Id");

                return BadRequest(ModelState);
            }

            BackOfficeAgentValidators backOfficeAgentValidators = new BackOfficeAgentValidators(_context);

            var doesBackOfficeAgentExist = backOfficeAgentValidators.IsBackOfficeAgentMobileNumberExist(backOfficeAgentViewModel.MobileNumber);

            if (!doesBackOfficeAgentExist)
            {
                ModelState.AddModelError("Error", "Back office agent cannot be updated. Back office agent mobile number " + backOfficeAgentViewModel.MobileNumber + " does not exist.");

                return BadRequest(ModelState);
            }

            var isMobileNumber = PhoneNumber.IsPhoneNbr(backOfficeAgentViewModel.NextOfKin.MobileNumber);

            if (!isMobileNumber)
            {
                ModelState.AddModelError("Error", "Back office agent cannot be updated. Next of kin mobile number is not in correct format");

                return BadRequest(ModelState);
            }

            var nextOfKin = _context.NextOfKins.Where(x => x.Id == backOfficeAgentViewModel.NextOfKinId).FirstOrDefault();

            if (nextOfKin == null)
            {
                ModelState.AddModelError("Error", "Back ofiice agent cannot be updated. NextOfKinId cannot be '0'");

                return BadRequest(ModelState);
            }

            nextOfKin.Firstname = backOfficeAgentViewModel.NextOfKin.Firstname;
            nextOfKin.Middlename = backOfficeAgentViewModel.NextOfKin.Middlename;
            nextOfKin.Lastname = backOfficeAgentViewModel.NextOfKin.Lastname;
            nextOfKin.MobileNumber = backOfficeAgentViewModel.NextOfKin.MobileNumber;
            nextOfKin.DateTimeModified = DateTime.Now;
            nextOfKin.ModifiedByUserId = backOfficeAgentViewModel.ModifiedByUserId;

            if (nextOfKin.ModifiedByUserId == null)
            {
                nextOfKin.ModifiedByUserId = 3;
            }

            var backOfficeAgentToBeUpdated = _context.BackOfficeAgents.Where(x => x.Id == id || x.Id == backOfficeAgentViewModel.Id).FirstOrDefault();

            backOfficeAgentToBeUpdated.Firstname = backOfficeAgentViewModel.Firstname;
            backOfficeAgentToBeUpdated.Middlename = backOfficeAgentViewModel.Middlename;
            backOfficeAgentToBeUpdated.Lastname = backOfficeAgentViewModel.Lastname;
            backOfficeAgentToBeUpdated.SupervisorId = backOfficeAgentViewModel.SupervisorId;
            backOfficeAgentToBeUpdated.Password = backOfficeAgentViewModel.Password;
            backOfficeAgentToBeUpdated.Gender = backOfficeAgentViewModel.Gender;
            backOfficeAgentToBeUpdated.Area = backOfficeAgentViewModel.Area;
            backOfficeAgentToBeUpdated.TownId = backOfficeAgentViewModel.TownId;
            backOfficeAgentToBeUpdated.ProvinceId = backOfficeAgentViewModel.ProvinceId;
            backOfficeAgentToBeUpdated.NationalityId = backOfficeAgentViewModel.NationalityId;
            backOfficeAgentToBeUpdated.NationalIdNumber = backOfficeAgentViewModel.NationalIdNumber;
            backOfficeAgentToBeUpdated.MobileNumber = backOfficeAgentViewModel.MobileNumber;
            backOfficeAgentToBeUpdated.AlternativeMobileNumber = backOfficeAgentViewModel.AlternativeMobileNumber;
            backOfficeAgentToBeUpdated.PotrailUrl = backOfficeAgentViewModel.PotrailUrl;
            backOfficeAgentToBeUpdated.NationalIdFrontUrl = backOfficeAgentViewModel.NationalIdFrontUrl;
            backOfficeAgentToBeUpdated.NationalIdBackUrl = backOfficeAgentViewModel.NationalIdBackUrl;
            backOfficeAgentToBeUpdated.SignatureUrl = backOfficeAgentViewModel.SignatureUrl;
            backOfficeAgentToBeUpdated.AgentContractFormUrl = backOfficeAgentViewModel.AgentContractFormUrl;
            backOfficeAgentToBeUpdated.IsVerified = backOfficeAgentViewModel.IsVerified;
            backOfficeAgentToBeUpdated.NextOfKinId = backOfficeAgentViewModel.NextOfKin.Id;
            backOfficeAgentToBeUpdated.DateTimeCreated = backOfficeAgentViewModel.DateTimeCreated;
            backOfficeAgentToBeUpdated.DateTimeModified = DateTime.Now;
            backOfficeAgentToBeUpdated.ModifiedByUserId = backOfficeAgentViewModel.ModifiedByUserId;
            backOfficeAgentToBeUpdated.RoleId = backOfficeAgentViewModel.RoleId;

            _context.Entry(nextOfKin).State = EntityState.Modified;
            _context.Entry(backOfficeAgentToBeUpdated).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BackOfficeAgentExists(id))
                {
                    ModelState.AddModelError("Error", "Back office agent cannot be updated. Back Ofiice Agent Id does not exist");

                    return NotFound(ModelState);
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/BackOfficeAgents
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<BackOfficeAgent>> PostBackOfficeAgent(BackOfficeAgentViewModel backOfficeAgentViewModel)
        {
            BackOfficeAgentValidators backOfficeAgentValidators = new BackOfficeAgentValidators(_context);

            var doesBackOfficeAgentMobileNumberExist = backOfficeAgentValidators.IsBackOfficeAgentMobileNumberExist(backOfficeAgentViewModel.MobileNumber);

            if (doesBackOfficeAgentMobileNumberExist)
            {
                ModelState.AddModelError("Error", "Back office agent cannot be created. Agent mobile number " + backOfficeAgentViewModel.MobileNumber + " already exists.");

                return BadRequest(ModelState);
            }

            var doesBackOfficeAgentIdNumberExist = backOfficeAgentValidators.IsBackOfficeAgentIdNumberExist(backOfficeAgentViewModel.NationalIdNumber);

            if (doesBackOfficeAgentIdNumberExist)
            {
                ModelState.AddModelError("Error", "Back office agent cannot be created. Agent national id number " + backOfficeAgentViewModel.NationalIdNumber + " already exists.");

                return BadRequest(ModelState);
            }

            var doesNextOfKinExist = backOfficeAgentValidators.IsNextOfKinMobileNumberExist(backOfficeAgentViewModel.MobileNumber);

            if (doesNextOfKinExist)
            {
                ModelState.AddModelError("Error", "Back office agent cannot be created. Next of kin mobile number " + backOfficeAgentViewModel.MobileNumber + " already exists.");

                return BadRequest(ModelState);
            }

            NextOfKin nextOfKin = new NextOfKin()
            {
                Firstname = backOfficeAgentViewModel.NextOfKin.Firstname,
                Middlename = backOfficeAgentViewModel.NextOfKin.Middlename,
                Lastname = backOfficeAgentViewModel.NextOfKin.Lastname,
                MobileNumber = backOfficeAgentViewModel.NextOfKin.MobileNumber,
                CreatedByUserId = backOfficeAgentViewModel.CreatedByUserId,
                DateTimeCreated = DateTime.Now
            };

            _context.NextOfKins.Add(nextOfKin);
            await _context.SaveChangesAsync();

            var lastAddedNextOfKin = _context.NextOfKins.OrderByDescending(x => x.Id).FirstOrDefault();

            backOfficeAgentViewModel.CreatedByUserId = 3;

            if (lastAddedNextOfKin != null)
            {
                BackOfficeAgent backOfficeAgent = new BackOfficeAgent()
                {
                    Firstname = backOfficeAgentViewModel.Firstname,
                    Middlename = backOfficeAgentViewModel.Middlename,
                    Lastname = backOfficeAgentViewModel.Lastname,
                    SupervisorId = backOfficeAgentViewModel.SupervisorId,
                    Password = backOfficeAgentViewModel.Password,
                    Gender = backOfficeAgentViewModel.Gender,
                    Area = backOfficeAgentViewModel.Area,
                    TownId = backOfficeAgentViewModel.TownId,
                    ProvinceId = backOfficeAgentViewModel.ProvinceId,
                    NationalityId = backOfficeAgentViewModel.NationalityId,
                    NationalIdNumber = backOfficeAgentViewModel.NationalIdNumber,
                    MobileNumber = backOfficeAgentViewModel.MobileNumber,
                    AlternativeMobileNumber = backOfficeAgentViewModel.AlternativeMobileNumber,
                    PotrailUrl = backOfficeAgentViewModel.PotrailUrl,
                    NationalIdFrontUrl = backOfficeAgentViewModel.NationalIdFrontUrl,
                    NationalIdBackUrl = backOfficeAgentViewModel.NationalIdBackUrl,
                    SignatureUrl = backOfficeAgentViewModel.SignatureUrl,
                    AgentContractFormUrl = backOfficeAgentViewModel.AgentContractFormUrl,
                    IsVerified = backOfficeAgentViewModel.IsVerified,
                    NextOfKinId = lastAddedNextOfKin.Id,
                    DateTimeCreated = DateTime.Now,
                    CreatedByUserId = backOfficeAgentViewModel.CreatedByUserId,
                    RoleId = backOfficeAgentViewModel.RoleId
                };

                _context.BackOfficeAgents.Add(backOfficeAgent);
                await _context.SaveChangesAsync();

                var lastAddedBackOfficeAgent = _context.BackOfficeAgents.OrderByDescending(x => x.Id).FirstOrDefault();

                var backOfficeAgentViewModel1 = new BackOfficeAgentViewModel()
                {
                    Id = lastAddedBackOfficeAgent.Id,
                    Firstname = lastAddedBackOfficeAgent.Firstname,
                    Middlename = lastAddedBackOfficeAgent.Middlename,
                    Lastname = lastAddedBackOfficeAgent.Lastname,
                    SupervisorId = lastAddedBackOfficeAgent.SupervisorId,
                    Password = lastAddedBackOfficeAgent.Password,
                    Gender = lastAddedBackOfficeAgent.Gender,
                    Area = lastAddedBackOfficeAgent.Area,
                    TownId = lastAddedBackOfficeAgent.TownId,
                    ProvinceId = lastAddedBackOfficeAgent.ProvinceId,
                    NationalityId = lastAddedBackOfficeAgent.NationalityId,
                    NationalIdNumber = lastAddedBackOfficeAgent.NationalIdNumber,
                    MobileNumber = lastAddedBackOfficeAgent.MobileNumber,
                    AlternativeMobileNumber = lastAddedBackOfficeAgent.AlternativeMobileNumber,
                    IsVerified = lastAddedBackOfficeAgent.IsVerified,
                    NextOfKin = _context.NextOfKins.Where(x => x.Id == lastAddedBackOfficeAgent.NextOfKinId).FirstOrDefault(),
                    DateTimeCreated = lastAddedBackOfficeAgent.DateTimeCreated,
                    CreatedByUserId = lastAddedBackOfficeAgent.CreatedByUserId,
                    ModifiedByUserId = lastAddedBackOfficeAgent.ModifiedByUserId
                };

                return CreatedAtAction("GetBackOfficeAgent", new { id = lastAddedBackOfficeAgent.Id }, backOfficeAgentViewModel1);
            }
            else
            {
                ModelState.AddModelError("Error", "There has been a fatal error in saving your Next of Kin Details");

                return BadRequest(ModelState);
            }
        }

        // DELETE: api/BackOfficeAgents/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<BackOfficeAgentViewModel>> DeleteBackOfficeAgent(int id)
        {
            var backOfficeAgent = _context.BackOfficeAgents.Where(x => x.Id == id).FirstOrDefault();

            if (backOfficeAgent == null)
            {
                ModelState.AddModelError("Error", "Back office agent cannot be deleted. Agent Id does not exist");

                return NotFound(ModelState);
            }

            var nextOfKin = _context.NextOfKins.Where(x => x.Id == backOfficeAgent.NextOfKinId).FirstOrDefault();

            if (nextOfKin == null)
            {
                ModelState.AddModelError("Error", "Back office agent cannot be deleted. Agent Id does not exist");

                return NotFound(ModelState);
            }

            _context.NextOfKins.Remove(nextOfKin);
            _context.BackOfficeAgents.Remove(backOfficeAgent);
            await _context.SaveChangesAsync();

            var backOfficeAgentViewModel = new BackOfficeAgentViewModel()
            {
                Firstname = backOfficeAgent.Firstname,
                Middlename = backOfficeAgent.Middlename,
                Lastname = backOfficeAgent.Lastname,
                SupervisorId = backOfficeAgent.SupervisorId,
                Password = backOfficeAgent.Password,
                Gender = backOfficeAgent.Gender,
                Area = backOfficeAgent.Area,
                TownId = backOfficeAgent.TownId,
                ProvinceId = backOfficeAgent.ProvinceId,
                NationalityId = backOfficeAgent.NationalityId,
                NationalIdNumber = backOfficeAgent.NationalIdNumber,
                MobileNumber = backOfficeAgent.MobileNumber,
                AlternativeMobileNumber = backOfficeAgent.AlternativeMobileNumber,
                IsVerified = backOfficeAgent.IsVerified,
                NextOfKinId = backOfficeAgent.NextOfKinId,
                NextOfKin = _context.NextOfKins.Where(x => x.Id == backOfficeAgent.NextOfKinId).FirstOrDefault(),
                DateTimeCreated = backOfficeAgent.DateTimeCreated,
                DateTimeModified = backOfficeAgent.DateTimeModified,
                CreatedByUserId = backOfficeAgent.CreatedByUserId,
                ModifiedByUserId = backOfficeAgent.ModifiedByUserId
            };

            return backOfficeAgentViewModel;
        }

        private bool BackOfficeAgentExists(int id)
        {
            return _context.BackOfficeAgents.Any(e => e.Id == id);
        }
    }
}
