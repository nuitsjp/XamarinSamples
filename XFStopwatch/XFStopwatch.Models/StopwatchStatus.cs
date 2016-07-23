namespace XFStopwatch.Models
{
    /// <summary>
    /// ストップウォッチの状態を表す
    /// </summary>
    public enum StopwatchStatus
    {
        /// <summary>
        /// 停止中で未計測を表す状態
        /// </summary>
        Stoped,
        /// <summary>
        /// 計測中を表す状態
        /// </summary>
        Running,
        /// <summary>
        /// 一時停止中
        /// </summary>
        Paused,
    }
}