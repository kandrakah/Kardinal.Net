using Kardinal.Net.Data;
using Kardinal.Net.Web.Samples.Data;
using Kardinal.Net.Web.Samples.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Kardinal.Net.Web.Samples.Controllers
{
    [ApiController]
    [Route("")]
    [Authorize]
    public class WeatherForecastController : AbstractController
    {
        private readonly IUnitOfWork _unitOfWork;

        public WeatherForecastController(IServiceProvider provider) : base(provider)
        {
            this._unitOfWork = this.GetService<IUnitOfWork>();            
        }

        [HttpGet]
        public ActionResult<IEnumerable<WeatherEntity>> Get()
        {
            try
            {
                var set = this._unitOfWork
                    .Set<WeatherEntity>();

                var data = set
                    .ToList();

                return data;
            }
            catch (Exception ex)
            {
                return this.Error(ex);
            }
        }

        [HttpPost]
        public ActionResult<WeatherEntity> Post(WeatherEntity entity)
        {
            try
            {
                var set = this._unitOfWork
                    .Set<WeatherEntity>();

                set.Add(entity);
                this._unitOfWork.SaveChanges();

                return entity;
            }
            catch (Exception ex)
            {
                return this.Error(ex);
            }
        }
    }
}
