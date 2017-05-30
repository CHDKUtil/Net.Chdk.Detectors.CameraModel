using Net.Chdk.Model.Software;
using System;

namespace Net.Chdk.Detectors.CameraModel
{
    public interface IProductCameraModelDetector
    {
        CameraModels GetCameraModels(SoftwareInfo softwareInfo, IProgress<double> progress);
    }
}
