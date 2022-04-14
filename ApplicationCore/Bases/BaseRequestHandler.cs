using ApplicationCore.Interfaces;
using AutoMapper;

namespace ApplicationCore.Bases
{
    public abstract class BaseRequestHandler
    {
        public BaseRequestHandler(IBaseRepository baseRepository, IMapper mapper)
        {
            BaseRepository = baseRepository;
            Mapper = mapper;
        }

        protected IBaseRepository BaseRepository { get; }

        protected IMapper Mapper { get; }
    }
}