using System.Diagnostics;
namespace wuxingogo.Editor
{
	public class XProcessUtlis
	{
		public static void ExcuteExternalCommand(string fileName, string arg, string directory)
		{
			ProcessStartInfo proc = new ProcessStartInfo();
			proc.FileName = fileName;
			proc.WorkingDirectory = directory;
			proc.Arguments = arg;
			proc.WindowStyle = ProcessWindowStyle.Minimized;
			proc.CreateNoWindow = true;
			var p = Process.Start(proc);
			p.StartInfo.UseShellExecute = false;
			p.StartInfo.RedirectStandardOutput = true;
			p.StartInfo.RedirectStandardError = true;
			p.OutputDataReceived += (object sender, DataReceivedEventArgs e) => {
				XLogger.Log(sender.ToString());
			}; 
			p.ErrorDataReceived += (object sender, DataReceivedEventArgs e) => {
				XLogger.Log(sender.ToString());
			}; 
			p.Start ();
			string result = p.StandardOutput.ReadToEnd ();
			if(result.Length != 0)
				UnityEngine.Debug.Log(result);
			result = p.StandardError.ReadToEnd();
			if(result.Length != 0)
				UnityEngine.Debug.Log(result);

			p.WaitForExit();
			p.Close();
		}
		public static void ExcuteExternalCommand(string fileName, string arg)
		{
			ExcuteExternalCommand (fileName, arg, XEditorSetting.ProjectPath);
		}
	}
}


