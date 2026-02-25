using System;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Reflection;
using System.Xml;
using Microsoft.SharePoint;
using SPEventReceiverManager.Properties;

namespace SPEventReceiverManager.AutoUpdater
{
    public class UpdateResultEventArgs : EventArgs
    {
        public string DownloadUrl { get; set; }
    }

    internal class UpdaterService : IDisposable
    {
        private BackgroundWorker m_BackgroundWorker;

        public event EventHandler<UpdateResultEventArgs> UpdateAvailable;

        public UpdaterService()
        {
            m_BackgroundWorker = new BackgroundWorker();
            m_BackgroundWorker.DoWork += delegate
            {
                CheckForUpdates();
            };
        }

        ~UpdaterService()
        {
            Dispose(false);
        }

        public void CheckForUpdatesAsync()
        {
            if (!m_BackgroundWorker.IsBusy)
            {
                m_BackgroundWorker.RunWorkerAsync();
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void OnUpdateAvailable(string downloadId)
        {
            if (this.UpdateAvailable != null)
            {
                UpdateResultEventArgs e = new UpdateResultEventArgs();
                e.DownloadUrl = Settings.Default.DownloadBaseUrl + downloadId;
                this.UpdateAvailable(this, e);
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing && m_BackgroundWorker != null)
            {
                m_BackgroundWorker.Dispose();
            }
        }

        private void CheckForUpdates()
        {
            string versionHistoryUrl = Settings.Default.VersionHistoryUrl;
            Version version = Assembly.GetExecutingAssembly().GetName().Version;
            Assembly assembly = Assembly.GetAssembly(typeof(SPSite));
            string text = ((assembly.GetName().Version.Major == 14) ? "2010" : "2007");
            string downloadId = "";
            Version version2 = new Version("1.0.0.0");
            try
            {
                HttpWebRequest httpWebRequest = WebRequest.Create(versionHistoryUrl) as HttpWebRequest;
                using (HttpWebResponse httpWebResponse = httpWebRequest.GetResponse() as HttpWebResponse)
                {
                    Stream responseStream = httpWebResponse.GetResponseStream();
                    XmlReader xmlReader = XmlReader.Create(responseStream);
                    while (xmlReader.Read())
                    {
                        if (string.Equals(xmlReader.Name, "Version", StringComparison.OrdinalIgnoreCase))
                        {
                            string attribute = xmlReader.GetAttribute("Number");
                            string attribute2 = xmlReader.GetAttribute("Platform");
                            Version version3 = new Version(attribute);
                            if (attribute2 == text && version3 > version2)
                            {
                                downloadId = xmlReader.GetAttribute("DownloadId");
                                version2 = version3;
                            }
                        }
                    }
                }
            }
            catch (WebException)
            {
                return;
            }
            if (version2 != version)
            {
                OnUpdateAvailable(downloadId);
            }
        }
    }
}
