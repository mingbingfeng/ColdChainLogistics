using C2LP.ColdStorageDataHubClient.DBHelper.BLL;
using C2LP.ColdStorageDataHubClient.DBHelper.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Configuration;
using System.Diagnostics;
using System.Reflection;
using System.Threading;

namespace C2LP.ColdStorageDataHubClient
{
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();
            _MainForm = this;
        }
        public static FrmMain _MainForm;
        /// <summary>
        /// 绑定关系
        /// </summary>
        public static List<AiInfoModel> _relationList = new List<AiInfoModel>();
        /// <summary>
        /// 日志保存路径
        /// </summary>
        public readonly static string _logSavePath = AppDomain.CurrentDomain.BaseDirectory + "log";
        /// <summary>
        /// 上报配置,配置说明请见配置文件
        /// </summary>
        public static int _nSpace, _aSpace, _uploadInteval, _uploadLimit;
        /// <summary>
        /// 日志保留天数
        /// </summary>
        public static int _logSaveDays;

        /// <summary>
        /// 是否启用自动滚屏
        /// </summary>
        private bool _enableAutoScroll = true;

        /// <summary>
        /// 上报接口
        /// </summary>
        public static WRUpload.ColdChainServer _uploadClient;

        #region 加载配置
        private void FrmMain_Load(object sender, EventArgs e)
        {
            Thread thLoad = new Thread(LoadConfig);
            thLoad.Name = "加载配置线程";
            thLoad.IsBackground = true;
            thLoad.Start();
        }

        /// <summary>
        /// 加载所有配置
        /// </summary>
        private void LoadConfig()
        {
            try
            {
                _nSpace = Convert.ToInt32(ConfigurationManager.AppSettings["NSpace"]);
                _aSpace = Convert.ToInt32(ConfigurationManager.AppSettings["ASpace"]);
                _uploadInteval = Convert.ToInt32(ConfigurationManager.AppSettings["UploadInteval"]);
                _uploadLimit = Convert.ToInt32(ConfigurationManager.AppSettings["UploadLimit"]);
                _logSaveDays = Convert.ToInt32(ConfigurationManager.AppSettings["LogSaveDays"]);
                _uploadClient = new WRUpload.ColdChainServer();
                AppendLogText("欢迎使用惊尘物流冷链数据上报系统,正在初始化...");
                _relationList = AiInfoServer.GetAllAiRelation();
                this.Invoke(new MethodInvoker(delegate
                {
                    dgvUploadTime.DataSource = AiInfoServer.GetAllUploadTime();
                    nudNSpace.Value = _nSpace;
                    nudASpace.Value = _aSpace;
                    nudUploadInteval.Value = _uploadInteval;
                    nudUploadLimit.Value = _uploadLimit;
                    gbUploadConfig.Enabled = true;
                    btnEnd.Enabled = false;
                }));
                AppendLogText(string.Format("初始化成功,您现在就可以绑定探头或者开始上报了", _logSavePath, _logSaveDays));
                AppendLogText(string.Format("日志保存路径[{0}] 日志保存天数[{1}]", _logSavePath, _logSaveDays));
            }
            catch (Exception ex)
            {
                AppendLogText("加载配置失败,请检查配置文件,错误信息:" + ex.Message);
                MessageBox.Show("错误：" + ex.Message, "加载配置失败,请检查配置文件", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnAppConfig_Click(null, null);
            }
        }
        #endregion

        #region 日志处理
        private delegate void AppendLogDelegate(string msg);
        /// <summary>
        /// 输出日志
        /// </summary>
        /// <param name="msg"></param>
        public void AppendLogText(string msg)
        {
            if (this.InvokeRequired)
            {
                AppendLogDelegate d = new AppendLogDelegate(AppendLogText);
                this.Invoke(d, msg);
            }
            else
            {
                msg = string.Format("{0}:{1}{2}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), msg, "\r\n");
                SaveLogToFile(msg);
                txtLog.AppendText(msg);
                if (_enableAutoScroll)
                {
                    this.txtLog.Focus();
                    this.txtLog.Select(this.txtLog.TextLength, 0);
                    this.txtLog.ScrollToCaret();
                }
            }
        }

        /// <summary>
        /// 保存日志到文件
        /// </summary>
        /// <param name="msg"></param>
        private void SaveLogToFile(string msg)
        {
            string currentPath = _logSavePath + "\\" + DateTime.Now.ToString("yyyyMMdd");
            if (!Directory.Exists(currentPath))
            {
                Directory.CreateDirectory(currentPath);
                while (Directory.GetDirectories(_logSavePath).Length > (_logSaveDays < 3 ? 3 : _logSaveDays))
                {
                    Directory.Delete(Directory.GetDirectories(_logSavePath)[0], true);
                }
            }
            bool saveToFileSuccess = false;
            int retryCount = 0;
            while (!saveToFileSuccess && retryCount < 3)
            {
                try
                {
                    string filePath = currentPath + "\\" + DateTime.Now.ToString("HH") + (retryCount == 0 ? "" : "_" + retryCount) + ".txt";
                    using (FileStream f = new FileStream(filePath, FileMode.Append))
                    {
                        using (StreamWriter sw = new StreamWriter(f))
                        {
                            sw.WriteLine(msg.Replace("\r\n", ""));
                            sw.Flush();
                            break;
                        }
                    }
                }
                catch
                {
                    retryCount++;
                }
            }
        }
        #endregion

        #region 按钮、事件
        private void btnBind_Click(object sender, EventArgs e)
        {
            FrmBindPort frm = new FrmBindPort();
            frm.ShowDialog();
            dgvUploadTime.DataSource = AiInfoServer.GetAllUploadTime();
            AppendLogText("打开绑定探头界面,重新加载上报进度");
        }

        private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            AppendLogText("程序退出");
            foreach (DataGridViewRow row in dgvUploadTime.Rows)
            {
                UploadHelper uh = row.Tag as UploadHelper;
                if (uh != null)
                {
                    uh.Disposed();
                }
            }
        }

        private void dgvUploadTime_DataSourceChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dgvUploadTime.Rows)
            {
                AppendLogText(string.Format("读取到上报进度：ProjectId[{0}] NetId[{1}] LastUploadTime[{2}]", row.Cells[1].Value, row.Cells[2].Value, row.Cells[3].Value));
            }
        }

        private void btnAppConfig_Click(object sender, EventArgs e)
        {
            string appPath = AppDomain.CurrentDomain.BaseDirectory + Assembly.GetExecutingAssembly().GetName().Name + ".exe";
            Process.Start(appPath + ".config");
            AppendLogText("打开配置文件");
            DialogResult dr = MessageBox.Show("是否现在就重启程序?", "修改配置文件后,将在下一次启动程序时生效!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                AppendLogText("重启程序");
                Process.Start(appPath);
                Application.Exit();
            }
        }

        private void tsmiStopScroll_Click(object sender, EventArgs e)
        {
            _enableAutoScroll = false;
        }

        private void tsmiStartScroll_Click(object sender, EventArgs e)
        {
            _enableAutoScroll = true;
        }

        private void tsmiOpenLogPath_Click(object sender, EventArgs e)
        {
            if (!Directory.Exists(_logSavePath))
                Directory.CreateDirectory(_logSavePath);
            Process.Start(_logSavePath);
        }

        private void btnBegin_Click(object sender, EventArgs e)
        {
            btnBegin.Enabled = false;
            btnEnd.Enabled = true;
            btnAppConfig.Enabled = false;
            btnBind.Enabled = false;
            AppendLogText("已开启数据上报");
            foreach (DataGridViewRow row in dgvUploadTime.Rows)
            {
                string projectId = row.Cells[1].Value.ToString();
                int netId = Convert.ToInt32(row.Cells[2].Value);
                DateTime lastUploadTime = Convert.ToDateTime(row.Cells[3].Value);
                UploadHelper uh = new UploadHelper(projectId, netId, lastUploadTime);
                row.Tag = uh;
                uh.Start();
            }
        }

        private void btnEnd_Click(object sender, EventArgs e)
        {
            btnBegin.Enabled = true;
            btnEnd.Enabled = false;
            btnAppConfig.Enabled = true;
            btnBind.Enabled = true;
            foreach (DataGridViewRow row in dgvUploadTime.Rows)
            {
                UploadHelper uh = row.Tag as UploadHelper;
                if (uh != null)
                {
                    uh.Stop();
                }
            }
            AppendLogText("本次上报结束后将停止继续上报");
        }

        /// <summary>
        /// 更新最后上报时间
        /// </summary>
        /// <param name="pid"></param>
        /// <param name="nid"></param>
        /// <param name="time"></param>
        public void UpdateLastTime(string pid, int nid, DateTime time)
        {
            this.Invoke(new MethodInvoker(delegate
            {
                foreach (DataGridViewRow row in dgvUploadTime.Rows)
                {
                    int id = Convert.ToInt32(row.Cells[0].Value);
                    string projectId = row.Cells[1].Value.ToString();
                    int netId = Convert.ToInt32(row.Cells[2].Value);
                    if (pid == projectId && nid == netId)
                    {
                        row.Cells[3].Value = time;
                        try
                        {
                            if (AiInfoServer.UpdateUploadLastTime(id, time))
                                AppendLogText(string.Format("上报成功[ProjectId={0}] [NetId={1}] 上报进度[{2}]", projectId, netId, time.ToString("yyyy-MM-dd HH:mm:ss")));
                            break;
                        }
                        catch (Exception ex)
                        {
                            AppendLogText(string.Format("更新上报进度失败[ProjectId={0}] [NetId={1}] 上报进度[{2}]:{3}", projectId, netId, time.ToString("yyyy-MM-dd HH:mm:ss"), ex.Message));
                        }
                    }
                }
            }));
        }
        #endregion

    }
}
