using AutoMapper;
using LEX_IdentityService;
using LEX_LegalSettings;
using LEX_LegalSettings.Dtos;
using LEX_LegalSettings.Models;
using LEX_LegalSettings.Models.Authenticate;

namespace LEX_SubscriptionService.Profiles;
public class LegalSettingsProfile : Profile
{
    public LegalSettingsProfile()
    {
        // Source -> Target
        CreateMap<RequestType, GrpcRequestTypeModel>()
            .ForMember(dest => dest.RequesttypeId, opt => opt.MapFrom(src =>src.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src =>src.Name));

        CreateMap<SubjectData, SubjectDataReadDto>();
        CreateMap<Definition, DefinitionReadDto>();
        CreateMap<Legislation, LegislationReadDto>();

        CreateMap<SubjectData, GrpcSubjectDataModel>()
            .ForMember(dest => dest.SubjectDataId, opt => opt.MapFrom(src =>src.Id))
            .ForMember(dest => dest.Controller, opt => opt.MapFrom(src =>src.Controller))
            .ForMember(dest => dest.Dpo, opt => opt.MapFrom(src =>src.DataProtectionOfficer));
        CreateMap<Definition, GrpcDefinitionModel>()
            .ForMember(dest => dest.DefinitionId, opt => opt.MapFrom(src =>src.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src =>src.Name))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src =>src.Description));
        CreateMap<Legislation, GrpcLegislationModel>()
            .ForMember(dest => dest.LegislationId, opt => opt.MapFrom(src =>src.Id))
            .ForMember(dest => dest.ArticleNo, opt => opt.MapFrom(src =>src.ArticleNo))
            .ForMember(dest => dest.Link, opt => opt.MapFrom(src =>src.Link))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src =>src.Name));
                    
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
