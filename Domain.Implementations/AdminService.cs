using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Contracts;
using Domain.Entities;
using Data.Entities;
using Data.Contracts;
using Common.Library;
using System.Data.Entity;
using Domain.Entities;
using Common.Library.Request;

namespace Domain.Implementations
{
    public class AdminService : IAdminService
    {
        private IStockManagementUnitOfWorkFactory unitOfWorkFactory;
        public AdminService(IStockManagementUnitOfWorkFactory unitOfWorkFactory)
        {
            this.unitOfWorkFactory = unitOfWorkFactory;
        }

        public async Task<CommonResponse> AddUser(AddUserRequest newUserValues)
        {
            using (var uow = this.unitOfWorkFactory.GetUnitOfWork())
            {
                var user = await uow.Repository<User>().Query().Select().FirstOrDefaultAsync(a=> a.UserName == newUserValues.userName);
                if(user != null)
                {
                    return CommonResponse.CreateError("Username is already exits.", "ERR001", "");
                }

                var newUser = new User
                {
                    FullName = newUserValues.name,
                    Country = newUserValues.country,
                    EmailId = newUserValues.emailId,
                    State = newUserValues.state,
                    PhoneNumber = newUserValues.phoneNumber,
                    UserPassword = newUserValues.password,
                    UserName = newUserValues.userName,
                    UserRole = newUserValues.userRole,
                    City = newUserValues.city
                };
                
                uow.Repository<User>().Add(newUser);
                var response = uow.SaveChanges();
                if(response.Response.status == false)
                {
                    return CommonResponse.CreateError("Error occured while adding user.", "ERR001", "");
                }

                return CommonResponse.CreateSuccess("User added successfully.", "SUCC001", "");
            }
        }

        public async Task<CommonResponse> DealDetails()
        {
            using (var uow = this.unitOfWorkFactory.GetUnitOfWork())
            {
                var dealDetails = await (from user in uow.Repository<User>().Query().Select()
                                         join sproduct in uow.Repository<SupplierProduct>().Query().Select()
                                         on user.Id equals sproduct.SupplierId
                                         join product in uow.Repository<Product>().Query().Select()
                                         on sproduct.ProductId equals product.ProductId
                                         join panalyzer in uow.Repository<ProductAnalyzer>().Query().Select()
                                         on sproduct.Id equals panalyzer.SupplilerProductId
                                         select new DealsDetailsDTO
                                         {
                                             AnalyzedComposition = panalyzer.AnalyzedComposition ?? 0,
                                             Composition = sproduct.Composition ?? 0,
                                             ProductAnalyzerId = panalyzer.Id,
                                             ProductName = product.ProductName,
                                             Quantity = sproduct.Quantity ?? 0,
                                             SupplierName = user.FullName
                                         }).ToListAsync();
                return CommonResponse.CreateSuccess("success", "Suc01", dealDetails);
            }
        }

        public async Task<CommonResponse> DecideTheDeal(MakeDealRequest values)
        {
            using (var uow = this.unitOfWorkFactory.GetUnitOfWork())
            {
                var transactionEntity = new ProductTransaction
                {
                    ProductAnalyzerId = values.productAnalyzeId,
                    Deal = values.DealStatus
                };
                uow.Repository<ProductTransaction>().Add(transactionEntity);
                uow.SaveChanges();
                return CommonResponse.CreateSuccess("success", "Suc01", transactionEntity);
            }
        }

        public async Task<CommonResponse> DisableUser(int userId)
        {
            using (var uow = this.unitOfWorkFactory.GetUnitOfWork())
            {
                var user = await uow.Repository<User>().Query().Select().SingleOrDefaultAsync(t => t.Id == userId);
                if (user != null)
                {
                    return CommonResponse.CreateError("Error", "Err02", "Uer not found");
                }
                uow.Repository<User>().Delete(user);
                return CommonResponse.CreateSuccess("Success", "Suc01", "User Removed");
            }
        }

        public async Task<CommonResponse> GetAllAgents()
        {
            using (var uow = this.unitOfWorkFactory.GetUnitOfWork())
            {
                var agents = await uow.Repository<User>().Query().Select().Where(t => t.UserRole == (int)Roles.Agent).Select(j => new UserDetail
                {
                    country = j.Country,
                    emailId = j.EmailId,
                    name = j.FullName,
                    phoneNumber= j.PhoneNumber,
                    state = j.State,
                    userName = j.UserName,
                    userId = j.Id,
                    city =j.City
                }).ToListAsync();

                return CommonResponse.CreateSuccess("Success", "Suc01", agents);
            }
        }

        public async Task<CommonResponse> GetAllAnalyzers()
        {
            using (var uow = this.unitOfWorkFactory.GetUnitOfWork())
            {
                var analyzers = await uow.Repository<User>().Query().Select().Where(t => t.UserRole == (int)Roles.Analyzer).Select(j => new UserDetail
                {
                    country = j.Country,
                    emailId = j.EmailId,
                    name = j.FullName,
                    phoneNumber = j.PhoneNumber,
                    state = j.State,
                    userName = j.UserName,
                    userId = j.Id,
                    city= j.City
                    
                }).ToListAsync();

                return CommonResponse.CreateSuccess("success", "Suc01", analyzers);
            }
        }

