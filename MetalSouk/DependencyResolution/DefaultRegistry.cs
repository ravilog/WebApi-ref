// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DefaultRegistry.cs" company="Web Advanced">
// Copyright 2012 Web Advanced (www.webadvanced.com)
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
// http://www.apache.org/licenses/LICENSE-2.0

// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetalSouk.DependencyResolution
{
    using StructureMap.Configuration.DSL;
    using StructureMap.Graph;
    using Domain.Contracts;
    using Domain.Implementations;
    using Data.Contracts;
    using Data.Implementations.UnitOfWorks;

    public class DefaultRegistry : Registry
    {
        #region Constructors and Destructors

        public DefaultRegistry()
        {
            Scan(
                scan =>
                {
                    scan.TheCallingAssembly();
                    scan.WithDefaultConventions();
                });
            //For<IExample>().Use<Example>();

            var connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["StockManagementEntities"].ConnectionString;
            For<IStockManagementUnitOfWorkFactory>().Use<StockManagementUnitOfWorkFactory>().Ctor<string>("connectionString").Is(connectionString);
            For<IAdminService>().Use<AdminService>();
            For<IAnalyzerService>().Use<AnalyzerService>();
            For<ISupplierService>().Use<SupplierService>();
            For<ILoginService>().Use<LoginService>();
            For<ITokenService>().Use<TokenService>();
        }

        #endregion
    }
}