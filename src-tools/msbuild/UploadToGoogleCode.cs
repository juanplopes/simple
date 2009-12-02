using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;
using System.IO;
using System.Net;

namespace Simple.Tools.MsBuild
{
    /// <summary>
    /// Uploads a file and summary to google code.
    /// Props to sjalex: http://code.google.com/p/nant-googlecode/
    /// MIT License
    /// Extended by devfuel
    /// </summary>
    public class UploadToGoogleCode : Task
    {
        private static readonly byte[] NewLineAsciiBytes = Encoding.ASCII.GetBytes("\r\n");
        private static readonly string Boundary = Guid.NewGuid().ToString();

        /// <summary>
        /// Gets or sets Google user name to authenticate as (this is just the username part, don't include the @gmail.com part.
        /// </summary>
        [Required]
        public string UserName { get; set; }

        /// <summary>
        /// Gets or sets the Google Code password (not the same as the gmail password).
        /// </summary>
        [Required]
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets the Google Code project name to upload to.
        /// </summary>
        [Required]
        public string ProjectName { get; set; }

        public string Labels { get; set; }

        /// <summary>
        /// Gets or sets the local path of the file to upload.
        /// </summary>
        [Required]
        public ITaskItem LocalFile { get; set; }

        [Output]
        public ITaskItem ReceiptFile { get; private set; }

        /// <summary>
        /// Gets or sets the file name that this file will be given on Google Code.
        /// </summary>
        [Required]
        public string TargetFileName { get; set; }

        /// <summary>
        /// Gets or sets the summary of the upload.
        /// </summary>
        [Required]
        public string Summary { get; set; }

        /// <summary>
        /// Uploads the contents of the file to the project's Google Code upload url. 
        /// Performs the basic http authentication required by Google Code.
        /// </summary>
        internal void Upload()
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(String.Format("https://{0}.googlecode.com/files", ProjectName));
            request.Method = "POST";
            request.ContentType = String.Concat("multipart/form-data; boundary=" + Boundary);
            request.UserAgent = String.Concat("SimpleBuilder");
            request.Headers.Add("Authorization", String.Concat("Basic ", CreateAuthorizationToken(UserName, Password)));

            Log.LogMessage(request.UserAgent);
            Log.LogMessage("Upload URL: {0}", request.Address);
            Log.LogMessage("Username: {0}", UserName);
            Log.LogMessage("File to send: {0}", LocalFile);
            Log.LogMessage("Target file: {0}", TargetFileName);
            Log.LogMessage("Summary: {0}", Summary);

            using (Stream stream = request.GetRequestStream())
            {
                Log.LogMessage("Sending summary...");
                WriteLine(stream, String.Concat("--", Boundary));
                WriteLine(stream, @"content-disposition: form-data; name=""summary""");
                WriteLine(stream, "");
                WriteLine(stream, Summary);


                if (Labels != null)
                {
                    string[] labelArray = Labels.Split(';');

                    if (labelArray != null && labelArray.Length > 0)
                    {
                        Log.LogMessage("Setting " + labelArray.Length + " label(s)");

                        foreach (var label in labelArray)
                        {
                            WriteLine(stream, "--" + Boundary);
                            WriteLine(stream, "content-disposition: form-data; name=\"label\"");
                            WriteLine(stream, "");
                            WriteLine(stream, (label ?? "").Trim());
                        }
                    }
                }

                Log.LogMessage("Sending file...");
                WriteLine(stream, String.Concat("--", Boundary));
                WriteLine(stream, String.Format(@"content-disposition: form-data; name=""filename""; filename=""{0}""", TargetFileName));
                WriteLine(stream, "Content-Type: application/octet-stream");
                WriteLine(stream, "");
                WriteFile(stream, LocalFile.ItemSpec);
                WriteLine(stream, "");
                WriteLine(stream, String.Concat("--", Boundary, "--"));
            }

            request.GetResponse();
        }

        /// <summary>
        /// Writes the specified file to the specified stream.
        /// </summary>
        internal void WriteFile(Stream outputStream, string fileToWrite)
        {
            if (outputStream == null)
                throw new ArgumentNullException("outputStream");

            if (fileToWrite == null)
                throw new ArgumentNullException("fileToWrite");

            using (FileStream fileStream = new FileStream(LocalFile.ItemSpec, FileMode.Open))
            {
                byte[] buffer = new byte[1024];
                int count;
                while ((count = fileStream.Read(buffer, 0, buffer.Length)) > 0)
                {
                    outputStream.Write(buffer, 0, count);
                }
            }
        }

        /// <summary>
        /// Writes the string to the specified stream and concatenates a newline.
        /// </summary>
        internal static void WriteLine(Stream outputStream, string valueToWrite)
        {
            if (valueToWrite == null)
                throw new ArgumentNullException("valueToWrite");

            List<byte> bytesToWrite = new List<byte>(Encoding.ASCII.GetBytes(valueToWrite));
            bytesToWrite.AddRange(NewLineAsciiBytes);
            outputStream.Write(bytesToWrite.ToArray(), 0, bytesToWrite.Count);
        }

        /// <summary>
        /// Creates the authorization token.
        /// </summary>
        internal static string CreateAuthorizationToken(string username, string password)
        {
            if (username == null)
                throw new ArgumentNullException("username");

            if (password == null)
                throw new ArgumentNullException("password");

            byte[] authBytes = Encoding.ASCII.GetBytes(String.Concat(username, ":", password));
            return Convert.ToBase64String(authBytes);
        }

        public override bool Execute()
        {
            Log.LogMessageFromText("Starting Google Code Upload", MessageImportance.Normal);

            try
            {
                if (UserName == null)
                    throw new InvalidOperationException("UserName cannot be null");

                if (Password == null)
                    throw new InvalidOperationException("Password cannot be null");

                if (ProjectName == null)
                    throw new InvalidOperationException("ProjectName cannot be null");

                if (LocalFile == null)
                    throw new InvalidOperationException("FileName cannot be null");

                if (TargetFileName == null)
                    throw new InvalidOperationException("TargetFileName cannot be null");

                if (Summary == null)
                    throw new InvalidOperationException("Summary cannot be null");
                Upload();
                string receiptFile = Path.ChangeExtension(LocalFile.ItemSpec, string.Format("{0}.gcu", this.Summary));
                File.WriteAllText(receiptFile, DateTime.Now.ToString());
                this.ReceiptFile = new TaskItem(receiptFile);
            }
            catch (Exception e)
            {
                Log.LogErrorFromException(e);
                return false;
            }
            return true;
        }
    }

}
