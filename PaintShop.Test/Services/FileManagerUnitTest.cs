using System.Collections.Generic;
using NUnit.Framework;
using Moq;
using PaintShop.Services.Repository;
using PaintShop.Services.Implementation;
using Moq.AutoMock;
using System;

namespace PaintShop.Test.Services
{
    /// <summary>
    /// Summary description for FileManagerUnitTest
    /// </summary>
    [TestFixture]
    public class FileManagerUnitTest
    {
        Mock<IReadFile> mockRepository;
        AutoMocker mocker;
        FileManager fileManager;

        [OneTimeSetUp]
        public void TestSetup()
        {
            mockRepository = new Mock<IReadFile>();
            mocker = new AutoMocker();
            fileManager = mocker.CreateInstance<FileManager>();
        }

        [Test]        
        public void ReadLine()
        {
            try
            {
                IEnumerable<string> readLinesFromFile = fileManager.ReadLine("testFile1.txt");
                Assert.NotNull(readLinesFromFile);
            }
            catch(Exception ex)
            {
                Assert.Fail(ex.Message);
            } 
        }

        [OneTimeTearDown]
        public void EndTestSetup()
        {
            mockRepository = null;
            mocker = null;
            fileManager = null;
        }
    }
}
