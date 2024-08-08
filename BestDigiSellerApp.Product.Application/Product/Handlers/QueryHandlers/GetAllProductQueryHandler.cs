using BestDigiSellerApp.Product.Application.Product.Queries.Request;
using BestDigiSellerApp.Product.Entity.Helpers;
using BestDigiSellerApp.Product.Entity;
using FluentResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BestDigiSellerApp.Product.Data.Abstract;
using BestDigiSellerApp.Product.Data.Concrete;

namespace BestDigiSellerApp.Product.Application.Product.Handlers.QueryHandlers
{
    public class GetAllProductQueryHandler : IRequestHandler<GetAllProductQueryRequest, Result<PagedList<ShapedEntity>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISortHelper<BestDigiSellerApp.Product.Entity.Product> _sortHelper;
        private readonly IDataShaper<BestDigiSellerApp.Product.Entity.Product> _dataShaper;
        public GetAllProductQueryHandler(IUnitOfWork unitOfWork, ISortHelper<BestDigiSellerApp.Product.Entity.Product> sortHelper, IDataShaper<BestDigiSellerApp.Product.Entity.Product> dataShaper)
        {
            _unitOfWork = unitOfWork;
            _sortHelper = sortHelper;
            _dataShaper = dataShaper;
        }
        public async Task<Result<PagedList<ShapedEntity>>> Handle(GetAllProductQueryRequest request, CancellationToken cancellationToken)
        {
            var products = _unitOfWork.ProductDal.GetAll(x => x.ProductDetail.Price >= request.MinPrice && request.MaxPrice <= request.MaxPrice);
            SearchByName(ref products, request.Name);
            var sortedProducts = _sortHelper.ApplySort(products, request.OrderBy);
            var shapedProducts = _dataShaper.ShapeData(sortedProducts, request.Fields);

            return Result.Ok(PagedList<ShapedEntity>.ToPagedList(shapedProducts,
         request.PageNumber,
         request.PageSize));
        }
        private void SearchByName(ref IQueryable<BestDigiSellerApp.Product.Entity.Product> products, string productName)
        {
            if (!products.Any() || string.IsNullOrWhiteSpace(productName))
                return;

            if (string.IsNullOrEmpty(productName))
                return;

            products = products.Where(o => o.Name == productName);
        }
    }
}