        public async Task<CommonResponse> GetAllSuppliers()
        {
            using (var uow = this.unitOfWorkFactory.GetUnitOfWork())
            {
                var suppliers = await uow.Repository<User>().Query().Select().Where(t => t.UserRole == (int)Roles.Supplier).Select(j => new UserDetail
                {
                    country = j.Country,
                    emailId = j.EmailId,
                    name = j.FullName,
                    phoneNumber = j.PhoneNumber,
                    state = j.State,
                    userName = j.UserName,
                    userId = j.Id,
                    city= j.City

                }).ToListAsync();

                return CommonResponse.CreateSuccess("success", "Suc01", suppliers);
            }
        }

        public async Task<CommonResponse> GetDealTransactionDetails()
        {
            using (var uow = this.unitOfWorkFactory.GetUnitOfWork())
            {
                var transactionDetails = await (from pTransaction in uow.Repository<ProductTransaction>().Query().Select()
                                                join pAnalyzer in uow.Repository<ProductAnalyzer>().Query().Select()
                                                on pTransaction.ProductAnalyzerId equals pAnalyzer.Id
                                                join pSupplier in uow.Repository<SupplierProduct>().Query().Select()
                                                on pAnalyzer.SupplilerProductId equals pSupplier.ProductId
                                                join user in uow.Repository<User>().Query().Select()
                                                on pSupplier.SupplierId equals user.Id
                                                join product in uow.Repository<Product>().Query().Select()
                                                on pSupplier.ProductId equals product.ProductId
                                                where pTransaction.Deal != null && pTransaction.Deal != ""
                                                select new ProductsTransactionDetailsDTO
                                                {
                                                    AnalyzedComposition = pAnalyzer.AnalyzedComposition ?? 0,
                                                    Composition = pSupplier.Composition ?? 0,
                                                    DealStatus = pTransaction.Deal,
                                                    ProductName = product.ProductName,
                                                    ProductTransactionId = pTransaction.Id,
                                                    supplierName = user.FullName
                                                }).ToListAsync();
                return CommonResponse.CreateSuccess("Success", "Suc01", transactionDetails);

            }
        }

        public async Task<CommonResponse> GetUserDetails(int userId)
        {
            using (var uow = this.unitOfWorkFactory.GetUnitOfWork())
            {
                var userDetails = await uow.Repository<User>().Query().Select().Where(t => t.Id == userId).Select(j => new UserDetail
                {
                    country = j.Country,
                    emailId = j.EmailId,
                    name = j.FullName,
                    phoneNumber = j.PhoneNumber,
                    state = j.State,
                    userName = j.UserName,
                    userId = j.Id,
                    city = j.City
                }).SingleOrDefaultAsync();

                return CommonResponse.CreateSuccess("success", "Suc01", userDetails);
            }
        }

        public async Task<CommonResponse> GetUserRoles()
        {
            using (var uow = this.unitOfWorkFactory.GetUnitOfWork())
            {
                var userRoles = await uow.Repository<User>().Query().Select().FirstOrDefaultAsync(t => t.UserRole == 2);
                var roles = new List<UserRolesDTO>();
                roles.Add(new UserRolesDTO
                {
                    roleId = (int)Roles.Admin,
                    roleName = Roles.Admin.ToString()
                });
                roles.Add(new UserRolesDTO
                {
                    roleId = (int)Roles.Agent,
                    roleName = Roles.Agent.ToString()
                });
                roles.Add(new UserRolesDTO
                {
                    roleId = (int)Roles.Analyzer,
                    roleName = Roles.Analyzer.ToString()
                });
                
                if (userRoles != null)
                {
                    roles.Add(new UserRolesDTO
                    {
                        roleId = (int)Roles.Supplier,
                        roleName = Roles.Supplier.ToString()
                    });
                }

                return CommonResponse.CreateSuccess("Success", "SUC001", roles);
            }
        }

        public async Task<CommonResponse> ModifyUser(ModifyUserRequest userDetailValues)
        {
            using (var uow = this.unitOfWorkFactory.GetUnitOfWork())
            {
                var userCount = await uow.Repository<User>().Query().Select().Where(a => a.Id != userDetailValues.userId && a.UserName == userDetailValues.userName).CountAsync();
                if(userCount > 0)
                {
                    return CommonResponse.CreateError("Username is already exits", "ERR001", "");
                }

                var userValues = await uow.Repository<User>().Query().Select().SingleOrDefaultAsync(t => t.Id == userDetailValues.userId);
                if (userValues == null)
                {
                    return CommonResponse.CreateError("Error", "Err02", "User Not found");
                }
                userValues.Country = userDetailValues.country;
                userValues.EmailId = userDetailValues.emailId;
                userValues.FullName = userDetailValues.name;
                userValues.UserName = userDetailValues.userName;
                userValues.UserPassword = userValues.UserPassword;
                userValues.UserRole = userDetailValues.userRole;
                userValues.City = userDetailValues.city;

                var response = uow.SaveChanges();
                if (response.Response.status == false)
                {
                    return CommonResponse.CreateError("Error occured while updating user.", "ERR001", "");
                }

                return CommonResponse.CreateSuccess("User updated successfully.", "SUC002", "");
            }
        }

       

        public async Task<CommonResponse> GetUsers()
        {
            using (var uow = this.unitOfWorkFactory.GetUnitOfWork())
            {
                var users = await uow.Repository<User>().Query().Select(j => new UserDetail
                {
                    country = j.Country,
                    emailId = j.EmailId,
                    name = j.FullName,
                    phoneNumber = j.PhoneNumber,
                    state = j.State,
                    userName = j.UserName,
                    userId = j.Id,
                    city = j.City,
                    userRole = j.UserRole,
                    userRoleName = j.Role.RoleName
                }).ToListAsync();

                return CommonResponse.CreateSuccess("Success", "SUC001", users);
            }
        }
    }
}
