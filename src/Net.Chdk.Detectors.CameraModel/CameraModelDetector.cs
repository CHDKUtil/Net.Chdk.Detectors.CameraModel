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
        private IEnumerable<IProductCameraModelDetector> ProductCameraModelDetectors { get; }
        private IEnumerable<IInnerCameraModelDetector> CameraModelDetectors { get; }
        private ICameraDetector CameraDetector { get; }

        public CameraModelDetector(IEnumerable<IProductCameraModelDetector> productCameraModelDetectors, IEnumerable<IInnerCameraModelDetector> cameraModelDetectors, ICameraDetector cameraDetector, ILoggerFactory loggerFactory)
        {
            Logger = loggerFactory.CreateLogger<CameraModelDetector>();
            ProductCameraModelDetectors = productCameraModelDetectors;
            CameraModelDetectors = cameraModelDetectors;
            CameraDetector = cameraDetector;
        }

        public CameraModels GetCameraModels(CardInfo cardInfo, SoftwareInfo softwareInfo)
        {
            Logger.LogTrace("Detecting camera models from {0}", cardInfo.DriveLetter);

            var models = GetCameraModels(softwareInfo);
            if (models != null)
                return models;

            var cameraInfo = CameraDetector.GetCamera(cardInfo);
            if (cameraInfo == null)
                return null;

            var cameraModels = GetCameraModels(cardInfo, cameraInfo);

            return new CameraModels
            {
                Info = cameraInfo,
                Models = cameraModels.Collapse(cameraInfo)
            };
        }

        private CameraModels GetCameraModels(SoftwareInfo softwareInfo)
        {
            return ProductCameraModelDetectors
                .Select(d => d.GetCameraModels(softwareInfo))
                .FirstOrDefault(c => c != null);
        }

        private CameraModelInfo[] GetCameraModels(CardInfo cardInfo, CameraInfo cameraInfo)
        {
            return CameraModelDetectors
                .Select(d => d.GetCameraModels(cardInfo, cameraInfo))
                .FirstOrDefault(c => c != null);
        }
    }
}
