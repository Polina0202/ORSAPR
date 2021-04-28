using NUnit.Framework;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using KOMPASConnector;

namespace DrillKOMPAS.UnitTests
{
    /// <summary>
    /// Класс для нагрузочного тестирования.
    /// </summary>
    [TestFixture]
    public class UnitTest1
    {
        [TestCase(TestName =
            "Нагрузочный тест потребления памяти и времени построения")]
        public void Start()
        {
            var writer = new StreamWriter(
                $@"{AppDomain.CurrentDomain.BaseDirectory}\StressTest.txt");

            var count = 200;

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
                var builder = new KOMPASWrapper();
                builder.BuildModel(parameters);

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
