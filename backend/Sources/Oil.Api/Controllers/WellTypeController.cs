using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Oil.Bll.Interfaces.Infrastructure;
using Oil.Dal.Interfaces.Repositories;
using Oil.Domain.Entity.Entities;
using Serilog;
using static Oil.Api.ViewModels.WellTypeViewModel;

namespace Oil.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WellTypeController : ControllerBase
    {
        private readonly IWellTypeRepository _wellTypeRepository;
        private readonly IMessageModelBuilder _messageModelBuilder;
        private readonly IMapper _mapper;

        public WellTypeController(IWellTypeRepository wellTypeRepository, IMessageModelBuilder messageModelBuilder, IMapper mapper)
        {
            _wellTypeRepository = wellTypeRepository;
            _messageModelBuilder = messageModelBuilder;
            _mapper = mapper;
        }

        [HttpGet, Route("")]
        public async Task<IActionResult> Get()
        {
            try
            {
                return Ok(_mapper.Map<IEnumerable<WellType>, IEnumerable<WellTypeView>>(await _wellTypeRepository.AllIncludingAsync()));
            }
            catch (Exception e)
            {
                Log.Error(e, "WellTypeController.Get");
                return BadRequest(_messageModelBuilder.CreateModel("500", e.Message));
            }
        }
    }
}