using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using KOMPASConnector;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;

namespace DrillKOMPAS.UnitTests
{
    /// <summary>
    /// Класс для нагрузочного тестирования.
    /// </summary>
    [TestFixture]
    public class StressTest
    {
        [TestCase(TestName =
            "Нагрузочный тест потребления памяти и времени построения")]
        public void Start()
        {
            var writer = new StreamWriter(
                $@"{AppDomain.CurrentDomain.BaseDirectory}\StressTest.txt");

            var count = 100;

            var processes = Process.GetProcessesByName("KOMPAS");
            var process = processes.First();

            var ramCounter = new PerformanceCounter("Process", "Working Set - Private", process.ProcessName);
            var cpuCounter = new PerformanceCounter("Process", "% Processor Time", process.ProcessName);
            var stopwatch = new Stopwatch();

            for (int i = 0; i < count; i++)
            {
                stopwatch.Start();

                cpuCounter.NextValue();
                var parameters = new DrillParameters();
                parameters.DrillLenght = 90;
                parameters.WorkingPartLenght = 61;
                parameters.DrillDiameter = 15.5;
                parameters.TenonLenght = 10;
                parameters.TenonWidth = 5.5;
                parameters.NeckLenght = 10;
                parameters.NeckWidth = 12.5;
                var wrapper = new KOMPASWrapper();
                wrapper.OpenKOMPAS();
                wrapper.BuildModel(parameters);

                stopwatch.Stop();

                var ram = ramCounter.NextValue();
                var cpu = cpuCounter.NextValue();

                writer.Write($"{i}. ");
                writer.Write($"RAM: {Math.Round(ram / 1024 / 1024)} MB");
                writer.Write($"\tCPU: {cpu} %");
                writer.Write($"\ttime: {stopwatch.Elapsed}");
                writer.Write(Environment.NewLine);
                writer.Flush();

                stopwatch.Reset();
            }
        }
    }
}
