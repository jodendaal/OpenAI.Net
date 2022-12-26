using OpenAI.Net.Models;

namespace OpenAI.Net.Tests.Services.FilesService_Tests
{
    internal class FileService_FileInfo : BaseServiceTest
    {

        [Test]
        public void FileInfoValidation_Null_Content()
        {
            bool exceptionOccured = false;
            string exceptionMessage = "";

            try
            {
                var image = new Models.FileContentInfo(null, "image.png");
            }
            catch(ArgumentException ex)
            {
                exceptionOccured = true;
                exceptionMessage = ex.Message;
            }

            Assert.That(exceptionOccured,Is.True, "Exception not raised for null input");
            Assert.That(exceptionMessage, Is.EqualTo("FileContent is required"), "Incorrect error message retuned");
        }

        [Test]
        public void FileInfoValidation_Zero_Length_Content()
        {
            bool exceptionOccured = false;
            string exceptionMessage = "";

            try
            {
                var image = new Models.FileContentInfo(new byte[] { }, "image.png");
            }
            catch (ArgumentException ex)
            {
                exceptionOccured = true;
                exceptionMessage = ex.Message;
            }

            Assert.That(exceptionOccured, Is.True, "Exception not raised for null input");
            Assert.That(exceptionMessage, Is.EqualTo("FileContent is required"),"Incorrect error message retuned");
        }

        [Test]
        public void FileInfoValidation_No_Filename()
        {
            bool exceptionOccured = false;
            string exceptionMessage = "";

            try
            {
                var image = new Models.FileContentInfo(new byte[] {1 }, null);
            }
            catch (ArgumentException ex)
            {
                exceptionOccured = true;
                exceptionMessage = ex.Message;
            }

            Assert.That(exceptionOccured, Is.True, "Exception not raised for null input");
            Assert.That(exceptionMessage, Is.EqualTo("FileName is required"), "Incorrect error message retuned");
        }

        [Test]
        public void FileInfoValidation_Load_InvalidPath()
        {
            bool exceptionOccured = false;

            try
            {
                var file = FileContentInfo.Load("ss.bak");
            }
            catch (FileNotFoundException)
            {
                exceptionOccured = true;
            }

            Assert.That(exceptionOccured, Is.True, "Exception not raised for null input");
            
        }

    }
}
