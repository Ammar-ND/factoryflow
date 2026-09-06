using FactoryFlow.Api.Contracts.ProductionOrders;
using FactoryFlow.Application.ProductionOrders.CancelProductionOrder;
using FactoryFlow.Application.ProductionOrders.CompleteProductionOrder;
using FactoryFlow.Application.ProductionOrders.CreateProductionOrder;
using FactoryFlow.Application.ProductionOrders.ScheduleProductionOrder;
using FactoryFlow.Application.ProductionOrders.StartProductionOrder;
using FactoryFlow.Domain.Products;
using FactoryFlow.Domain.ProductionOrders;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FactoryFlow.Api.Controllers;

[Route("api/production-orders")]
public sealed class ProductionOrdersController : ApiController
{
    private readonly ISender _sender;

    public ProductionOrdersController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        CreateProductionOrderRequest request,
        CancellationToken cancellationToken)
    {
        var command = new CreateProductionOrderCommand(
            new ProductId(request.ProductId),
            request.Quantity);

        var result = await _sender.Send(
            command,
            cancellationToken);

        if (result.IsFailure)
        {
            return BadRequest(new
            {
                error = result.Error
            });
        }

        return StatusCode(
            StatusCodes.Status201Created,
            new
            {
                id = result.Value
            });
    }

    [HttpPost("{id:guid}/schedule")]
    public async Task<IActionResult> Schedule(
        Guid id,
        CancellationToken cancellationToken)
    {
        var command = new ScheduleProductionOrderCommand(
            new ProductionOrderId(id));

        var result = await _sender.Send(
            command,
            cancellationToken);

        if (result.IsFailure)
        {
            return BadRequest(new
            {
                error = result.Error
            });
        }

        return NoContent();
    }

    [HttpPost("{id:guid}/start")]
    public async Task<IActionResult> Start(
        Guid id,
        CancellationToken cancellationToken)
    {
        var command = new StartProductionOrderCommand(
            new ProductionOrderId(id));

        var result = await _sender.Send(
            command,
            cancellationToken);

        if (result.IsFailure)
        {
            return BadRequest(new
            {
                error = result.Error
            });
        }

        return NoContent();
    }

    [HttpPost("{id:guid}/complete")]
    public async Task<IActionResult> Complete(
        Guid id,
        CancellationToken cancellationToken)
    {
        var command = new CompleteProductionOrderCommand(
            new ProductionOrderId(id));

        var result = await _sender.Send(
            command,
            cancellationToken);

        if (result.IsFailure)
        {
            return BadRequest(new
            {
                error = result.Error
            });
        }

        return NoContent();
    }

    [HttpPost("{id:guid}/cancel")]
    public async Task<IActionResult> Cancel(
        Guid id,
        CancellationToken cancellationToken)
    {
        var command = new CancelProductionOrderCommand(
            new ProductionOrderId(id));

        var result = await _sender.Send(
            command,
            cancellationToken);

        if (result.IsFailure)
        {
            return BadRequest(new
            {
                error = result.Error
            });
        }

        return NoContent();
    }
}