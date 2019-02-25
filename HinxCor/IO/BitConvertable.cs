using System;
using System.IO;

namespace HinxCor.IO
{

    public abstract class BitConvertable<T> : IBitConvertable<T>
    {
        protected byte[] data;
        public abstract int Length { get; }
        public abstract T ToObject(byte[] array);
        public abstract T ToObject();
        public string profileName { get; protected set; }
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

        public int GetInt(int offset)
        {
            return BitConverter.ToInt32(data, offset);
        }

        public void SetInt(int value, int offset)
        {
            BitConverter.GetBytes(value).CopyTo(data, offset);
        }

        public bool GetBoolean(int offset)
        {
            return BitConverter.ToBoolean(data, offset);
        }
        public void SetBoolean(bool value, int offset)
        {
            BitConverter.GetBytes(value).CopyTo(data, offset);
        }

        public float GetSingle(int offset)
        {
            return BitConverter.ToSingle(data, offset);
        }
        public void SetSingle(float value, int offset)
        {
            BitConverter.GetBytes(value).CopyTo(data, offset);
        }

        public double GetDouble(int offset)
        {
            return BitConverter.ToDouble(data, offset);
        }
        public void SetDouble(double value, int offset)
        {
            BitConverter.GetBytes(value).CopyTo(data, offset);
        }

        public byte[] GetBytes(int size, int offste)
        {
            var dat = new byte[size];
            for (int i = 0; i < size; i++)
                dat[i] = data[i + offste];
            return dat;
        }

        public void SetBytes(byte[] dat, int offset)
        {
            dat.CopyTo(data, offset);
        }

    }

}