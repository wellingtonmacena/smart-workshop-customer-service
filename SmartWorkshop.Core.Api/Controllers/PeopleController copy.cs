using MediatR;
using Microsoft.AspNetCore.Mvc;
using SmartWorkshop.Workshop.Application.UseCases.People.Create;
using SmartWorkshop.Workshop.Application.UseCases.People.Get;
using SmartWorkshop.Workshop.Application.UseCases.People.GetAll;
using SmartWorkshop.Workshop.Domain.Entities;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace SmartWorkshop.Workshop.Api.Controllers;

/// <summary>
/// Controller for managing people (clients and employees)
/// </summary>
[ApiController]
[Route("api/v1/[controller]")]
public class PeopleController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<PeopleController> _logger;

    public PeopleController(IMediator mediator, ILogger<PeopleController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    /// <summary>
    /// Get all people (clients and employees)
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<Person>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var query = new GetAllPeopleQuery();
        var result = await _mediator.Send(query, cancellationToken);

        if (!result.IsSuccess)
        {
            return StatusCode((int)result.StatusCode, new ProblemDetails
            {
                Title = "Error retrieving people",
                Detail = string.Join(", ", result.Reasons.Select(r => r.Message)),
                Status = (int)result.StatusCode
            });
        }

        return Ok(result.Data);
    }

    /// <summary>
    /// Get person by ID
    /// </summary>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(Person), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.NotFound)]
    public async Task<IActionResult> GetById([FromRoute][Required] Guid id, CancellationToken cancellationToken)
    {
        var query = new GetPersonByIdQuery(id);
        var result = await _mediator.Send(query, cancellationToken);

        if (!result.IsSuccess)
        {
            return StatusCode((int)result.StatusCode, new ProblemDetails
            {
                Title = "Error retrieving person",
                Detail = string.Join(", ", result.Reasons.Select(r => r.Message)),
                Status = (int)result.StatusCode
            });
        }

        return Ok(result.Data);
    }

    /// <summary>
    /// Create a new person (client or employee)
    /// </summary>
    [HttpPost]
    [ProducesResponseType(typeof(Person), (int)HttpStatusCode.Created)]
    [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> Create([FromBody][Required] CreatePersonCommand command, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(command, cancellationToken);

        if (!result.IsSuccess)
        {
            return StatusCode((int)result.StatusCode, new ProblemDetails
            {
                Title = "Error creating person",
                Detail = string.Join(", ", result.Reasons.Select(r => r.Message)),
                Status = (int)result.StatusCode
            });
        }

        return CreatedAtAction(nameof(GetById), new { id = result.Data.Id }, result.Data);
    }
}
