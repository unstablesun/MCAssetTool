using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class TerminalLaunchButton : MonoBehaviour 
{

	void Start () 
	{
		
	}
	
	// Update is called once per frame
	public void TerminalLaunch () 
	{

		ProcessStartInfo proc = new ProcessStartInfo();
		proc.FileName = "/Applications/Utilities/Terminal.app";
		proc.Arguments = "1";
		Process.Start(proc);


		
        //ProcessStartInfo exportProc = new ProcessStartInfo();

/* 
		exportProc.FileName = Application.dataPath+"/exportBundles.sh";
		exportProc.UseShellExecute = false; 
		exportProc.RedirectStandardOutput = false;
		exportProc.Arguments = "arg1 arg2 arg3";
		exportProc.WorkingDirectory = Application.dataPath;
*/

		//exportProc.FileName = "/bin/sh";
		//exportProc.UseShellExecute = true; 
		//exportProc.RedirectStandardOutput = false;
		//exportProc.Arguments = Application.dataPath + "/exportBundles.sh";// + " arg1 arg2 arg3";

		//proc.FileName = "/Applications/Utilities/Terminal.app/Contents/MacOS/Terminal";
		//proc.
		//Process p = Process.Start(exportProc);

		//string strOutput = p.StandardOutput.ReadToEnd(); 
		//p.WaitForExit(); 
		//UnityEngine.Debug.Log(strOutput);
	}
}
