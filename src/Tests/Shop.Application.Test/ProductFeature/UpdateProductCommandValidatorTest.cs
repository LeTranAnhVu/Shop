using System.Collections.Generic;
using System.Threading.Tasks;
using FluentValidation.TestHelper;
using Moq;
using Moq.EntityFrameworkCore;
using Shop.Application.Features.ProductFeature.Commands;
using Shop.Application.Features.ProductFeature.Models;
using Shop.Application.Features.ProductFeature.Validators;
using Shop.Application.Interfaces;
using Shop.Application.Models.Enums;
using Xunit;

namespace Shop.Application.Test.ProductFeature;

public class UpdateProductCommandValidatorTest
{
    private readonly Mock<IApplicationDbContext> _mockContext;
    private UpdateProductCommandValidator _validator;

    public UpdateProductCommandValidatorTest()
    {
        _mockContext = new Mock<IApplicationDbContext>();
        _validator = new UpdateProductCommandValidator(_mockContext.Object);
    }
    
    [Theory]
    [InlineData(0, ProductStatus.Unknown)]
    [InlineData(1, ProductStatus.InStock)]
    [InlineData(50, ProductStatus.OutOfStock)]
    public async Task UpdateProductCommandValidator_Passed_When_InputData_Is_Valid(int numberOfItems, ProductStatus status)
    {
        //
        var existedProduct = new Product()
        {
            Id = 1,
            Name = "b"
        };
        
        _mockContext.Setup(c => c.Products).ReturnsDbSet(new List<Product>{existedProduct});

        var command = new UpdateProductCommand {
            Id = 1,
            Name = "a",
            NumberOfItems = numberOfItems,
            ProductStatus = status
        };
        
        // 
        var result = await _validator.TestValidateAsync(command);
        
        //
        result.ShouldNotHaveAnyValidationErrors();
    }
    
    [Fact]
    public async Task UpdateProductCommandValidator_Throw_Errors()
    {
        //
        var existedProduct = new Product()
        {
            Id = 2,
            Name = "b"
        };
        _mockContext.Setup(c => c.Products).ReturnsDbSet(new List<Product>{existedProduct});

        var command = new UpdateProductCommand {
            Id = 1, // Non existed id
            Name = "b", // Changed name is existed
            NumberOfItems = -1, // Number of Items is < 0
        };
        
        // 
        var result = await _validator.TestValidateAsync(command);
        
        //
        result.ShouldHaveValidationErrorFor(command => command.Id);
        result.ShouldHaveValidationErrorFor(command => command.Name);
        result.ShouldHaveValidationErrorFor(command => command.NumberOfItems);
    }
}