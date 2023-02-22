using System;
using Unity.Collections.LowLevel.Unsafe;

namespace AgileString
{
    public static class ReuseUtil
    {
        
        private const int CHAR_SIZE = sizeof(char);
        
        internal static unsafe void MemoryCopy(char* dest, char* src, int count)
        {
            // 此处换成Unity的内存拷贝函数，性能高3%，坏处是非Unity项目用不了
            UnsafeUtility.MemCpy(dest, src, count * CHAR_SIZE);
            // byteCopy((byte*)dest, (byte*)src, count * m_charLen);
        }
        
        internal static unsafe void MemoryCopy(string dst, string src)
        {
            if (dst.Length != src.Length)
                throw new InvalidOperationException("两个字符串参数长度不一致。");
            fixed (char* dst_ptr = dst)
            {
                fixed (char* src_ptr = src)
                {
                    MemoryCopy(dst_ptr, src_ptr, dst.Length);
                }
            }
        }
        
        
    }
}