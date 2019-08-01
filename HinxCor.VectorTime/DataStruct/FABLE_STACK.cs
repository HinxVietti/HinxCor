using System;
using System.Collections.Generic;

namespace HinxCor.VectorTime
{

    public class FABLE_STACK : FABLE_STACK<object>
    {

    }

    public class FABLE_STACK<T> : ISABLE<T>
    {
        private readonly int MaxSize;

        public T this[int index] { get => data[index]; set => data[index] = value; }
        public int Length => count;
        public int Count => count;
        T[] data;
        private int count = 0;
        public bool isFull => count >= MaxSize;


        public FABLE_STACK(int maxSize = 4)
        {
            MaxSize = maxSize;
            data = new T[maxSize];
        }

        public void Add(T dat)
        {
            if (!isFull)
            {
                data[count] = dat;
                count++;
            }
            else throw new SableStackOverflowException();
        }

        public void Delete(T dat)
        {
            for (int i = 0; i < count; i++)
            {
                if (data[i].Equals(dat))
                {
                    MoveLeft(i);
                    count--;
                    return;
                }
            }
            throw new NothingDeleteException();
        }


        private void MoveLeft(int startIndex = 0)
        {
            for (int i = startIndex; i < count; i++)
            {
                int dest = i + 1;
                if (dest < MaxSize)
                    data[i] = data[i + 1];
                else
                    data[i] = default(T);
            }
        }



        public void Delete(IMatch<T> match)
        {
            for (int i = 0; i < count; i++)
            {
                if (match.match(data[i]))
                {
                    MoveLeft(i);
                    count--;
                    return;
                }
            }
            throw new NothingDeleteException();
        }

        public void DeleteAll(IMatch<T> match)
        {
            reloop:
            for (int i = 0; i < count; i++)
            {
                if (match.match(data[i]))
                {
                    MoveLeft(i);
                    count--;
                    goto reloop;
                }
            }
        }

        public void DROP()
        {
            for (int i = 0; i < count; i++)
            {
                data[i] = default(T);
                count = 0;
            }
        }

        public T Find(IMatch<T> match)
        {
            for (int i = 0; i < count; i++)
            {
                if (match.match(data[i])) return data[i];
            }
            return default(T);
        }

        public void Update(T old, T onew)
        {
            for (int i = 0; i < count; i++)
                if (data[i].Equals(old))
                {
                    data[i] = onew;
                    return;
                }
        }

        public void Update(IMatch<T> match, T onew)
        {
            for (int i = 0; i < count; i++)
                if (match.match(data[i]))
                {
                    data[i] = onew;
                    return;
                }
        }

        public class NothingDeleteException : Exception
        {
            public override string HelpLink { get => @"https://github.com/HinxVietti/HinxCor"; set => base.HelpLink = value; }
            public override string Message => "没能从STACK中删除任何数据。";
        }

        public class DataNotFoundException : Exception
        {
            public override string HelpLink { get => @"https://github.com/HinxVietti/HinxCor"; set => base.HelpLink = value; }
            public override string Message => "没有找到正确的内容，但是返回是必须的。";

        }

        public class SableStackOverflowException : Exception
        {
            public override string HelpLink { get => @"https://github.com/HinxVietti/HinxCor"; set => base.HelpLink = value; }
            public override string Message => "发生了栈区溢出异常，可能尝试再一个已经满了的栈堆中继续添加内容。";
        }
    }

}
