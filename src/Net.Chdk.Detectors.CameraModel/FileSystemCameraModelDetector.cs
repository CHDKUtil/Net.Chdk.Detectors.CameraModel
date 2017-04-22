using Net.Chdk.Model.Camera;
using Net.Chdk.Model.CameraModel;
using Net.Chdk.Model.Card;
using Net.Chdk.Providers.CameraModel;

namespace Net.Chdk.Detectors.CameraModel
{
    sealed class FileSystemCameraModelDetector : IInnerCameraModelDetector
    {
        private ICameraModelProvider CameraModelProvider { get; }

        public FileSystemCameraModelDetector(ICameraModelProvider cameraModelProvider)
        {
            CameraModelProvider = cameraModelProvider;
        }

        public CameraModelInfo[] GetCameraModels(CardInfo cardInfo, CameraInfo cameraInfo)
        {
            return CameraModelProvider.GetCameraModels(cameraInfo);
        }
    }
}
