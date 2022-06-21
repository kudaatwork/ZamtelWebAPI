using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
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
    [Route("api/[controller]")]
    [ApiController]
    public class SimRegistrationDetailsController : ControllerBase
    {
        private readonly ZamtelContext _context;

        private static IWebHostEnvironment _webHostEnvironment;

        //public SimRegistrationDetailsController(ZamtelContext context)
        //{
        //    _context = context;
        //}

        public SimRegistrationDetailsController(ZamtelContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: api/SimRegistrationDetails
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SimRegistrationViewModel>>> GetSimRegistrationDetails()
        {
            var simRegistrationDetails = _context.SimRegistrationDetails.ToList();

            var simRegistrationViewModels = new List<SimRegistrationViewModel>();

            foreach (var simRegistrationDetail in simRegistrationDetails)
            {
                var customer = _context.Customers.Where(x => x.Id == simRegistrationDetail.CustomerId).FirstOrDefault();
                var nextOfKin = new NextOfKin();

                if (customer != null)
                {
                    nextOfKin = _context.NextOfKins.Where(x => x.Id == customer.NextOfKinId).FirstOrDefault();
                }
                else
                {
                    nextOfKin = null;
                    customer = new Customer();
                }

                var corporate = _context.Corporates.Where(x => x.Id == simRegistrationDetail.CorporateId).FirstOrDefault();
                var representative = new Representative();

                if (corporate != null)
                {
                    representative = _context.Representatives.Where(x => x.Id == corporate.RepresantativeId).FirstOrDefault();
                }
                else
                {
                    representative = null;
                    corporate = new Corporate();
                }

                var simRegistrationViewModel = new SimRegistrationViewModel()
                {
                    Id = simRegistrationDetail.Id,
                    RegistrationType = simRegistrationDetail.RegistrationType,
                    CategoryType = simRegistrationDetail.CategoryType,
                    SimSerialNumber = simRegistrationDetail.SimSerialNumber,
                    CustomerId = simRegistrationDetail.CustomerId,
                    CorporateId = simRegistrationDetail.CorporateId,
                    DateTimeCreated = simRegistrationDetail.DateTimeCreated,
                    DateTimeModified = simRegistrationDetail.DateTimeModified,
                    CreatedByUserId = simRegistrationDetail.CreatedByUserId,
                    ModifiedByUserId = simRegistrationDetail.ModifiedByUserId,
                    Customer = new CustomerViewModel()
                    {
                        Id = customer.Id,
                        Firstname = customer?.Firstname,
                        Middlename = customer?.Middlename,
                        Lastname = customer?.Lastname,
                        Gender = customer?.Gender,
                        DateOfBirth = customer?.DateOfBirth,
                        NationalityId = customer?.NationalityId,
                        IdType = customer?.IdType,
                        NationalIdNumber = customer?.NationalIdNumber,
                        ProvinceId = customer?.ProvinceId,
                        TownId = customer?.TownId,
                        Area = customer?.Area,
                        Address = customer?.Address,
                        Email = customer?.Email,
                        Occupation = customer?.Occupation,
                        MobileNumber = customer?.MobileNumber,
                        AlternativeMobileNumber = customer?.AlternativeMobileNumber,
                        PlotNumber = customer?.PlotNumber,
                        UnitNumber = customer?.UnitNumber,
                        Village = customer?.Village,
                        Landmark = customer?.Landmark,
                        Road = customer?.Road,
                        Chiefdom = customer?.Chiefdom,
                        Neighborhood = customer?.Neighborhood,
                        Section = customer?.Section,
                        NationalIdFrontUrl = customer?.NationalIdFrontUrl,
                        NationalIdBackUrl = customer?.NationalIdBackUrl,
                        PortraitUrl = customer?.PlotNumber,
                        SignatureUrl = customer?.SignatureUrl,
                        IsForeigner = customer?.IsForeigner,
                        DateTimeCreated = customer?.DateTimeCreated,
                        DateTimeModified = customer?.DateTimeModified,
                        CreatedByUserId = customer?.CreatedByUserId,
                        ModifiedByUserId = customer?.ModifiedByUserId,
                        NextOfKinId = customer?.NextOfKinId,
                        NextOfKin = nextOfKin
                    },
                    Corporate = new CorporateViewModel()
                    {
                        Id = corporate.Id,
                        CorporateIdNumber = corporate?.CorporateIdNumber,
                        CertificateUrl = corporate?.CertificateUrl,
                        LetterUrl = corporate?.LetterUrl,
                        BatchUrl = corporate?.BatchUrl,
                        RepresantativeId = corporate?.RepresantativeId,
                        CompanyName = corporate?.CompanyName,
                        CompanyCoinumber = corporate?.CompanyCoinumber,
                        RegistrationDate = corporate?.RegistrationDate,
                        ProvinceId = corporate?.ProvinceId,
                        TownId = corporate?.TownId,
                        Address = corporate?.Address,
                        CompanyEmail = corporate?.CompanyEmail,
                        AlternativeMobileNumber = corporate?.AlternativeMobileNumber,
                        CorporateMobileNumber = corporate?.CorporateMobileNumber,
                        DateTimeCreated = corporate?.DateTimeCreated,
                        DateTimeModified = corporate?.DateTimeModified,
                        CreatedByUserId = corporate?.CreatedByUserId,
                        ModifiedByUserId = corporate?.ModifiedByUserId,
                        IsCompanyGo = corporate?.IsCompanyGo,
                        Representative = representative
                    }
                };

                simRegistrationViewModels.Add(simRegistrationViewModel);
            }

            return simRegistrationViewModels;
        }

        // GET: api/SimRegistrationDetails/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SimRegistrationViewModel>> GetSimRegistrationDetail(int id)
        {
            var simRegistrationDetail = _context.SimRegistrationDetails.Where(x => x.Id == id).FirstOrDefault();

            if (simRegistrationDetail == null)
            {
                return NotFound();
            }

            var customer = _context.Customers.Where(x => x.Id == simRegistrationDetail.CustomerId).FirstOrDefault();
            var nextOfKin = new NextOfKin();

            if (customer != null)
            {
                nextOfKin = _context.NextOfKins.Where(x => x.Id == customer.NextOfKinId).FirstOrDefault();
            }
            else
            {
                nextOfKin = null;
                customer = new Customer();
            }

            var corporate = _context.Corporates.Where(x => x.Id == simRegistrationDetail.CorporateId).FirstOrDefault();
            var representative = new Representative();

            if (corporate != null)
            {
                representative = _context.Representatives.Where(x => x.Id == corporate.RepresantativeId).FirstOrDefault();
            }
            else
            {
                representative = null;
                corporate = new Corporate();
            }

            var simRegistrationViewModel = new SimRegistrationViewModel()
            {
                Id = simRegistrationDetail.Id,
                RegistrationType = simRegistrationDetail.RegistrationType,
                CategoryType = simRegistrationDetail.CategoryType,
                SimSerialNumber = simRegistrationDetail.SimSerialNumber,
                CustomerId = simRegistrationDetail.CustomerId,
                CorporateId = simRegistrationDetail.CorporateId,
                DateTimeCreated = simRegistrationDetail.DateTimeCreated,
                DateTimeModified = simRegistrationDetail.DateTimeModified,
                CreatedByUserId = simRegistrationDetail.CreatedByUserId,
                ModifiedByUserId = simRegistrationDetail.ModifiedByUserId,
                Customer = new CustomerViewModel()
                {
                    Id = customer.Id,
                    Firstname = customer?.Firstname,
                    Middlename = customer?.Middlename,
                    Lastname = customer?.Lastname,
                    Gender = customer?.Gender,
                    DateOfBirth = customer?.DateOfBirth,
                    NationalityId = customer?.NationalityId,
                    IdType = customer?.IdType,
                    NationalIdNumber = customer?.NationalIdNumber,
                    ProvinceId = customer?.ProvinceId,
                    TownId = customer?.TownId,
                    Area = customer?.Area,
                    Address = customer?.Address,
                    Email = customer?.Email,
                    Occupation = customer?.Occupation,
                    MobileNumber = customer?.MobileNumber,
                    AlternativeMobileNumber = customer?.AlternativeMobileNumber,
                    PlotNumber = customer?.PlotNumber,
                    UnitNumber = customer?.UnitNumber,
                    Village = customer?.Village,
                    Landmark = customer?.Landmark,
                    Road = customer?.Road,
                    Chiefdom = customer?.Chiefdom,
                    Neighborhood = customer?.Neighborhood,
                    Section = customer?.Section,
                    NationalIdFrontUrl = customer?.NationalIdFrontUrl,
                    NationalIdBackUrl = customer?.NationalIdBackUrl,
                    PortraitUrl = customer?.PlotNumber,
                    SignatureUrl = customer?.SignatureUrl,
                    IsForeigner = customer?.IsForeigner,
                    DateTimeCreated = customer?.DateTimeCreated,
                    DateTimeModified = customer?.DateTimeModified,
                    CreatedByUserId = customer?.CreatedByUserId,
                    ModifiedByUserId = customer?.ModifiedByUserId,
                    NextOfKinId = customer?.NextOfKinId,
                    NextOfKin = nextOfKin
                },
                Corporate = new CorporateViewModel()
                {
                    Id = corporate.Id,
                    CorporateIdNumber = corporate?.CorporateIdNumber,
                    CertificateUrl = corporate?.CertificateUrl,
                    LetterUrl = corporate?.LetterUrl,
                    BatchUrl = corporate?.BatchUrl,
                    RepresantativeId = corporate?.RepresantativeId,
                    CompanyName = corporate?.CompanyName,
                    CompanyCoinumber = corporate?.CompanyCoinumber,
                    RegistrationDate = corporate?.RegistrationDate,
                    ProvinceId = corporate?.ProvinceId,
                    TownId = corporate?.TownId,
                    Address = corporate?.Address,
                    CompanyEmail = corporate?.CompanyEmail,
                    AlternativeMobileNumber = corporate?.AlternativeMobileNumber,
                    CorporateMobileNumber = corporate?.CorporateMobileNumber,
                    DateTimeCreated = corporate?.DateTimeCreated,
                    DateTimeModified = corporate?.DateTimeModified,
                    CreatedByUserId = corporate?.CreatedByUserId,
                    ModifiedByUserId = corporate?.ModifiedByUserId,
                    IsCompanyGo = corporate?.IsCompanyGo,
                    Representative = representative
                }
            };

            return simRegistrationViewModel;
        }

        // PUT: api/SimRegistrationDetails/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSimRegistrationDetail(int id, [FromForm] SimRegistration registrationDetails)
        {
            SimRegistrationValidators simRegistrationValidators = new SimRegistrationValidators(_context);

            var simRegistrationViewModel = JsonConvert.DeserializeObject<SimRegistrationViewModel>(registrationDetails.SimRegistrationDetails);

            var simRegistrationFiles = new List<IFormFile>();

            if (id != simRegistrationViewModel.Id)
            {
                ModelState.AddModelError("Error", "Sim details cannot be updated. Id sent does not match back office agent Id");

                return BadRequest(ModelState);
            }

            if (registrationDetails.Portrait.Length > 0 && registrationDetails.NationalIdFront.Length > 0 && registrationDetails.NationalIdBack.Length > 0)
            {
                simRegistrationFiles.Add(registrationDetails.Portrait);
                simRegistrationFiles.Add(registrationDetails.NationalIdFront);
                simRegistrationFiles.Add(registrationDetails.NationalIdBack);

                if (registrationDetails.Signature.Length > 0)
                {
                    simRegistrationFiles.Add(registrationDetails.Signature);
                }

                if (registrationDetails.ProofOfStay.Length > 0)
                {
                    simRegistrationFiles.Add(registrationDetails.ProofOfStay);
                }

                if (registrationDetails.LastDayOfStaySupportingDocument.Length > 0)
                {
                    simRegistrationFiles.Add(registrationDetails.LastDayOfStaySupportingDocument);
                }

                try
                {
                    if (!Directory.Exists(_webHostEnvironment.WebRootPath + "\\Uploads\\"))
                    {
                        Directory.CreateDirectory(_webHostEnvironment.WebRootPath + "\\Uploads\\");
                    }

                    foreach (var item in simRegistrationFiles)
                    {
                        using (FileStream fileStream = System.IO.File.Create(_webHostEnvironment.WebRootPath + "\\Uploads\\" + item.FileName))
                        {
                            item.CopyTo(fileStream);
                            fileStream.Flush();
                        }
                    }

                    if (registrationDetails.Portrait != null)
                    {
                        simRegistrationViewModel.Customer.PortraitUrl = "\\Uploads\\" + registrationDetails.Portrait.FileName;
                    }
                    else if (registrationDetails.NationalIdFront != null)
                    {
                        simRegistrationViewModel.Customer.NationalIdFrontUrl = "\\Uploads\\" + registrationDetails.NationalIdFront.FileName;
                    }
                    else if (registrationDetails.NationalIdBack != null)
                    {
                        simRegistrationViewModel.Customer.NationalIdBackUrl = "\\Uploads\\" + registrationDetails.NationalIdBack.FileName;
                    }
                    else if (registrationDetails.Signature != null)
                    {
                        simRegistrationViewModel.Customer.SignatureUrl = "\\Uploads\\" + registrationDetails.Signature.FileName;
                    }
                    else if (registrationDetails.ProofOfStay != null)
                    {
                        simRegistrationViewModel.Customer.ProofOfStayUrl = "\\Uploads\\" + registrationDetails.ProofOfStay.FileName;
                    }
                    else if (registrationDetails.LastDayOfStaySupportingDocument != null)
                    {
                        simRegistrationViewModel.Customer.LastDayOfStaySupportingDocumentUrl = "\\Uploads\\" + registrationDetails.LastDayOfStaySupportingDocument.FileName;
                    }
                    else if (!string.IsNullOrEmpty(registrationDetails.SignatureBase64))
                    {
                        byte[] bytes = Convert.FromBase64String(registrationDetails.SignatureBase64);

                        Image image;

                        using (MemoryStream ms = new MemoryStream(bytes))
                        {
                            image = Image.FromStream(ms);
                        }

                        var imageToBeSaved = JsonConvert.DeserializeObject<Images>(registrationDetails.SignatureBase64);

                        image.Save(_webHostEnvironment.WebRootPath + "\\Uploads\\" + imageToBeSaved.Type, ImageFormat.Png);

                        simRegistrationViewModel.Customer.SignatureUrl = _webHostEnvironment.WebRootPath + "\\Uploads\\" + imageToBeSaved.Type;
                    }

                    var doesSerialNumberExist = simRegistrationValidators.IsSerialNumberExist(simRegistrationViewModel.SimSerialNumber);

                    if (!doesSerialNumberExist)
                    {
                        ModelState.AddModelError("Error", "Sim details update failed. Serial number " + simRegistrationViewModel.SimSerialNumber + " does not exists.");

                        return BadRequest(ModelState);
                    }

                    if (simRegistrationViewModel.Customer != null)
                    {
                        var doesCustomerMobileNumberExist = simRegistrationValidators.IsCustomerMobileNumberExist(simRegistrationViewModel.Customer.MobileNumber);

                        if (!doesCustomerMobileNumberExist)
                        {
                            ModelState.AddModelError("Error", "Sim registration failed. Customer mobile number " + simRegistrationViewModel.Customer.MobileNumber + " does not exist.");

                            return BadRequest(ModelState);
                        }

                        var doesCustomerIdNumberExist = simRegistrationValidators.IsCustomerIdNumberExist(simRegistrationViewModel.Customer.NationalIdNumber);

                        if (!doesCustomerIdNumberExist)
                        {
                            ModelState.AddModelError("Error", "Sim registration failed. Customer national id number " + simRegistrationViewModel.Customer.NationalIdNumber + " does not exist.");

                            return BadRequest(ModelState);
                        }
                    }

                    if (simRegistrationViewModel.Corporate != null)
                    {
                        if (simRegistrationViewModel.Corporate.Id == 0)
                        {
                            ModelState.AddModelError("Error", "Sim registration failed. Coorporate Id Number " + simRegistrationViewModel.Corporate.Id + " does not exist.");

                            return BadRequest(ModelState);
                        }

                        var doesCorporateMobileNumberExist = simRegistrationValidators.IsCorporateMobileNumberExist(simRegistrationViewModel.Corporate.CorporateMobileNumber);

                        if (!doesCorporateMobileNumberExist)
                        {
                            ModelState.AddModelError("Error", "Sim registration failed. Coorporate mobile number " + simRegistrationViewModel.Corporate.CorporateMobileNumber + " does not exists.");

                            return BadRequest(ModelState);
                        }

                        var doesCorporateIdNumberExist = simRegistrationValidators.IsCorporateIdNumberExist(simRegistrationViewModel.Corporate.CorporateIdNumber);

                        if (doesCorporateIdNumberExist)
                        {
                            ModelState.AddModelError("Error", "Sim registration failed. Coorporate id number " + simRegistrationViewModel.Corporate.CorporateIdNumber + " already exists.");

                            return BadRequest(ModelState);
                        }
                    }
                    else
                    {
                        simRegistrationViewModel.Corporate = null;
                    }

                    if (simRegistrationViewModel.Customer != null)
                    {
                        var customer = _context.Customers.Where(x => x.Id == simRegistrationViewModel.CustomerId).FirstOrDefault();

                        if (customer == null)
                        {
                            ModelState.AddModelError("Error", "Sim details cannot be updated. CustomerId cannot be '0'");

                            return BadRequest(ModelState);
                        }

                        customer.Firstname = simRegistrationViewModel.Customer?.Firstname;
                        customer.Middlename = simRegistrationViewModel.Customer?.Middlename;
                        customer.Lastname = simRegistrationViewModel.Customer?.Lastname;
                        customer.Gender = simRegistrationViewModel.Customer?.Gender;
                        customer.DateOfBirth = simRegistrationViewModel.Customer?.DateOfBirth;
                        customer.NationalityId = simRegistrationViewModel.Customer?.NationalityId;
                        customer.IdType = simRegistrationViewModel.Customer?.IdType;
                        customer.NationalIdNumber = simRegistrationViewModel.Customer?.NationalIdNumber;
                        customer.ProvinceId = simRegistrationViewModel.Customer?.ProvinceId;
                        customer.TownId = simRegistrationViewModel.Customer?.TownId;
                        customer.Area = simRegistrationViewModel.Customer?.Area;
                        customer.Address = simRegistrationViewModel.Customer?.Address;
                        customer.Email = simRegistrationViewModel.Customer?.Email;
                        customer.Occupation = simRegistrationViewModel.Customer?.Occupation;
                        customer.MobileNumber = simRegistrationViewModel.Customer?.MobileNumber;
                        customer.AlternativeMobileNumber = simRegistrationViewModel.Customer?.AlternativeMobileNumber;
                        customer.PlotNumber = simRegistrationViewModel.Customer?.PlotNumber;
                        customer.UnitNumber = simRegistrationViewModel.Customer?.UnitNumber;
                        customer.Village = simRegistrationViewModel.Customer?.Village;
                        customer.Landmark = simRegistrationViewModel.Customer?.Landmark;
                        customer.Road = simRegistrationViewModel.Customer?.Road;
                        customer.Chiefdom = simRegistrationViewModel.Customer?.Chiefdom;
                        customer.Neighborhood = simRegistrationViewModel.Customer?.Neighborhood;
                        customer.Section = simRegistrationViewModel.Customer?.Section;
                        customer.NationalIdFrontUrl = simRegistrationViewModel.Customer?.NationalIdFrontUrl;
                        customer.NationalIdBackUrl = simRegistrationViewModel.Customer?.NationalIdBackUrl;
                        customer.PortraitUrl = simRegistrationViewModel.Customer?.PortraitUrl;
                        customer.SignatureUrl = simRegistrationViewModel.Customer?.SignatureUrl;
                        customer.IsForeigner = simRegistrationViewModel.Customer?.IsForeigner;
                        customer.DateTimeCreated = simRegistrationViewModel.Customer?.DateTimeCreated;
                        customer.DateTimeModified = DateTime.Now;
                        customer.CreatedByUserId = simRegistrationViewModel.Customer?.CreatedByUserId;
                        customer.ModifiedByUserId = simRegistrationViewModel.Customer?.ModifiedByUserId;
                        customer.ProofOfStayUrl = simRegistrationViewModel.Customer?.ProofOfStayUrl;

                        if (customer.ModifiedByUserId == null)
                        {
                            customer.ModifiedByUserId = 3;
                        }

                        var simRegistrationDetailToBeUpdated = _context.SimRegistrationDetails.Where(x => x.Id == id || x.Id == simRegistrationViewModel.Id).FirstOrDefault();

                        simRegistrationDetailToBeUpdated.RegistrationType = simRegistrationViewModel?.RegistrationType;
                        simRegistrationDetailToBeUpdated.CategoryType = simRegistrationViewModel?.CategoryType;
                        simRegistrationDetailToBeUpdated.CustomerId = customer.Id;
                        simRegistrationDetailToBeUpdated.DateTimeCreated = simRegistrationViewModel?.DateTimeCreated;
                        simRegistrationDetailToBeUpdated.DateTimeModified = DateTime.Now;
                        simRegistrationDetailToBeUpdated.CreatedByUserId = simRegistrationViewModel?.CreatedByUserId;
                        simRegistrationDetailToBeUpdated.ModifiedByUserId = simRegistrationViewModel?.ModifiedByUserId;

                        var nextOfKin = _context.NextOfKins.Where(x => x.Id == customer.NextOfKinId).FirstOrDefault();

                        if (nextOfKin == null)
                        {
                            ModelState.AddModelError("Error", "Sim details cannot be updated. CorporateId cannot be '0'");

                            return BadRequest(ModelState);
                        }

                        if (nextOfKin.ModifiedByUserId == 0)
                        {
                            nextOfKin.ModifiedByUserId = 3;
                        }

                        nextOfKin.Firstname = simRegistrationViewModel.Customer?.NextOfKin?.Firstname;
                        nextOfKin.Middlename = simRegistrationViewModel.Customer?.NextOfKin?.Middlename;
                        nextOfKin.Lastname = simRegistrationViewModel.Customer?.NextOfKin?.Lastname;
                        nextOfKin.MobileNumber = simRegistrationViewModel.Customer?.NextOfKin?.MobileNumber;
                        nextOfKin.ModifiedByUserId = simRegistrationViewModel.Customer?.NextOfKin?.ModifiedByUserId;
                        nextOfKin.DateTimeModified = DateTime.Now;

                        _context.Entry(nextOfKin).State = EntityState.Modified;
                        _context.Entry(customer).State = EntityState.Modified;
                        _context.Entry(simRegistrationDetailToBeUpdated).State = EntityState.Modified;
                    }
                    else if (simRegistrationViewModel.Corporate != null)
                    {
                        simRegistrationViewModel.Corporate = null;

                        var corporate = _context.Corporates.Where(x => x.Id == simRegistrationViewModel.CorporateId).FirstOrDefault();

                        if (corporate == null)
                        {
                            ModelState.AddModelError("Error", "Sim details cannot be updated. CorporateId cannot be '0'");

                            return BadRequest(ModelState);
                        }

                        corporate.CorporateIdNumber = simRegistrationViewModel.Corporate?.CorporateIdNumber;
                        corporate.CertificateUrl = simRegistrationViewModel.Corporate?.CertificateUrl;
                        corporate.LetterUrl = simRegistrationViewModel.Corporate?.LetterUrl;
                        corporate.BatchUrl = simRegistrationViewModel.Corporate?.BatchUrl;
                        corporate.RepresantativeId = simRegistrationViewModel.Corporate?.RepresantativeId;
                        corporate.CompanyName = simRegistrationViewModel.Corporate?.CompanyName;
                        corporate.CompanyCoinumber = simRegistrationViewModel.Corporate?.CompanyCoinumber;
                        corporate.RegistrationDate = simRegistrationViewModel.Corporate?.RegistrationDate;
                        corporate.ProvinceId = simRegistrationViewModel.Corporate?.ProvinceId;
                        corporate.TownId = simRegistrationViewModel.Corporate?.TownId;
                        corporate.Address = simRegistrationViewModel.Corporate?.Address;
                        corporate.CompanyEmail = simRegistrationViewModel.Corporate?.CompanyEmail;
                        corporate.AlternativeMobileNumber = simRegistrationViewModel.Corporate?.AlternativeMobileNumber;
                        corporate.CorporateMobileNumber = simRegistrationViewModel.Corporate?.CorporateMobileNumber;
                        corporate.DateTimeCreated = simRegistrationViewModel.Corporate?.DateTimeCreated;
                        corporate.DateTimeModified = simRegistrationViewModel.Corporate?.DateTimeModified;
                        corporate.CreatedByUserId = simRegistrationViewModel.Corporate?.CreatedByUserId;
                        corporate.ModifiedByUserId = simRegistrationViewModel.Corporate?.ModifiedByUserId;
                        corporate.IsCompanyGo = simRegistrationViewModel.Corporate?.IsCompanyGo;

                        if (corporate.ModifiedByUserId == null)
                        {
                            corporate.ModifiedByUserId = 3;
                        }

                        var representative = _context.Representatives.Where(x => x.Id == corporate.RepresantativeId).FirstOrDefault();

                        if (representative != null)
                        {
                            representative.Firstname = simRegistrationViewModel.Corporate?.Representative?.Firstname;
                            representative.Middlename = simRegistrationViewModel.Corporate?.Representative?.Middlename;
                            representative.Lastname = simRegistrationViewModel.Corporate?.Representative?.Lastname;
                            representative.IdType = simRegistrationViewModel.Corporate?.Representative?.IdType;
                            representative.NationalIdNumber = simRegistrationViewModel.Corporate?.Representative?.NationalIdNumber;
                            representative.InvitingEntity = simRegistrationViewModel.Corporate?.Representative?.InvitingEntity;
                            representative.PortraitUrl = simRegistrationViewModel.Corporate?.Representative?.PortraitUrl;
                            representative.NationalIdFrontUrl = simRegistrationViewModel.Corporate?.Representative?.NationalIdFrontUrl;
                            representative.NationalIdBackUrl = simRegistrationViewModel.Corporate?.Representative?.NationalIdBackUrl;
                            representative.SignatureUrl = simRegistrationViewModel.Corporate?.Representative?.SignatureUrl;
                            representative.DateTimeCreated = simRegistrationViewModel.Corporate?.Representative?.DateTimeCreated;
                            representative.DateTimeModified = simRegistrationViewModel.Corporate?.Representative?.DateTimeModified;
                            representative.CreatedByUserId = simRegistrationViewModel.Corporate?.Representative?.CreatedByUserId;
                            representative.ModifiedByUserId = simRegistrationViewModel.Corporate?.Representative?.ModifiedByUserId;

                            var simRegistrationDetailToBeUpdated = _context.SimRegistrationDetails.Where(x => x.Id == id || x.Id == simRegistrationViewModel.Id).FirstOrDefault();

                            simRegistrationDetailToBeUpdated.RegistrationType = simRegistrationViewModel?.RegistrationType;
                            simRegistrationDetailToBeUpdated.CategoryType = simRegistrationViewModel?.CategoryType;
                            simRegistrationDetailToBeUpdated.CorporateId = corporate.Id;
                            simRegistrationDetailToBeUpdated.DateTimeCreated = simRegistrationViewModel?.DateTimeCreated;
                            simRegistrationDetailToBeUpdated.DateTimeModified = DateTime.Now;
                            simRegistrationDetailToBeUpdated.CreatedByUserId = simRegistrationViewModel?.CreatedByUserId;
                            simRegistrationDetailToBeUpdated.ModifiedByUserId = simRegistrationViewModel?.ModifiedByUserId;

                            _context.Entry(representative).State = EntityState.Modified;
                            _context.Entry(corporate).State = EntityState.Modified;
                            _context.Entry(simRegistrationDetailToBeUpdated).State = EntityState.Modified;
                        }
                        else
                        {
                            ModelState.AddModelError("Error", "Sim details cannot be updated. RepresentativeId cannot be '0'");

                            return BadRequest(ModelState);
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("Error", "There has been a fatal error in updating your sim registration details");

                        return BadRequest(ModelState);
                    }

                    try
                    {
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!SimRegistrationDetailExists(id))
                        {
                            ModelState.AddModelError("Error", "Sim details cannot be updated. Back Ofiice Agent Id does not exist");

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

                    return BadRequest(ModelState);
                }
            }
            else
            {
                ModelState.AddModelError("Error", "Images required");

                return BadRequest(ModelState);
            }            
        }

        // POST: api/SimRegistrationDetails
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<SimRegistrationViewModel>> PostSimRegistrationDetail([FromForm] SimRegistration registrationDetails)
        {
            SimRegistrationValidators simRegistrationValidators = new SimRegistrationValidators(_context);

            var globalCustomer = new Customer();
            var globalCorporate = new Corporate();
            var globalRepresentative = new Representative();
            var globalNextOfKin = new NextOfKin();
            var simRegistrationFiles = new List<IFormFile>();
            
            var simRegistrationViewModel = JsonConvert.DeserializeObject<SimRegistrationViewModel>(registrationDetails.SimRegistrationDetails);

            if (registrationDetails.Portrait.Length > 0 && registrationDetails.NationalIdFront.Length > 0 && registrationDetails.NationalIdBack.Length > 0)
            {                
                simRegistrationFiles.Add(registrationDetails.Portrait);
                simRegistrationFiles.Add(registrationDetails.NationalIdFront);
                simRegistrationFiles.Add(registrationDetails.NationalIdBack);

                if (registrationDetails.Signature.Length > 0)
                {
                    simRegistrationFiles.Add(registrationDetails.Signature);
                }

                if (registrationDetails.ProofOfStay.Length > 0)
                {
                    simRegistrationFiles.Add(registrationDetails.ProofOfStay);
                }

                if (registrationDetails.LastDayOfStaySupportingDocument.Length > 0)
                {
                    simRegistrationFiles.Add(registrationDetails.LastDayOfStaySupportingDocument);
                }

                try
                {
                    if (!Directory.Exists(_webHostEnvironment.WebRootPath + "\\Uploads\\"))
                    {
                        Directory.CreateDirectory(_webHostEnvironment.WebRootPath + "\\Uploads\\");
                    }

                    foreach (var item in simRegistrationFiles)
                    {
                        using (FileStream fileStream = System.IO.File.Create(_webHostEnvironment.WebRootPath + "\\Uploads\\" + item.FileName))
                        {
                            item.CopyTo(fileStream);
                            fileStream.Flush();
                        }
                    }

                    if (registrationDetails.Portrait != null)
                    {
                        simRegistrationViewModel.Customer.PortraitUrl = "\\Uploads\\" + registrationDetails.Portrait.FileName;
                    }
                    else if (registrationDetails.NationalIdFront != null)
                    {
                        simRegistrationViewModel.Customer.NationalIdFrontUrl = "\\Uploads\\" + registrationDetails.NationalIdFront.FileName;
                    }
                    else if (registrationDetails.NationalIdBack != null)
                    {
                        simRegistrationViewModel.Customer.NationalIdBackUrl = "\\Uploads\\" + registrationDetails.NationalIdBack.FileName;
                    }
                    else if (registrationDetails.Signature != null)
                    {
                        simRegistrationViewModel.Customer.SignatureUrl = "\\Uploads\\" + registrationDetails.Signature.FileName;
                    }
                    else if (registrationDetails.ProofOfStay != null)
                    {
                        simRegistrationViewModel.Customer.ProofOfStayUrl = "\\Uploads\\" + registrationDetails.ProofOfStay.FileName;
                    }
                    else if (registrationDetails.LastDayOfStaySupportingDocument != null)
                    {
                        simRegistrationViewModel.Customer.LastDayOfStaySupportingDocumentUrl = "\\Uploads\\" + registrationDetails.LastDayOfStaySupportingDocument.FileName;
                    }
                    else if (!string.IsNullOrEmpty(registrationDetails.SignatureBase64))
                    {
                        byte[] bytes = Convert.FromBase64String(registrationDetails.SignatureBase64);

                        Image image;

                        using (MemoryStream ms = new MemoryStream(bytes))
                        {
                            image = Image.FromStream(ms);
                        }

                        var imageToBeSaved = JsonConvert.DeserializeObject<Images>(registrationDetails.SignatureBase64);

                        image.Save(_webHostEnvironment.WebRootPath + "\\Uploads\\" + imageToBeSaved.Type, ImageFormat.Png);

                        simRegistrationViewModel.Customer.SignatureUrl = _webHostEnvironment.WebRootPath + "\\Uploads\\" + imageToBeSaved.Type;                       
                    }

                    #region Actual Upload

                    var doesSerialNumberExist = simRegistrationValidators.IsSerialNumberExist(simRegistrationViewModel.SimSerialNumber);

                    if (doesSerialNumberExist)
                    {
                        ModelState.AddModelError("Error", "Sim registration failed. Serial number " + simRegistrationViewModel.SimSerialNumber + " already exists.");

                        return BadRequest(ModelState);
                    }

                    if (simRegistrationViewModel.Customer != null)
                    {
                        var doesNextOfKinMobileNumberExist = simRegistrationValidators.IsNextOfKinMobileNumberExist(simRegistrationViewModel.Customer.NextOfKin.MobileNumber);

                        if (doesNextOfKinMobileNumberExist)
                        {
                            globalNextOfKin = _context.NextOfKins.Where(x => x.MobileNumber == simRegistrationViewModel.Customer.NextOfKin.MobileNumber).FirstOrDefault();
                        }

                        var doesCustomerMobileNumberExist = simRegistrationValidators.IsCustomerMobileNumberExist(simRegistrationViewModel.Customer.MobileNumber);

                        if (doesCustomerMobileNumberExist)
                        {
                            ModelState.AddModelError("Error", "Sim registration failed. Customer mobile number " + simRegistrationViewModel.Customer.MobileNumber + " already exists.");

                            return BadRequest(ModelState);
                        }

                        var doesCustomerIdNumberExist = simRegistrationValidators.IsCustomerIdNumberExist(simRegistrationViewModel.Customer.NationalIdNumber);

                        if (doesCustomerIdNumberExist)
                        {
                            ModelState.AddModelError("Error", "Sim registration failed. Customer national id number " + simRegistrationViewModel.Customer.NationalIdNumber + " already exists.");

                            return BadRequest(ModelState);
                        }
                    }

                    if (simRegistrationViewModel.Corporate != null)
                    {
                        var doesCorporateMobileNumberExist = simRegistrationValidators.IsCorporateMobileNumberExist(simRegistrationViewModel.Corporate.CorporateMobileNumber);

                        if (doesCorporateMobileNumberExist)
                        {
                            ModelState.AddModelError("Error", "Sim registration failed. Coorporate mobile number " + simRegistrationViewModel.Corporate.CorporateMobileNumber + " already exists.");

                            return BadRequest(ModelState);
                        }

                        var doesCorporateIdNumberExist = simRegistrationValidators.IsCorporateIdNumberExist(simRegistrationViewModel.Corporate.CorporateIdNumber);

                        if (doesCorporateIdNumberExist)
                        {
                            ModelState.AddModelError("Error", "Sim registration failed. Coorporate id number " + simRegistrationViewModel.Corporate.CorporateIdNumber + " already exists.");

                            return BadRequest(ModelState);
                        }
                    }
                    else if (simRegistrationViewModel.Customer != null)
                    {
                        if (globalNextOfKin.Id == 0)
                        {
                            if (simRegistrationViewModel.Customer.NextOfKin?.CreatedByUserId == 0)
                            {
                                simRegistrationViewModel.Customer.NextOfKin.CreatedByUserId = 3;
                            }

                            var nextOfKin = new NextOfKin()
                            {
                                Firstname = simRegistrationViewModel.Customer?.NextOfKin?.Firstname,
                                Middlename = simRegistrationViewModel.Customer?.NextOfKin?.Middlename,
                                Lastname = simRegistrationViewModel.Customer?.NextOfKin?.Lastname,
                                MobileNumber = simRegistrationViewModel.Customer?.NextOfKin?.MobileNumber,
                                CreatedByUserId = simRegistrationViewModel.Customer?.NextOfKin?.CreatedByUserId,
                                DateTimeCreated = DateTime.Now
                            };

                            _context.NextOfKins.Add(nextOfKin);
                            await _context.SaveChangesAsync();

                            var lastAddedNextOfKin = _context.NextOfKins.OrderByDescending(x => x.Id).FirstOrDefault();

                            var customer = new Customer()
                            {
                                Firstname = simRegistrationViewModel.Customer?.Firstname,
                                Middlename = simRegistrationViewModel.Customer?.Middlename,
                                Lastname = simRegistrationViewModel.Customer?.Lastname,
                                Gender = simRegistrationViewModel.Customer?.Gender,
                                DateOfBirth = simRegistrationViewModel.Customer?.DateOfBirth,
                                NationalityId = simRegistrationViewModel.Customer?.NationalityId,
                                IdType = simRegistrationViewModel.Customer?.IdType,
                                NationalIdNumber = simRegistrationViewModel.Customer?.NationalIdNumber,
                                ProvinceId = simRegistrationViewModel.Customer?.ProvinceId,
                                TownId = simRegistrationViewModel.Customer?.TownId,
                                Area = simRegistrationViewModel.Customer?.Area,
                                Address = simRegistrationViewModel.Customer?.Address,
                                Email = simRegistrationViewModel.Customer?.Email,
                                Occupation = simRegistrationViewModel.Customer?.Occupation,
                                MobileNumber = simRegistrationViewModel.Customer?.MobileNumber,
                                AlternativeMobileNumber = simRegistrationViewModel.Customer?.AlternativeMobileNumber,
                                PlotNumber = simRegistrationViewModel.Customer?.PlotNumber,
                                UnitNumber = simRegistrationViewModel.Customer?.UnitNumber,
                                Village = simRegistrationViewModel.Customer?.Village,
                                Landmark = simRegistrationViewModel.Customer?.Landmark,
                                Road = simRegistrationViewModel.Customer?.Road,
                                Chiefdom = simRegistrationViewModel.Customer?.Chiefdom,
                                Neighborhood = simRegistrationViewModel.Customer?.Neighborhood,
                                Section = simRegistrationViewModel.Customer?.Section,
                                NationalIdFrontUrl = simRegistrationViewModel.Customer?.NationalIdFrontUrl,
                                NationalIdBackUrl = simRegistrationViewModel.Customer?.NationalIdBackUrl,
                                PortraitUrl = simRegistrationViewModel.Customer?.PortraitUrl,
                                SignatureUrl = simRegistrationViewModel.Customer?.SignatureUrl,
                                IsForeigner = simRegistrationViewModel.Customer?.IsForeigner,
                                ProofOfStayUrl = simRegistrationViewModel.Customer?.ProofOfStayUrl,
                                CreatedByUserId = simRegistrationViewModel.Customer?.CreatedByUserId,
                                DateTimeCreated = DateTime.Now,
                                NextOfKinId = lastAddedNextOfKin?.Id
                            };

                            if (customer.CreatedByUserId == 0)
                            {
                                customer.CreatedByUserId = 3;
                            }

                            _context.Customers.Add(customer);
                            await _context.SaveChangesAsync();
                        }
                        else
                        {
                            var customer = new Customer()
                            {
                                Firstname = simRegistrationViewModel.Customer?.Firstname,
                                Middlename = simRegistrationViewModel.Customer?.Middlename,
                                Lastname = simRegistrationViewModel.Customer?.Lastname,
                                Gender = simRegistrationViewModel.Customer?.Gender,
                                DateOfBirth = simRegistrationViewModel.Customer?.DateOfBirth,
                                NationalityId = simRegistrationViewModel.Customer?.NationalityId,
                                IdType = simRegistrationViewModel.Customer?.IdType,
                                NationalIdNumber = simRegistrationViewModel.Customer?.NationalIdNumber,
                                ProvinceId = simRegistrationViewModel.Customer?.ProvinceId,
                                TownId = simRegistrationViewModel.Customer?.TownId,
                                Area = simRegistrationViewModel.Customer?.Area,
                                Address = simRegistrationViewModel.Customer?.Address,
                                Email = simRegistrationViewModel.Customer?.Email,
                                Occupation = simRegistrationViewModel.Customer?.Occupation,
                                MobileNumber = simRegistrationViewModel.Customer?.MobileNumber,
                                AlternativeMobileNumber = simRegistrationViewModel.Customer?.AlternativeMobileNumber,
                                PlotNumber = simRegistrationViewModel.Customer?.PlotNumber,
                                UnitNumber = simRegistrationViewModel.Customer?.UnitNumber,
                                Village = simRegistrationViewModel.Customer?.Village,
                                Landmark = simRegistrationViewModel.Customer?.Landmark,
                                Road = simRegistrationViewModel.Customer?.Road,
                                Chiefdom = simRegistrationViewModel.Customer?.Chiefdom,
                                Neighborhood = simRegistrationViewModel.Customer?.Neighborhood,
                                Section = simRegistrationViewModel.Customer?.Section,
                                NationalIdFrontUrl = simRegistrationViewModel.Customer?.NationalIdFrontUrl,
                                NationalIdBackUrl = simRegistrationViewModel.Customer?.NationalIdBackUrl,
                                PortraitUrl = simRegistrationViewModel.Customer?.PortraitUrl,
                                SignatureUrl = simRegistrationViewModel.Customer?.SignatureUrl,
                                IsForeigner = simRegistrationViewModel.Customer?.IsForeigner,
                                ProofOfStayUrl = simRegistrationViewModel.Customer?.ProofOfStayUrl,
                                CreatedByUserId = simRegistrationViewModel.Customer?.CreatedByUserId,
                                DateTimeCreated = DateTime.Now,
                                NextOfKinId = globalNextOfKin?.Id
                            };

                            if (customer.CreatedByUserId == 0)
                            {
                                customer.CreatedByUserId = 3;
                            }

                            _context.Customers.Add(customer);
                            await _context.SaveChangesAsync();
                        }
                    }
                    else if (simRegistrationViewModel.Corporate != null)
                    {
                        var representative = new Representative()
                        {
                            Firstname = simRegistrationViewModel.Corporate?.Representative?.Firstname,
                            Middlename = simRegistrationViewModel.Corporate?.Representative?.Middlename,
                            Lastname = simRegistrationViewModel.Corporate?.Representative?.Lastname,
                            IdType = simRegistrationViewModel.Corporate?.Representative?.IdType,
                            NationalIdNumber = simRegistrationViewModel.Corporate?.Representative?.NationalIdNumber,
                            InvitingEntity = simRegistrationViewModel.Corporate?.Representative?.InvitingEntity,
                            PortraitUrl = simRegistrationViewModel.Corporate?.Representative?.PortraitUrl,
                            NationalIdFrontUrl = simRegistrationViewModel.Corporate?.Representative?.NationalIdFrontUrl,
                            NationalIdBackUrl = simRegistrationViewModel.Corporate?.Representative?.NationalIdBackUrl,
                            SignatureUrl = simRegistrationViewModel.Corporate?.Representative?.SignatureUrl,
                            DateTimeCreated = simRegistrationViewModel.Corporate?.Representative?.DateTimeCreated,
                            DateTimeModified = simRegistrationViewModel.Corporate?.Representative?.DateTimeModified,
                            CreatedByUserId = simRegistrationViewModel.Corporate?.Representative?.CreatedByUserId,
                            ModifiedByUserId = simRegistrationViewModel.Corporate?.Representative?.ModifiedByUserId
                        };

                        if (representative.CreatedByUserId == 0)
                        {
                            representative.CreatedByUserId = 3;
                        }

                        _context.Representatives.Add(representative);
                        await _context.SaveChangesAsync();

                        var lastRepRecord = _context.Representatives.OrderByDescending(x => x.Id).FirstOrDefault();

                        var corporate = new Corporate()
                        {
                            CorporateIdNumber = simRegistrationViewModel.Corporate?.CorporateIdNumber,
                            CertificateUrl = simRegistrationViewModel.Corporate?.CertificateUrl,
                            LetterUrl = simRegistrationViewModel.Corporate?.LetterUrl,
                            BatchUrl = simRegistrationViewModel.Corporate?.BatchUrl,
                            RepresantativeId = lastRepRecord?.Id,
                            CompanyName = simRegistrationViewModel.Corporate?.CompanyName,
                            CompanyCoinumber = simRegistrationViewModel.Corporate?.CompanyCoinumber,
                            RegistrationDate = simRegistrationViewModel.Corporate?.RegistrationDate,
                            ProvinceId = simRegistrationViewModel.Corporate?.ProvinceId,
                            TownId = simRegistrationViewModel.Corporate?.TownId,
                            Address = simRegistrationViewModel.Corporate?.Address,
                            CompanyEmail = simRegistrationViewModel.Corporate?.CompanyEmail,
                            AlternativeMobileNumber = simRegistrationViewModel.Corporate?.AlternativeMobileNumber,
                            CorporateMobileNumber = simRegistrationViewModel.Corporate?.CorporateMobileNumber,
                            DateTimeCreated = simRegistrationViewModel.Corporate?.DateTimeCreated,
                            DateTimeModified = simRegistrationViewModel.Corporate?.DateTimeModified,
                            CreatedByUserId = simRegistrationViewModel.Corporate?.CreatedByUserId,
                            ModifiedByUserId = simRegistrationViewModel.Corporate?.ModifiedByUserId,
                            IsCompanyGo = simRegistrationViewModel.Corporate?.IsCompanyGo
                        };

                        if (corporate.CreatedByUserId == 0)
                        {
                            corporate.CreatedByUserId = 3;
                        }

                        _context.Corporates.Add(corporate);
                        await _context.SaveChangesAsync();
                    }

                    var lastAddedCustomer = _context.Customers.OrderByDescending(x => x.Id).FirstOrDefault();
                    var lastAddedNextofKin = new NextOfKin();

                    if (lastAddedCustomer != null)
                    {
                        lastAddedNextofKin = _context.NextOfKins.OrderByDescending(x => x.Id).FirstOrDefault();
                    }
                    else
                    {
                        lastAddedNextofKin = null;
                    }

                    var lastAddedCorporate = _context.Corporates.OrderByDescending(x => x.Id).FirstOrDefault();
                    var lastAddedRepresentative = new Representative();

                    if (lastAddedCorporate != null)
                    {
                        lastAddedRepresentative = _context.Representatives.OrderByDescending(x => x.Id).FirstOrDefault();
                    }
                    else
                    {
                        lastAddedRepresentative = null;
                    }

                    if (simRegistrationViewModel.CreatedByUserId == 0)
                    {
                        simRegistrationViewModel.CreatedByUserId = 3;
                    }

                    if (lastAddedCustomer != null && lastAddedNextofKin != null)
                    {
                        var simRegistrationDetail = new SimRegistrationDetail()
                        {
                            RegistrationType = simRegistrationViewModel.RegistrationType,
                            CategoryType = simRegistrationViewModel.CategoryType,
                            SimSerialNumber = simRegistrationViewModel.SimSerialNumber,
                            CustomerId = lastAddedCustomer.Id,
                            DateTimeCreated = DateTime.Now,
                            CreatedByUserId = simRegistrationViewModel.CreatedByUserId
                        };

                        _context.SimRegistrationDetails.Add(simRegistrationDetail);
                        await _context.SaveChangesAsync();

                        var lastAddedSimRecord = _context.SimRegistrationDetails.OrderByDescending(x => x.Id).FirstOrDefault();

                        var simRegistrationViewModel1 = new SimRegistrationViewModel()
                        {
                            Id = lastAddedSimRecord.Id,
                            RegistrationType = lastAddedSimRecord.RegistrationType,
                            CategoryType = lastAddedSimRecord.CategoryType,
                            SimSerialNumber = lastAddedSimRecord.SimSerialNumber,
                            CustomerId = lastAddedCustomer.Id,
                            CreatedByUserId = lastAddedSimRecord.CreatedByUserId,
                            ModifiedByUserId = lastAddedSimRecord.ModifiedByUserId,
                            DateTimeCreated = lastAddedSimRecord.DateTimeCreated,
                            Customer = new CustomerViewModel()
                            {
                                Firstname = lastAddedCustomer?.Firstname,
                                Middlename = lastAddedCustomer?.Middlename,
                                Lastname = lastAddedCustomer?.Lastname,
                                Gender = lastAddedCustomer?.Gender,
                                DateOfBirth = lastAddedCustomer?.DateOfBirth,
                                NationalityId = lastAddedCustomer?.NationalityId,
                                IdType = lastAddedCustomer?.IdType,
                                NationalIdNumber = lastAddedCustomer?.NationalIdNumber,
                                ProvinceId = lastAddedCustomer?.ProvinceId,
                                TownId = lastAddedCustomer?.TownId,
                                Area = lastAddedCustomer?.Area,
                                Address = lastAddedCustomer?.Address,
                                Email = lastAddedCustomer?.Email,
                                Occupation = lastAddedCustomer?.Occupation,
                                MobileNumber = lastAddedCustomer?.MobileNumber,
                                AlternativeMobileNumber = lastAddedCustomer?.AlternativeMobileNumber,
                                PlotNumber = lastAddedCustomer?.PlotNumber,
                                UnitNumber = lastAddedCustomer?.UnitNumber,
                                Village = lastAddedCustomer?.Village,
                                Landmark = lastAddedCustomer?.Landmark,
                                Road = lastAddedCustomer?.Road,
                                Chiefdom = lastAddedCustomer?.Chiefdom,
                                Neighborhood = lastAddedCustomer?.Neighborhood,
                                Section = lastAddedCustomer?.Section,
                                NationalIdFrontUrl = lastAddedCustomer?.NationalIdFrontUrl,
                                NationalIdBackUrl = lastAddedCustomer.NationalIdBackUrl,
                                PortraitUrl = lastAddedCustomer?.PortraitUrl,
                                SignatureUrl = lastAddedCustomer?.SignatureUrl,
                                IsForeigner = lastAddedCustomer?.IsForeigner,
                                ProofOfStayUrl = lastAddedCustomer?.ProofOfStayUrl,
                                CreatedByUserId = lastAddedCustomer?.CreatedByUserId,
                                DateTimeCreated = lastAddedCustomer?.DateTimeCreated,
                                NextOfKin = new NextOfKin()
                                {
                                    Firstname = lastAddedNextofKin?.Firstname,
                                    Middlename = lastAddedNextofKin?.Middlename,
                                    Lastname = lastAddedNextofKin?.Lastname,
                                    MobileNumber = lastAddedNextofKin?.MobileNumber,
                                    CreatedByUserId = lastAddedNextofKin?.CreatedByUserId,
                                    DateTimeCreated = lastAddedNextofKin?.DateTimeCreated
                                }
                            }
                        };

                        return CreatedAtAction("GetSimRegistrationDetail", new { id = simRegistrationDetail.Id }, simRegistrationViewModel1);
                    }
                    else if (lastAddedCustomer != null && globalNextOfKin != null)
                    {
                        var simRegistrationDetail = new SimRegistrationDetail()
                        {
                            RegistrationType = simRegistrationViewModel.RegistrationType,
                            CategoryType = simRegistrationViewModel.CategoryType,
                            SimSerialNumber = simRegistrationViewModel.SimSerialNumber,
                            CustomerId = lastAddedCustomer.Id,
                            DateTimeCreated = DateTime.Now,
                            CreatedByUserId = simRegistrationViewModel.CreatedByUserId
                        };

                        _context.SimRegistrationDetails.Add(simRegistrationDetail);
                        await _context.SaveChangesAsync();

                        var lastAddedSimRecord = _context.SimRegistrationDetails.OrderByDescending(x => x.Id).FirstOrDefault();

                        var simRegistrationViewModel1 = new SimRegistrationViewModel()
                        {
                            Id = lastAddedSimRecord.Id,
                            RegistrationType = lastAddedSimRecord?.RegistrationType,
                            CategoryType = lastAddedSimRecord?.CategoryType,
                            SimSerialNumber = lastAddedSimRecord?.SimSerialNumber,
                            CustomerId = lastAddedCustomer?.Id,
                            CreatedByUserId = lastAddedSimRecord?.CreatedByUserId,
                            ModifiedByUserId = lastAddedSimRecord?.ModifiedByUserId,
                            DateTimeCreated = lastAddedCustomer?.DateTimeCreated,
                            Customer = new CustomerViewModel()
                            {
                                Firstname = lastAddedCustomer?.Firstname,
                                Middlename = lastAddedCustomer?.Middlename,
                                Lastname = lastAddedCustomer?.Lastname,
                                Gender = lastAddedCustomer?.Gender,
                                DateOfBirth = lastAddedCustomer?.DateOfBirth,
                                NationalityId = lastAddedCustomer?.NationalityId,
                                IdType = lastAddedCustomer?.IdType,
                                NationalIdNumber = lastAddedCustomer?.NationalIdNumber,
                                ProvinceId = lastAddedCustomer?.ProvinceId,
                                TownId = lastAddedCustomer?.TownId,
                                Area = lastAddedCustomer?.Area,
                                Address = lastAddedCustomer?.Address,
                                Email = lastAddedCustomer?.Email,
                                Occupation = lastAddedCustomer?.Occupation,
                                MobileNumber = lastAddedCustomer?.MobileNumber,
                                AlternativeMobileNumber = lastAddedCustomer?.AlternativeMobileNumber,
                                PlotNumber = lastAddedCustomer?.PlotNumber,
                                UnitNumber = lastAddedCustomer?.UnitNumber,
                                Village = lastAddedCustomer?.Village,
                                Landmark = lastAddedCustomer?.Landmark,
                                Road = lastAddedCustomer?.Road,
                                Chiefdom = lastAddedCustomer?.Chiefdom,
                                Neighborhood = lastAddedCustomer?.Neighborhood,
                                Section = lastAddedCustomer?.Section,
                                NationalIdFrontUrl = lastAddedCustomer?.NationalIdFrontUrl,
                                NationalIdBackUrl = lastAddedCustomer?.NationalIdBackUrl,
                                PortraitUrl = lastAddedCustomer?.PortraitUrl,
                                SignatureUrl = lastAddedCustomer?.SignatureUrl,
                                IsForeigner = lastAddedCustomer?.IsForeigner,
                                ProofOfStayUrl = lastAddedCustomer?.ProofOfStayUrl,
                                CreatedByUserId = lastAddedCustomer?.CreatedByUserId,
                                DateTimeCreated = lastAddedCustomer?.DateTimeCreated,
                                NextOfKin = new NextOfKin()
                                {
                                    Firstname = globalNextOfKin?.Firstname,
                                    Middlename = globalNextOfKin?.Middlename,
                                    Lastname = globalNextOfKin?.Lastname,
                                    MobileNumber = globalNextOfKin?.MobileNumber,
                                    CreatedByUserId = globalNextOfKin?.CreatedByUserId,
                                    DateTimeCreated = globalNextOfKin?.DateTimeCreated
                                }
                            }
                        };

                        return CreatedAtAction("GetSimRegistrationDetail", new { id = simRegistrationDetail.Id }, simRegistrationViewModel1);
                    }
                    else if (lastAddedCorporate != null)
                    {
                        var simRegistrationDetail = new SimRegistrationDetail()
                        {
                            RegistrationType = simRegistrationViewModel.RegistrationType,
                            CategoryType = simRegistrationViewModel.CategoryType,
                            SimSerialNumber = simRegistrationViewModel.SimSerialNumber,
                            CorporateId = lastAddedCorporate.Id,
                            DateTimeCreated = DateTime.Now,
                            CreatedByUserId = simRegistrationViewModel.CreatedByUserId
                        };

                        _context.SimRegistrationDetails.Add(simRegistrationDetail);
                        await _context.SaveChangesAsync();

                        var lastAddedSimRecord = _context.SimRegistrationDetails.OrderByDescending(x => x.Id).FirstOrDefault();

                        var simRegistrationViewModel1 = new SimRegistrationViewModel()
                        {
                            Id = lastAddedSimRecord.Id,
                            RegistrationType = lastAddedSimRecord?.RegistrationType,
                            CategoryType = lastAddedSimRecord?.CategoryType,
                            SimSerialNumber = lastAddedSimRecord?.SimSerialNumber,
                            CorporateId = lastAddedCorporate?.Id,
                            DateTimeCreated = lastAddedCustomer?.DateTimeCreated,
                            Corporate = new CorporateViewModel()
                            {
                                CorporateIdNumber = lastAddedCorporate?.CorporateIdNumber,
                                CertificateUrl = lastAddedCorporate?.CertificateUrl,
                                LetterUrl = lastAddedCorporate?.LetterUrl,
                                BatchUrl = lastAddedCorporate?.BatchUrl,
                                RepresantativeId = lastAddedCorporate?.RepresantativeId,
                                CompanyName = lastAddedCorporate?.CompanyName,
                                CompanyCoinumber = lastAddedCorporate?.CompanyCoinumber,
                                RegistrationDate = lastAddedCorporate?.RegistrationDate,
                                ProvinceId = lastAddedCorporate?.ProvinceId,
                                TownId = lastAddedCorporate?.TownId,
                                Address = lastAddedCorporate?.Address,
                                CompanyEmail = lastAddedCorporate?.CompanyEmail,
                                AlternativeMobileNumber = lastAddedCorporate?.AlternativeMobileNumber,
                                CorporateMobileNumber = lastAddedCorporate?.CorporateMobileNumber,
                                DateTimeCreated = lastAddedCorporate?.DateTimeCreated,
                                DateTimeModified = lastAddedCorporate?.DateTimeModified,
                                CreatedByUserId = lastAddedCorporate?.CreatedByUserId,
                                ModifiedByUserId = lastAddedCorporate?.ModifiedByUserId,
                                IsCompanyGo = lastAddedCorporate?.IsCompanyGo,
                                Representative = lastAddedRepresentative
                            }
                        };

                        return CreatedAtAction("GetSimRegistrationDetail", new { id = simRegistrationDetail.Id }, simRegistrationViewModel1);
                    }
                    else
                    {
                        ModelState.AddModelError("Error", "There has been a fatal error in saving your sim registration details");

                        return BadRequest(ModelState);
                    }

                    #endregion

                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("Error", ex.ToString());

                    return BadRequest(ModelState);
                }
            }
            else
            {
                ModelState.AddModelError("Error", "Images required");

                return BadRequest(ModelState);
            }            
        }

        // DELETE: api/SimRegistrationDetails/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<SimRegistrationViewModel>> DeleteSimRegistrationDetail(int id)
        {
            var customer = new Customer();
            var corporate = new Corporate();
            var representative = new Representative();
            var nextOfKin = new NextOfKin();

            var simRegistrationDetail = _context.SimRegistrationDetails.Where(x => x.Id == id).FirstOrDefault();

            if (simRegistrationDetail == null)
            {
                ModelState.AddModelError("Error", "Sim Registreation Record cannot be deleted. Sim Registration Id passed does not exist");

                return NotFound(ModelState);
            }

            customer = _context.Customers.Where(x => x.Id == simRegistrationDetail.CustomerId).FirstOrDefault();

            if (customer == null)
            {
                if (customer.Id == 0)
                {
                    ModelState.AddModelError("Error", "Sim Registration Record cannot be deleted. Either Customer Id cannot be 0");

                    return NotFound(ModelState);
                }

                customer = new Customer();                

                corporate = _context.Corporates.Where(x => x.Id == simRegistrationDetail.CorporateId).FirstOrDefault();

                if (corporate == null)
                {
                    ModelState.AddModelError("Error", "Sim Registration Record cannot be deleted. Either Customer or Corporate Id does not exist");

                    return NotFound(ModelState);
                }
                else
                {
                    representative = _context.Representatives.Where(x => x.Id == corporate.RepresantativeId).FirstOrDefault();

                    if (representative != null)
                    {
                        _context.Representatives.Remove(representative);
                    }
                    else
                    {
                        ModelState.AddModelError("Error", "Sim Registration Record cannot be deleted. Representative Id does not exist");

                        return NotFound(ModelState);
                    }

                    _context.Corporates.Remove(corporate);
                }
            }
            else
            {
                corporate = new Corporate();
                representative = new Representative();

                nextOfKin = _context.NextOfKins.Where(x => x.Id == customer.NextOfKinId).FirstOrDefault();

                //if (nextOfKin != null)
                //{
                //    _context.NextOfKins.Remove(nextOfKin);
                //}

                _context.Customers.Remove(customer);
            }

            _context.SimRegistrationDetails.Remove(simRegistrationDetail);
            await _context.SaveChangesAsync();

            var simRegistrationViewModel = new SimRegistrationViewModel()
            {
                Id = simRegistrationDetail.Id,
                RegistrationType = simRegistrationDetail.RegistrationType,
                CategoryType = simRegistrationDetail.CategoryType,
                SimSerialNumber = simRegistrationDetail.SimSerialNumber,
                CustomerId = simRegistrationDetail.CustomerId,
                CorporateId = simRegistrationDetail.CorporateId,
                DateTimeCreated = simRegistrationDetail.DateTimeCreated,
                DateTimeModified = simRegistrationDetail.DateTimeModified,
                CreatedByUserId = simRegistrationDetail.CreatedByUserId,
                ModifiedByUserId = simRegistrationDetail.ModifiedByUserId,
                Customer = new CustomerViewModel()
                {
                    Id = customer.Id,
                    Firstname = customer?.Firstname,
                    Middlename = customer?.Middlename,
                    Lastname = customer?.Lastname,
                    Gender = customer?.Gender,
                    DateOfBirth = customer?.DateOfBirth,
                    NationalityId = customer?.NationalityId,
                    IdType = customer?.IdType,
                    NationalIdNumber = customer?.NationalIdNumber,
                    ProvinceId = customer?.ProvinceId,
                    TownId = customer?.TownId,
                    Area = customer?.Area,
                    Address = customer?.Address,
                    Email = customer?.Email,
                    Occupation = customer?.Occupation,
                    MobileNumber = customer?.MobileNumber,
                    AlternativeMobileNumber = customer?.AlternativeMobileNumber,
                    PlotNumber = customer?.PlotNumber,
                    UnitNumber = customer?.UnitNumber,
                    Village = customer?.Village,
                    Landmark = customer?.Landmark,
                    Road = customer?.Road,
                    Chiefdom = customer?.Chiefdom,
                    Neighborhood = customer?.Neighborhood,
                    Section = customer?.Section,
                    NationalIdFrontUrl = customer?.NationalIdFrontUrl,
                    NationalIdBackUrl = customer?.NationalIdBackUrl,
                    PortraitUrl = customer?.PlotNumber,
                    SignatureUrl = customer?.SignatureUrl,
                    IsForeigner = customer?.IsForeigner,
                    DateTimeCreated = customer?.DateTimeCreated,
                    DateTimeModified = customer?.DateTimeModified,
                    CreatedByUserId = customer?.CreatedByUserId,
                    ModifiedByUserId = customer?.ModifiedByUserId,
                    NextOfKin = nextOfKin
                },
                Corporate = new CorporateViewModel()
                {
                    Id = corporate.Id,
                    CorporateIdNumber = corporate.CorporateIdNumber,
                    CertificateUrl = corporate.CertificateUrl,
                    LetterUrl = corporate.LetterUrl,
                    BatchUrl = corporate.BatchUrl,
                    RepresantativeId = corporate.RepresantativeId,
                    CompanyName = corporate.CompanyName,
                    CompanyCoinumber = corporate.CompanyCoinumber,
                    RegistrationDate = corporate.RegistrationDate,
                    ProvinceId = corporate.ProvinceId,
                    TownId = corporate.TownId,
                    Address = corporate.Address,
                    CompanyEmail = corporate.CompanyEmail,
                    AlternativeMobileNumber = corporate.AlternativeMobileNumber,
                    CorporateMobileNumber = corporate.CorporateMobileNumber,
                    DateTimeCreated = corporate.DateTimeCreated,
                    DateTimeModified = corporate.DateTimeModified,
                    CreatedByUserId = corporate.CreatedByUserId,
                    ModifiedByUserId = corporate.ModifiedByUserId,
                    IsCompanyGo = corporate.IsCompanyGo,
                    Representative = representative
                }
            };

            return simRegistrationViewModel;
        }

        private bool SimRegistrationDetailExists(int id)
        {
            return _context.SimRegistrationDetails.Any(e => e.Id == id);
        }
    }
}
