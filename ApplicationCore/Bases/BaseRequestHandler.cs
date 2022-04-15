using ApplicationCore.Interfaces.Repositories;
using AutoMapper;

namespace ApplicationCore.Bases
{
    public abstract class BaseRequestHandler
    {
        public BaseRequestHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            Mapper = mapper;
            UnitOfWork = unitOfWork;
        }

        protected IMapper Mapper { get; }

        protected IUnitOfWork UnitOfWork { get; }
    }
}