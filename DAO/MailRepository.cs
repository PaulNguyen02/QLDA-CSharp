using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MailKit.Net.Imap;
using MailKit.Search;
using MailKit;
using MimeKit;
using System.Net;
using System.Threading;
using System.Windows.Forms;

namespace WindowsFormsApp1.DAO
{
    class MailRepository
    {
        private string mailServer, login, password;
        private int port;
        private bool ssl;
        private string datetime, sender, subject, content;
        private DTO.Messages m;
        private List<DTO.Messages> mess = new List<DTO.Messages>();
        private List<IMailFolder> listfolder = new List<IMailFolder>();
        private int mailcount;
        public MailRepository(string mailServer, int port, bool ssl, string login, string password)
        {
            this.mailServer = mailServer;
            this.port = port;
            this.ssl = ssl;
            this.login = login;
            this.password = password;
        }
        public int getIbcount()     //Đếm tổng số lượng thư trong hộp thư
        {
            try
            {
                using (var client = new ImapClient())
                {
                    using (var cancel = new CancellationTokenSource())
                    {
                        client.Connect(mailServer, port, ssl, cancel.Token);
                        client.AuthenticationMechanisms.Remove("XOAUTH");

                        client.Authenticate(login, password, cancel.Token);
                        var inbox = client.Inbox;
                        inbox.Open(FolderAccess.ReadOnly, cancel.Token);
                        mailcount = inbox.Count;
                    }
                }
            }
            catch(Exception sq)
            {
                MessageBox.Show("Tài khoản đăng nhập dịch vụ này bị lỗi, thử lại sau !","Đăng nhập dih5 vụ thất bại !");
            }
            return mailcount;
        }
        public int NumofSentEmail()     //Số lượng mail đã gửi
        {
            int num = 0;
            using (var client = new ImapClient())
            {
                using (var cancel = new CancellationTokenSource())
                {
                    client.Connect(mailServer, port, ssl, cancel.Token);

                    // Nếu muốn tắt một cơ chế bảo mât, có thể xóa cơ chế đó như sau: 
                    client.AuthenticationMechanisms.Remove("XOAUTH");
                    client.Authenticate(login, password, cancel.Token);
                    IMailFolder personal = client.GetFolder(client.PersonalNamespaces[0]);
                    foreach (IMailFolder folder in personal.GetSubfolders(false, new CancellationToken()))
                    {
                        listfolder.Add(folder);
                    }
                    var sent = listfolder.ElementAt(6);          //Lấy phần tử vị trí 6 là folder hộp thư
                    sent.Open(FolderAccess.ReadOnly, cancel.Token);     //Cho phép truy cập folder hộp thư
                    num += sent.Count;
                }
            }
            return num;
        }
        public List<DTO.Messages> LoadMail()
        {        
            try
            {
                using (var client = new ImapClient())
                {
                    using (var cancel = new CancellationTokenSource())
                    {
                        client.Connect(mailServer, port, ssl, cancel.Token);

                        // Nếu muốn tắt một cơ chế bảo mât, có thể xóa cơ chế đó như sau:
                        client.AuthenticationMechanisms.Remove("XOAUTH");
                        client.Authenticate(login, password, cancel.Token);

                        // Thư mục hộp thư luôn có sẵn
                        var inbox = client.Inbox;
                        inbox.Open(FolderAccess.ReadOnly, cancel.Token);

                        // Tải mỗi tin nhắn dựa trên chỉ số tin nhắn
                        for (int i = 0; i < inbox.Count; i++)
                        {
                            var message = inbox.GetMessage(i, cancel.Token);
                            datetime = message.Date.DateTime.ToString();
                            sender = message.From.OfType<MailboxAddress>().Single().Address.ToString();
                            subject = message.Subject;
                            content = message.TextBody;
                            m = new DTO.Messages() { datetime = datetime, sender = sender, subject = subject, content = content };
                            mess.Add(m);
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Lỗi đăng nhập","Thử lại sau");
            }                
            return mess;
        }
        public List<DTO.Messages> LoadSpecifiedMail(String email)
        {
            using (var client = new ImapClient())
            {
                using (var cancel = new CancellationTokenSource())
                {
                    client.Connect(mailServer, port, ssl, cancel.Token);
                    client.AuthenticationMechanisms.Remove("XOAUTH");
                    client.Authenticate(login, password, cancel.Token);
                    var inbox = client.Inbox;
                    inbox.Open(FolderAccess.ReadOnly, cancel.Token);
                    var query = SearchQuery.FromContains(email);    //Tìm theo địa chỉ email
                    var uids = inbox.Search(query);     //uids là tập các tin nhắn được xác định bởi 1 email
                    // Duyệt tập tin nhắn của 1 địa chỉ email xác định
                    foreach (var uid in uids)
                    {
                        var message = inbox.GetMessage(uid);
                        datetime = message.Date.DateTime.ToString();
                        sender = message.From.OfType<MailboxAddress>().Single().Address.ToString();
                        subject = message.Subject;
                        content = message.TextBody;
                        m = new DTO.Messages() { datetime = datetime, sender = sender, subject = subject, content = content };
                        mess.Add(m);
                    }
                }
            }
            return mess;
        }
    }
}
