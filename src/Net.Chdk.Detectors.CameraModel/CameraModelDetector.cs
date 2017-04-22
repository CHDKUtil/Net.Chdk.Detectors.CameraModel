using Microsoft.Extensions.Logging;
using Net.Chdk.Detectors.Camera;
using Net.Chdk.Model.Camera;
using Net.Chdk.Model.CameraModel;
using Net.Chdk.Model.Card;
using Net.Chdk.Providers.CameraModel;
using System.Collections.Generic;
using System.Linq;

namespace Net.Chdk.Detectors.CameraModel
{
    public sealed class CameraModelDetector : CameraModelDetectorBase, ICameraModelDetector
    {
        private ICameraDetector CameraDetector { get; }
        private IEnumerable<IInnerCameraModelDetector> CameraModelDetectors { get; }

        public CameraModelDetector(ICameraDetector cameraDetector, ICameraModelProvider cameraModelProvider, ILoggerFactory loggerFactory)
            : base(loggerFactory)
        {
            CameraDetector = cameraDetector;
            CameraModelDetectors = new IInnerCameraModelDetector[]
            {
                new MetadataCameraModelDetector(LoggerFactory),
                new FileSystemCameraModelDetector(cameraModelProvider)
            };
        }

        public CameraModels GetCameraModels(CardInfo cardInfo)
        {
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
