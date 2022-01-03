using AutoMapper;
using Core.entities;
using Hermes.API.Dtos;

namespace Hermes.API.Util.UrlResolvers

{
    public class ReporterUrlResolver : IValueResolver<Reporter, ReporterToReturnDto, string>
    {
        private readonly IConfiguration _configuration;

        public ReporterUrlResolver(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string Resolve(Reporter source, ReporterToReturnDto destination, string destMember, ResolutionContext context)
        {
            //Check if image name is null
            if (!string.IsNullOrEmpty(source.Image))
            {
                   return _configuration["ApiUrl"] + "uploads/reporters/" + source.Image;
            }

            return  null;
        }
    }
}
