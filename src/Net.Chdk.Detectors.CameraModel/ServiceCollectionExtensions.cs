using Microsoft.Extensions.DependencyInjection;

namespace Net.Chdk.Detectors.CameraModel
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCameraModelDetector(this IServiceCollection serviceCollection)
        {
            return serviceCollection
                .AddSingleton<ICameraModelDetector, CameraModelDetector>()
                .AddSingleton<IInnerCameraModelDetector, MetadataCameraModelDetector>()
                .AddSingleton<IInnerCameraModelDetector, FileSystemCameraModelDetector>();
        }

        public static IServiceCollection AddFileCameraModelDetector(this IServiceCollection serviceCollection)
        {
            return serviceCollection
                .AddSingleton<IFileCameraModelDetector, FileCameraModelDetector>();
        }
    }
}
