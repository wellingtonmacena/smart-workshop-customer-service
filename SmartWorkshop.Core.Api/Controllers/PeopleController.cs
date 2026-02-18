using MediatR;
using Microsoft.AspNetCore.Mvc;
using SmartWorkshop.Core.Application.Commands.CreatePerson;
using SmartWorkshop.Core.Application.Queries.GetAllPeople;
using SmartWorkshop.Core.Application.Queries.GetPersonById;

namespace SmartWorkshop.Core.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PeopleController : ControllerBase
{
    private readonly IMediator _mediator;

    public PeopleController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Lista todas as pessoas (clientes e funcionários)
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var query = new GetAllPeopleQuery();
        var result = await _mediator.Send(query, cancellationToken);
        return Ok(result);
    }

    /// <summary>
    /// Busca uma pessoa por ID
    /// </summary>
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
    {
        var query = new GetPersonByIdQuery(id);
        var result = await _mediator.Send(query, cancellationToken);

        if (result == null)
            return NotFound(new { message = "Pessoa não encontrada" });

        return Ok(result);
    }

    /// <summary>
    /// Cria uma nova pessoa (cliente ou funcionário)
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreatePersonCommand command, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(command, cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }
}
