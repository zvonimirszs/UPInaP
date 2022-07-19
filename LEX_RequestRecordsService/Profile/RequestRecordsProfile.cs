using AutoMapper;
using LEX_IdentityService;
using LEX_LegalSettings;
using LEX_RequestRecordsService.Dtos;
using LEX_RequestRecordsService.Models;
using LEX_RequestRecordsService.Models.Authenticate;

namespace LEX_RequestRecordsService.Profiles
{
    public class RequestRecordsProfile : Profile
    {
        public RequestRecordsProfile()
        {
            // Source -> Target
            CreateMap<RequestType, RequestTypeReadDto>();
            CreateMap<RequestCreateDto, Request>();
            CreateMap<Request, RequestReadDto>()
                    .ForMember(dest => dest.RequestTypeName, opt => opt.MapFrom(src => src.RequestType.Name));
            CreateMap<RequestReadDto, RequestPublishedDto>();
            
            CreateMap<GrpcRequestTypeModel, RequestType>()
                .ForMember(dest => dest.ExternalId, opt => opt.MapFrom(src => src.RequesttypeId))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));

             CreateMap<GrpcIdentityModel, AuthenticateResponse>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.IdentityId))
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.Firstname))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.Lastname))
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Username))
                .ForMember(dest => dest.JwtToken, opt => opt.MapFrom(src => src.Jwttoken));
            CreateMap<AuthenticateRequest, GrpcUserModel>()
                .ForMember(dest => dest.Username, opt => opt.MapFrom(src =>src.Username))
                .ForMember(dest => dest.Password, opt => opt.MapFrom(src =>src.Password));

            CreateMap<User, AuthenticateResponse>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Username));            
            CreateMap<AuthenticateResponse, User>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
                .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.UserName));
        }
    }
}