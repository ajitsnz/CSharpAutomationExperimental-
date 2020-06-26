using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Expression.Encoder.Profiles;
using Microsoft.Expression.Encoder.ScreenCapture;
using NUnit.Framework;

namespace QaAutoTests.Extensions
{
    public class ScreenRecorder
    {
        private static int i=0;
        ScreenCaptureJob screenCaptureJob = new ScreenCaptureJob(); 
       // private readonly string OutputDirectoryName = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
       // private readonly string OutputDirectoryName = "c:\\Data\\Video\\";

        private readonly string OutputDirectoryName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory + "..\\..\\report\\video\\");
        public void SetVideoOutputLocation(string testName = "")
        {
            if (string.IsNullOrEmpty(testName))
                testName = "AutomationTest";
            screenCaptureJob.OutputScreenCaptureFileName = Path.Combine(OutputDirectoryName, string.Format("{0}_{1}_{2}.avi", testName, i++ , DateTime.UtcNow.ToString("MMddyyyy_Hmmss" )));
        }
        public void DeleteOldRecordings()
        {
            int daysCount = Convert.ToInt16(ConfigurationManager.AppSettings["recordingHistory"]);
            Directory.GetFiles(OutputDirectoryName)
                .Select(f => new FileInfo(f))
                .Where(f => (f.LastAccessTime < DateTime.Now.AddDays(-daysCount)) && (f.FullName.Contains(".avi")))
                .ToList()
                .ForEach(f => f.Delete());
        }

        public void StartRecording()
        {
            //DeleteOldRecordings();
            screenCaptureJob.Start();
        }
        public void StopRecording()
        {
            screenCaptureJob.Stop();
        }
    }
}
