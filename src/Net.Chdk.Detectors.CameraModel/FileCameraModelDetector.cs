using Net.Chdk.Detectors.Camera;
using Net.Chdk.Model.CameraModel;
using Net.Chdk.Providers.CameraModel;

namespace Net.Chdk.Detectors.CameraModel
{
    public sealed class FileCameraModelDetector : IFileCameraModelDetector
    {
        private IFileCameraDetector FileCameraDetector { get; }
        private ICameraModelProvider CameraModelProvider { get; }

        public FileCameraModelDetector(IFileCameraDetector fileCameraDetector, ICameraModelProvider cameraModelProvider)
        {
            FileCameraDetector = fileCameraDetector;
            CameraModelProvider = cameraModelProvider;
        }

        public CameraModelInfo[] GetCameraModels(string filePath)
        {
            var cameraInfo = FileCameraDetector.GetCamera(filePath);
            if (cameraInfo == null)
                return null;
            return CameraModelProvider.GetCameraModels(cameraInfo);
        }
    }
}
