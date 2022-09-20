using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FluentAssertions;
using Moq;
using Moq.EntityFrameworkCore;
using Shop.Application.Features.ProductFeature.Dtos;
using Shop.Application.Features.ProductFeature.Models;
using Shop.Application.Features.ProductFeature.Queries;
using Shop.Application.Interfaces;
using Shop.Application.Test.Common;
using Xunit;

namespace Shop.Application.Test.ProductFeature;

public class GetAllProductsQueryHandlerTest: IClassFixture<AutoMapperFixture>
{
    private readonly Mock<IApplicationDbContext> _mockContext;
    private readonly IMapper _mapper;
    
    private IList<Product> SampleProducts => new List<Product>()
    {
        new (){ Id = 1, Name = "a"},
        new (){ Id = 2, Name = "b"},
        new (){ Id = 3, Name = "c"},
    };

    private IList<ProductResponseDto> SampleProductsResponse =>  new List<ProductResponseDto>()
    {
        new (){ Id = 1, Name = "a"},
        new (){ Id = 2, Name = "b"},
        new (){ Id = 3, Name = "c"},
    };

    public GetAllProductsQueryHandlerTest(AutoMapperFixture autoMapperFixture)
    {
        _mockContext = new Mock<IApplicationDbContext>();
        _mapper = autoMapperFixture.Mapper;
    }

    
    [Fact]
    public async Task GetAllProductsHandler_Returns_Products()
    {
        // Setup
        _mockContext.Setup(x => x.Products).ReturnsDbSet(SampleProducts);
        
        // Run
        var products = await (new GetAllProductsHandler(_mockContext.Object, _mapper))
            .Handle(new GetAllProductsQuery(), CancellationToken.None);
        
        // Assert
        products.Should().NotBeEmpty()
            .And.HaveCount(SampleProductsResponse.Count)
            .And.BeEquivalentTo(SampleProductsResponse);
    }
    
    [Fact]
    public async Task GetAllProductsHandler_Returns_NoProducts()
    {
        // Setup
        _mockContext.Setup(x => x.Products).ReturnsDbSet(new List<Product>());
        
        // Run
        var products = await (new GetAllProductsHandler(_mockContext.Object, _mapper))
            .Handle(new GetAllProductsQuery(), CancellationToken.None);
        
        // Assert
        products.Should().BeEmpty()
            .And.HaveCount(0);
    }

}