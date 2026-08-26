using System;
using System.Collections.Generic;
using System.Text;

namespace FactoryFlow.Domain.Products;


    public readonly record struct ProductId(Guid Value)
    {
        public static ProductId New() 
        {
            return new ProductId(Guid.NewGuid());
        }

    }
