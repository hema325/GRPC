using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using GRPC.Protos;
using static GRPC.Protos.TrackingService;

namespace GRPC.Service
{
    public class TrackingService: TrackingServiceBase
    {
        public override async Task<TrackingResponse> Track(TrackingMessage request, ServerCallContext context)
        {
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
