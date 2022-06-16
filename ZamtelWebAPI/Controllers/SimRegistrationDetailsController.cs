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
    public class SimRegistrationDetailsController : ControllerBase
    {
        private readonly ZamtelContext _context;

        public SimRegistrationDetailsController(ZamtelContext context)
        {
            _context = context;
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
                var corporate = _context.Corporates.Where(x => x.Id == simRegistrationDetail.CorporateId).FirstOrDefault();
                var representative = _context.Representatives.Where(x => x.Id == corporate.RepresantativeId).FirstOrDefault();

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
                    Customer =
                {
                    Firstname = customer.Firstname,
                        Middlename = customer.Middlename,
                        Lastname = customer.Lastname,
                        Gender = customer.Gender,
                        DateOfBirth = customer.DateOfBirth,
                        NationalityId = customer.NationalityId,
                        IdType = customer.IdType,
                        NationalIdNumber = customer.NationalIdNumber,
                        ProvinceId = customer.ProvinceId,
                        TownId = customer.TownId,
                        Area = customer.Area,
                        Address = customer.Address,
                        Email = customer.Email,
                        Occupation = customer.Occupation,
                        MobileNumber = customer.MobileNumber,
                        AlternativeMobileNumber = customer.AlternativeMobileNumber,
                        PlotNumber = customer.PlotNumber,
                        UnitNumber = customer.UnitNumber,
                        Village = customer.Village,
                        Landmark = customer.Landmark,
                        Road = customer.Road,
                        Chiefdom = customer.Chiefdom,
                        Neighborhood = customer.Neighborhood,
                        Section = customer.Section,
                        NationalIdFrontUrl = customer.NationalIdFrontUrl,
                        NationalIdBackUrl = customer.NationalIdBackUrl,
                        PortraitUrl = customer.PlotNumber,
                        SignatureUrl = customer.SignatureUrl,
                        IsForeigner = customer.IsForeigner,
                        DateTimeCreated = customer.DateTimeCreated,
                        DateTimeModified = customer.DateTimeModified,
                        CreatedByUserId = customer.CreatedByUserId,
                        ModifiedByUserId = customer.ModifiedByUserId
                },
                    Corporate =
                {
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
                    Representative =
                    {
                        Firstname = representative.Firstname,
                        Middlename = representative.Middlename,
                        Lastname = representative.Lastname,
                        IdType = representative.IdType,
                        NationalIdNumber = representative.NationalIdNumber,
                        InvitingEntity = representative.InvitingEntity,
                        PortraitUrl = representative.PortraitUrl,
                        NationalIdFrontUrl = representative.NationalIdFrontUrl,
                        NationalIdBackUrl = representative.NationalIdBackUrl,
                        SignatureUrl = representative.SignatureUrl,
                        DateTimeCreated = representative.DateTimeCreated,
                        DateTimeModified = representative.DateTimeModified,
                        CreatedByUserId = representative.CreatedByUserId,
                        ModifiedByUserId = representative.ModifiedByUserId
                    }
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
            var corporate = _context.Corporates.Where(x => x.Id == simRegistrationDetail.CorporateId).FirstOrDefault();
            var representative = _context.Representatives.Where(x => x.Id == corporate.RepresantativeId).FirstOrDefault();

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
                Customer =
                {
                    Firstname = customer.Firstname,
                        Middlename = customer.Middlename,
                        Lastname = customer.Lastname,
                        Gender = customer.Gender,
                        DateOfBirth = customer.DateOfBirth,
                        NationalityId = customer.NationalityId,
                        IdType = customer.IdType,
                        NationalIdNumber = customer.NationalIdNumber,
                        ProvinceId = customer.ProvinceId,
                        TownId = customer.TownId,
                        Area = customer.Area,
                        Address = customer.Address,
                        Email = customer.Email,
                        Occupation = customer.Occupation,
                        MobileNumber = customer.MobileNumber,
                        AlternativeMobileNumber = customer.AlternativeMobileNumber,
                        PlotNumber = customer.PlotNumber,
                        UnitNumber = customer.UnitNumber,
                        Village = customer.Village,
                        Landmark = customer.Landmark,
                        Road = customer.Road,
                        Chiefdom = customer.Chiefdom,
                        Neighborhood = customer.Neighborhood,
                        Section = customer.Section,
                        NationalIdFrontUrl = customer.NationalIdFrontUrl,
                        NationalIdBackUrl = customer.NationalIdBackUrl,
                        PortraitUrl = customer.PlotNumber,
                        SignatureUrl = customer.SignatureUrl,
                        IsForeigner = customer.IsForeigner,
                        DateTimeCreated = customer.DateTimeCreated,
                        DateTimeModified = customer.DateTimeModified,
                        CreatedByUserId = customer.CreatedByUserId,
                        ModifiedByUserId = customer.ModifiedByUserId
                },
                Corporate =
                {
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
                    Representative =
                    {
                        Firstname = representative.Firstname,
                        Middlename = representative.Middlename,
                        Lastname = representative.Lastname,
                        IdType = representative.IdType,
                        NationalIdNumber = representative.NationalIdNumber,
                        InvitingEntity = representative.InvitingEntity,
                        PortraitUrl = representative.PortraitUrl,
                        NationalIdFrontUrl = representative.NationalIdFrontUrl,
                        NationalIdBackUrl = representative.NationalIdBackUrl,
                        SignatureUrl = representative.SignatureUrl,
                        DateTimeCreated = representative.DateTimeCreated,
                        DateTimeModified = representative.DateTimeModified,
                        CreatedByUserId = representative.CreatedByUserId,
                        ModifiedByUserId = representative.ModifiedByUserId
                    }
                }
            };

            return simRegistrationViewModel;
        }

        // PUT: api/SimRegistrationDetails/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSimRegistrationDetail(int id, SimRegistrationViewModel simRegistrationViewModel)
        {
            if (id != simRegistrationViewModel.Id)
            {
                ModelState.AddModelError("Error", "Sim details cannot be updated. Id sent does not match back office agent Id");

                return BadRequest(ModelState);
            }

            SimRegistrationValidators simRegistrationValidators = new SimRegistrationValidators(_context);

            var doesSimRecordExist = simRegistrationValidators.IsCustomerMobileNumberExist(simRegistrationViewModel.Customer.MobileNumber);

            if (!doesSimRecordExist)
            {
                ModelState.AddModelError("Error", "Sim details cannot be updated. Customer mobile number " + simRegistrationViewModel.Customer.MobileNumber + " does not exist.");

                return BadRequest(ModelState);
            }

            var customer = _context.Customers.Where(x => x.Id == simRegistrationViewModel.CustomerId).FirstOrDefault();

            if (customer == null)
            {
                ModelState.AddModelError("Error", "Sim details cannot be updated. CustomerId cannot be '0'");

                return BadRequest(ModelState);
            }

            customer.Firstname = simRegistrationViewModel.Customer.Firstname;
            customer.Middlename = simRegistrationViewModel.Customer.Middlename;
            customer.Lastname = simRegistrationViewModel.Customer.Lastname;
            customer.Gender = simRegistrationViewModel.Customer.Gender;
            customer.DateOfBirth = simRegistrationViewModel.Customer.DateOfBirth;
            customer.NationalityId = simRegistrationViewModel.Customer.NationalityId;
            customer.IdType = simRegistrationViewModel.Customer.IdType;
            customer.NationalIdNumber = simRegistrationViewModel.Customer.NationalIdNumber;
            customer.ProvinceId = simRegistrationViewModel.Customer.ProvinceId;
            customer.TownId = simRegistrationViewModel.Customer.TownId;
            customer.Area = simRegistrationViewModel.Customer.Area;
            customer.Address = simRegistrationViewModel.Customer.Address;
            customer.Email = simRegistrationViewModel.Customer.Email;
            customer.Occupation = simRegistrationViewModel.Customer.Occupation;
            customer.MobileNumber = simRegistrationViewModel.Customer.MobileNumber;
            customer.AlternativeMobileNumber = simRegistrationViewModel.Customer.AlternativeMobileNumber;
            customer.PlotNumber = simRegistrationViewModel.Customer.PlotNumber;
            customer.UnitNumber = simRegistrationViewModel.Customer.UnitNumber;
            customer.Village = simRegistrationViewModel.Customer.Village;
            customer.Landmark = simRegistrationViewModel.Customer.Landmark;
            customer.Road = simRegistrationViewModel.Customer.Road;
            customer.Chiefdom = simRegistrationViewModel.Customer.Chiefdom;
            customer.Neighborhood = simRegistrationViewModel.Customer.Neighborhood;
            customer.Section = simRegistrationViewModel.Customer.Section;
            customer.NationalIdFrontUrl = simRegistrationViewModel.Customer.NationalIdFrontUrl;
            customer.NationalIdBackUrl = simRegistrationViewModel.Customer.NationalIdBackUrl;
            customer.PortraitUrl = simRegistrationViewModel.Customer.PortraitUrl;
            customer.SignatureUrl = simRegistrationViewModel.Customer.SignatureUrl;
            customer.IsForeigner = simRegistrationViewModel.Customer.IsForeigner;
            customer.DateTimeCreated = simRegistrationViewModel.Customer.DateTimeCreated;
            customer.DateTimeModified = DateTime.Now;
            customer.CreatedByUserId = simRegistrationViewModel.Customer.CreatedByUserId;
            customer.ModifiedByUserId = simRegistrationViewModel.Customer.ModifiedByUserId;
            customer.ProofOfStayUrl = simRegistrationViewModel.Customer.ProofOfStayUrl;

            if (customer.ModifiedByUserId == null)
            {
                customer.ModifiedByUserId = 3;
            }

            var simRegistrationDetailToBeUpdated = _context.SimRegistrationDetails.Where(x => x.Id == id || x.Id == simRegistrationViewModel.Id).FirstOrDefault();

            simRegistrationDetailToBeUpdated.RegistrationType = simRegistrationViewModel.RegistrationType;
            simRegistrationDetailToBeUpdated.CategoryType = simRegistrationViewModel.CategoryType;
            simRegistrationDetailToBeUpdated.CustomerId = simRegistrationViewModel.CustomerId;
            simRegistrationDetailToBeUpdated.DateTimeCreated = simRegistrationViewModel.DateTimeCreated;
            simRegistrationDetailToBeUpdated.DateTimeModified = DateTime.Now;
            simRegistrationDetailToBeUpdated.CreatedByUserId = simRegistrationViewModel.CreatedByUserId;
            simRegistrationDetailToBeUpdated.ModifiedByUserId = simRegistrationViewModel.ModifiedByUserId;

            _context.Entry(customer).State = EntityState.Modified;
            _context.Entry(simRegistrationDetailToBeUpdated).State = EntityState.Modified;

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

        // POST: api/SimRegistrationDetails
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<SimRegistrationViewModel>> PostSimRegistrationDetail(SimRegistrationViewModel simRegistrationViewModel)
        {
            SimRegistrationValidators simRegistrationValidators = new SimRegistrationValidators(_context);

            if (simRegistrationViewModel.Customer.MobileNumber != null)
            {
                var doesCustomerMobileNumberExist = simRegistrationValidators.IsCustomerMobileNumberExist(simRegistrationViewModel.Customer.MobileNumber);

                if (doesCustomerMobileNumberExist)
                {
                    ModelState.AddModelError("Error", "Sim registration failed. Customer mobile number " + simRegistrationViewModel.Customer.MobileNumber + " already exists.");

                    return BadRequest(ModelState);
                }
            }

            if (simRegistrationViewModel.Customer.NationalIdNumber != null)
            {
                var doesCustomerIdNumberExist = simRegistrationValidators.IsCustomerIdNumberExist(simRegistrationViewModel.Customer.NationalIdNumber);

                if (doesCustomerIdNumberExist)
                {
                    ModelState.AddModelError("Error", "Sim registration failed. Customer national id number " + simRegistrationViewModel.Customer.NationalIdNumber + " already exists.");

                    return BadRequest(ModelState);
                }
            }

            if (simRegistrationViewModel.Corporate.CorporateMobileNumber != null)
            {
                var doesCustomerIdNumberExist = simRegistrationValidators.IsCorporateMobileNumberExist(simRegistrationViewModel.Corporate.CorporateMobileNumber);

                if (doesCustomerIdNumberExist)
                {
                    ModelState.AddModelError("Error", "Sim registration failed. Coorporate mobile number " + simRegistrationViewModel.Corporate.CorporateMobileNumber + " already exists.");

                    return BadRequest(ModelState);
                }
            }

            if (simRegistrationViewModel.Corporate.CorporateIdNumber != null)
            {
                var doesCustomerIdNumberExist = simRegistrationValidators.IsCorporateMobileNumberExist(simRegistrationViewModel.Corporate.CorporateIdNumber);

                if (doesCustomerIdNumberExist)
                {
                    ModelState.AddModelError("Error", "Sim registration failed. Coorporate id number " + simRegistrationViewModel.Corporate.CorporateIdNumber + " already exists.");

                    return BadRequest(ModelState);
                }
            }

            Customer customer = new Customer()
            {
                Firstname = simRegistrationViewModel.Customer.Firstname,
                Middlename = simRegistrationViewModel.Customer.Middlename,
                Lastname = simRegistrationViewModel.Customer.Lastname,
                Gender = simRegistrationViewModel.Customer.Gender,
                DateOfBirth = simRegistrationViewModel.Customer.DateOfBirth,
                NationalityId = simRegistrationViewModel.Customer.NationalityId,
                IdType = simRegistrationViewModel.Customer.IdType,
                NationalIdNumber = simRegistrationViewModel.Customer.NationalIdNumber,
                ProvinceId = simRegistrationViewModel.Customer.ProvinceId,
                TownId = simRegistrationViewModel.Customer.TownId,
                Area = simRegistrationViewModel.Customer.Area,
                Address = simRegistrationViewModel.Customer.Address,
                Email = simRegistrationViewModel.Customer.Email,
                Occupation = simRegistrationViewModel.Customer.Occupation,
                MobileNumber = simRegistrationViewModel.Customer.MobileNumber,
                AlternativeMobileNumber = simRegistrationViewModel.Customer.AlternativeMobileNumber,
                PlotNumber = simRegistrationViewModel.Customer.PlotNumber,
                UnitNumber = simRegistrationViewModel.Customer.UnitNumber,
                Village = simRegistrationViewModel.Customer.Village,
                Landmark = simRegistrationViewModel.Customer.Landmark,
                Road = simRegistrationViewModel.Customer.Road,
                Chiefdom = simRegistrationViewModel.Customer.Chiefdom,
                Neighborhood = simRegistrationViewModel.Customer.Neighborhood,
                Section = simRegistrationViewModel.Customer.Section,
                NationalIdFrontUrl = simRegistrationViewModel.Customer.NationalIdFrontUrl,
                NationalIdBackUrl = simRegistrationViewModel.Customer.NationalIdBackUrl,
                PortraitUrl = simRegistrationViewModel.Customer.PortraitUrl,
                SignatureUrl = simRegistrationViewModel.Customer.SignatureUrl,
                IsForeigner = simRegistrationViewModel.Customer.IsForeigner,
                ProofOfStayUrl = simRegistrationViewModel.Customer.ProofOfStayUrl,
                CreatedByUserId = simRegistrationViewModel.CreatedByUserId,
                DateTimeCreated = DateTime.Now
            };

            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();

            var lastAddedCustomer = _context.Customers.OrderByDescending(x => x.Id).FirstOrDefault();

            simRegistrationViewModel.CreatedByUserId = 3;

            if (lastAddedCustomer != null)
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
                    DateTimeCreated = lastAddedCustomer.DateTimeCreated
                };

                return CreatedAtAction("GetSimRegistrationDetail", new { id = simRegistrationDetail.Id }, simRegistrationViewModel1);
            }
            else
            {
                ModelState.AddModelError("Error", "There has been a fatal error in saving your Customer");

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

            var simRegistrationDetail = _context.SimRegistrationDetails.Where(x => x.Id == id).FirstOrDefault();

            if (simRegistrationDetail == null)
            {
                ModelState.AddModelError("Error", "Sim Registreation Record cannot be deleted. Sim Registration Id passed does not exist");

                return NotFound(ModelState);
            }

            customer = _context.Customers.Where(x => x.Id == simRegistrationDetail.CustomerId).FirstOrDefault();

            if (customer == null)
            {
                corporate = _context.Corporates.Where(x => x.Id == simRegistrationDetail.CorporateId).FirstOrDefault();

                if (corporate == null)
                {
                    ModelState.AddModelError("Error", "Sim Registration Record cannot be deleted. Either Customer or Corporate Id does not exist");

                    return NotFound(ModelState);
                }
                else
                {
                    representative = _context.Representatives.Where(x => x.Id == corporate.RepresantativeId).FirstOrDefault();

                    _context.Representatives.Remove(representative);
                    _context.Corporates.Remove(corporate);
                }
            }
            else
            {
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
                Customer =
                {
                    Firstname = customer.Firstname,
                        Middlename = customer.Middlename,
                        Lastname = customer.Lastname,
                        Gender = customer.Gender,
                        DateOfBirth = customer.DateOfBirth,
                        NationalityId = customer.NationalityId,
                        IdType = customer.IdType,
                        NationalIdNumber = customer.NationalIdNumber,
                        ProvinceId = customer.ProvinceId,
                        TownId = customer.TownId,
                        Area = customer.Area,
                        Address = customer.Address,
                        Email = customer.Email,
                        Occupation = customer.Occupation,
                        MobileNumber = customer.MobileNumber,
                        AlternativeMobileNumber = customer.AlternativeMobileNumber,
                        PlotNumber = customer.PlotNumber,
                        UnitNumber = customer.UnitNumber,
                        Village = customer.Village,
                        Landmark = customer.Landmark,
                        Road = customer.Road,
                        Chiefdom = customer.Chiefdom,
                        Neighborhood = customer.Neighborhood,
                        Section = customer.Section,
                        NationalIdFrontUrl = customer.NationalIdFrontUrl,
                        NationalIdBackUrl = customer.NationalIdBackUrl,
                        PortraitUrl = customer.PlotNumber,
                        SignatureUrl = customer.SignatureUrl,
                        IsForeigner = customer.IsForeigner,
                        DateTimeCreated = customer.DateTimeCreated,
                        DateTimeModified = customer.DateTimeModified,
                        CreatedByUserId = customer.CreatedByUserId,
                        ModifiedByUserId = customer.ModifiedByUserId
                },
                Corporate =
                {
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
                    Representative =
                    {
                        Firstname = representative.Firstname,
                        Middlename = representative.Middlename,
                        Lastname = representative.Lastname,
                        IdType = representative.IdType,
                        NationalIdNumber = representative.NationalIdNumber,
                        InvitingEntity = representative.InvitingEntity,
                        PortraitUrl = representative.PortraitUrl,
                        NationalIdFrontUrl = representative.NationalIdFrontUrl,
                        NationalIdBackUrl = representative.NationalIdBackUrl,
                        SignatureUrl = representative.SignatureUrl,
                        DateTimeCreated = representative.DateTimeCreated,
                        DateTimeModified = representative.DateTimeModified,
                        CreatedByUserId = representative.CreatedByUserId,
                        ModifiedByUserId = representative.ModifiedByUserId
                    }
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
