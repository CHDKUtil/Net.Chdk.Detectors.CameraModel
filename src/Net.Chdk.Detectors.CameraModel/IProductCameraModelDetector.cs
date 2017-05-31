using Net.Chdk.Model.Software;
using System;
using System.Threading;

namespace Net.Chdk.Detectors.CameraModel
{
    public interface IProductCameraModelDetector
    {
        CameraModels GetCameraModels(SoftwareInfo softwareInfo, IProgress<double> progress, CancellationToken token);
    }
}
