using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AndroidExplorer {
    class DoCommand {
        public static string Exec(String command) {
            Console.WriteLine("command : " + command);
            System.Diagnostics.Process p = new System.Diagnostics.Process();
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.CreateNoWindow = true;
            p.StartInfo.FileName = "cmd.exe";
            p.StartInfo.Arguments = "/c " + command;
            p.StartInfo.RedirectStandardError = true;
            p.StartInfo.RedirectStandardInput = true;
            p.StartInfo.RedirectStandardOutput = true;

            p.Start();

            var stdout = new StringBuilder();
            var stderr = new StringBuilder();
            p.OutputDataReceived += (sender, e) => {
                if (e.Data != null) {
                    stdout.AppendLine(e.Data);
                }
            };
            p.ErrorDataReceived += (sendar, e) => {
                if (e.Data != null) {
                    stderr.AppendLine(e.Data);
                }
            };
            p.BeginOutputReadLine();
            p.BeginErrorReadLine();

            var output = new StringBuilder();
            var isTimeOut = false;
            if (!p.WaitForExit(3000)) {
                isTimeOut = true;
                p.Kill();
            }
            p.CancelOutputRead();
            p.CancelErrorRead();
            output.Append(stdout.ToString());
            output.Append(stderr.ToString());
            if (isTimeOut) {
                output.Append("TIMEOUT.");
            }
            return output.ToString();
        }
    }
}
