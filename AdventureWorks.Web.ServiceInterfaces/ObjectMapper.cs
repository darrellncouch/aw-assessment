using AutoMapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventureWorks.Web.ServiceInterfaces
{
    public interface IObjectMapper
    {
        public IMapper Mapper { get; set; }
    }

    public class ObjectMapper : IObjectMapper
    {
        public IMapper Mapper { get; set;}

        public ObjectMapper()
        {
            Mapper = Configuration();
        }

        public IMapper Configuration()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.ShouldMapProperty = p => p.GetMethod.IsPublic || p.GetMethod.IsAssembly;
                cfg.AddProfile<ProductProfile>();
            });

            return config.CreateMapper();
        }
    }
}
