using Microsoft.Extensions.Logging;
using Net.Chdk.Model.Camera;
using Net.Chdk.Model.CameraModel;
using System;

namespace Net.Chdk.Detectors.CameraModel
{
    public abstract class CameraModelDetectorBase
    {
        protected ILoggerFactory LoggerFactory { get; }

        protected CameraModelDetectorBase(ILoggerFactory loggerFactory)
        {
            LoggerFactory = loggerFactory;
        }

        protected CameraList GetCameraList(CameraInfo cameraInfo, CameraModelInfo[] cameraModels)
        {
            return new CameraList
            {
                Info = cameraInfo,
                Models = SelectModels(cameraInfo, cameraModels)
            };
        }

        private CameraModelInfo[] SelectModels(CameraInfo cameraInfo, CameraModelInfo[] cameraModels)
        {
            // IXUS 132/135
            if (cameraModels?.Length > 1 && cameraModels[0].Names.Length > 1)
            {
                for (int i = 0; i < cameraModels.Length; i++)
                {
                    var model = cameraModels[i];
                    foreach (var name in model.Names)
                    {
                        if (name.Equals(cameraInfo.Base.Model, StringComparison.InvariantCultureIgnoreCase))
                        {
                            return new[] { cameraModels[i] };
                        }
                    }
                }
            }
            return cameraModels;
        }
    }
}
