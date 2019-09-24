using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Oil.Api.ViewModels;
using Oil.Bll.Interfaces.Infrastructure;
using Oil.Dal.Interfaces.Repositories;
using Oil.Domain.Entity.Entities;
using Serilog;
using static Oil.Api.ViewModels.CompanyViewModel;

namespace Oil.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IWellRepository _wellRepository;
        private readonly IShopRepository _shopRepository;
        private readonly IFieldRepository _fieldRepository;
        private readonly IMessageModelBuilder _messageModelBuilder;
        private readonly IMapper _mapper;

        public CompanyController(
            ICompanyRepository companyRepository, 
            IWellRepository wellRepository, 
            IShopRepository shopRepository, 
            IFieldRepository fieldRepository,
            IMessageModelBuilder messageModelBuilder, 
            IMapper mapper)
        {
            _companyRepository = companyRepository;
            _wellRepository = wellRepository;
            _shopRepository = shopRepository;
            _fieldRepository = fieldRepository;
            _messageModelBuilder = messageModelBuilder;
            _mapper = mapper;
        }

        [HttpGet, Route("")]
        public async Task<IActionResult> Get()
        {
            try
            {
                return Ok(_mapper.Map<IEnumerable<Company>, IEnumerable<CompanyView>>(await _companyRepository.GetAllAsync()));
            }
            catch (Exception e)
            {
                Log.Error(e, "CompanyController.Get");
                return BadRequest(_messageModelBuilder.CreateModel("500", e.Message));
            }
        }

        [HttpGet, Route("{id}")]
        public async Task<IActionResult> Get(Int64 id)
        {
            try
            {
                return Ok(_mapper.Map<Company, CompanyView>(await _companyRepository.GetSingleAsync(id)));
            }
            catch (Exception e)
            {
                Log.Error(e, "CompanyController.GetById");
                return BadRequest(_messageModelBuilder.CreateModel("500", e.Message));
            }
        }

        [HttpPost, Route("create")]
        public async Task<IActionResult> Create(CompanyView model)
        {
            try
            {
                await _companyRepository.AddOrUpdateAsync(_mapper.Map<CompanyView, Company>(model), true);
                return Ok();
            }
            catch (Exception e)
            {
                Log.Error(e, "CompanyController.Create");
                return BadRequest(_messageModelBuilder.CreateModel("500", e.Message));
            }
        }

        [HttpPost, Route("edit")]
        public async Task<IActionResult> Edit(CompanyView model)
        {
            try
            {
                var editedCompany = (await _companyRepository.GetSingleAsync(model.Id));
                if (editedCompany == null) return NotFound();
                editedCompany.Name = model.Name;
                editedCompany.ShortName = model.ShortName;
                await _companyRepository.AddOrUpdateAsync(editedCompany, true);
                return Ok();
            }
            catch (Exception e)
            {
                Log.Error(e, "CompanyController.Edit");
                return BadRequest(_messageModelBuilder.CreateModel("500", e.Message));
            }
        }

        [HttpPost, Route("delete")]
        public async Task<IActionResult> Delete(DeleteViewModel model)
        {
            try
            {
                var findCompany = (await _companyRepository.GetSingleAsync(model.Id, x=>x.Shops, x=>x.Fields, x=>x.Wells));
                if (findCompany == null) return NotFound();
                ///Каскадно удаляем сначала все скважины в компании, т.к. EF настроен как OnDelete(DeleteBehavior.Restrict)
                if (findCompany.Wells.Any()) _wellRepository.DeleteRange(findCompany.Wells);
                ///Затем каскадно удаляем скважины в цехах, которые принадлежат этой компании
                if (findCompany.Shops.Any())
                {
                    foreach(var shop in findCompany.Shops)
                    {
                        var findShop = await _shopRepository.GetSingleAsync(shop.Id, x => x.Wells);
                        if (findShop.Wells.Any()) _wellRepository.DeleteRange(findShop.Wells);
                    }
                }
                ///Затем каскадно удаляем скважины на месторождениях, которые принадлежат этой компании
                if (findCompany.Fields.Any())
                {
                    foreach (var field in findCompany.Fields)
                    {
                        var findField = await _fieldRepository.GetSingleAsync(field.Id, x => x.Wells);
                        if (findField.Wells.Any()) _wellRepository.DeleteRange(findField.Wells);
                    }
                }
                ///Затем удаляем саму компанию
                _companyRepository.Delete(await _companyRepository.GetSingleAsync(model.Id));
                return Ok();
            }
            catch (Exception e)
            {
                Log.Error(e, "CompanyController.Delete");
                return BadRequest(_messageModelBuilder.CreateModel("500", e.Message));
            }
        }
    }
}