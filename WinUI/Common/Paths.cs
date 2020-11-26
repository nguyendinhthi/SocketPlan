using System;
using System.Collections.Generic;
using System.IO;

namespace SocketPlan.WinUI
{
    public class Paths
    {
        /// <summary>
        /// 画像のアドレスを取得する。画像が存在しなければ既定のエラー画像を返す。
        /// </summary>
        /// <param name="relativePath">Imagesフォルダからの相対パスを指定</param>
        public static string GetImagePath(string relativePath)
        {
            string imageDirectory = Properties.Settings.Default.ImageDirectory;

            var imagePath = imageDirectory + relativePath;

            if (File.Exists(imagePath))
                return imagePath;
            else
                return imageDirectory + "NotFound.png";
        }

        public static string GetImageFullPath(string path)
        {
            if (File.Exists(path))
                return path;
            else
                return Properties.Settings.Default.ImageDirectory + "NotFound.png";
        }

        public static string GetRelativeImagePath(string fullPath)
        {
            string imageDirectory = Properties.Settings.Default.ImageDirectory;

            return fullPath.Replace(imageDirectory, "");
        }

        /// <summary>
        /// Blockのアドレスを取得する。Blockが存在しなければ例外
        /// </summary>
        /// <param name="relativePath">Blocksフォルダからの相対パスを指定</param>
        public static string GetBlockPath(string relativePath)
        {
            string blockDirectory = Properties.Settings.Default.BlockDirectory;

            var blockPath = blockDirectory + relativePath;

            if (File.Exists(blockPath))
                return blockPath;
            else
                throw new ApplicationException(Messages.NotFoundBlock(blockPath));
        }

        public static string GetBlockPathWithoutException(string relativePath)
        {
            string blockDirectory = Properties.Settings.Default.BlockDirectory;

            return blockDirectory + relativePath;
        }

        public static string GetRelativeBlockPath(string fullPath)
        {
            string blockDirectory = Properties.Settings.Default.BlockDirectory;

            return fullPath.Replace(blockDirectory, "");
        }

        /// <summary>
        /// SystemBlocksのアドレスを取得する。SystemBlocksが存在しなければ例外
        /// </summary>
        /// <param name="relativePath">Blocksフォルダからの相対パスを指定</param>
        public static string GetSystemBlockPath(string relativePath)
        {
            string blockDirectory = Properties.Settings.Default.SystemBlockDirectory;

            var blockPath = blockDirectory + relativePath;

            if (File.Exists(blockPath))
                return blockPath;
            else
                throw new ApplicationException(Messages.NotFoundBlock(blockPath));
        }

        public static string GetServerSystemDirectory()
        {
#if DEBUG
            return Properties.Settings.Default.ServerSystemDirectory_Debug;
#else
            return Properties.Settings.Default.ServerSystemDirectory_Release;
#endif
        }

        public static List<string> GetServerDrawingDirectories()
        {
            var list = new List<string>();

#if DEBUG
            var directories = Properties.Settings.Default.ServerDrawingDirectories_Debug;
#else
            var directories = Properties.Settings.Default.ServerDrawingDirectories_Release;
#endif

            foreach (var directory in directories)
            {
                list.Add(directory);
            }

            return list;
        }

        public static string GetLightSerialCsvDirectory()
        {
#if DEBUG
            return Properties.Settings.Default.LightSerialCsvExportDirectory_Debug;
#else
            return Properties.Settings.Default.LightSerialCsvExportDirectory_Release;
#endif
        }
    }
}
