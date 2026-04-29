using System;
using System.Collections.Generic;
using System.Text;

namespace App.Interfaces
{
    public interface ISettings
    {
        string GetConfigValue(string key);
    }
}
