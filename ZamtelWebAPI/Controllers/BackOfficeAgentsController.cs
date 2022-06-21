using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using ZamtelWebAPI.Models;
using ZamtelWebAPI.Validators;
using ZamtelWebAPI.ViewModels;

namespace ZamtelWebAPI.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class BackOfficeAgentsController : ControllerBase
    {
        private readonly ZamtelContext _context;

        private static IWebHostEnvironment _webHostEnvironment;

        public BackOfficeAgentsController(ZamtelContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;

            _webHostEnvironment = webHostEnvironment;
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
        public async Task<IActionResult> PutBackOfficeAgent(int id, BackOfficeAgentRegistration backOfficeAgentRegistration)
        {
            var backOfficeAgentViewModel = JsonConvert.DeserializeObject<BackOfficeAgentViewModel>(backOfficeAgentRegistration.AgentRegistrationDetails);
            BackOfficeAgentValidators backOfficeAgentValidators = new BackOfficeAgentValidators(_context);
            var agentRegistrationFiles = new List<IFormFile>();

            if (id != backOfficeAgentViewModel.Id)
            {
                ModelState.AddModelError("Error", "Back office agent cannot be updated. Id sent does not match back office agent Id");

                return BadRequest(ModelState);
            }

            if (backOfficeAgentRegistration.Portrait.Length > 0 && backOfficeAgentRegistration.NationalIdFront.Length > 0 && backOfficeAgentRegistration.NationalIdBack.Length > 0)
            {
                agentRegistrationFiles.Add(backOfficeAgentRegistration.Portrait);
                agentRegistrationFiles.Add(backOfficeAgentRegistration.NationalIdFront);
                agentRegistrationFiles.Add(backOfficeAgentRegistration.NationalIdBack);

                if (backOfficeAgentRegistration.Signature.Length > 0)
                {
                    agentRegistrationFiles.Add(backOfficeAgentRegistration.Signature);
                }

                if (backOfficeAgentRegistration.AgentContractForm.Length > 0)
                {
                    agentRegistrationFiles.Add(backOfficeAgentRegistration.AgentContractForm);
                }

                try
                {
                    if (!Directory.Exists(_webHostEnvironment.WebRootPath + "\\Uploads\\"))
                    {
                        Directory.CreateDirectory(_webHostEnvironment.WebRootPath + "\\Uploads\\");
                    }

                    foreach (var item in agentRegistrationFiles)
                    {
                        using (FileStream fileStream = System.IO.File.Create(_webHostEnvironment.WebRootPath + "\\Uploads\\" + item.FileName))
                        {
                            item.CopyTo(fileStream);
                            fileStream.Flush();
                        }
                    }

                    if (backOfficeAgentRegistration.Portrait != null)
                    {
                        backOfficeAgentViewModel.PotrailUrl = "\\Uploads\\" + backOfficeAgentRegistration.Portrait.FileName;
                    }
                    else if (backOfficeAgentRegistration.NationalIdFront != null)
                    {
                        backOfficeAgentViewModel.NationalIdFrontUrl = "\\Uploads\\" + backOfficeAgentRegistration.NationalIdFront.FileName;
                    }
                    else if (backOfficeAgentRegistration.NationalIdBack != null)
                    {
                        backOfficeAgentViewModel.NationalIdBackUrl = "\\Uploads\\" + backOfficeAgentRegistration.NationalIdBack.FileName;
                    }
                    else if (backOfficeAgentRegistration.Signature != null)
                    {
                        backOfficeAgentViewModel.SignatureUrl = "\\Uploads\\" + backOfficeAgentRegistration.Signature.FileName;
                    }
                    else if (!string.IsNullOrEmpty(backOfficeAgentRegistration.SignatureBase64))
                    {
                        byte[] bytes = Convert.FromBase64String(backOfficeAgentRegistration.SignatureBase64);

                        Image image;

                        using (MemoryStream ms = new MemoryStream(bytes))
                        {
                            image = Image.FromStream(ms);
                        }

                        var imageToBeSaved = JsonConvert.DeserializeObject<Images>(backOfficeAgentRegistration.SignatureBase64);

                        image.Save(_webHostEnvironment.WebRootPath + "\\Uploads\\" + imageToBeSaved.Type, ImageFormat.Png);

                        backOfficeAgentViewModel.SignatureUrl = _webHostEnvironment.WebRootPath + "\\Uploads\\" + imageToBeSaved.Type;
                    }

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
                catch (Exception ex)
                {
                    ModelState.AddModelError("Error", ex.ToString());

                    return NotFound(ModelState);
                }
            }
            else
            {
                ModelState.AddModelError("Error", "Images Required");

                return NotFound(ModelState);
            }
        }

        // POST: api/BackOfficeAgents
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<BackOfficeAgent>> PostBackOfficeAgent(BackOfficeAgentRegistration backOfficeAgentRegistration)
        {
            var backOfficeAgentViewModel = JsonConvert.DeserializeObject<BackOfficeAgentViewModel>(backOfficeAgentRegistration.AgentRegistrationDetails);

            var agentRegistrationFiles = new List<IFormFile>();

            BackOfficeAgentValidators backOfficeAgentValidators = new BackOfficeAgentValidators(_context);

            NextOfKin nextOfKinGlobal = new NextOfKin();

            if (backOfficeAgentRegistration.Portrait.Length > 0 && backOfficeAgentRegistration.NationalIdFront.Length > 0 && backOfficeAgentRegistration.NationalIdBack.Length > 0)
            {
                agentRegistrationFiles.Add(backOfficeAgentRegistration.Portrait);
                agentRegistrationFiles.Add(backOfficeAgentRegistration.NationalIdFront);
                agentRegistrationFiles.Add(backOfficeAgentRegistration.NationalIdBack);

                if (backOfficeAgentRegistration.Signature.Length > 0)
                {
                    agentRegistrationFiles.Add(backOfficeAgentRegistration.Signature);
                }

                if (backOfficeAgentRegistration.AgentContractForm.Length > 0)
                {
                    agentRegistrationFiles.Add(backOfficeAgentRegistration.AgentContractForm);
                }

                try
                {
                    if (!Directory.Exists(_webHostEnvironment.WebRootPath + "\\Uploads\\"))
                    {
                        Directory.CreateDirectory(_webHostEnvironment.WebRootPath + "\\Uploads\\");
                    }

                    foreach (var item in agentRegistrationFiles)
                    {
                        using (FileStream fileStream = System.IO.File.Create(_webHostEnvironment.WebRootPath + "\\Uploads\\" + item.FileName))
                        {
                            item.CopyTo(fileStream);
                            fileStream.Flush();
                        }
                    }

                    if (backOfficeAgentRegistration.Portrait != null)
                    {
                        backOfficeAgentViewModel.PotrailUrl = "\\Uploads\\" + backOfficeAgentRegistration.Portrait.FileName;
                    }
                    else if (backOfficeAgentRegistration.NationalIdFront != null)
                    {
                        backOfficeAgentViewModel.NationalIdFrontUrl = "\\Uploads\\" + backOfficeAgentRegistration.NationalIdFront.FileName;
                    }
                    else if (backOfficeAgentRegistration.NationalIdBack != null)
                    {
                        backOfficeAgentViewModel.NationalIdBackUrl = "\\Uploads\\" + backOfficeAgentRegistration.NationalIdBack.FileName;
                    }
                    else if (backOfficeAgentRegistration.Signature != null)
                    {
                        backOfficeAgentViewModel.SignatureUrl = "\\Uploads\\" + backOfficeAgentRegistration.Signature.FileName;
                    }
                    else if (!string.IsNullOrEmpty(backOfficeAgentRegistration.SignatureBase64))
                    {
                        byte[] bytes = Convert.FromBase64String(backOfficeAgentRegistration.SignatureBase64);

                        Image image;

                        using (MemoryStream ms = new MemoryStream(bytes))
                        {
                            image = Image.FromStream(ms);
                        }

                        var imageToBeSaved = JsonConvert.DeserializeObject<Images>(backOfficeAgentRegistration.SignatureBase64);

                        image.Save(_webHostEnvironment.WebRootPath + "\\Uploads\\" + imageToBeSaved.Type, ImageFormat.Png);

                        backOfficeAgentViewModel.SignatureUrl = _webHostEnvironment.WebRootPath + "\\Uploads\\" + imageToBeSaved.Type;
                    }

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
                        nextOfKinGlobal = _context.NextOfKins.Where(x => x.MobileNumber == backOfficeAgentViewModel.MobileNumber).FirstOrDefault();
                    }

                    if (nextOfKinGlobal != null)
                    {
                        if (nextOfKinGlobal.Id > 0)
                        {
                            nextOfKinGlobal.Firstname = backOfficeAgentViewModel.NextOfKin.Firstname;
                            nextOfKinGlobal.Middlename = backOfficeAgentViewModel.NextOfKin.Middlename;
                            nextOfKinGlobal.Lastname = backOfficeAgentViewModel.NextOfKin.Lastname;
                            nextOfKinGlobal.MobileNumber = backOfficeAgentViewModel.NextOfKin.MobileNumber;
                            nextOfKinGlobal.CreatedByUserId = backOfficeAgentViewModel.CreatedByUserId;
                            nextOfKinGlobal.DateTimeCreated = DateTime.Now;

                            _context.NextOfKins.Add(nextOfKinGlobal);
                            await _context.SaveChangesAsync();

                            backOfficeAgentViewModel.CreatedByUserId = 3;

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
                                NextOfKinId = nextOfKinGlobal.Id,
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
                    }
                    else
                    {
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
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("Error", ex.ToString());

                    return BadRequest(ModelState);
                }
            }
            else
            {
                ModelState.AddModelError("Error", "Images Required");

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

            //_context.NextOfKins.Remove(nextOfKin);
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
