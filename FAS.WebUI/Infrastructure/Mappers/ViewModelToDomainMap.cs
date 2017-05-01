using AutoMapper;
using FAS.Domain;
using FAS.WebUI.Models;

namespace FAS.WebUI.Infrastructure.Mappers
{
    public class ViewModelToDomainMap
    {
        public static void CreateMap(IMapperConfigurationExpression config)
        {
            config.CreateMap<CreateAddressViewModel, Score>()
                    .IgnoreProperty(m => m.Id);
            config.CreateMap<CreateScoreViewModel, Score>()
                    .IgnoreProperty(m => m.Id);
            config.CreateMap<CreateTypeOfScoreViewModel, Score>()
                    .IgnoreProperty(m => m.Id);
            config.CreateMap<CreateStatusScoreViewModel, Score>()
                    .IgnoreProperty(m => m.Id);
            config.CreateMap<CreateViewScoreViewModel, Score>()
                    .IgnoreProperty(m => m.Id);
            config.CreateMap<CreateCategoryViewModel, Score>()
                    .IgnoreProperty(m => m.Id);
            config.CreateMap<CreateTransactionViewModel, Score>()
                    .IgnoreProperty(m => m.Id);
            config.CreateMap<CreateTransactionTypeViewModel, Score>()
                    .IgnoreProperty(m => m.Id);
            config.CreateMap<CreateBankViewModel, Score>()
                    .IgnoreProperty(m => m.Id);
        }
    }
}