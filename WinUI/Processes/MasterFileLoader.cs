using System;
using System.ComponentModel;
using System.IO;
using System.Text;
using System.Runtime.InteropServices;

namespace SocketPlan.WinUI
{
    public class MasterFileLoader
    {
        private BackgroundWorker bw;

        int localVersion;
        int serverVersion;
        private int fileCount;
        private int progressCount;

        public const string VERSION_TEXT = @"MasterVersion.txt";

        public void Run(object sender, DoWorkEventArgs e)
        {
            this.bw = (BackgroundWorker)sender;
            this.bw.ReportProgress(0, "Checking update...");

            var source = Paths.GetServerSystemDirectory();
            var destination = Properties.Settings.Default.SystemDirectory;

            var userName = Properties.Settings.Default.ServerUserName;
            var password = Properties.Settings.Default.ServerPassword;
            MasterFileLoader.Authorize(source, userName, password);

            this.localVersion = GetMasterVersion(destination);
            this.serverVersion = GetMasterVersion(source);
            if (this.serverVersion == 0)
                throw new ApplicationException(Messages.FailedToGetMasterVersion(source + VERSION_TEXT));

            if (this.serverVersion <= this.localVersion)
                return;

            this.progressCount = 0;
            this.fileCount = this.GetFileCount(source);
            if (this.fileCount == 0)
                return;

            this.CopyDirectory(source, destination);
        }

        public static int GetMasterVersion(string unitWiringDirectory)
        {
            var filePath = unitWiringDirectory + VERSION_TEXT;

            if (!File.Exists(filePath))
                return 0;

            string text = string.Empty;
            using (var sr = new StreamReader(filePath, Encoding.GetEncoding("Shift_JIS")))
            {
                text = sr.ReadToEnd();
            }

            int version;
            if (int.TryParse(text, out version))
                return version;
            else
                return 0;
        }

        private int GetFileCount(string sourceDirName)
        {
            int count = 0;

            string[] files = Directory.GetFiles(sourceDirName);
            count += files.Length;

            string[] dirs = Directory.GetDirectories(sourceDirName);
            foreach (string dir in dirs)
            {
                count += GetFileCount(dir);
            }

            return count;
        }

        private void CopyDirectory(string sourceDirName, string destDirName)
        {
            //コピー先のディレクトリがないときは作る
            if (!Directory.Exists(destDirName))
            {
                Directory.CreateDirectory(destDirName);
                ////属性もコピー
                File.SetAttributes(destDirName, File.GetAttributes(sourceDirName));
            }

            //コピー先のディレクトリ名の末尾に"\"をつける
            if (destDirName[destDirName.Length - 1] != Path.DirectorySeparatorChar)
                destDirName = destDirName + Path.DirectorySeparatorChar;

            //コピー元のディレクトリにあるファイルをコピー
            string[] files = Directory.GetFiles(sourceDirName);
            foreach (string file in files)
            {
                var dest = destDirName + Path.GetFileName(file);

                //読み取り専用のファイルに上書きしようとして、エラーになることがあるので、属性を外す
                if (File.Exists(dest))
                {
                    FileInfo fileInfo = new FileInfo(dest);
                    if ((fileInfo.Attributes & FileAttributes.ReadOnly) == FileAttributes.ReadOnly)
                        fileInfo.Attributes = FileAttributes.Normal;
                }

                //更新日時が違っていたらコピーする
                var updatedDateServer = File.GetLastWriteTime(file);
                var updatedDateLocal = File.GetLastWriteTime(dest);
                if(updatedDateServer != updatedDateLocal)
                    File.Copy(file, dest, true);

                this.progressCount++;
                var progress = (decimal)this.progressCount / (decimal)this.fileCount * 100;
                this.bw.ReportProgress((int)progress, "Updating Master files. Ver." + this.localVersion + "->" + this.serverVersion + " ["+this.progressCount+"/"+this.fileCount+"]");
            }

            //コピー元のディレクトリにあるディレクトリについて、
            //再帰的に呼び出す
            string[] dirs = Directory.GetDirectories(sourceDirName);
            foreach (string dir in dirs)
            {
                this.CopyDirectory(dir, destDirName + Path.GetFileName(dir));
            }
        }

        public static void Authorize(string shareName, string userName, string password)
        {
            //参考：http://handcraft.blogsite.org/Memo/Article/Archives/49

            NETRESOURCE netResource = new NETRESOURCE();
            netResource.dwScope = 0;
            netResource.dwType = 1;
            netResource.dwDisplayType = 0;
            netResource.dwUsage = 0;
            netResource.lpLocalName = ""; // ネットワークドライブにする場合は"z:"などドライブレター設定  
            netResource.lpRemoteName = shareName.TrimEnd('\\');
            netResource.lpProvider = "";

            try
            {
                //共有フォルダに認証情報を使って接続
                WNetAddConnection2(ref netResource, password, userName, 0);
            }
            catch
            {
                //既に認証されている場合は例外が発生するので握りつぶす
            }
        }

        [DllImport("mpr.dll", EntryPoint = "WNetAddConnection2", CharSet = CharSet.Unicode)]
        private static extern int WNetAddConnection2(ref NETRESOURCE lpNetResource, string lpPassword, string lpUsername, Int32 dwFlags);

        [StructLayout(LayoutKind.Sequential)]
        internal struct NETRESOURCE
        {
            public int dwScope;
            public int dwType;
            public int dwDisplayType;
            public int dwUsage;
            [MarshalAs(UnmanagedType.LPWStr)]
            public string lpLocalName;
            [MarshalAs(UnmanagedType.LPWStr)]
            public string lpRemoteName;
            [MarshalAs(UnmanagedType.LPWStr)]
            public string lpComment;
            [MarshalAs(UnmanagedType.LPWStr)]
            public string lpProvider;
        }
    }
}