using AutoMapper;
using Saturday_Back.Common.Repositories;
using Saturday_Back.Features.PaymentTypes.Dtos;

namespace Saturday_Back.Features.PaymentTypes
{
    public class PaymentTypeService
    {
        private readonly ICachedRepository<PaymentType> _repository;
        private readonly IMapper _mapper;

        public PaymentTypeService(ICachedRepository<PaymentType> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<PaymentTypeResponseDto>> GetAllAsync()
        {
            var entities = await _repository.GetAllAsync();
            return _mapper.Map<List<PaymentTypeResponseDto>>(entities);
        }

        public async Task<PaymentTypeResponseDto> CreateAsync(PaymentTypeRequestDto request)
        {
            var entity = _mapper.Map<PaymentType>(request);
            await _repository.AddAsync(entity);
            return _mapper.Map<PaymentTypeResponseDto>(entity);
        }

        public async Task<PaymentTypeResponseDto> UpdateAsync(int id, PaymentTypeRequestDto request)
        {
            var entity = _mapper.Map<PaymentType>(request);
            entity.Id = id;
            await _repository.UpdateAsync(entity);
            return _mapper.Map<PaymentTypeResponseDto>(entity);
        }
    }
}

