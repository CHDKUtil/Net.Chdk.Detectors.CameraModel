using Microsoft.Extensions.Logging;
using Net.Chdk.Model.CameraModel;
using Net.Chdk.Model.Card;
using System.Collections.Generic;
using System.Linq;

namespace Net.Chdk.Detectors.CameraModel
{
    public sealed class CameraModelDetector : ICameraModelDetector
    {
        private ILoggerFactory LoggerFactory { get; }
        private IEnumerable<ICameraModelDetector> CameraModelDetectors { get; }

        public CameraModelDetector(ILoggerFactory loggerFactory)
        {
            LoggerFactory = loggerFactory;
            CameraModelDetectors = new ICameraModelDetector[]
            {
                new MetadataCameraModelDetector(LoggerFactory),
            };
        }

        public CameraModelInfo GetCameraModel(CardInfo cardInfo)
        {
            return CameraModelDetectors
                .Select(d => d.GetCameraModel(cardInfo))
                .FirstOrDefault(c => c != null);
        }
    }
}
