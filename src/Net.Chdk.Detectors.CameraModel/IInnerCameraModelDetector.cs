using Net.Chdk.Model.Camera;
using Net.Chdk.Model.CameraModel;
using Net.Chdk.Model.Card;
using Net.Chdk.Model.Software;

namespace Net.Chdk.Detectors.CameraModel
{
    public interface IInnerCameraModelDetector
    {
        CameraModelInfo[] GetCameraModels(CardInfo cardInfo, SoftwareInfo softwareInfo, CameraInfo cameraInfo);
    }
}
