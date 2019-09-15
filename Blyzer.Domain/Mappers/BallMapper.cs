using AutoMapper;
using Blyzer.Domain.Dtos;
using Blyzer.Domain.Entities;

namespace Blyzer.Domain.Mappers
{
    public class BallMapper : Profile
    {
        public BallMapper()
        {
            CreateMap<Ball, BallDto>();
        }
    }
}
