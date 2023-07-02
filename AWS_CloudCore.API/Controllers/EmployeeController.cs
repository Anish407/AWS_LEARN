﻿using Amazon.SecretsManager.Model;
using Amazon.SecretsManager;
using AWS_CloudCore.Core.Models.Configs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Amazon;

namespace AWS_CloudCore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly SNSConfig _snsConfig;

        public EmployeeController(IOptionsSnapshot<SNSConfig> options)
        {
            _snsConfig = options.Value;
        }

        [HttpGet("GetEmployee")]
        public async Task<IActionResult> GetEmployee()
        {
            
            return Ok();
        }
    }
}
