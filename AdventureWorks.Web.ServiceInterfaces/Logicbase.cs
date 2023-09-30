using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventureWorks.Web.ServiceInterfaces
{
    public interface ILogicBase
    {
        public static IMapper Mapper { get; }

    }

    public class LogicBase
    {
        private static IObjectMapper ObjectMapper = new ObjectMapper();

        public LogicBase()
        {
        }

        public static IMapper Mapper = ObjectMapper.Mapper;

    }
}
