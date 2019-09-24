using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Oil.Api.ViewModels;
using Oil.Bll.Interfaces.Infrastructure;
using Oil.Dal.Interfaces.Repositories;
using Oil.Domain.Entity.Entities;
using Serilog;
using static Oil.Api.ViewModels.ShopViewModel;

namespace Oil.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShopController : ControllerBase
    {
        private readonly IShopRepository _shopRepository;
        private readonly IWellRepository _wellRepository;
        private readonly IMessageModelBuilder _messageModelBuilder;
        private readonly IMapper _mapper;

        public ShopController(IShopRepository shopRepository, IMessageModelBuilder messageModelBuilder, IMapper mapper, IWellRepository wellRepository)
        {
            _shopRepository = shopRepository;
            _wellRepository = wellRepository;
            _messageModelBuilder = messageModelBuilder;
            _mapper = mapper;
        }

        [HttpGet, Route("")]
        public async Task<IActionResult> Get()
        {
            try
            {
                return Ok(_mapper.Map<IEnumerable<Shop>, IEnumerable<ShopView>>(await _shopRepository.AllIncludingAsync(x => x.Company)));
            }
            catch (Exception e)
            {
                Log.Error(e, "ShopController.Get");
                return BadRequest(_messageModelBuilder.CreateModel("500", e.Message));
            }
        }

        [HttpGet, Route("{id}")]
        public async Task<IActionResult> Get(Int64 id)
        {
            try
            {
                return Ok(_mapper.Map<Shop, ShopView>(await _shopRepository.GetSingleAsync(id, x => x.Company)));
            }
            catch (Exception e)
            {
                Log.Error(e, "ShopController.GetById");
                return BadRequest(_messageModelBuilder.CreateModel("500", e.Message));
            }
        }

        [HttpPost, Route("create")]
        public async Task<IActionResult> Create(ShopView model)
        {
            try
            {
                await _shopRepository.AddOrUpdateAsync(_mapper.Map<ShopView, Shop>(model), true);
                return Ok();
            }
            catch (Exception e)
            {
                Log.Error(e, "ShopController.Create");
                return BadRequest(_messageModelBuilder.CreateModel("500", e.Message));
            }
        }

        [HttpPost, Route("edit")]
        public async Task<IActionResult> Edit(ShopView model)
        {
            try
            {
                var editedItem = (await _shopRepository.GetSingleAsync(model.Id));
                if (editedItem == null) return NotFound();
                editedItem.Name = model.Name;
                editedItem.CompanyId = model.CompanyId;
                await _shopRepository.AddOrUpdateAsync(editedItem, true);
                return Ok();
            }
            catch (Exception e)
            {
                Log.Error(e, "ShopController.Edit");
                return BadRequest(_messageModelBuilder.CreateModel("500", e.Message));
            }
        }

        [HttpPost, Route("delete")]
        public async Task<IActionResult> Delete(DeleteViewModel model)
        {
            try
            {
                var findItem = await _shopRepository.GetSingleAsync(model.Id, x=> x.Wells);
                if (findItem == null) return NotFound();
                ///Каскадно удаляем сначала все скважины в цехе, т.к. EF настроен как OnDelete(DeleteBehavior.Restrict)
                if (findItem.Wells.Any()) _wellRepository.DeleteRange(findItem.Wells);
                ///Затем удаляем сам цех
                _shopRepository.Delete(findItem);
                return Ok();
            }
            catch (Exception e)
            {
                Log.Error(e, "ShopController.Delete");
                return BadRequest(_messageModelBuilder.CreateModel("500", e.Message));
            }
        }
    }
}