using Microsoft.Extensions.Logging;
using Net.Chdk.Model.Camera;
using Net.Chdk.Model.CameraModel;
using Net.Chdk.Model.Card;
using System;
using System.Linq;

namespace Net.Chdk.Detectors.CameraModel
{
    sealed class MetadataCameraModelDetector : MetadataDetector<MetadataCameraModelDetector, CameraModelInfo>, IInnerCameraModelDetector
    {
        public MetadataCameraModelDetector(ILoggerFactory loggerFactory)
            : base(loggerFactory)
        {
        }

        public CameraModelInfo[] GetCameraModels(CardInfo cardInfo, CameraInfo cameraInfo)
        {
            Logger.LogTrace("Detecting camera models from {0} metadata", cardInfo.DriveLetter);

            var modelInfo = GetValue(cardInfo);
            if (modelInfo == null)
                return null;

            if (!ValidateVersion(modelInfo))
                return null;

            if (!ValidateNames(modelInfo))
                return null;

            return new[] { modelInfo };
        }

        protected override string FileName => "MODEL.JSN";

        private static bool ValidateVersion(CameraModelInfo modelInfo)
        {
            Version version;
            if (!Version.TryParse(modelInfo.Version, out version))
                return false;

            if (version.Major < 1 || version.Minor < 0)
                return false;

            return true;
        }

        private static bool ValidateNames(CameraModelInfo modelInfo)
        {
            if (modelInfo.Names == null)
                return false;

            if (modelInfo.Names.Length == 0)
                return false;

            if (modelInfo.Names.Any(string.IsNullOrEmpty))
                return false;

            return true;
        }
    }
}
