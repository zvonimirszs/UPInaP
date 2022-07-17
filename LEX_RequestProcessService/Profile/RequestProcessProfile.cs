using AutoMapper;
using LEX_IdentityService;
using LEX_RequestProcessService.Dtos;
using LEX_RequestProcessService.Models;
using LEX_RequestProcessService.Models.Authenticate;
using LEX_SubscriptionService;

namespace LEX_RequestProcessService.Profiles
{
    public class RequestProcessProfile : Profile
    {
        public RequestProcessProfile()
        {
            // Source -> Target
            CreateMap<RequestCreateDto, Request>();
            CreateMap<Request, RequestReadDto>();
            CreateMap<ResponseType, ResponseTypeReadDto>();
            CreateMap<RequestReadDto, RequestPublishedDto>();
            CreateMap<EntityPublishedDto, Entity>()
                .ForMember(dest => dest.ExternalId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.SourceKey, opt => opt.MapFrom(src => src.SourceKey))                
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email));
            CreateMap<Entity, EntityReadDto>();
            CreateMap<EntityReadDto, EntityPublishedDto>();
            CreateMap<Subscription, SubscriptionReadDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Purpose, opt => opt.MapFrom(src => src.Purpose))
                .ForMember(dest => dest.ServiceName, opt => opt.Ignore());
            CreateMap<SubscriptionPublishedDto, Subscription>()
                .ForMember(dest => dest.ExternalId, opt => opt.MapFrom(src => src.Id));
            CreateMap<GrpcSubscriptionModel, Subscription>()
                .ForMember(dest => dest.ExternalId, opt => opt.MapFrom(src => src.SubscriptionId))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Key, opt => opt.MapFrom(src => src.Key))
                .ForMember(dest => dest.Purpose, opt => opt.Ignore())
                .ForMember(dest => dest.Description, opt => opt.Ignore())
                .ForMember(dest => dest.ServiceId, opt => opt.Ignore());
            CreateMap<GrpcEntityModel, Entity>()
                .ForMember(dest => dest.ExternalId, opt => opt.MapFrom(src => src.EntityId))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.SourceKey, opt => opt.MapFrom(src => src.SourceKey))
                .ForMember(dest => dest.SubscriptionId, opt => opt.MapFrom(src =>src.SubscriptionId));
            CreateMap<Entity, GrpcEntityModel>()
                .ForMember(dest => dest.EntityId, opt => opt.MapFrom(src => src.ExternalId))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.SourceKey, opt => opt.MapFrom(src => src.SourceKey))
                .ForMember(dest => dest.SubscriptionId, opt => opt.MapFrom(src =>src.SubscriptionId));
            CreateMap<GrpcSourceModel, Source>()
                .ForMember(dest => dest.ExternalId, opt => opt.MapFrom(src => src.SourceId))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.SourceKey, opt => opt.MapFrom(src => src.SourceKey))
                .ForMember(dest => dest.LawfulnessProcessing, opt => opt.MapFrom(src => src.LawfulnessProcessing));
            
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