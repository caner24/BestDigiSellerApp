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

namespace BestDigiSellerApp.Product.Application.Product.Handlers.QueryHandlers
{
    public class GetAllCategoryQueryHandler : IRequestHandler<GetAllCategoryQueryRequest, Result<PagedList<ShapedEntity>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISortHelper<Category> _sortHelper;
        private readonly IDataShaper<Category> _dataShaper;
        public GetAllCategoryQueryHandler(IUnitOfWork unitOfWork, ISortHelper<Category> sortHelper, IDataShaper<Category> dataShaper)
        {
            _unitOfWork = unitOfWork;
            _sortHelper = sortHelper;
            _dataShaper = dataShaper;
        }
        public async Task<Result<PagedList<ShapedEntity>>> Handle(GetAllCategoryQueryRequest request, CancellationToken cancellationToken)
        {
            var products = _unitOfWork.CategoryDal.GetAll();
            SearchByName(ref products, request.Name);
            SearchByTag(ref products, request.Name);
            var sortedProducts = _sortHelper.ApplySort(products, request.OrderBy);
            var shapedProducts = _dataShaper.ShapeData(sortedProducts, request.Fields);

            return Result.Ok(PagedList<ShapedEntity>.ToPagedList(shapedProducts,
              request.PageNumber,
              request.PageSize));
        }
        private void SearchByName(ref IQueryable<Category> categories, string categoryNames)
        {
            if (!categories.Any() || string.IsNullOrWhiteSpace(categoryNames))
                return;

            if (string.IsNullOrEmpty(categoryNames))
                return;

            categories = categories.Where(o => o.Name == categoryNames);
        }
        private void SearchByTag(ref IQueryable<Category> categories, string categoryTag)
        {
            if (!categories.Any() || string.IsNullOrWhiteSpace(categoryTag))
                return;

            if (string.IsNullOrEmpty(categoryTag))
                return;

            categories = categories.Where(o => o.Tag == categoryTag);
        }
    }
}
