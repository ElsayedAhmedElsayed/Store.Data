﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Repository.Specifications.ProductSpecs
{
    public class ProductSpecification
    {
        public int? BrandId { get; set; }

        public int? TypeId { get; set; }

        public string?  Sort { get; set; }

        public int PageIndex { get; set; }

        public const int MAXPAGESIZE = 50;

        private int _pageSize;

        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = ( value > MAXPAGESIZE ) ? int.MaxValue : value;
        }

        public string? _search;

        public string? Search
        {
            get => _search;
            set => _search = value?.Trim().ToLower();
        }
    }
}
