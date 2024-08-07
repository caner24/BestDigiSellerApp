using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestDigiSellerApp.Product.Entity.Results
{
    public class PhotosCouldNotAddedResult:Error
    {
        public PhotosCouldNotAddedResult():base("Product photos could not be added")
        {
            
        }

    }
}
