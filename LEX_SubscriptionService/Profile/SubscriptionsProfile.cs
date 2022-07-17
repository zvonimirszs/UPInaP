
using AutoMapper;
using LEX_IdentityService;
using LEX_SubscriptionService.Dtos;
using LEX_SubscriptionService.Models;
using LEX_SubscriptionService.Models.Authenticate;

namespace LEX_SubscriptionService.Profiles
{
    public class SubscriptionsProfile : Profile
    {
        public SubscriptionsProfile()
        {
            // Source -> Target
            CreateMap<Subscription, SubscriptionReadDto>()
                    .ForMember(dest => dest.ServiceName, opt => opt.MapFrom(src => src.Service.Name));
             CreateMap<Subscription, GrpcSubscriptionModel>()
                .ForMember(dest => dest.SubscriptionId, opt => opt.MapFrom(src =>src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src =>src.Name))
                .ForMember(dest => dest.Key, opt => opt.MapFrom(src =>src.Key));

            CreateMap<Entity, EntityReadDto>()
                    .ForMember(dest => dest.SubscriptionName, opt => opt.MapFrom(src => src.Subscription.Name))
                    .ForMember(dest => dest.SubscriptionKey, opt => opt.MapFrom(src => src.Subscription.Key));
            CreateMap<EntityCreateDto, Entity>();
            CreateMap<EntityReadDto, EntityPublishedDto>();
            CreateMap<Entity, GrpcEntityModel>()
                .ForMember(dest => dest.EntityId, opt => opt.MapFrom(src =>src.Id))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src =>src.Email))
                .ForMember(dest => dest.SourceKey, opt => opt.MapFrom(src =>src.SourceKey))
                .ForMember(dest => dest.SubscriptionId, opt => opt.MapFrom(src =>src.SubscriptionId));
            CreateMap<Source, GrpcSourceModel>()
                .ForMember(dest => dest.SourceId, opt => opt.MapFrom(src =>src.Id))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src =>src.Description))
                .ForMember(dest => dest.LawfulnessProcessing, opt => opt.MapFrom(src =>src.LawfulnessProcessing))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src =>src.Name))
                .ForMember(dest => dest.SourceKey, opt => opt.MapFrom(src =>src.SourceKey));
            
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