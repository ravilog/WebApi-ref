using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using System.Threading.Tasks;
using Domain.Contracts;
using Domain.Entities;
using Data.Contracts;
using Common.Library;
using Data.Entities;

namespace Domain.Implementations
{
    public class AnalyzerService : IAnalyzerService
    {
        private IStockManagementUnitOfWorkFactory unitOfWorkFactory;

        public AnalyzerService(IStockManagementUnitOfWorkFactory unitOfWorkFactory)
        {
            this.unitOfWorkFactory = unitOfWorkFactory;
        }

        public async Task<CommonResponse> GetProducts()
        {
            using (var uow = this.unitOfWorkFactory.GetUnitOfWork())
            {
                var products = await (from product in uow.Repository<Product>().Query().Select()
                                      join supplierProduct in uow.Repository<SupplierProduct>().Query().Select()
                                      on product.ProductId equals supplierProduct.ProductId
                                      where supplierProduct.AvailableStatus.ToLower() == "a"
                                      select new SupplierProductsDTO
                                      {
                                          Composition = supplierProduct.Composition ?? 0,
                                          ProductName = product.ProductName,
                                          Quantity = supplierProduct.Quantity ?? 0,
                                          SupplierProductId = supplierProduct.Id

                                      }).ToListAsync();

                return CommonResponse.CreateSuccess("Success", "Suc01", products);
            }
        }

        public async Task<CommonResponse> SubmitAnalyzedSample(AnalyserSampleRequest values, int userId)
        {
            using (var uow = this.unitOfWorkFactory.GetUnitOfWork())
            {
                var productSupplierEntity = await uow.Repository<SupplierProduct>().Query().Select().SingleOrDefaultAsync(t => t.Id == values.ProductSupplierId);
                if (productSupplierEntity == null)
                {
                    return CommonResponse.CreateError("Error", "Err02", "Product not found");
                }

                var productAnalyzer = new ProductAnalyzer
                {
                    AnalyzerId = userId,
                    SupplilerProductId = values.ProductSupplierId??1,
                    AnalyzedComposition = values.AnalysedComposition??1,
                    IsDealPlaced = "y"
                };

                uow.Repository<ProductAnalyzer>().Add(productAnalyzer);
                productSupplierEntity.Composition -= productAnalyzer.AnalyzedComposition;
                var response = uow.SaveChanges();
                if(response.Response.status == false)
                {
                    return CommonResponse.CreateError("Error occured while adding analysing composition.", "ERR001", "");
                }

                return CommonResponse.CreateSuccess("Success", "SUC001", "");
            }
        }
    }
}
