using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Iql
{
    public static class Md5
    {
        public static long EnsureValue(this Dictionary<long, long> dictionary, long key)
        {
            if (!dictionary.ContainsKey(key))
            {
                dictionary.Add(key, 0);
            }

            return dictionary[key];
        }

        public static long GetValue(this Dictionary<long, long> dictionary, long key)
        {
            dictionary.EnsureValue(key);
            return dictionary[key];
        }

        private static string Prepare(string a)
        {
            a = Regex.Replace(a, "\r\n", "\n", RegexOptions.Multiline);
            var b = "";
            for (var d = 0; d < a.Length; d++)
            {
                var c = a[d];
                if (128 > c)
                {
                    b += c;
                }
                else if (127 < c && 2048 > c)
                {
                    b += c >> 6 | 192;
                }
                else
                {
                    b += c >> 12 | 224;
                    b += c >> 6 & 63 | 128;
                    b += c & 63 | 128;
                }
            }
            return b;
        }

        private static Dictionary<long, long> BuildLookup(string b)
        {
            var a = 0;
            var c = b.Length;
            a = c + 8;
            var d = 16 * ((a - a % 64) / 64 + 1);
            var e = new Dictionary<long, long>();
            var f = 0;
            var g = 0;
            for (; g < c;)
            {
                a = (g - g % 4) / 4;
                f = g % 4 * 8;
                if (!e.ContainsKey(a))
                {
                    e.Add(a, 0);
                }
                var ev = e.EnsureValue(a);
                ev = ev | b[g] << f;
                e[a] = ev;
                g++;
            }
            a = (g - g % 4) / 4;
            var v = e.EnsureValue(a);
            v = v | 128 << g % 4 * 8;
            e[a] = v;
            e.EnsureValue(d - 2);
            e[d - 2] = c << 3;
            e.EnsureValue(d - 1);
            e[d - 1] = UnsignedRightShift(c, 29);
            return e;
        }

        private static long UnsignedRightShift(long x, int shift)
        {
            return (long)((uint)x >> shift);
        }

        public static string Hash(string str)
        {
            str = Prepare(str);
            var e = 0;
            var f = BuildLookup(str);
            long a = 1732584193;
            long b = 4023233417;
            long c = 2562383102;
            long d = 271733878;
            long q = 0;
            long r = 0;
            long s = 0;
            long t = 0;
            for (e = 0; e < f.Count; e += 16)
            {
                q = a;
                r = b;
                s = c;
                t = d;
                a = k(a, b, c, d, (long)f.GetValue(e + 0), 7, 3614090360);
                d = k(d, a, b, c, (long)f.GetValue(e + 1), 12, 3905402710);
                c = k(c, d, a, b, (long)f.GetValue(e + 2), 17, 606105819);
                b = k(b, c, d, a, (long)f.GetValue(e + 3), 22, 3250441966);
                a = k(a, b, c, d, (long)f.GetValue(e + 4), 7, 4118548399);
                d = k(d, a, b, c, (long)f.GetValue(e + 5), 12, 1200080426);
                c = k(c, d, a, b, (long)f.GetValue(e + 6), 17, 2821735955);
                b = k(b, c, d, a, (long)f.GetValue(e + 7), 22, 4249261313);
                a = k(a, b, c, d, (long)f.GetValue(e + 8), 7, 1770035416);
                d = k(d, a, b, c, (long)f.GetValue(e + 9), 12, 2336552879);
                c = k(c, d, a, b, (long)f.GetValue(e + 10), 17, 4294925233);
                b = k(b, c, d, a, (long)f.GetValue(e + 11), 22, 2304563134);
                a = k(a, b, c, d, (long)f.GetValue(e + 12), 7, 1804603682);
                d = k(d, a, b, c, (long)f.GetValue(e + 13), 12, 4254626195);
                c = k(c, d, a, b, (long)f.GetValue(e + 14), 17, 2792965006);
                b = k(b, c, d, a, (long)f.GetValue(e + 15), 22, 1236535329);
                a = l(a, b, c, d, (long)f.GetValue(e + 1), 5, 4129170786);
                d = l(d, a, b, c, (long)f.GetValue(e + 6), 9, 3225465664);
                c = l(c, d, a, b, (long)f.GetValue(e + 11), 14, 643717713);
                b = l(b, c, d, a, (long)f.GetValue(e + 0), 20, 3921069994);
                a = l(a, b, c, d, (long)f.GetValue(e + 5), 5, 3593408605);
                d = l(d, a, b, c, (long)f.GetValue(e + 10), 9, 38016083);
                c = l(c, d, a, b, (long)f.GetValue(e + 15), 14, 3634488961);
                b = l(b, c, d, a, (long)f.GetValue(e + 4), 20, 3889429448);
                a = l(a, b, c, d, (long)f.GetValue(e + 9), 5, 568446438);
                d = l(d, a, b, c, (long)f.GetValue(e + 14), 9, 3275163606);
                c = l(c, d, a, b, (long)f.GetValue(e + 3), 14, 4107603335);
                b = l(b, c, d, a, (long)f.GetValue(e + 8), 20, 1163531501);
                a = l(a, b, c, d, (long)f.GetValue(e + 13), 5, 2850285829);
                d = l(d, a, b, c, (long)f.GetValue(e + 2), 9, 4243563512);
                c = l(c, d, a, b, (long)f.GetValue(e + 7), 14, 1735328473);
                b = l(b, c, d, a, (long)f.GetValue(e + 12), 20, 2368359562);
                a = m(a, b, c, d, (long)f.GetValue(e + 5), 4, 4294588738);
                d = m(d, a, b, c, (long)f.GetValue(e + 8), 11, 2272392833);
                c = m(c, d, a, b, (long)f.GetValue(e + 11), 16, 1839030562);
                b = m(b, c, d, a, (long)f.GetValue(e + 14), 23, 4259657740);
                a = m(a, b, c, d, (long)f.GetValue(e + 1), 4, 2763975236);
                d = m(d, a, b, c, (long)f.GetValue(e + 4), 11, 1272893353);
                c = m(c, d, a, b, (long)f.GetValue(e + 7), 16, 4139469664);
                b = m(b, c, d, a, (long)f.GetValue(e + 10), 23, 3200236656);
                a = m(a, b, c, d, (long)f.GetValue(e + 13), 4, 681279174);
                d = m(d, a, b, c, (long)f.GetValue(e + 0), 11, 3936430074);
                c = m(c, d, a, b, (long)f.GetValue(e + 3), 16, 3572445317);
                b = m(b, c, d, a, (long)f.GetValue(e + 6), 23, 76029189);
                a = m(a, b, c, d, (long)f.GetValue(e + 9), 4, 3654602809);
                d = m(d, a, b, c, (long)f.GetValue(e + 12), 11, 3873151461);
                c = m(c, d, a, b, (long)f.GetValue(e + 15), 16, 530742520);
                b = m(b, c, d, a, (long)f.GetValue(e + 2), 23, 3299628645);
                a = n(a, b, c, d, (long)f.GetValue(e + 0), 6, 4096336452);
                d = n(d, a, b, c, (long)f.GetValue(e + 7), 10, 1126891415);
                c = n(c, d, a, b, (long)f.GetValue(e + 14), 15, 2878612391);
                b = n(b, c, d, a, (long)f.GetValue(e + 5), 21, 4237533241);
                a = n(a, b, c, d, (long)f.GetValue(e + 12), 6, 1700485571);
                d = n(d, a, b, c, (long)f.GetValue(e + 3), 10, 2399980690);
                c = n(c, d, a, b, (long)f.GetValue(e + 10), 15, 4293915773);
                b = n(b, c, d, a, (long)f.GetValue(e + 1), 21, 2240044497);
                a = n(a, b, c, d, (long)f.GetValue(e + 8), 6, 1873313359);
                d = n(d, a, b, c, (long)f.GetValue(e + 15), 10, 4264355552);
                c = n(c, d, a, b, (long)f.GetValue(e + 6), 15, 2734768916);
                b = n(b, c, d, a, (long)f.GetValue(e + 13), 21, 1309151649);
                a = n(a, b, c, d, (long)f.GetValue(e + 4), 6, 4149444226);
                d = n(d, a, b, c, (long)f.GetValue(e + 11), 10, 3174756917);
                c = n(c, d, a, b, (long)f.GetValue(e + 2), 15, 718787259);
                b = n(b, c, d, a, (long)f.GetValue(e + 9), 21, 3951481745);
                a = h(a, q);
                b = h(b, r);
                c = h(c, s);
                d = h(d, t);
            }
            return $"{p(a)}{p(b)}{p(c)}{p(d)}".ToLower();
        }

        private static long h(long a, long b)
        {
            var e = a & 2147483648;
            var f = b & 2147483648;
            var c = a & 1073741824;
            var d = b & 1073741824;
            var g = (a & 1073741823) + (b & 1073741823);
            if ((c & d) > 0)
            {
                return unchecked((int)(g ^ 2147483648 ^ e ^ f));
            }
            else if ((c | d) > 0)
            {
                if ((g & 1073741824) > 0)
                {
                    return unchecked((int)(g ^ 3221225472 ^ e ^ f));
                }
                return unchecked((int)(g ^ 1073741824 ^ e ^ f));
            }
            return unchecked((int)(g ^ e ^ f));
            //return (c & d) > 0 ? g ^ 2147483648 ^ e ^ f : (c | d) > 0 ? (g & 1073741824) > 0 ? g ^ 3221225472 ^ e ^ f : g ^ 1073741824 ^ e ^ f : g ^ e ^ f
        }

        private static long k(long a, long b, long c, long d, long e, long f, long g)
        {
            a = h(a, h(h(b & c | ~b & d, e), g));
            var v = (long)((int)a << (int)f | UnsignedRightShift((long)a, 32 - (int)f));
            return h(v, b);

        }

        private static long l(long a, long b, long c, long d, long e, long f, long g)
        {
            a = h(a, h(h(b & d | c & ~d, e), g));
            return h((long)((int)a << (int)f | UnsignedRightShift((long)a, 32 - (int)f)), b);

        }

        private static long m(long a, long b, long d, long c, long e, long f, long g)
        {
            a = h(a, h(h(b ^ d ^ c, e), g));
            return h((long)((int)a << (int)f | UnsignedRightShift((long)a, 32 - (int)f)), b);

        }

        private static long n(long a, long b, long d, long c, long e, long f, long g)
        {
            a = h(a, h(h(d ^ (b | ~c), e), g));
            return h((long)((int)a << (int)f | UnsignedRightShift((long)a, 32 - (int)f)), b);

        }

        private static string p(long a)
        {
            var b = "";
            var d = "";
            var c = 0;
            for (c = 0; 3 >= c; c++)
            {
                var d1 = (UnsignedRightShift((long)a, 8 * c & 255));
                d = "0" + d1.ToString("X");
                b += d.Substring(d.Length - 2, 2);
            }
            return b;
        }
    }
}