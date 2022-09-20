using AutoMapper;
using Shop.Application.Features.ProductFeature.Commands;
using Shop.Application.Features.ProductFeature.Dtos;
using Shop.Application.Features.ProductFeature.Models;

namespace Shop.Application.Common;

public class ApplicationProfile: Profile
{
    public ApplicationProfile()
    {
        // Source -> Destination
        CreateMap<CreateProductCommand, Product>();
        CreateMap<UpdateProductCommand, Product>();
        CreateMap<Product, ProductResponseDto>();
    } 
}