﻿using AutoMapper;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using WebMarket.Model.Api;
using WebMarket.Model.Domain;
using WebMarket.Model.Dto;

namespace WebMarket.Pipeline.CartItemLogic.Process.GetAll
{
    public class MapDtoProcess : IProcess<CartItemParameters>
    {
        private readonly ILogger<CartItemParameters> logger;
        private readonly IMapper mapper;

        public MapDtoProcess(ILogger<CartItemParameters> logger, IMapper mapper)
        {
            this.logger = logger;
            this.mapper = mapper;
        }

        public Task<CartItemParameters> ExecuteAsync(CartItemParameters parameters)
        {
            parameters.Response = new ApiResponse(HttpStatusCode.OK, "" , mapper.Map<List<CartItem>, List<CartItemDto>>(parameters.Models));
            logger.LogDebug("Cart Item Dto mapped");
            return Task.FromResult(parameters);
        }
    }
}
