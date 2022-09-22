using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation.TestHelper;
using Moq;
using Moq.EntityFrameworkCore;
using Shop.Application.Common.Interfaces;
using Shop.Application.Features.ProductFeature.Commands;
using Shop.Application.Features.ProductFeature.Models;
using Shop.Application.Features.ProductFeature.Validators;
using Shop.Application.Test.Common;
using Xunit;

namespace Shop.Application.Test.ProductFeature;

public class DeleteProductCommandValidatorTest
{
    private readonly Mock<IApplicationDbContext> _mockContext;
    private DeleteProductCommandValidator _validator;

    public DeleteProductCommandValidatorTest()
    {
        _mockContext = new Mock<IApplicationDbContext>();
        _validator = new DeleteProductCommandValidator(_mockContext.Object);
    }
    
    [Fact]
    public async Task DeleteProductCommandValidator_Passed_When_Product_Is_Found()
    {
        //
        var existedProduct = new Product()
        {
            Id = 1,
            Name = "b"
        };
        
        _mockContext.Setup(c => c.Products).ReturnsDbSet(new List<Product>{existedProduct});

        var command = new DeleteProductCommand(1);
        
        // 
        var result = await _validator.TestValidateAsync(command);
        
        //
        result.ShouldNotHaveValidationErrorFor(command => command.Id);
    }
    
    [Fact]
    public async Task DeleteProductCommandValidator_Throw_Error_When_Product_Not_Found()
    {
        // 
        var existedProduct = new Product()
        {
            Id = 2,
            Name = "b"
        };
        
        _mockContext.Setup(c => c.Products).ReturnsDbSet(new List<Product>{existedProduct});
        var command = new DeleteProductCommand(1);
        
        // 
        var result = await _validator.TestValidateAsync(command);
        
        //
        result.ShouldHaveValidationErrorFor(command => command.Id);
    }
}