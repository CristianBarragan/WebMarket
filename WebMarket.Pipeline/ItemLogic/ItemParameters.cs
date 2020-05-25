using WebMarket.Model.Api;
using WebMarket.Model.Dto;
using WebMarket.Model.Domain;
using WebMarket.Model.Data;
using System.Collections.Generic;

namespace WebMarket.Pipeline.ItemLogic
{
    public class ItemParameters : ILogicPipelineObject
    {
        public bool Abort { get; set; }

        public ApiResponse Response { get; set; }

        public int PageSize { get; set; }

        public int Page { get; set; }

        public long ItemId { get; set; }

        public string Name { get; set; }

        public Item Model { get; set; }

        public ItemDto ModelDto { get; set; }

        public Product Product { get; set; }

        public List<Category> Categories { get; set; }

        public List<Item> Models { get; set; }

    }
}
