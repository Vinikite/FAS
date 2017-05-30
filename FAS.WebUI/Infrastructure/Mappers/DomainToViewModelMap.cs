using AutoMapper;
using FAS.Domain;
using FAS.WebUI.Models;

namespace FAS.WebUI.Infrastructure.Mappers
{
    public class DomainToViewModelMap
    {
        public static void CreateMap(IMapperConfigurationExpression config)
        {
            config.CreateMap<Address, SimpleAddressViewModel>();
            config.CreateMap<Address, CreateAddressViewModel>();
            config.CreateMap<Address, ChangeAddressViewModel>();

            config.CreateMap<Score, SimpleScoreViewModel>();
            config.CreateMap<Score, CreateScoreViewModel>();
            config.CreateMap<Score, ChangeScoreViewModel>();
            config.CreateMap<Score, ScoreItemModel>()
                    .ForMember(x => x.Name, option => option.MapFrom(f => f.Notation))
                    .ForMember(x => x.ViewType, option => option.MapFrom(f => f.ViewScore.Name))
                    .ForMember(x => x.Type, option => option.MapFrom(f => f.TypeScore.Name))
                    .ForMember(x => x.Status, option => option.MapFrom(x => x.StatusScore.Name));

            config.CreateMap<TypeScore, SimpleTypeOfScoreViewModel>();
            config.CreateMap<TypeScore, CreateTypeOfScoreViewModel>();
            config.CreateMap<TypeScore, ChangeTypeOfScoreViewModel>();

            config.CreateMap<StatusScore, SimpleStatusScoreViewModel>();
            config.CreateMap<StatusScore, CreateStatusScoreViewModel>();
            config.CreateMap<StatusScore, ChangeStatusScoreViewModel>();

            config.CreateMap<ViewScore, SimpleViewScoreViewModel>();
            config.CreateMap<ViewScore, CreateViewScoreViewModel>();
            config.CreateMap<ViewScore, ChangeViewScoreViewModel>();

            config.CreateMap<Category, SimpleCategoryViewModel>();
            config.CreateMap<Category, CreateCategoryViewModel>();
            config.CreateMap<Category, ChangeCategoryViewModel>();

            config.CreateMap<MyGoals, SimpleMyGoalsViewModel>();
            config.CreateMap<MyGoals, CreateMyGoalsViewModel>();
            config.CreateMap<MyGoals, ChangeMyGoalsViewModel>();

            config.CreateMap<Transaction, SimpleTransactionViewModel>();
            config.CreateMap<Transaction, CreateTransactionViewModel>();
            config.CreateMap<Transaction, ChangeTransactionViewModel>();
            config.CreateMap<Transaction, TransactionItemModel>()
                    .ForMember(x => x.Type, option => option.MapFrom(f => f.TransactionType.Name))
                    .ForMember(x => x.Category, option => option.MapFrom(f => f.Category.Name))
                    .ForMember(x => x.Score, option => option.MapFrom(f => f.Score.Notation))
                    .ForMember(x => x.Bank, option => option.MapFrom(f => f.Bank.Name));

            config.CreateMap<TransactionType, SimpleTransactionTypeViewModel>();
            config.CreateMap<TransactionType, CreateTransactionTypeViewModel>();
            config.CreateMap<TransactionType, ChangeTransactionTypeViewModel>();

            config.CreateMap<Bank, SimpleBankViewModel>();
            config.CreateMap<Bank, CreateBankViewModel>();
            config.CreateMap<Bank, ChangeBankViewModel>();

            config.CreateMap<User, SimpleUserViewModel>();
            config.CreateMap<User, CreateUserViewModel>();
            config.CreateMap<User, ChangeUserViewModel>();
        }
    }
}