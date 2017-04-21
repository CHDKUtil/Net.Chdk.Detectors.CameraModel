using Microsoft.Extensions.Logging;
using Net.Chdk.Detectors.Camera;
using Net.Chdk.Model.CameraModel;
using Net.Chdk.Model.Card;
using Net.Chdk.Providers.CameraModel;
using System.Collections.Generic;
using System.Linq;

namespace Net.Chdk.Detectors.CameraModel
{
    public sealed class CameraModelDetector : ICameraModelDetector
    {
        private ILoggerFactory LoggerFactory { get; }
        private IEnumerable<ICameraModelDetector> CameraModelDetectors { get; }

        public CameraModelDetector(ICameraDetector cameraDetector, ICameraModelProvider cameraModelProvider, ILoggerFactory loggerFactory)
        {
            LoggerFactory = loggerFactory;
            CameraModelDetectors = new ICameraModelDetector[]
            {
                new MetadataCameraModelDetector(LoggerFactory),
                new FileSystemCameraModelDetector(cameraDetector, cameraModelProvider)
            };
        }

        public CameraModelInfo[] GetCameraModels(CardInfo cardInfo)
        {
            return CameraModelDetectors
                .Select(d => d.GetCameraModels(cardInfo))
                .FirstOrDefault(c => c != null);
        }
    }
}
