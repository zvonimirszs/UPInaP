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

    /// <summary>Client for GrpcSubscription</summary>
    public partial class GrpcSubscriptionClient : grpc::ClientBase<GrpcSubscriptionClient>
    {
      /// <summary>Creates a new client for GrpcSubscription</summary>
      /// <param name="channel">The channel to use to make remote calls.</param>
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public GrpcSubscriptionClient(grpc::ChannelBase channel) : base(channel)
      {
      }
      /// <summary>Creates a new client for GrpcSubscription that uses a custom <c>CallInvoker</c>.</summary>
      /// <param name="callInvoker">The callInvoker to use to make remote calls.</param>
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public GrpcSubscriptionClient(grpc::CallInvoker callInvoker) : base(callInvoker)
      {
      }
      /// <summary>Protected parameterless constructor to allow creation of test doubles.</summary>
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      protected GrpcSubscriptionClient() : base()
      {
      }
      /// <summary>Protected constructor to allow creation of configured clients.</summary>
      /// <param name="configuration">The client configuration.</param>
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      protected GrpcSubscriptionClient(ClientBaseConfiguration configuration) : base(configuration)
      {
      }

      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual global::LEX_SubscriptionService.SubscriptionResponse GetAllSubscriptions(global::LEX_SubscriptionService.GetAllRequest request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return GetAllSubscriptions(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual global::LEX_SubscriptionService.SubscriptionResponse GetAllSubscriptions(global::LEX_SubscriptionService.GetAllRequest request, grpc::CallOptions options)
      {
        return CallInvoker.BlockingUnaryCall(__Method_GetAllSubscriptions, null, options, request);
      }
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual grpc::AsyncUnaryCall<global::LEX_SubscriptionService.SubscriptionResponse> GetAllSubscriptionsAsync(global::LEX_SubscriptionService.GetAllRequest request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return GetAllSubscriptionsAsync(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual grpc::AsyncUnaryCall<global::LEX_SubscriptionService.SubscriptionResponse> GetAllSubscriptionsAsync(global::LEX_SubscriptionService.GetAllRequest request, grpc::CallOptions options)
      {
        return CallInvoker.AsyncUnaryCall(__Method_GetAllSubscriptions, null, options, request);
      }
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual global::LEX_SubscriptionService.EntityResponse GetAllEntitys(global::LEX_SubscriptionService.GetAllRequest request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return GetAllEntitys(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual global::LEX_SubscriptionService.EntityResponse GetAllEntitys(global::LEX_SubscriptionService.GetAllRequest request, grpc::CallOptions options)
      {
        return CallInvoker.BlockingUnaryCall(__Method_GetAllEntitys, null, options, request);
      }
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual grpc::AsyncUnaryCall<global::LEX_SubscriptionService.EntityResponse> GetAllEntitysAsync(global::LEX_SubscriptionService.GetAllRequest request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return GetAllEntitysAsync(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual grpc::AsyncUnaryCall<global::LEX_SubscriptionService.EntityResponse> GetAllEntitysAsync(global::LEX_SubscriptionService.GetAllRequest request, grpc::CallOptions options)
      {
        return CallInvoker.AsyncUnaryCall(__Method_GetAllEntitys, null, options, request);
      }
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual global::LEX_SubscriptionService.SourceResponse GetAllSources(global::LEX_SubscriptionService.GetAllRequest request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return GetAllSources(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual global::LEX_SubscriptionService.SourceResponse GetAllSources(global::LEX_SubscriptionService.GetAllRequest request, grpc::CallOptions options)
      {
        return CallInvoker.BlockingUnaryCall(__Method_GetAllSources, null, options, request);
      }
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual grpc::AsyncUnaryCall<global::LEX_SubscriptionService.SourceResponse> GetAllSourcesAsync(global::LEX_SubscriptionService.GetAllRequest request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return GetAllSourcesAsync(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual grpc::AsyncUnaryCall<global::LEX_SubscriptionService.SourceResponse> GetAllSourcesAsync(global::LEX_SubscriptionService.GetAllRequest request, grpc::CallOptions options)
      {
        return CallInvoker.AsyncUnaryCall(__Method_GetAllSources, null, options, request);
      }
      /// <summary>Creates a new instance of client from given <c>ClientBaseConfiguration</c>.</summary>
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      protected override GrpcSubscriptionClient NewInstance(ClientBaseConfiguration configuration)
      {
        return new GrpcSubscriptionClient(configuration);
      }
    }

  }
  public static partial class GrpcEntity
  {
    static readonly string __ServiceName = "GrpcEntity";


    /// <summary>Service descriptor</summary>
    public static global::Google.Protobuf.Reflection.ServiceDescriptor Descriptor
    {
      get { return global::LEX_SubscriptionService.SubscriptionsReflection.Descriptor.Services[1]; }
    }

    /// <summary>Client for GrpcEntity</summary>
    public partial class GrpcEntityClient : grpc::ClientBase<GrpcEntityClient>
    {
      /// <summary>Creates a new client for GrpcEntity</summary>
      /// <param name="channel">The channel to use to make remote calls.</param>
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public GrpcEntityClient(grpc::ChannelBase channel) : base(channel)
      {
      }
      /// <summary>Creates a new client for GrpcEntity that uses a custom <c>CallInvoker</c>.</summary>
      /// <param name="callInvoker">The callInvoker to use to make remote calls.</param>
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public GrpcEntityClient(grpc::CallInvoker callInvoker) : base(callInvoker)
      {
      }
      /// <summary>Protected parameterless constructor to allow creation of test doubles.</summary>
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      protected GrpcEntityClient() : base()
      {
      }
      /// <summary>Protected constructor to allow creation of configured clients.</summary>
      /// <param name="configuration">The client configuration.</param>
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      protected GrpcEntityClient(ClientBaseConfiguration configuration) : base(configuration)
      {
      }

      /// <summary>Creates a new instance of client from given <c>ClientBaseConfiguration</c>.</summary>
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      protected override GrpcEntityClient NewInstance(ClientBaseConfiguration configuration)
      {
        return new GrpcEntityClient(configuration);
      }
    }

  }
}
#endregion