using AutoMapper;
using Manufacturing.Api.Models;
using Manufacturing.Domain.Aggregates;
using Manufacturing.Domain.Configuration;
using StructureMap;

namespace Manufacturing.Api.Common
{
    public static class Bootstrap
    {
        public static IContainer Init()
        {
            var container = IoC();

            SetupDatabase(container);

            return container;
        }

        public static void SetupDatabase(IContainer container)
        {
            container.Configure(x => x.AddRegistry<DomainRegistry>());
        }

        public static IMapper SetupAutoMapper()
        {
            return new MapperConfiguration(x => x.AddProfiles(typeof(Bootstrap).Assembly)).CreateMapper();
        }

        public static IContainer IoC()
        {
            return new Container(x =>
            {
               
                x.For<IMapper>().Singleton().Use(SetupAutoMapper());
            });
        }
    }

    public class WorkOrderMapperProfile:Profile
    {
        public WorkOrderMapperProfile()
        {
            CreateMap<WorkOrder, WorkOrderModel>();
            CreateMap<WorkOrderItem, WorkOrderItemModel>();
        }
    }
}