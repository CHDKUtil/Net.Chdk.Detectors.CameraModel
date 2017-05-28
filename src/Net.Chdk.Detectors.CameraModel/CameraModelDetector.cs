using Microsoft.Extensions.Logging;
using Net.Chdk.Detectors.Camera;
using Net.Chdk.Model.Camera;
using Net.Chdk.Model.CameraModel;
using Net.Chdk.Model.Card;
using Net.Chdk.Model.Software;
using System.Collections.Generic;
using System.Linq;

namespace Net.Chdk.Detectors.CameraModel
{
    public sealed class CameraModelDetector : ICameraModelDetector
    {
        private ILogger Logger { get; }
        private IEnumerable<IInnerCameraModelDetector> CameraModelDetectors { get; }
        private ICameraDetector CameraDetector { get; }

        public CameraModelDetector(IEnumerable<IInnerCameraModelDetector> cameraModelDetectors, ICameraDetector cameraDetector, ILoggerFactory loggerFactory)
        {
            Logger = loggerFactory.CreateLogger<CameraModelDetector>();
            CameraModelDetectors = cameraModelDetectors;
            CameraDetector = cameraDetector;
        }

        public CameraModels GetCameraModels(CardInfo cardInfo, SoftwareInfo softwareInfo)
        {
            Logger.LogTrace("Detecting camera models from {0}", cardInfo.DriveLetter);

            var cameraInfo = CameraDetector.GetCamera(cardInfo, softwareInfo);
            if (cameraInfo == null)
                return null;

            var cameraModels = GetCameraModels(cardInfo, softwareInfo, cameraInfo);

            return new CameraModels
            {
                Info = cameraInfo,
                Models = cameraModels.Collapse(cameraInfo)
            };
        }

        private CameraModelInfo[] GetCameraModels(CardInfo cardInfo, SoftwareInfo softwareInfo, CameraInfo cameraInfo)
        {
            return CameraModelDetectors
                .Select(d => d.GetCameraModels(cardInfo, softwareInfo, cameraInfo))
                .FirstOrDefault(c => c != null);
        }
    }
}
