using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Principal;
using Microsoft.Win32;

public static class RegistryUtil
{
    public static readonly bool hasAdminAccess;

    static RegistryUtil()
    {
        hasAdminAccess = IsUserAdministrator();
    
    }



    public static void SetFileStartInfo(string fileType,string startProgram,string fileIcon)
    {



    }




    public static bool IsUserAdministrator()
    {
        //bool value to hold our return value
        bool isAdmin;
        WindowsIdentity user = null;
        try
        {
            //get the currently logged in user
            user = WindowsIdentity.GetCurrent();
            WindowsPrincipal principal = new WindowsPrincipal(user);
            isAdmin = principal.IsInRole(WindowsBuiltInRole.Administrator);
        }
        catch (UnauthorizedAccessException ex)
        {
            isAdmin = false;
        }
        catch (Exception ex)
        {
            isAdmin = false;
        }
        finally
        {
            if (user != null)
                user.Dispose();
        }
        return isAdmin;
    }


}
