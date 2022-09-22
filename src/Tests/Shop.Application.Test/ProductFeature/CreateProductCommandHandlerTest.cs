using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Moq;
using Moq.EntityFrameworkCore;
using Shop.Application.Features.ProductFeature.Commands;
using Shop.Application.Features.ProductFeature.Models;
using Shop.Application.Interfaces;
using Shop.Application.Models.Enums;
using Shop.Application.Test.Common;
using Xunit;

namespace Shop.Application.Test.ProductFeature;

public class CreateProductCommandHandlerTest:  IClassFixture<AutoMapperFixture>
{
    private readonly Mock<IApplicationDbContext> _mockContext;
    private readonly IMapper _mapper;

    public CreateProductCommandHandlerTest(AutoMapperFixture fixture)
    {
        _mockContext = new Mock<IApplicationDbContext>();
        _mockContext.Setup(c => c.Products).ReturnsDbSet(new List<Product>());
        _mapper = fixture.Mapper;
    }
    
    [Fact]
    public async Task CreateProductCommandHandler_Creates_NonExistedProduct_With_ZeroItems()
    {
        // 
        var command = new CreateProductCommand {
            Name = "a",
            NumberOfItems = 0
        };
        
        // 
        var result = await new CreateProductCommandHandler(_mockContext.Object, _mapper).Handle(command, CancellationToken.None);
        
        //
        Assert.Equal(command.Name, result.Name);
        Assert.Equal(command.NumberOfItems, result.NumberOfItems);
        Assert.Equal(ProductStatus.OutOfStock, result.ProductStatus);
    }
    
    [Fact]
    public async Task CreateProductCommandHandler_Creates_NonExistedProduct_With_Items()
    {
        // 
        var command = new CreateProductCommand {
            Name = "a",
            NumberOfItems = 1
        };
        
        // 
        var result = await new CreateProductCommandHandler(_mockContext.Object, _mapper).Handle(command, CancellationToken.None);
        //

        Assert.Equal(command.Name, result.Name);
        Assert.Equal(command.NumberOfItems, result.NumberOfItems);
        Assert.Equal(ProductStatus.InStock, result.ProductStatus);
    }
}