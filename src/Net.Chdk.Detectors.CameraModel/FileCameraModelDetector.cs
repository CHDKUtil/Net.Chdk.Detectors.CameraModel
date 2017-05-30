using Microsoft.Extensions.Logging;
using Net.Chdk.Detectors.Camera;
using Net.Chdk.Providers.CameraModel;
using System;

namespace Net.Chdk.Detectors.CameraModel
{
    public sealed class FileCameraModelDetector : IFileCameraModelDetector
    {
        private ILogger Logger { get; }
        private IFileCameraDetector FileCameraDetector { get; }
        private ICameraModelProvider CameraModelProvider { get; }

        public FileCameraModelDetector(IFileCameraDetector fileCameraDetector, ICameraModelProvider cameraModelProvider, ILoggerFactory loggerFactory)
        {
            Logger = loggerFactory.CreateLogger<FileCameraModelDetector>();
            FileCameraDetector = fileCameraDetector;
            CameraModelProvider = cameraModelProvider;
        }

        public CameraModels GetCameraModels(string filePath, IProgress<double> progress)
        {
            Logger.LogTrace("Detecting camera models from {0}", filePath);

            var cameraInfo = FileCameraDetector.GetCamera(filePath);
            if (cameraInfo == null)
                return null;

            var cameraModels = CameraModelProvider.GetCameraModels(cameraInfo);

            return new CameraModels
            {
                Info = cameraInfo,
                Models = cameraModels.Collapse(cameraInfo)
            };
        }
    }
}
