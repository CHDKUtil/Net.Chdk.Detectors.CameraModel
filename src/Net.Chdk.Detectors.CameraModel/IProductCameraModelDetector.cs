using Net.Chdk.Model.Software;

namespace Net.Chdk.Detectors.CameraModel
{
    public interface IProductCameraModelDetector
    {
        CameraModels GetCameraModels(SoftwareInfo softwareInfo);
    }
}
