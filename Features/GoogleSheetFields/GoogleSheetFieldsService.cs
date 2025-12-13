using System.Diagnostics.CodeAnalysis;
using Saturday_Back.Features.BenefitTypes;
using Saturday_Back.Features.GoogleSheetFields.Dtos;
using Saturday_Back.Features.PaymentTypes;
using Saturday_Back.Features.Subjects;

public class GoogleSheetFieldsService(PaymentTypeService paymentTypeService, BenefitTypeService benefitTypeService, SubjectService subjectService)
{
    private readonly PaymentTypeService _paymentTypeService = paymentTypeService;
    private readonly BenefitTypeService _benefitTypeService = benefitTypeService;
    private readonly SubjectService _subjectService = subjectService;

    public async Task<GoogleSheetFieldsResponse> GetAllAsync()
    {
        var paymentTypes = (await _paymentTypeService.GetAllAsync()).OrderBy(x => x.Id).ToList();
        var benefitTypes = (await _benefitTypeService.GetAllAsync()).OrderBy(x => x.Id).ToList();
        var subjects = (await _subjectService.GetAllAsync()).OrderBy(x => x.Id).ToList();

        var paymentTypesStrings = paymentTypes.Select(x => $"{x.Id}. {x.Name}").ToList();
        var benefitTypesStrings = benefitTypes.Select(x => $"{x.Id}. {x.Name}").ToList();
        var subjectsStrings = subjects.Select(x => $"{x.Id}. {x.Name}").ToList();

        return new GoogleSheetFieldsResponse
        {
            PaymentTypes = paymentTypesStrings,
            BenefitTypes = benefitTypesStrings,
            Subjects = subjectsStrings,
        };
    }
}