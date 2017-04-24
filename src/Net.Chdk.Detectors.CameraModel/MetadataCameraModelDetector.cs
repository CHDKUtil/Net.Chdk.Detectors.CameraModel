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

            var cameraModel = GetValue(cardInfo);
            if (cameraModel == null)
                return null;

            if (!Validate(cameraModel.Version))
                return null;

            if (!Validate(cameraModel.Names))
                return null;

            return new[] { cameraModel };
        }

        protected override string FileName => "MODEL.JSN";

        private static bool Validate(Version version)
        {
            if (version == null)
                return false;

            if (version.Major < 1 || version.Minor < 0)
                return false;

            return true;
        }

        private static bool Validate(string[] names)
        {
            if (names == null)
                return false;

            if (names.Length == 0)
                return false;

            if (names.Any(string.IsNullOrEmpty))
                return false;

            return true;
        }
    }
}
