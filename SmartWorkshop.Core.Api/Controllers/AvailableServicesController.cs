using MediatR;
using Microsoft.AspNetCore.Mvc;
using SmartWorkshop.Workshop.Application.UseCases.AvailableServices.Create;
using SmartWorkshop.Workshop.Application.UseCases.AvailableServices.Get;
using SmartWorkshop.Workshop.Application.UseCases.AvailableServices.GetAll;
using SmartWorkshop.Workshop.Domain.Entities;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace SmartWorkshop.Workshop.Api.Controllers;

/// <summary>
/// Controller for managing available services
/// </summary>
[ApiController]
[Route("api/v1/[controller]")]
public class AvailableServicesController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<AvailableServicesController> _logger;

    public AvailableServicesController(IMediator mediator, ILogger<AvailableServicesController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    /// <summary>
    /// Get all available services
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<AvailableService>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var query = new GetAllAvailableServicesQuery();
        var result = await _mediator.Send(query, cancellationToken);

        if (!result.IsSuccess)
        {
            return StatusCode((int)result.StatusCode, new ProblemDetails
            {
                Title = "Error retrieving available services",
                Detail = string.Join(", ", result.Reasons.Select(r => r.Message)),
                Status = (int)result.StatusCode
            });
        }

        return Ok(result.Data);
    }

    /// <summary>
    /// Get available service by ID
    /// </summary>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(AvailableService), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.NotFound)]
    public async Task<IActionResult> GetById([FromRoute][Required] Guid id, CancellationToken cancellationToken)
    {
        var query = new GetAvailableServiceByIdQuery(id);
        var result = await _mediator.Send(query, cancellationToken);

        if (!result.IsSuccess)
        {
            return StatusCode((int)result.StatusCode, new ProblemDetails
            {
                Title = "Error retrieving available service",
                Detail = string.Join(", ", result.Reasons.Select(r => r.Message)),
                Status = (int)result.StatusCode
            });
        }

        return Ok(result.Data);
    }

    /// <summary>
    /// Create a new available service
    /// </summary>
    [HttpPost]
    [ProducesResponseType(typeof(AvailableService), (int)HttpStatusCode.Created)]
    [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> Create([FromBody][Required] CreateAvailableServiceCommand command, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(command, cancellationToken);

        if (!result.IsSuccess)
        {
            return StatusCode((int)result.StatusCode, new ProblemDetails
            {
                Title = "Error creating available service",
                Detail = string.Join(", ", result.Reasons.Select(r => r.Message)),
                Status = (int)result.StatusCode
            });
        }

        return CreatedAtAction(nameof(GetById), new { id = result.Data.Id }, result.Data);
    }
}
