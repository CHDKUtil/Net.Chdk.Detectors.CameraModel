using Microsoft.Extensions.Logging;
using Net.Chdk.Detectors.Camera;
using Net.Chdk.Model.Camera;
using Net.Chdk.Model.CameraModel;
using Net.Chdk.Model.Card;
using System.Collections.Generic;
using System.Linq;

namespace Net.Chdk.Detectors.CameraModel
{
    public sealed class CameraModelDetector : CameraModelDetectorBase, ICameraModelDetector
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

        public CameraModels GetCameraModels(CardInfo cardInfo)
        {
            Logger.LogTrace("Detecting camera models from {0}", cardInfo.DriveLetter);

            var cameraInfo = CameraDetector.GetCamera(cardInfo);
            if (cameraInfo == null)
                return null;

            var cameraModels = GetCameraModels(cardInfo, cameraInfo);

            return GetCameraModels(cameraInfo, cameraModels);
        }

        private CameraModelInfo[] GetCameraModels(CardInfo cardInfo, CameraInfo cameraInfo)
        {
            return CameraModelDetectors
                .Select(d => d.GetCameraModels(cardInfo, cameraInfo))
                .FirstOrDefault(c => c != null);
        }
    }
}
