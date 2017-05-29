using Net.Chdk.Model.Card;
using Net.Chdk.Model.Software;
using System.Collections.Generic;
using System.Linq;

namespace Net.Chdk.Detectors.CameraModel
{
    public sealed class CameraModelDetector : ICameraModelDetector
    {
        private IEnumerable<IOuterCameraModelDetector> CameraModelDetectors { get; }

        public CameraModelDetector(IEnumerable<IOuterCameraModelDetector> cameraModelDetectors)
        {
            CameraModelDetectors = cameraModelDetectors;
        }

        public CameraModels GetCameraModels(CardInfo cardInfo, SoftwareInfo softwareInfo)
        {
            return CameraModelDetectors
                .Select(d => d.GetCameraModels(cardInfo, softwareInfo))
                .FirstOrDefault(c => c != null);
        }
    }
}
