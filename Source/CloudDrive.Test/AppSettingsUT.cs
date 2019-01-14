using CloudDrive.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CloudDrive.Test
{
    [TestClass]
    public class AppSettingsUT : BaseUT
    {
        [TestMethod]
        public void AppSettingsTest()
        {
            Assert.IsTrue(AppSettings.Instance.RunAtStartUp);
        }
    }
}