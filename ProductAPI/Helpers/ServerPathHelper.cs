
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Renci.SshNet;
using Renci.SshNet.Sftp;
using System;
using System.IO;
using System.Threading.Tasks;

namespace SeminarAPI.Helpers
{
    public class ServerPathHelper
    {
        private readonly string _host;
        private readonly string _username;
        private readonly string _password;

        public ServerPathHelper(string host, string username, string password)
        {
            _host = host;
            _username = username;
            _password = password;
        }

        public string ListDirectoryContents(string remoteDirectoryPath)
        {
            using (var client = new SshClient(_host, _username, _password))
            {
                client.Connect();

                if (client.IsConnected)
                {
                    var sftp = new SftpClient(client.ConnectionInfo);
                    sftp.Connect();

                    var files = sftp.ListDirectory(remoteDirectoryPath);

                    Console.WriteLine($"Contents of {remoteDirectoryPath}:");

                    foreach (var file in files)
                    {
                        Console.WriteLine(file.Name);
                    }

                    sftp.Disconnect();
                }
                else
                {
                    Console.WriteLine("Unable to connect to the server.");
                }
                client.Disconnect();
                return null;
            }
        }

        public void CreateDirectoryFolder(string remoteDirectoryPath)
        {
            using (var client = new SshClient(_host, _username, _password))
            {
                client.Connect();

                if (client.IsConnected)
                {
                    var command = client.RunCommand($"ls {remoteDirectoryPath}");

                    // check folder
                    // nếu tồn tại folder thì disconect kết nối
                    // không tồn tại thì khởi tạo folder.
                    if (string.IsNullOrEmpty(command.Error) && !string.IsNullOrEmpty(command.Result))
                    {
                        client.Disconnect();
                    }
                    else
                    {
                        client.RunCommand($"mkdir -p {remoteDirectoryPath}");
                    }

                    client.Disconnect();
                }
               
                client.Disconnect();
            }
        }

        public string UploadFile(IFormFile file, string remoteFilePath)
        {
            using (var client = new SftpClient(_host, _username, _password))
            {
                client.Connect();

                if (client.IsConnected)
                {
                    using (var fileStream = file.OpenReadStream())
                    {
                        client.UploadFile(fileStream, remoteFilePath);

                        return remoteFilePath;
                    }
                }
                else
                {
                    return null;
                }
            }
        }

        public IFormFile DownloadFile(string remoteFilePath)
        {
            using (var client = new SftpClient(_host, _username, _password))
            {
                client.Connect();

                if (client.IsConnected)
                {
                    try
                    {
                        using (var fileStream = client.OpenRead(remoteFilePath))
                        {
                            return ConvertFileToFormFileAsync(fileStream, remoteFilePath);
                        }
                    }
                    catch (Exception ex)
                    {
                        return null;
                    }
                    finally
                    {
                        client.Disconnect();
                    }
                }
                else
                {
                    return null;
                }

            }
        }

        private IFormFile ConvertFileToFormFileAsync(SftpFileStream fileStream, string fileName)
        {
            var memoryStream = new MemoryStream();
            fileStream.CopyToAsync(memoryStream);
            memoryStream.Seek(0, SeekOrigin.Begin);

            return new FormFile(memoryStream, 0, memoryStream.Length, "file", fileName)
            {
                Headers = new HeaderDictionary(),
                ContentType = "application/octet-stream"
            };
        }

        public bool CheckFileExists(string remoteFilePath)
        {
            using (var client = new SshClient(_host, _username, _password))
            {
                client.Connect();

                if (client.IsConnected)
                {
                    try
                    {
                        using (var sftp = new SftpClient(client.ConnectionInfo))
                        {
                            sftp.Connect();

                            // Check if the file exists
                            if (sftp.Exists(remoteFilePath))
                            {
                                return true;
                            }
                            else
                            {
                                return false;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        return false;
                    }
                    finally
                    {
                        client.Disconnect();
                    }
                }
                else
                {
                    return false;
                }
            }
        }

        public bool DeleteFile(string remoteFilePath)
        {
            using (var client = new SshClient(_host, _username, _password))
            {
                client.Connect();

                if (client.IsConnected)
                {
                    try
                    {
                        using (var sftp = new SftpClient(client.ConnectionInfo))
                        {
                            sftp.Connect();

                            // Check if the file exists before deleting
                            if (sftp.Exists(remoteFilePath))
                            {
                                sftp.DeleteFile(remoteFilePath);
                                return true;
                            }
                            else
                            {
                                return false;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        return false;
                    }
                    finally
                    {
                        client.Disconnect();
                    }
                }
                else
                {
                    return false;
                }
            }
        }
    }
}
