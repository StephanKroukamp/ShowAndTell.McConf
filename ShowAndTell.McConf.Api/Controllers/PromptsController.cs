using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Amazon.Runtime.Internal.Util;
using Intent.RoslynWeaver.Attributes;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using ShowAndTell.McConf.Api.Controllers.ResponseTypes;
using ShowAndTell.McConf.Api.Models;
using ShowAndTell.McConf.Application.Prompts;
using ShowAndTell.McConf.Application.Prompts.CreatePrompt;
using ShowAndTell.McConf.Application.Prompts.DeletePrompt;
using ShowAndTell.McConf.Application.Prompts.GenerateMessageByDistance;
using ShowAndTell.McConf.Application.Prompts.GenerateVideo;
using ShowAndTell.McConf.Application.Prompts.GetPromptById;
using ShowAndTell.McConf.Application.Prompts.GetPrompts;
using ShowAndTell.McConf.Application.Prompts.UpdatePrompt;
using ShowAndTell.McConf.Domain.Models;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.AspNetCore.Controllers.Controller", Version = "1.0")]

namespace ShowAndTell.McConf.Api.Controllers
{
    [ApiController]
    [EnableCors("OpenCORSPolicy")]
    [Route("api/[controller]")]
    public class PromptsController : ControllerBase
    {
        private readonly ISender _mediator;
        private readonly ILogger<PromptsController> _logger;

        public PromptsController(ISender mediator, ILogger<PromptsController> logger)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _logger = logger;
        }

        /// <summary>
        /// </summary>
        /// <response code="201">Successfully created.</response>
        /// <response code="400">One or more validation errors have occurred.</response>
        [HttpPost]
        [ProducesResponseType(typeof(JsonResponse<Guid>), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Guid>> Post([FromBody] CreatePromptCommand command, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(command, cancellationToken);
            return CreatedAtAction(nameof(Get), new { id = result }, new { Id = result });
        }

        /// <summary>
        /// </summary>
        /// <response code="200">Returns the specified PromptDto.</response>
        /// <response code="400">One or more validation errors have occurred.</response>
        /// <response code="404">Can't find an PromptDto with the parameters provided.</response>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(PromptDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<PromptDto>> Get([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetPromptByIdQuery { Id = id }, cancellationToken);
            return result != null ? Ok(result) : NotFound();
        }

        /// <summary>
        /// </summary>
        /// <response code="200">Returns the specified List&lt;PromptDto&gt;.</response>
        [HttpGet]
        [ProducesResponseType(typeof(List<PromptDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<List<PromptDto>>> GetAll(CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetPromptsQuery(), cancellationToken);
            return Ok(result);
        }

        /// <summary>
        /// </summary>
        /// <response code="204">Successfully updated.</response>
        /// <response code="400">One or more validation errors have occurred.</response>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Put([FromRoute] Guid id, [FromBody] UpdatePromptCommand command, CancellationToken cancellationToken)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }

            await _mediator.Send(command, cancellationToken);
            return NoContent();
        }

        /// <summary>
        /// </summary>
        /// <response code="200">Successfully deleted.</response>
        /// <response code="400">One or more validation errors have occurred.</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Delete([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            await _mediator.Send(new DeletePromptCommand { Id = id }, cancellationToken);
            return Ok();
        }

        /// <summary>
        /// </summary>
        /// <response code="200">Returns the specified string.</response>
        /// <response code="400">One or more validation errors have occurred.</response>
        /// <response code="404">Can't find an string with the parameters provided.</response>
        [HttpGet("[action]")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [IntentManaged(Mode.Ignore)]
        public async Task<ActionResult<GenerateResponse>> GenerateMessage(decimal promptText, string apiKey, CancellationToken cancellationToken)
        {
            var generatedMessage = await _mediator.Send(new GenerateMessageByDistanceQuery { Distance = promptText, ApiKey = apiKey}, cancellationToken);

            var script = new Script{
                Text = generatedMessage,
                Width = 512,
                Height = 512,
            };

            var request = new GenerateVideoRequest { Script = script, ApiKey = apiKey };
            var video = await _mediator.Send(request);

            var result = new GenerateResponse
            {
                ImageUrl = video.Url,
                InputText = generatedMessage
            };

            _logger.LogInformation(JsonConvert.SerializeObject(result));

            return result != null ? Ok(result) : NotFound();
        }
    }
}