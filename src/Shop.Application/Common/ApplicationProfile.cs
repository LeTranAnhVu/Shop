using AutoMapper;
using Shop.Application.Features.ProductFeature.Dtos;
using Shop.Application.Features.ProductFeature.Models;
using Shop.Application.Models;

namespace Shop.Application.Common;

public class ApplicationProfile: Profile
{
    public ApplicationProfile()
    {
        // Source -> Destination
        CreateMap<CreateProductRequestDto, Product>();
        CreateMap<UpdateProductRequestDto, Product>();
        CreateMap<Product, ProductResponseDto>();
    } 
}