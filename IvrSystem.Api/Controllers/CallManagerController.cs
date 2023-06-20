using IvrSystem.Application.IvrFeatures.CallManagerFeatures.Commands;
using IvrSystem.Application.IvrFeatures.CallManagerFeatures.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Twilio.AspNet.Core;

namespace IvrSystem.Api.Controllers
{
    [Route("ivr/[controller]")]
    [AllowAnonymous]
    public class CallManagerController : TwilioController
    {
        private readonly IMediator _mediator;

        public CallManagerController(IMediator Mediator)
        {
            _mediator = Mediator;
        }

        [HttpGet("Call")]
        public async Task<TwiMLResult> Call(string from, string to)
        {
            var response = await _mediator.Send(new Call_CallManagerCommand(new Call_CallManagerCommandRequest { 
                CallerPhone = from,
                TwilioPhone = to
            }));
            return TwiML(response);
        }
    }
}
