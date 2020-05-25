using WebMarket.Model.Api;
using WebMarket.Model.Dto;
using WebMarket.Model.Domain;
using WebMarket.Model.Data;
using System.Collections.Generic;

namespace WebMarket.Pipeline.CartItemLogic
{
    public class CartItemParameters : ILogicPipelineObject
    {
        public bool Abort { get; set; }

        public ApiResponse Response { get; set; }

        public int PageSize { get; set; }

        public int Page { get; set; }

        public long OrderId { get; set; }

        public long ProductId { get; set; }

        public Product Product { get; set; }

        public Order Order { get; set; }

        public CartItemDto ModelDto { get; set; }

        public OrderProduct OrderProduct { get; set; }

        public CartItem Model { get; set; }

        public List<CartItem> Models { get; set; }
    }
}
