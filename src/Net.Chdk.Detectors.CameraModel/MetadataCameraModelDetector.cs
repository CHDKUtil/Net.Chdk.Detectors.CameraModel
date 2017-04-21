using Net.Chdk.Model.CameraModel;
using Net.Chdk.Model.Card;
using Microsoft.Extensions.Logging;

namespace Net.Chdk.Detectors.CameraModel
{
    sealed class MetadataCameraModelDetector : MetadataDetector<MetadataCameraModelDetector, CameraModelInfo>, ICameraModelDetector
    {
        public MetadataCameraModelDetector(ILoggerFactory loggerFactory)
            : base(loggerFactory)
        {
        }

        public CameraModelInfo[] GetCameraModels(CardInfo cardInfo)
        {
            var modelInfo = GetValue(cardInfo);
            return modelInfo != null
                ? new[] { modelInfo }
                : null;
        }

        protected override string FileName => "MODEL.JSN";
    }
}
