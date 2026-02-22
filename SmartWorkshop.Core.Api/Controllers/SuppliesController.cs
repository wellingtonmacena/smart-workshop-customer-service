using MediatR;
using Microsoft.AspNetCore.Mvc;
using SmartWorkshop.Workshop.Application.UseCases.Supplies.Create;
using SmartWorkshop.Workshop.Application.UseCases.Supplies.Get;
using SmartWorkshop.Workshop.Application.UseCases.Supplies.GetAll;
using SmartWorkshop.Workshop.Domain.Entities;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace SmartWorkshop.Workshop.Api.Controllers;

/// <summary>
/// Controller for managing supplies (insumos/materiais)
/// </summary>
[ApiController]
[Route("api/v1/[controller]")]
public class SuppliesController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<SuppliesController> _logger;

    public SuppliesController(IMediator mediator, ILogger<SuppliesController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    /// <summary>
    /// Get all supplies
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<Supply>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var query = new GetAllSuppliesQuery();
        var result = await _mediator.Send(query, cancellationToken);

        if (!result.IsSuccess)
        {
            return StatusCode((int)result.StatusCode, new ProblemDetails
            {
                Title = "Error retrieving supplies",
                Detail = string.Join(", ", result.Reasons.Select(r => r.Message)),
                Status = (int)result.StatusCode
            });
        }

        return Ok(result.Data);
    }

    /// <summary>
    /// Get supply by ID
    /// </summary>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(Supply), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.NotFound)]
    public async Task<IActionResult> GetById([FromRoute][Required] Guid id, CancellationToken cancellationToken)
    {
        var query = new GetSupplyByIdQuery(id);
        var result = await _mediator.Send(query, cancellationToken);

        if (!result.IsSuccess)
        {
            return StatusCode((int)result.StatusCode, new ProblemDetails
            {
                Title = "Error retrieving supply",
                Detail = string.Join(", ", result.Reasons.Select(r => r.Message)),
                Status = (int)result.StatusCode
            });
        }

        return Ok(result.Data);
    }

    /// <summary>
    /// Create a new supply
    /// </summary>
    [HttpPost]
    [ProducesResponseType(typeof(Supply), (int)HttpStatusCode.Created)]
    [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> Create([FromBody][Required] CreateSupplyCommand command, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(command, cancellationToken);

        if (!result.IsSuccess)
        {
            return StatusCode((int)result.StatusCode, new ProblemDetails
            {
                Title = "Error creating supply",
                Detail = string.Join(", ", result.Reasons.Select(r => r.Message)),
                Status = (int)result.StatusCode
            });
        }

        return CreatedAtAction(nameof(GetById), new { id = result.Data.Id }, result.Data);
    }
}
