using WebMarket.Model.Api;
using WebMarket.Model.Dto;
using WebMarket.Model.Data;
using System.Collections.Generic;

namespace WebMarket.Pipeline.CategoryLogic
{
    public class CategoryParameters : ILogicPipelineObject
    {
        public bool Abort { get; set; }

        public ApiResponse Response { get; set; }

        public int PageSize { get; set; }
        
        public int Page { get; set; }

        public long CategoryId { get; set; }

        public string Name { get; set; }

        public Category Model { get; set; }

        public CategoryDto ModelDto { get; set; }
        
        public List<Category> Models { get; set; }
        
    }
}
