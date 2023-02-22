using System;

namespace AgileString
{
    public class ReuseString : IDisposable
    {
        [NonSerialized]
        private string m_Value;
        [NonSerialized]
        private bool m_Disposed;
        [NonSerialized]
        private bool m_IsShallow;

        /// <summary>
        /// 空字符串填充
        /// </summary>
        const char NEW_ALLOC_CHAR = 'X';

        public ReuseString(int length)
        {
            m_Value = new string(NEW_ALLOC_CHAR, length);
        }
        
        
        private ReuseString(string value, bool shallow)
        {
            if (!shallow)
            {
                throw new NotSupportedException();
            }
            m_Value = value;
            m_IsShallow = true;
        }


        public void Dispose()
        {
            m_Disposed = true;
        }

        private static ReuseString StringConvert(string value)
        {
            if (value == null)
                return null;
            var result = get(value.Length);
            ReuseUtil.MemoryCopy(dst: result, src: value);//内存拷贝
            return result;
        }
    }
}