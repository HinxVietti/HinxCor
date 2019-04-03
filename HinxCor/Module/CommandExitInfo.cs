using System;
using System.Collections.Generic;

public struct CommandExitInfo
{
    public int ExitCode { get; set; }
    public string Error { get; set; }
    public string Output { get; set; }
    public bool hasError { get { return !string.IsNullOrEmpty(Error); } }
    public bool hasOutput { get { return !string.IsNullOrEmpty(Output); } }

    public CommandExitInfo(int exitcode = 0)
    {
        ExitCode = exitcode;
        Error = Output = string.Empty;
    }

}

