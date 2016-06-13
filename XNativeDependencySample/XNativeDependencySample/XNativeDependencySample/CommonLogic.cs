using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XNativeDependencySample
{
    /// <summary>
    /// 共通で利用したい何らかのロジッククラス
    /// </summary>
    public class CommonLogic
    {
        /// <summary>
        /// プラットフォーム固有処理の移譲先
        /// </summary>
        private readonly ISystemInfo systemInfo;

        /// <summary>
        /// インスタンスを生成する
        /// </summary>
        /// <param name="systemInfo"></param>
        public CommonLogic(ISystemInfo systemInfo)
        {
            this.systemInfo = systemInfo;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="systemInfo"></param>
        /// <returns></returns>
        public string GetSdkVersion()
        {
            // プラットフォーム固有の処理を呼び出す
            var sdkVersion = systemInfo.SdkVersion;

            // 共通のロジック。しょぼくてスマン
            var result = string.Format("SDK_VERSION:{0}", sdkVersion);
            return result;
        }
    }
}
