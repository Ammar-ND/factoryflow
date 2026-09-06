using FactoryFlow.Api.Contracts.Products;
using FactoryFlow.Application.Products.CreateProduct;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using FactoryFlow.Application.Products.GetProductById;
using FactoryFlow.Domain.Products;

namespace FactoryFlow.Api.Controllers;

[Route("api/products")]
public sealed class ProductsController : ApiController
{
    private readonly ISender _sender;

    public ProductsController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        CreateProductRequest request,
        CancellationToken cancellationToken)
    {
        var command = new CreateProductCommand(
            request.Name,
            request.Code);

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

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(
    Guid id,
    CancellationToken cancellationToken)
    {
        var query = new GetProductByIdQuery(
            new ProductId(id));

        var result = await _sender.Send(
            query,
            cancellationToken);

        if (result.IsFailure)
        {
            return NotFound(new
            {
                error = result.Error
            });
        }

        return Ok(result.Value);
    }
}