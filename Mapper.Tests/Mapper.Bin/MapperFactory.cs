using System.Collections.Generic;

namespace Mapper.Bin
{
    public class MapperFactory : IMapperFactory
    {
        private IDictionary<string, IMapper> _mappersDictionary;

        public MapperFactory()
        {
            _mappersDictionary = new Dictionary<string, IMapper>();
        }

        public IMapper GetMapper<TSrc, TDest>(TSrc source, TDest destination)
        {
            return null;
        }
    }
}