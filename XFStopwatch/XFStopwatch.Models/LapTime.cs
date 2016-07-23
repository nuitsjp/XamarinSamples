using System;

namespace XFStopwatch.Models
{
    /// <summary>
    /// ラップタイムを表すクラス
    /// </summary>
    public class LapTime
    {
        /// <summary>
        /// Noを取得する
        /// </summary>
        public int No { get; }
        /// <summary>
        /// 経過時間を取得する
        /// </summary>
        public TimeSpan ElapsedTime { get; }
        /// <summary>
        /// インスタンスを初期化する
        /// </summary>
        /// <param name="no"></param>
        /// <param name="elapsedTime"></param>
        public LapTime(int no, TimeSpan elapsedTime)
        {
            No = no;
            ElapsedTime = elapsedTime;
        }
    }
}
