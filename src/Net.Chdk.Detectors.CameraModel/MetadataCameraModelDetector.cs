using Microsoft.Extensions.Logging;
using Net.Chdk.Model.Camera;
using Net.Chdk.Model.CameraModel;
using Net.Chdk.Model.Card;

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
            return modelInfo != null
                ? new[] { modelInfo }
                : null;
        }

        protected override string FileName => "MODEL.JSN";
    }
}
