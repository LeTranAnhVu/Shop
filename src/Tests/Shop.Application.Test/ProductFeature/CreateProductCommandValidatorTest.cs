using System.Collections.Generic;
using System.Threading.Tasks;
using FluentValidation.TestHelper;
using Moq;
using Moq.EntityFrameworkCore;
using Shop.Application.Features.ProductFeature.Commands;
using Shop.Application.Features.ProductFeature.Models;
using Shop.Application.Features.ProductFeature.Validators;
using Shop.Application.Interfaces;
using Shop.Application.Test.Common;
using Xunit;

namespace Shop.Application.Test.ProductFeature;

public class CreateProductCommandValidatorTest:  IClassFixture<AutoMapperFixture>
{
    private readonly Mock<IApplicationDbContext> _mockContext;
    private CreateProductCommandValidator _validator;

    public CreateProductCommandValidatorTest()
    {
        _mockContext = new Mock<IApplicationDbContext>();
        _validator = new CreateProductCommandValidator(_mockContext.Object);
    }
    
    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    [InlineData(50)]
    public async Task CreateProductCommandValidator_Passed_When_InputData_Is_Valid(int numberOfItems)
    {
        //
        var existedProduct = new Product()
        {
            Id = 1,
            Name = "b"
        };
        
        _mockContext.Setup(c => c.Products).ReturnsDbSet(new List<Product>{existedProduct});

        var command = new CreateProductCommand {
            Name = "a",
            NumberOfItems = numberOfItems
        };
        
        // 
        var result = await _validator.TestValidateAsync(command);
        
        //
        result.ShouldNotHaveAnyValidationErrors();
    }
    
    [Fact]
    public async Task CreateProductCommandValidator_Throw_Error_When_NumberIfItems_IsNot_GreaterThanOrEqualTo_Zero()
    {
        // 
        _mockContext.Setup(c => c.Products).ReturnsDbSet(new List<Product>());

        var command = new CreateProductCommand {
            Name = "a",
            NumberOfItems = -1
        };
        
        // 
        var result = await _validator.TestValidateAsync(command);
        
        //
        result.ShouldHaveValidationErrorFor(command => command.NumberOfItems);
    }

    [Fact]
    public async Task CreateProductCommandValidator_Throw_Error_When_Name_Is_Exited()
    {
        // 
        var existedProduct = new Product()
        {
            Id = 1,
            Name = "a"
        };
        
        _mockContext.Setup(c => c.Products).ReturnsDbSet(new List<Product>{existedProduct});

        var command = new CreateProductCommand {
            Name = "a",
            NumberOfItems = 0
        };
        
        // 
        var result = await _validator.TestValidateAsync(command);
        
        //
        result.ShouldHaveValidationErrorFor(command => command.Name);
    }
}