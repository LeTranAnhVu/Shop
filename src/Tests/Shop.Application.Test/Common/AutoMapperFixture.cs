using System;
using System.Reflection;
using AutoMapper;
using MediatR;
using Shop.Application.Common;

namespace Shop.Application.Test.Common;

/// <summary>
/// Reference link: https://xunit.net/docs/shared-context
/// </summary>
public class AutoMapperFixture : IDisposable
{
    public readonly IMapper Mapper;

    public AutoMapperFixture()
    {
        var mapperConfiguration = new MapperConfiguration(c =>
            c.AddProfile(new ApplicationProfile()));

        Mapper = mapperConfiguration.CreateMapper();
    }

    public void Dispose()
    {
    }
}