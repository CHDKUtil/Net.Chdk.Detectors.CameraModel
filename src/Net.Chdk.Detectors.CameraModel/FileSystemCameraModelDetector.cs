using Net.Chdk.Detectors.Camera;
using Net.Chdk.Model.CameraModel;
using Net.Chdk.Model.Card;
using Net.Chdk.Providers.CameraModel;

namespace Net.Chdk.Detectors.CameraModel
{
    sealed class FileSystemCameraModelDetector : ICameraModelDetector
    {
        private ICameraDetector CameraDetector { get; }
        private ICameraModelProvider CameraModelProvider { get; }

        public FileSystemCameraModelDetector(ICameraDetector cameraDetector, ICameraModelProvider cameraModelProvider)
        {
            CameraDetector = cameraDetector;
            CameraModelProvider = cameraModelProvider;
        }

        public CameraModelInfo[] GetCameraModels(CardInfo cardInfo)
        {
            var cameraInfo = CameraDetector.GetCamera(cardInfo);
            if (cameraInfo == null)
                return null;
            return CameraModelProvider.GetCameraModels(cameraInfo);
        }
    }
}
