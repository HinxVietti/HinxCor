using System;
using System.IO;

namespace HinxCor.IO
{
    /// <summary>
    /// 字节序列化转换工具
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class BitConvertable<T> : IBitConvertable<T>
    {
        /// <summary>
        /// 数据内容
        /// </summary>
        protected byte[] data;
        /// <summary>
        /// 数据长度
        /// </summary>
        public abstract int Length { get; }
        /// <summary>
        /// 转换为对象
        /// </summary>
        /// <param name="array"></param>
        /// <returns></returns>
        public abstract T ToObject(byte[] array);
        /// <summary>
        /// 转换为对象
        /// </summary>
        /// <returns></returns>
        public abstract T ToObject();
        /// <summary>
        /// 配置文件路径
        /// </summary>
        public string profileName { get; protected set; }
        /// <summary>
        /// 获取数据
        /// </summary>
        /// <returns></returns>
        public virtual byte[] GetByteArray()
        {
            return data;
        }

        /// <summary>
        /// 加载读取文件数据
        /// </summary>
        /// <param name="fileName"></param>
        public BitConvertable(string fileName)
        {
            data = File.ReadAllBytes(fileName);
        }
        /// <summary>
        /// 存入data到对象
        /// </summary>
        /// <param name="data"></param>
        public BitConvertable(byte[] data)
        {
            this.data = data;
        }
        /// <summary>
        /// 读取int
        /// </summary>
        /// <param name="offset"></param>
        /// <returns></returns>
        public int GetInt(int offset)
        {
            return BitConverter.ToInt32(data, offset);
        }
        /// <summary>
        /// 设置int
        /// </summary>
        /// <param name="value"></param>
        /// <param name="offset"></param>
        public void SetInt(int value, int offset)
        {
            BitConverter.GetBytes(value).CopyTo(data, offset);
        }
        /// <summary>
        /// 读取布尔
        /// </summary>
        /// <param name="offset"></param>
        /// <returns></returns>
        public bool GetBoolean(int offset)
        {
            return BitConverter.ToBoolean(data, offset);
        }
        /// <summary>
        /// 设置布尔
        /// </summary>
        /// <param name="value"></param>
        /// <param name="offset"></param>
        public void SetBoolean(bool value, int offset)
        {
            BitConverter.GetBytes(value).CopyTo(data, offset);
        }
        /// <summary>
        /// 获取float
        /// </summary>
        /// <param name="offset"></param>
        /// <returns></returns>
        public float GetSingle(int offset)
        {
            return BitConverter.ToSingle(data, offset);
        }
        /// <summary>
        /// 设置float
        /// </summary>
        /// <param name="value"></param>
        /// <param name="offset"></param>
        public void SetSingle(float value, int offset)
        {
            BitConverter.GetBytes(value).CopyTo(data, offset);
        }
        /// <summary>
        /// 获取double
        /// </summary>
        /// <param name="offset"></param>
        /// <returns></returns>
        public double GetDouble(int offset)
        {
            return BitConverter.ToDouble(data, offset);
        }
        /// <summary>
        /// 设置double
        /// </summary>
        /// <param name="value"></param>
        /// <param name="offset"></param>
        public void SetDouble(double value, int offset)
        {
            BitConverter.GetBytes(value).CopyTo(data, offset);
        }
        /// <summary>
        /// 获取字节
        /// </summary>
        /// <param name="size"></param>
        /// <param name="offste"></param>
        /// <returns></returns>
        public byte[] GetBytes(int size, int offste)
        {
            var dat = new byte[size];
            for (int i = 0; i < size; i++)
                dat[i] = data[i + offste];
            return dat;
        }/// <summary>
         /// 设置字节
         /// </summary>
         /// <param name="dat"></param>
         /// <param name="offset"></param>

        public void SetBytes(byte[] dat, int offset)
        {
            dat.CopyTo(data, offset);
        }

    }

}