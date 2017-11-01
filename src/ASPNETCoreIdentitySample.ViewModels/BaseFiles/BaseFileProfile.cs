using AutoMapper;
using ASPNETCoreIdentitySample.Entities;
using ASPNETCoreIdentitySample.ViewModels.Helpers.Filters;

namespace ASPNETCoreIdentitySample.ViewModels
{
    public class BaseFileProfile : Profile
    {
        public BaseFileProfile()
        {
            CreateMap<BaseFile, BaseFileForZipArchiveCreateViewModel>();
            CreateMap<BaseFile, BaseFileDetailsViewModel>()
                .ForMember(x => x.FileTypeCssFontAwesomeIcon, opt => opt.MapFrom(src => ProcessFileTypeExtenstions.ProcessExtensionCssFontAwesomeIcon(src.FileContentType)))
                .ForMember(x => x.FileTypeExtension, opt => opt.MapFrom(src => ProcessFileTypeExtenstions.ProcessExtension(src.FileContentType)))
                .ForMember(x => x.FileSize, opt => opt.MapFrom(src => string.Format("{0:0.##}", src.FileSize.Replace("/","."))));
        }

    }
}