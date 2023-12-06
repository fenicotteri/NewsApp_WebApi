using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NewsApp.Interface;

namespace NewsApp.Controllers
{
    public class RatingController : Controller
    {
        private readonly IRatingRepository _ratingRepository;
        private readonly IMapper _mapper;

        public RatingController(IRatingRepository ratingRepository, IMapper mapper)
        {
            _ratingRepository = ratingRepository;
            _mapper = mapper;
        }
    }
}
