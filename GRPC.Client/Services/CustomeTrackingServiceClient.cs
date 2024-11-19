using Grpc.Core;
using static GRPC.Protos.TrackingService;

namespace GRPC.Client.Services
{
    public class CustomeTrackingServiceClient : TrackingServiceClient
    {
        public CustomeTrackingServiceClient(CallInvoker callInvoker) : base(callInvoker)
        {
        }
    }
}
