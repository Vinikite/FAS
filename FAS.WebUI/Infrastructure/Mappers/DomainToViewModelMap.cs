using AutoMapper;
using FAS.Domain;
using FAS.WebUI.Models;

namespace FAS.WebUI.Infrastructure.Mappers
{
    public class DomainToViewModelMap
    {
        public static void CreateMap(IMapperConfigurationExpression config)
        {
            config.CreateMap<Score, SimpleAddressViewModel>();
            config.CreateMap<Score, CreateAddressViewModel>();
            config.CreateMap<Score, ChangeAddressViewModel>();

            config.CreateMap<Score, SimpleScoreViewModel>();
            config.CreateMap<Score, CreateScoreViewModel>();
            config.CreateMap<Score, ChangeScoreViewModel>();

            config.CreateMap<Score, SimpleTypeOfScoreViewModel>();
            config.CreateMap<Score, CreateTypeOfScoreViewModel>();
            config.CreateMap<Score, ChangeTypeOfScoreViewModel>();

            config.CreateMap<Score, SimpleStatusScoreViewModel>();
            config.CreateMap<Score, CreateStatusScoreViewModel>();
            config.CreateMap<Score, ChangeStatusScoreViewModel>();

            config.CreateMap<Score, SimpleViewScoreViewModel>();
            config.CreateMap<Score, CreateViewScoreViewModel>();
            config.CreateMap<Score, ChangeViewScoreViewModel>();

            config.CreateMap<Score, SimpleCategoryViewModel>();
            config.CreateMap<Score, CreateCategoryViewModel>();
            config.CreateMap<Score, ChangeCategoryViewModel>();

            config.CreateMap<Score, SimpleTransactionViewModel>();
            config.CreateMap<Score, CreateTransactionViewModel>();
            config.CreateMap<Score, ChangeTransactionViewModel>();

            config.CreateMap<Score, SimpleTransactionTypeViewModel>();
            config.CreateMap<Score, CreateTransactionTypeViewModel>();
            config.CreateMap<Score, ChangeTransactionTypeViewModel>();

            config.CreateMap<Score, SimpleBankViewModel>();
            config.CreateMap<Score, CreateBankViewModel>();
            config.CreateMap<Score, ChangeBankViewModel>();
        }
    }
}