using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestDigiSellerApp.Product.Entity.Results
{
    public class CategoryNotFoundResult : Error
    {
        public CategoryNotFoundResult(Guid categoryId) : base($"The category was not found : {categoryId}")
        {

        }

    }
}
