﻿using System.Threading.Tasks;
using WebMarket.Model.Api;

namespace WebMarket.Pipeline.CategoryLogic
{
    public interface IDeleteCategoryPipeline
    {
        Task<ApiResponse> ExecutePipeline(long categoryId);

        Task<ApiResponse> ExecutePipeline(string name);
    }
}
