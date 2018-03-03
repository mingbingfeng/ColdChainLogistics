using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace C2LP.WebService.Model
{
    /// <summary>
    /// 接口返回值
    /// Code:0表示成功;1：表示失败;
    /// Message:成功时保持为空;失败时附带失败信息;
    /// Data:需要时所附带的返回数据.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ResultModel<T>
    {
        public ResultModel()
        {
            Code = 0;
            Message = string.Empty;
            Data = default(T);
        }
        /// <summary>
        /// 0表示成功;1：表示失败;
        /// </summary>
        public int Code { get; set; }

        /// <summary>
        /// 成功时保持为空;失败时附带失败信息;
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 需要时所附带的返回数据.
        /// </summary>
        public T Data { get; set; }

        

    }
}
