using AutoMapper;
using FAS.Domain;
using FAS.WebUI.Models;

namespace FAS.WebUI.Infrastructure.Mappers
{
    public class ViewModelToDomainMap
    {
        public static void CreateMap(IMapperConfigurationExpression config)
        {
            config.CreateMap<CreateBookViewModel, Book>()
                    .IgnoreProperty(m => m.Id);
        }
    }
}