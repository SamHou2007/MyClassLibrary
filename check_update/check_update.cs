using System.Diagnostics;
using System.IO;
using System.Net;
using System.Windows.Forms;


namespace MyClassLibrary
{
    ///<summary>
    ///检查更新
    ///</summary>
    public static class check_update
    {
        /// <summary>
        /// 开始进行分解
        /// </summary>
        /// <param name="start">指示检查时是否为启动时，如果为true，当没有更新时将不弹出窗口</param>
        /// <param name="version_number">指示当前版本号</param>
        /// <param name="text_url">指示检查更新版本号文件的url</param>
        /// <param name="installer_url">指示安装包的url</param>
        /// <param name="release_notes_url">指示更新说明的url</param>
        public static void start_check_update(bool start, string version_number, string text_url,  string installer_url,string release_notes_url)
        {
            string temp = System.Environment.GetEnvironmentVariable("TEMP");
            bool need_update = false;
            WebClient webClient = new WebClient();
            try
            {
                File.Delete($@"{temp}\Setup.msi");
                File.Delete($@"{temp}\version_number.txt");
                File.Delete($@"{temp}\release_notes");
            }
            catch
            {
                
            }
            try
            {
                webClient.DownloadFile(text_url, $@"{temp}\version_number.txt");
                webClient.DownloadFile(release_notes_url, $@"{temp}\release_notes.txt");
            }
            catch
            {
                MessageBox.Show("无网络连接！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (File.ReadAllText($@"{temp}\version_number.txt") != version_number)
            {
                DialogResult dialogResult = MessageBox.Show("检测到更新，是否下载并安装？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                DialogResult showResult = MessageBox.Show("是否显示更新日志？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (dialogResult == DialogResult.Yes)
                {
                    if (showResult == DialogResult.Yes)
                    {
                        if (File.Exists($@"{temp}\release_notes.txt"))
                            Process.Start("notepad.exe", $@"{temp}\release_notes.txt");
                        else
                            MessageBox.Show("暂时无法显示日志，文件可能已经被删除。", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    MessageBox.Show("已开始下载，请稍等。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    try
                    {
                        webClient.DownloadFile(installer_url, $@"{temp}\Setup.msi");
                        need_update = true;
                    }
                    catch
                    {
                        need_update = false;
                        MessageBox.Show("下载失败，请检查网络连接并重试！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                if (start == false)
                {
                    MessageBox.Show("没有更新！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            if (need_update == true)
            {
                Process process = new Process();
                process.StartInfo.FileName = $@"{temp}\Setup.msi";
                process.Start();
            }
        }
    }
}