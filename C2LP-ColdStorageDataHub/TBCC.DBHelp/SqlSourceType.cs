using System;

namespace DBHelp
{
    /// <summary>
    /// 数据库类型
    /// </summary>
    [Serializable]
    public enum SqlSourceType
    {
        Oracle,
        MSSql,
        MySql,
        Access
    }
}
