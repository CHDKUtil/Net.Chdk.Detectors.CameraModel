using Microsoft.Extensions.Logging;
using Net.Chdk.Model.Camera;
using Net.Chdk.Model.CameraModel;
using Net.Chdk.Model.Card;
using Net.Chdk.Model.Software;
using Net.Chdk.Providers.CameraModel;

namespace Net.Chdk.Detectors.CameraModel
{
    sealed class FileSystemCameraModelDetector : IInnerCameraModelDetector
    {
        private ILogger Logger { get; }
        private ICameraModelProvider CameraModelProvider { get; }

        public FileSystemCameraModelDetector(ICameraModelProvider cameraModelProvider, ILoggerFactory loggerFactory)
        {
            Logger = loggerFactory.CreateLogger<FileSystemCameraModelDetector>();
            CameraModelProvider = cameraModelProvider;
        }

        public CameraModelInfo[] GetCameraModels(CardInfo cardInfo, SoftwareInfo softwareInfo, CameraInfo cameraInfo)
        {
            Logger.LogTrace("Detecting camera models from camera info");

            return CameraModelProvider.GetCameraModels(cameraInfo);
        }
    }
}
