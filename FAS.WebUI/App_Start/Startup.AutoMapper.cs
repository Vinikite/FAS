using AutoMapper;
using FAS.WebUI.Infrastructure.Mappers;

namespace FAS.WebUI
{
    public partial class Startup
    {
        public void ConfigureAutoMapper()
        {
            Mapper.Initialize(config => {
                DomainToViewModelMap.CreateMap(config);
                ViewModelToDomainMap.CreateMap(config);
            });
        }
    }
}