using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CloudDrive.ElectronClient.Models;
using ElectronNET.API;
using System.Threading;

namespace CloudDrive.ElectronClient.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            if (HybridSupport.IsElectronActive)
            {
                Electron.IpcMain.On("getCpuActivity", async (args) =>
                {
                    while (true)
                    {
                        var processes = await Electron.App.GetAppMetricsAsync();
                        var firstCpuPercentUsage = processes.First().Cpu.PercentCPUUsage;

                        var mainWindow = Electron.WindowManager.BrowserWindows.First();
                        Electron.IpcMain.Send(mainWindow, "cpuActivityReply", firstCpuPercentUsage);

                        Thread.Sleep(1000);
                    }
                });
            }
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
