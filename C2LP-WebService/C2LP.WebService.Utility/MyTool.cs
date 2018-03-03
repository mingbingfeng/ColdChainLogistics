using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;

namespace C2LP.WebService.Utility
{
    public class MyTool
    {
        

        /// <summary>
        /// MD5　32位加密
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string UserMd5(string source)
        {
            using (var md5Hash = MD5.Create())
            {
                // Byte array representation of source string
                var sourceBytes = Encoding.UTF8.GetBytes(source);

                // Generate hash value(Byte Array) for input data
                var hashBytes = md5Hash.ComputeHash(sourceBytes);

                // Convert hash byte array to string
                var hash = BitConverter.ToString(hashBytes).Replace("-", string.Empty);

                // Output the MD5 hash
                return hash;
            }
        }

        /// <summary>
        /// 将字节流保存到指定路径
        /// </summary>
        /// <param name="fileBytes">图片字节流</param>
        /// <param name="imagePath">图片路径</param>
        /// <returns></returns>
        public static void SaveImage(byte[] fileBytes, string imagePath) {
            try
            {
                MemoryStream memoryStream = new MemoryStream(fileBytes); //1.定义并实例化一个内存流，以存放提交上来的字节数组。  
                FileStream fileUpload = new FileStream(imagePath, FileMode.Create); ///2.定义实际文件对象，保存上载的文件。  
                memoryStream.WriteTo(fileUpload); ///3.把内存流里的数据写入物理文件  
                memoryStream.Close();
                fileUpload.Close();
                fileUpload = null;
                memoryStream = null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 解压缩图片字节
        /// </summary>
        /// <param name="picBytes"></param>
        /// <returns></returns>
        public static byte[] GetGzipPicBytes(byte[] picBytes)
        {
            byte[] result = null;
            using (MemoryStream dms = new MemoryStream())
            {
                using (MemoryStream cms = new MemoryStream(picBytes))
                {
                    using (System.IO.Compression.GZipStream gzip = new System.IO.Compression.GZipStream(cms, System.IO.Compression.CompressionMode.Decompress))
                    {
                        byte[] bytes = new byte[1024];
                        int len = 0;
                        //读取压缩流，同时会被解压
                        while ((len = gzip.Read(bytes, 0, bytes.Length)) > 0)
                        {
                            dms.Write(bytes, 0, len);
                        }
                    }
                }
                result = dms.ToArray();
            }
            return result;
        }
    }
}
