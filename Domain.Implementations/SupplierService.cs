using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Data.Entities;
using System.Threading.Tasks;
using Common.Library;
using Domain.Contracts;
using Data.Contracts;
using Domain.Entities;
using System.Data.Entity;

namespace Domain.Implementations
{
    public class SupplierService : ISupplierService
    {
        private IStockManagementUnitOfWorkFactory unitOfWorkFactory;

        public SupplierService(IStockManagementUnitOfWorkFactory unitOfWorkFactory)
        {
            this.unitOfWorkFactory = unitOfWorkFactory;
        }

        public async Task<CommonResponse> AddProduct(AddProductRequest productValues, int userId)
        {
            using (var uow = this.unitOfWorkFactory.GetUnitOfWork())
            {
                var product = new Product
                {
                    ProductName = productValues.ProductName
                };

                uow.Repository<Product>().Add(product);
                var responseProduct = uow.SaveChanges();
                if(responseProduct.Response.status == false)
                {
                    return CommonResponse.CreateError("Error occured while adding supplier products.", "ERR001", "");
                }

                var supplierProduct = new SupplierProduct
                {
                    ProductId = product.ProductId,
                    Quantity = productValues.Quantity,
                    Composition = productValues.Composition,
                    SupplierId = userId,
                    AvailableStatus = "a"
                };
                uow.Repository<SupplierProduct>().Add(supplierProduct);

                var response= uow.SaveChanges();
                if (response.Response.status == false)
                {
                    return CommonResponse.CreateError("Error occured while adding supplier products.", "ERR001", "");
                }
               
                return CommonResponse.CreateSuccess("Success", "SUC001", "");
            }
        }

        public async Task<CommonResponse> GetSupplierTransactionDetails(int userId)
        {
            using (var uow = this.unitOfWorkFactory.GetUnitOfWork())
            {
                var transactionDetails = await(from pTransaction in uow.Repository<ProductTransaction>().Query().Select()
                                               join pAnalyzer in uow.Repository<ProductAnalyzer>().Query().Select()
                                               on pTransaction.ProductAnalyzerId equals pAnalyzer.Id
                                               join pSupplier in uow.Repository<SupplierProduct>().Query().Select()
                                               on pAnalyzer.SupplilerProductId equals pSupplier.ProductId
                                               join user in uow.Repository<User>().Query().Select()
                                               on pSupplier.SupplierId equals user.Id
                                               join product in uow.Repository<Product>().Query().Select()
                                               on pSupplier.ProductId equals product.ProductId
                                               where pTransaction.Deal != null && pTransaction.Deal != "" && user.Id == userId
                                               select new ProductsTransactionDetailsDTO
                                               {
                                                   AnalyzedComposition = pAnalyzer.AnalyzedComposition ?? 0,
                                                   Composition = pSupplier.Composition ?? 0,
                                                   DealStatus = pTransaction.Deal,
                                                   ProductName = product.ProductName,
                                                   ProductTransactionId = pTransaction.Id,
                                               }).ToListAsync();
                return CommonResponse.CreateSuccess("Success", "Suc01", transactionDetails);

            }
        }
    }
}
