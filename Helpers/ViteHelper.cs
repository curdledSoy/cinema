// Helpers/ViteHelper.cs
using System.Diagnostics;

namespace cinema.Helpers;
public static class ViteHelper
{
    private static Process? _viteProcess;

    public static void EnsureViteDevServerRunning()
    {
        try
        {
            using var client = new HttpClient();
            var result = client.GetAsync("http://localhost:5173").Result;

            if (!result.IsSuccessStatusCode)
            {
                StartViteDevServer();
            }
        }
        catch
        {
            StartViteDevServer();
        }
    }

    private static void StartViteDevServer()
    {
        if (_viteProcess != null && !_viteProcess.HasExited)
            return;

        _viteProcess = new Process
        {
            StartInfo = new ProcessStartInfo
            {
                FileName = "npm",
                Arguments = "run dev",
                WorkingDirectory = Path.Combine(Directory.GetCurrentDirectory(), "clientapp"),
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            }
        };

        _viteProcess.OutputDataReceived += (sender, e) => Console.WriteLine(e.Data);
        _viteProcess.ErrorDataReceived += (sender, e) => Console.Error.WriteLine(e.Data);

        _viteProcess.Start();
        _viteProcess.BeginOutputReadLine();
        _viteProcess.BeginErrorReadLine();
    }

    public static void StopViteDevServer()
    {
        if (_viteProcess != null && !_viteProcess.HasExited)
        {
            _viteProcess.Kill();
            _viteProcess.Dispose();
            _viteProcess = null;
        }
    }
}

