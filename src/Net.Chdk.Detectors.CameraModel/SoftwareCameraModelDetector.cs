using Microsoft.Extensions.Logging;
using Net.Chdk.Model.Card;
using Net.Chdk.Model.Software;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Net.Chdk.Detectors.CameraModel
{
    sealed class SoftwareCameraModelDetector : IOuterCameraModelDetector
    {
        private ILogger Logger { get; }
        private IEnumerable<IProductCameraModelDetector> CameraModelDetectors { get; }

        public SoftwareCameraModelDetector(IEnumerable<IProductCameraModelDetector> cameraModelDetectors, ILoggerFactory loggerFactory)
        {
            Logger = loggerFactory.CreateLogger<SoftwareCameraModelDetector>();
            CameraModelDetectors = cameraModelDetectors;
        }

        public CameraModels GetCameraModels(CardInfo cardInfo, SoftwareInfo softwareInfo, IProgress<double> progress)
        {
            Logger.LogTrace("Detecting camera models from {0} software", cardInfo.DriveLetter);

            return CameraModelDetectors
                .Select(d => d.GetCameraModels(softwareInfo, progress))
                .FirstOrDefault(c => c != null);
        }
    }
}
