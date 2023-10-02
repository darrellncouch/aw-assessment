using AutoMapper;

namespace AdventureWorks.Web.ServiceInterfaces
{
    public interface ILogicBase
    {
        public static IMapper Mapper { get; set; } = default!;

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
