using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using GRPC.Protos;
using Microsoft.AspNetCore.Authorization;
using static GRPC.Protos.TrackingService;

namespace GRPC.Service
{
    public class TrackingService: TrackingServiceBase
    {
        [Authorize]
        public override async Task<TrackingResponse> Track(TrackingMessage request, ServerCallContext context)
        {
            if(!request.Locations.Any())
            {
                //throw new RpcException(Status.DefaultCancelled);
                throw new RpcException(new Status(StatusCode.InvalidArgument, "Locations must have at least one item."));
            }

            return new TrackingResponse
            {
                Success = true,
                TimeStamp = DateTime.UtcNow.ToTimestamp()
            };
        }

        public override async Task<TrackingResponse> StreamRequest(IAsyncStreamReader<TrackingMessage> requestStream, ServerCallContext context)
        {
            //await foreach(var message in requestStream.ReadAllAsync())
            //{

            //}

            while(await requestStream.MoveNext())
            {
                var message = requestStream.Current;
            }

            return new TrackingResponse
            {
                Success = true,
                TimeStamp = DateTime.UtcNow.ToTimestamp()
            };
        }

        public override async Task StreamResponse(TrackingMessage request, IServerStreamWriter<TrackingResponse> responseStream, ServerCallContext context)
        {
            await responseStream.WriteAsync(new TrackingResponse
            {
                Success = true,
                TimeStamp = DateTime.UtcNow.ToTimestamp()
            });

            await responseStream.WriteAsync(new TrackingResponse
            {
                Success = true,
                TimeStamp = DateTime.UtcNow.ToTimestamp()
            });
        }

        public override async Task Stream(IAsyncStreamReader<TrackingMessage> requestStream, IServerStreamWriter<TrackingResponse> responseStream, ServerCallContext context)
        {
            while(await requestStream.MoveNext())
            {
                var message = requestStream.Current;
            }

            await responseStream.WriteAsync(new TrackingResponse
            {
                Success = true,
                TimeStamp = DateTime.UtcNow.ToTimestamp()
            });
            
            await responseStream.WriteAsync(new TrackingResponse
            {
                Success = true,
                TimeStamp = DateTime.UtcNow.ToTimestamp()
            });
        }
    }
}
