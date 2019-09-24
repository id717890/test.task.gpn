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
using static Oil.Api.ViewModels.FieldViewModel;

namespace Oil.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FieldController : ControllerBase
    {
        private readonly IFieldRepository _fieldRepository;
        private readonly IWellRepository _wellRepository;
        private readonly IMessageModelBuilder _messageModelBuilder;
        private readonly IMapper _mapper;

        public FieldController(IFieldRepository fieldRepository, IMessageModelBuilder messageModelBuilder, IMapper mapper, IWellRepository wellRepository)
        {
            _fieldRepository = fieldRepository;
            _wellRepository = wellRepository;
            _messageModelBuilder = messageModelBuilder;
            _mapper = mapper;
        }

        [HttpGet, Route("")]
        public async Task<IActionResult> Get()
        {
            try
            {
                return Ok(_mapper.Map<IEnumerable<Field>, IEnumerable<FieldView>>(await _fieldRepository.AllIncludingAsync(x => x.Company)));
            }
            catch (Exception e)
            {
                Log.Error(e, "FieldController.Get");
                return BadRequest(_messageModelBuilder.CreateModel("500", e.Message));
            }
        }

        [HttpGet, Route("{id}")]
        public async Task<IActionResult> Get(Int64 id)
        {
            try
            {
                return Ok(_mapper.Map<Field, FieldView>(await _fieldRepository.GetSingleAsync(id, x => x.Company)));
            }
            catch (Exception e)
            {
                Log.Error(e, "FieldController.GetById");
                return BadRequest(_messageModelBuilder.CreateModel("500", e.Message));
            }
        }

        [HttpPost, Route("create")]
        public async Task<IActionResult> Create(FieldView model)
        {
            try
            {
                await _fieldRepository.AddOrUpdateAsync(_mapper.Map<FieldView, Field>(model), true);
                return Ok();
            }
            catch (Exception e)
            {
                Log.Error(e, "FieldController.Create");
                return BadRequest(_messageModelBuilder.CreateModel("500", e.Message));
            }
        }

        [HttpPost, Route("edit")]
        public async Task<IActionResult> Edit(FieldView model)
        {
            try
            {
                var editedItem = (await _fieldRepository.GetSingleAsync(model.Id));
                if (editedItem == null) return NotFound();
                editedItem.Name = model.Name;
                editedItem.CompanyId = model.CompanyId;
                await _fieldRepository.AddOrUpdateAsync(editedItem, true);
                return Ok();
            }
            catch (Exception e)
            {
                Log.Error(e, "FieldController.Edit");
                return BadRequest(_messageModelBuilder.CreateModel("500", e.Message));
            }
        }

        [HttpPost, Route("delete")]
        public async Task<IActionResult> Delete(DeleteViewModel model)
        {
            try
            {
                var findItem = await _fieldRepository.GetSingleAsync(model.Id, x => x.Wells);
                if (findItem == null) return NotFound();
                ///Каскадно удаляем сначала все скважины на месторождении, т.к. EF настроен как OnDelete(DeleteBehavior.Restrict)
                if (findItem.Wells.Any()) _wellRepository.DeleteRange(findItem.Wells);
                ///Затем удаляем сам цех
                _fieldRepository.Delete(findItem);
                return Ok();
            }
            catch (Exception e)
            {
                Log.Error(e, "FieldController.Delete");
                return BadRequest(_messageModelBuilder.CreateModel("500", e.Message));
            }
        }
    }
}