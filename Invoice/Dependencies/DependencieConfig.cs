using Invoice.Business.Business;
using Invoice.Business.IBusiness;
using Invoice.Repository.IRepository;
using Invoice.Repository.Repository;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Invoice.Dependencies
{
    public static class DependencieConfig
    {
        public static void SolveDependency(IServiceCollection services)
        {
            services.AddScoped<IInvoiceBusiness, InvoiceBusiness>();
            services.AddScoped<IInvoiceRepository, InvoiceRepository>();
            services.AddScoped<IHomeBusiness, HomeBusiness>();
            services.AddScoped<IHomeRepository, HomeRepository>();
        }
    }
}
