using Net.Chdk.Model.Camera;
using Net.Chdk.Model.CameraModel;
using Net.Chdk.Model.Card;

namespace Net.Chdk.Detectors.CameraModel
{
    public interface IInnerCameraModelDetector
    {
        CameraModelInfo[] GetCameraModels(CardInfo cardInfo, CameraInfo cameraInfo);
    }
}
