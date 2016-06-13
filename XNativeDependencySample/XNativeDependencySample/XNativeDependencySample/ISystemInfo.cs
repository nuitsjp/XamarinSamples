using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XNativeDependencySample
{
    /// <summary>
    /// 共通ロジックから呼び出したいプラットフォーム固有の処理
    /// </summary>
    public interface ISystemInfo
    {
        string SdkVersion { get; }
    }
}
