// <auto-generated>
//     Generated by the protocol buffer compiler.  DO NOT EDIT!
//     source: Protos/subscriptions.proto
// </auto-generated>
#pragma warning disable 0414, 1591, 8981
#region Designer generated code

using grpc = global::Grpc.Core;

namespace LEX_SubscriptionService {
  public static partial class GrpcSubscription
  {
    static readonly string __ServiceName = "GrpcSubscription";

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static void __Helper_SerializeMessage(global::Google.Protobuf.IMessage message, grpc::SerializationContext context)
    {
      #if !GRPC_DISABLE_PROTOBUF_BUFFER_SERIALIZATION
      if (message is global::Google.Protobuf.IBufferMessage)
      {
        context.SetPayloadLength(message.CalculateSize());
        global::Google.Protobuf.MessageExtensions.WriteTo(message, context.GetBufferWriter());
        context.Complete();
        return;
      }
      #endif
      context.Complete(global::Google.Protobuf.MessageExtensions.ToByteArray(message));
    }

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static class __Helper_MessageCache<T>
    {
      public static readonly bool IsBufferMessage = global::System.Reflection.IntrospectionExtensions.GetTypeInfo(typeof(global::Google.Protobuf.IBufferMessage)).IsAssignableFrom(typeof(T));
    }

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static T __Helper_DeserializeMessage<T>(grpc::DeserializationContext context, global::Google.Protobuf.MessageParser<T> parser) where T : global::Google.Protobuf.IMessage<T>
    {
      #if !GRPC_DISABLE_PROTOBUF_BUFFER_SERIALIZATION
      if (__Helper_MessageCache<T>.IsBufferMessage)
      {
        return parser.ParseFrom(context.PayloadAsReadOnlySequence());
      }
      #endif
      return parser.ParseFrom(context.PayloadAsNewBuffer());
    }

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Marshaller<global::LEX_SubscriptionService.GetAllRequest> __Marshaller_GetAllRequest = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::LEX_SubscriptionService.GetAllRequest.Parser));
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Marshaller<global::LEX_SubscriptionService.SubscriptionResponse> __Marshaller_SubscriptionResponse = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::LEX_SubscriptionService.SubscriptionResponse.Parser));
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Marshaller<global::LEX_SubscriptionService.EntityResponse> __Marshaller_EntityResponse = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::LEX_SubscriptionService.EntityResponse.Parser));
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Marshaller<global::LEX_SubscriptionService.SourceResponse> __Marshaller_SourceResponse = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::LEX_SubscriptionService.SourceResponse.Parser));

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Method<global::LEX_SubscriptionService.GetAllRequest, global::LEX_SubscriptionService.SubscriptionResponse> __Method_GetAllSubscriptions = new grpc::Method<global::LEX_SubscriptionService.GetAllRequest, global::LEX_SubscriptionService.SubscriptionResponse>(
        grpc::MethodType.Unary,
        __ServiceName,
        "GetAllSubscriptions",
        __Marshaller_GetAllRequest,
        __Marshaller_SubscriptionResponse);

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Method<global::LEX_SubscriptionService.GetAllRequest, global::LEX_SubscriptionService.EntityResponse> __Method_GetAllEntitys = new grpc::Method<global::LEX_SubscriptionService.GetAllRequest, global::LEX_SubscriptionService.EntityResponse>(
        grpc::MethodType.Unary,
        __ServiceName,
        "GetAllEntitys",
        __Marshaller_GetAllRequest,
        __Marshaller_EntityResponse);

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Method<global::LEX_SubscriptionService.GetAllRequest, global::LEX_SubscriptionService.SourceResponse> __Method_GetAllSources = new grpc::Method<global::LEX_SubscriptionService.GetAllRequest, global::LEX_SubscriptionService.SourceResponse>(
        grpc::MethodType.Unary,
        __ServiceName,
        "GetAllSources",
        __Marshaller_GetAllRequest,
        __Marshaller_SourceResponse);

    /// <summary>Service descriptor</summary>
    public static global::Google.Protobuf.Reflection.ServiceDescriptor Descriptor
    {
      get { return global::LEX_SubscriptionService.SubscriptionsReflection.Descriptor.Services[0]; }
    }

    /// <summary>Base class for server-side implementations of GrpcSubscription</summary>
    [grpc::BindServiceMethod(typeof(GrpcSubscription), "BindService")]
    public abstract partial class GrpcSubscriptionBase
    {
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual global::System.Threading.Tasks.Task<global::LEX_SubscriptionService.SubscriptionResponse> GetAllSubscriptions(global::LEX_SubscriptionService.GetAllRequest request, grpc::ServerCallContext context)
      {
        throw new grpc::RpcException(new grpc::Status(grpc::StatusCode.Unimplemented, ""));
      }

      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual global::System.Threading.Tasks.Task<global::LEX_SubscriptionService.EntityResponse> GetAllEntitys(global::LEX_SubscriptionService.GetAllRequest request, grpc::ServerCallContext context)
      {
        throw new grpc::RpcException(new grpc::Status(grpc::StatusCode.Unimplemented, ""));
      }

      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual global::System.Threading.Tasks.Task<global::LEX_SubscriptionService.SourceResponse> GetAllSources(global::LEX_SubscriptionService.GetAllRequest request, grpc::ServerCallContext context)
      {
        throw new grpc::RpcException(new grpc::Status(grpc::StatusCode.Unimplemented, ""));
      }

    }

    /// <summary>Creates service definition that can be registered with a server</summary>
    /// <param name="serviceImpl">An object implementing the server-side handling logic.</param>
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    public static grpc::ServerServiceDefinition BindService(GrpcSubscriptionBase serviceImpl)
    {
      return grpc::ServerServiceDefinition.CreateBuilder()
          .AddMethod(__Method_GetAllSubscriptions, serviceImpl.GetAllSubscriptions)
          .AddMethod(__Method_GetAllEntitys, serviceImpl.GetAllEntitys)
          .AddMethod(__Method_GetAllSources, serviceImpl.GetAllSources).Build();
    }

    /// <summary>Register service method with a service binder with or without implementation. Useful when customizing the service binding logic.
    /// Note: this method is part of an experimental API that can change or be removed without any prior notice.</summary>
    /// <param name="serviceBinder">Service methods will be bound by calling <c>AddMethod</c> on this object.</param>
    /// <param name="serviceImpl">An object implementing the server-side handling logic.</param>
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    public static void BindService(grpc::ServiceBinderBase serviceBinder, GrpcSubscriptionBase serviceImpl)
    {
      serviceBinder.AddMethod(__Method_GetAllSubscriptions, serviceImpl == null ? null : new grpc::UnaryServerMethod<global::LEX_SubscriptionService.GetAllRequest, global::LEX_SubscriptionService.SubscriptionResponse>(serviceImpl.GetAllSubscriptions));
      serviceBinder.AddMethod(__Method_GetAllEntitys, serviceImpl == null ? null : new grpc::UnaryServerMethod<global::LEX_SubscriptionService.GetAllRequest, global::LEX_SubscriptionService.EntityResponse>(serviceImpl.GetAllEntitys));
      serviceBinder.AddMethod(__Method_GetAllSources, serviceImpl == null ? null : new grpc::UnaryServerMethod<global::LEX_SubscriptionService.GetAllRequest, global::LEX_SubscriptionService.SourceResponse>(serviceImpl.GetAllSources));
    }

  }
}
#endregion
