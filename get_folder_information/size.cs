using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace get_folder_information
{
    /// <summary>
    /// 文件夹大小的操作
    /// </summary>
    public static class size
    {
        /// <summary>
        /// 获取路径所指向文件夹的大小(单位为B)
        /// </summary>
        /// <param name="path">要计算的文件夹路径</param>
        /// <returns>返回大小</returns>
        public static long GetSize_B(string path)
        {
            long size = 0;
            DirectoryInfo directoryInfo = new DirectoryInfo(path);
            foreach (FileInfo fileInfo in directoryInfo.GetFiles())//获取文件夹下所有文件
            {
                size += fileInfo.Length;//把总额加上当前文件的大小
            }

            DirectoryInfo[] directoryInfos = directoryInfo.GetDirectories();
            //创建一个包含所有子文件夹的集合
            if (directoryInfos.Length > 0)
            {
                for (int i = 0; i < directoryInfos.Length; i++)
                {
                    size += GetSize_B(directoryInfos[i].FullName);//递归，再次获取子文件夹大小
                }
            }
            return size;
        }

        /// <summary>
        /// 获取路径所指向文件夹的大小(单位为KB)
        /// </summary>
        /// <param name="path">要计算的文件夹路径</param>
        /// <returns>返回大小</returns>
        public static long GetSize_KB(string path)
        {
            long size = GetSize_B(path);
            size /= 1024;
            return size;
        }

        /// <summary>
        /// 获取路径所指向文件夹的大小(单位为MB)
        /// </summary>
        /// <param name="path">要计算的文件夹路径</param>
        /// <returns>返回大小</returns>
        public static long GetSize_MB(string path)
        {
            long size = GetSize_KB(path);
            size /= 1024;
            return size;
        }

        /// <summary>
        /// 获取路径所指向文件夹的大小(单位为GB)
        /// </summary>
        /// <param name="path">要计算的文件夹路径</param>
        /// <returns>返回大小</returns>
        public static long GetSize_GB(string path)
        {
            long size = GetSize_MB(path);
            size /= 1024;
            return size;
        }

        /// <summary>
        /// 获取路径所指向文件夹的大小(单位为KB)
        /// </summary>
        /// <param name="path">要计算的文件夹路径</param>
        /// <returns>返回大小</returns>
        public static string GetSize_Auto(string path)
        {
            long size = GetSize_B(path);
            string[] unit = new string[] { "B", "KB", "MB", "GB" };
            int i = 0;
            while (size > 1024)
            {
                size /= 1024;
                i++;
            }
            if (i > unit.Length)
            {
                return ("错误：文件夹太大，超出计算范围。");
            }
            return size + unit[i];
        }
    }
}