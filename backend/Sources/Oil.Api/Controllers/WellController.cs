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
using static Oil.Api.ViewModels.WellViewModel;

namespace Oil.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WellController : ControllerBase
    {
        private readonly IWellRepository _wellRepository;
        private readonly IMessageModelBuilder _messageModelBuilder;
        private readonly IMapper _mapper;

        public WellController(IWellRepository wellRepository, IMessageModelBuilder messageModelBuilder, IMapper mapper)
        {
            _wellRepository = wellRepository;
            _messageModelBuilder = messageModelBuilder;
            _mapper = mapper;
        }

        [HttpGet, Route("")]
        public async Task<IActionResult> Get()
        {
            try
            {
                return Ok(_mapper.Map<IEnumerable<Well>, IEnumerable<WellView>>(await _wellRepository.AllIncludingAsync(x => x.Company, x => x.Shop, x => x.Shop.Company, x => x.Field, x=> x.Field.Company, x => x.WellType)));
            }
            catch (Exception e)
            {
                Log.Error(e, "WellController.Get");
                return BadRequest(_messageModelBuilder.CreateModel("500", e.Message));
            }
        }

        [HttpGet, Route("{id}")]
        public async Task<IActionResult> Get(Int64 id)
        {
            try
            {
                return Ok(_mapper.Map<Well, WellView>(await _wellRepository.GetSingleAsync(id, x => x.Company, x => x.Shop, x => x.Shop.Company, x => x.Field, x => x.Field.Company, x => x.WellType)));
            }
            catch (Exception e)
            {
                Log.Error(e, "WellController.GetById");
                return BadRequest(_messageModelBuilder.CreateModel("500", e.Message));
            }
        }

        [HttpPost, Route("create")]
        public async Task<IActionResult> Create(WellView model)
        {
            try
            {
                await _wellRepository.AddOrUpdateAsync(_mapper.Map<WellView, Well>(model), true);
                return Ok();
            }
            catch (Exception e)
            {
                Log.Error(e, "WellController.Create");
                return BadRequest(_messageModelBuilder.CreateModel("500", e.Message));
            }
        }

        [HttpPost, Route("edit")]
        public async Task<IActionResult> Edit(WellView model)
        {
            try
            {
                var editedItem = (await _wellRepository.GetSingleAsync(model.Id));
                if (editedItem == null) return NotFound();
                editedItem.Name = model.Name;
                editedItem.CompanyId = model.CompanyId;
                editedItem.ShopId = model.ShopId;
                editedItem.FieldId = model.FieldId;
                editedItem.WellTypeId = model.WellTypeId;
                editedItem.Altitude = model.Altitude;
                editedItem.ZabI = model.ZabI;
                editedItem.ZabF = model.ZabF;
                await _wellRepository.AddOrUpdateAsync(editedItem, true);
                return Ok();
            }
            catch (Exception e)
            {
                Log.Error(e, "WellController.Edit");
                return BadRequest(_messageModelBuilder.CreateModel("500", e.Message));
            }
        }

        [HttpPost, Route("delete")]
        public async Task<IActionResult> Delete(DeleteViewModel model)
        {
            try
            {
                var findItem = await _wellRepository.GetSingleAsync(model.Id);
                if (findItem == null) return NotFound();
                _wellRepository.Delete(findItem);
                return Ok();
            }
            catch (Exception e)
            {
                Log.Error(e, "WellController.Delete");
                return BadRequest(_messageModelBuilder.CreateModel("500", e.Message));
            }
        }
    }
}