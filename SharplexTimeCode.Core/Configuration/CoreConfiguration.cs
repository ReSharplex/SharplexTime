﻿using Microsoft.Extensions.DependencyInjection;
using SharplexTimeCode.Core.Repositories;
using SharplexTimeCode.Core.Services;

namespace SharplexTimeCode.Core.Configuration;

public static class CoreConfiguration
{
    public static void ConfigureCore(this IServiceCollection collection)
    {
        collection.AddSingleton<IBookingRepository, BookingRepository>();
        collection.AddSingleton<IBookingTypeRepository, BookingTypeRepository>();
        
        collection.AddSingleton<IBookingTypeService, BookingTypeService>();
    }
}