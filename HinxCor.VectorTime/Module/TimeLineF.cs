using HinxCor.VectorTime;
using System;
using System.Collections.Generic;

namespace HinxCor.VectorTime
{
    public class TimeLineF : ISABLE<IFrame>
    {
        public IFrame this[int index] { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }



        public int Length => throw new NotImplementedException();

        public int Count => throw new NotImplementedException();

        public void Add(IFrame dat)
        {
            throw new NotImplementedException();
        }

        public void Delete(IFrame dat)
        {
            throw new NotImplementedException();
        }

        public void Delete(IMatch<IFrame> match)
        {
            throw new NotImplementedException();
        }

        public void DeleteAll(IMatch<IFrame> match)
        {
            throw new NotImplementedException();
        }

        public void DROP()
        {
            throw new NotImplementedException();
        }

        public IFrame Find(IMatch<IFrame> match)
        {
            throw new NotImplementedException();
        }

        public void Update(IFrame old, IFrame onew)
        {
            throw new NotImplementedException();
        }

        public void Update(IMatch<IFrame> match, IFrame onew)
        {
            throw new NotImplementedException();
        }


        private class dc<T> : ISABLE<T>
        {
            public T this[int index] { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

            List<FABLE_STACK<T>> datas;
            FABLE_STACK<T> Current;

            public int Length { get { return 0; } }

            public int Count => Length;

            public void Add(T dat)
            {
                throw new NotImplementedException();
            }

            public void Delete(T dat)
            {
                throw new NotImplementedException();
            }

            public void Delete(IMatch<T> match)
            {
                throw new NotImplementedException();
            }

            public void DeleteAll(IMatch<T> match)
            {
                throw new NotImplementedException();
            }

            public void DROP()
            {
                throw new NotImplementedException();
            }

            public T Find(IMatch<T> match)
            {
                throw new NotImplementedException();
            }

            public void Update(T old, T onew)
            {
                throw new NotImplementedException();
            }

            public void Update(IMatch<T> match, T onew)
            {
                throw new NotImplementedException();
            }
        }

    }



}
