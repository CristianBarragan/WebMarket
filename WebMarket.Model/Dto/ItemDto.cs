﻿using System.Collections.Generic;

namespace WebMarket.Model.Dto
{
    public class ItemDto
    {
        public long ItemId { get; set; }

        public string Name { get; set; }

        public int Quantity { get; set; }

        public decimal Price { get; set; }

        public List<long> CategoryIds { get; set; }

        public List<string> CategoryNames { get; set; }

    }
}
