using Microsoft.Extensions.Logging;
using Net.Chdk.Detectors.Camera;
using Net.Chdk.Providers.CameraModel;

namespace Net.Chdk.Detectors.CameraModel
{
    public sealed class FileCameraModelDetector : CameraModelDetectorBase, IFileCameraModelDetector
    {
        private IFileCameraDetector FileCameraDetector { get; }
        private ICameraModelProvider CameraModelProvider { get; }

        public FileCameraModelDetector(IFileCameraDetector fileCameraDetector, ICameraModelProvider cameraModelProvider, ILoggerFactory loggerFactory)
            : base(loggerFactory)
        {
            FileCameraDetector = fileCameraDetector;
            CameraModelProvider = cameraModelProvider;
        }

        public CameraModels GetCameraModels(string filePath)
        {
            var cameraInfo = FileCameraDetector.GetCamera(filePath);
            if (cameraInfo == null)
                return null;

            var cameraModels = CameraModelProvider.GetCameraModels(cameraInfo);

            return GetCameraModels(cameraInfo, cameraModels);
        }
    }
}
