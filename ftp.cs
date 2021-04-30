using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using System.Windows.Forms;

namespace FileCom
{
    public class ftp
    {
        private string host = "";
        private string UserId = "";
        private string Password = "";

        public ftp(string hostInp, string userInp, string passwordInp)
        {
            this.host = "ftp://" + hostInp;
            this.UserId = userInp;
            this.Password = passwordInp;
        }

        public string getHost()
        {
            return this.host;
        }

        public string getUser()
        {
            return this.UserId;
        }

        public bool CreateFolder(string path)
        {
            bool IsCreated = true;
            try
            {
                WebRequest request = WebRequest.Create(host + path);
                request.Method = WebRequestMethods.Ftp.MakeDirectory;
                request.Credentials = new NetworkCredential(UserId, Password);
                using (var resp = (FtpWebResponse)request.GetResponse())
                {
                    //MessageBox.Show(Convert.ToString(resp.StatusCode));
                    MessageBox.Show("Папка успешно создана!");
                }
            }
            catch (Exception ex)
            {
                IsCreated = false;
            }
            return IsCreated;
        }

        public void UploadFile(string From, string To)
        {
            using (WebClient client = new WebClient())
            {
                client.Credentials = new NetworkCredential(UserId, Password);
                client.UploadFile(To, WebRequestMethods.Ftp.UploadFile, From);
            }
        }

        public List<string> GetAllFtpFiles(string Folderpath)
        {
            try
            {
                string ParentFolderpath = host + Folderpath;
                FtpWebRequest ftpRequest = (FtpWebRequest)WebRequest.Create(ParentFolderpath);
                ftpRequest.Credentials = new NetworkCredential(UserId, Password);
                ftpRequest.Method = WebRequestMethods.Ftp.ListDirectory;
                FtpWebResponse response = (FtpWebResponse)ftpRequest.GetResponse();
                StreamReader streamReader = new StreamReader(response.GetResponseStream());

                List<string> directories = new List<string>();

                string line = streamReader.ReadLine();
                while (!string.IsNullOrEmpty(line))
                {
                    var lineArr = line.Split('/');
                    line = lineArr[lineArr.Count() - 1];
                    directories.Add(line);
                    line = streamReader.ReadLine();
                }

                streamReader.Close();

                return directories;
            }
            catch (Exception ex)
            {
                MessageBox.Show(Convert.ToString(ex));
                throw ex;
            }
        }

        public void DeleteFTPFolder(string Folderpath)
        {
            try
            {
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(host + Folderpath);
                request.Method = WebRequestMethods.Ftp.RemoveDirectory;
                request.Credentials = new System.Net.NetworkCredential(UserId, Password); ;
                request.GetResponse().Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(Convert.ToString(ex));
            }
        }


    }
}
