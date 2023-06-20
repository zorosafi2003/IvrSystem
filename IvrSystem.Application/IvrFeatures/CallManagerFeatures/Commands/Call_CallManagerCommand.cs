using IvrSystem.Application.IvrFeatures.CallManagerFeatures.Models;
using IvrSystem.Contracts.Dtos;
using IvrSystem.Persistence.IProviders;
using MediatR;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Twilio.TwiML;
using Twilio.TwiML.Voice;

namespace IvrSystem.Application.IvrFeatures.CallManagerFeatures.Commands
{
    public class Call_CallManagerCommand : IRequest<VoiceResponse>
    {
        public Call_CallManagerCommandRequest Model { get; set; }
        public Call_CallManagerCommand(Call_CallManagerCommandRequest model)
        {
            Model = model;
        }
        public class Call_CallManagerCommandHandler : IRequestHandler<Call_CallManagerCommand, VoiceResponse>
        {
            private readonly IHttpProvider _httpProvider;
            private readonly IConfiguration _configuration;

            public Call_CallManagerCommandHandler(IHttpProvider HttpProvider, IConfiguration Configuration)
            {
                _httpProvider = HttpProvider;
                _configuration = Configuration;
            }
            public async Task<VoiceResponse> Handle(Call_CallManagerCommand command, CancellationToken cancellationToken)
            {
                var response = new VoiceResponse();

                var forwardCallUrl = _configuration["Config:ForwardCallUrl"].ToString();

                var forwardCallResponse = await _httpProvider.Post<ForwardCallDto>(forwardCallUrl, new { TwilioPhone = command.Model.TwilioPhone, CallerPhone =command.Model.CallerPhone });

                if (forwardCallResponse.IsSuccessful == false)
                {
                    response.Say("Sorry we have proplem now , you can try again later");
                    return response;
                }

                if (string.IsNullOrEmpty(forwardCallResponse.Data.ForwardNumber))
                {
                    response.Say("Sorry not have forward number , you can try again later");
                    return response;
                }

                var dial = new Dial(number: forwardCallResponse.Data.ForwardNumber, callerId: command.Model.TwilioPhone);

                if (forwardCallResponse.Data.IsRecording == true)
                {
                    dial.Record = Dial.RecordEnum.RecordFromAnswer;
                }

                response.Append(dial);

                return response;
            }
        }
    }
}
