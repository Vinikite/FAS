using AutoMapper;
using FAS.Domain;
using FAS.WebUI.Models;

namespace FAS.WebUI.Infrastructure.Mappers
{
    public class ViewModelToDomainMap
    {
        public static void CreateMap(IMapperConfigurationExpression config)
        {
            config.CreateMap<CreateAddressViewModel, Address>()
                    .IgnoreProperty(m => m.Id);
            config.CreateMap<CreateUserViewModel, User>()
        .IgnoreProperty(m => m.Id);
            config.CreateMap<CreateScoreViewModel, Score>()
                    .IgnoreProperty(m => m.Id);
            config.CreateMap<CreateTypeOfScoreViewModel, TypeScore>()
                    .IgnoreProperty(m => m.Id);
            config.CreateMap<CreateStatusScoreViewModel, StatusScore>()
                    .IgnoreProperty(m => m.Id);
            config.CreateMap<CreateViewScoreViewModel, ViewScore>()
                    .IgnoreProperty(m => m.Id); 
            config.CreateMap<CreateCategoryViewModel, Category>()
                    .IgnoreProperty(m => m.Id);
            config.CreateMap<CreateMyGoalsViewModel, MyGoals>()
                    .IgnoreProperty(m => m.Id);
            config.CreateMap<CreateTransactionViewModel, Transaction>()
                    .IgnoreProperty(m => m.Id);
            config.CreateMap<CreateTransactionTypeViewModel, TransactionType>()
                    .IgnoreProperty(m => m.Id);
            config.CreateMap<CreateBankViewModel, Bank>()
                    .IgnoreProperty(m => m.Id);
        }
    }
}