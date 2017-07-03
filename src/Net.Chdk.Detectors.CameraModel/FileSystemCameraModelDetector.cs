﻿using Microsoft.Extensions.Logging;
using Net.Chdk.Model.Camera;
using Net.Chdk.Model.CameraModel;
using Net.Chdk.Model.Card;
using Net.Chdk.Providers.CameraModel;
using System;
using System.Threading;

namespace Net.Chdk.Detectors.CameraModel
{
    sealed class FileSystemCameraModelDetector : IInnerCameraModelDetector
    {
        private ILogger Logger { get; }
        private ICameraModelProvider CameraModelProvider { get; }

        public FileSystemCameraModelDetector(ICameraModelProvider cameraModelProvider, ILoggerFactory loggerFactory)
        {
            Logger = loggerFactory.CreateLogger<FileSystemCameraModelDetector>();
            CameraModelProvider = cameraModelProvider;
        }

        public CameraModelInfo[] GetCameraModels(CardInfo cardInfo, CameraInfo cameraInfo, IProgress<double> progress, CancellationToken token)
        {
            Logger.LogTrace("Detecting camera models from camera info");

            var models = CameraModelProvider.GetCameraModels(cameraInfo);
            if (models == null)
                return null;

            return models.Models;
        }
    }
}
