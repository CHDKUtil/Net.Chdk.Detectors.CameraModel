using Microsoft.Extensions.Logging;
using Net.Chdk.Model.Camera;
using Net.Chdk.Model.CameraModel;
using Net.Chdk.Model.Card;
using Net.Chdk.Validators;
using System;

namespace Net.Chdk.Detectors.CameraModel
{
    [Obsolete]
    sealed class MetadataCameraModelDetector : MetadataDetector<MetadataCameraModelDetector, CameraModelInfo>, IInnerCameraModelDetector
    {
        public MetadataCameraModelDetector(IValidator<CameraModelInfo> validator, ILoggerFactory loggerFactory)
            : base(validator, loggerFactory)
        {
        }

        public CameraModelInfo[] GetCameraModels(CardInfo cardInfo, CameraInfo cameraInfo, IProgress<double> progress)
        {
            Logger.LogTrace("Detecting camera models from {0} metadata", cardInfo.DriveLetter);

            var cameraModel = GetValue(cardInfo, progress);
            if (cameraModel == null)
                return null;

            return new[] { cameraModel };
        }

        protected override string FileName => Files.Metadata.CameraModel;
    }
}
