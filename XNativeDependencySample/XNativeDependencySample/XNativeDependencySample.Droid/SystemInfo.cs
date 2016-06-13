using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace XNativeDependencySample.Droid
{
    /// <summary>
    /// Android用の固有処理の実装クラス
    /// </summary>
    public class SystemInfo : ISystemInfo
    {
        public string SdkVersion
        {
            get
            {
                return Android.OS.Build.VERSION.Sdk;
            }
        }
    }
}