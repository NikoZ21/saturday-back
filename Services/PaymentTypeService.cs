using Saturday_Back.Dtos;
using Saturday_Back.Entities;
using Saturday_Back.Repositories;

namespace Saturday_Back.Services
{
    public class PaymentTypeService(ICachedRepository<PaymentType, PaymentTypeResponseDto> repository)
    {
        private readonly ICachedRepository<PaymentType, PaymentTypeResponseDto> _repository = repository;

        public Task<List<PaymentTypeResponseDto>> GetAllAsync() => _repository.GetAllAsync();
        public Task UpdatePaymentTypeAsync(PaymentType updatedPayment) => _repository.UpdateAsync(updatedPayment);
    }
}
