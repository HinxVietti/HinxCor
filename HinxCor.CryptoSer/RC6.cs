using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HinxCor.CryptoSer
{
    /// <summary>
    /// 山寨rc6 算法
    /// </summary>
    public class RC6
    {
        /// <summary>
        /// 对称加解密算法RC6的C#实现
        /// 公开维普网_朱明海先生未公布的源码
        /// 程序完善设计者:四川威远_老耙子 先生
        /// 2010-11-28
        /// 本程序只提供了明文长度32的算法。
        /// 如有需要，请与本人联系。
        /// Mail:chscwyyg@163.com 电话:0832-8229211
        /// </summary>
        private string m_sEncryptionKey;                                             //密码方法通过KEY属性返回值
        /// <summary>
        /// 加密解密字符串返回值
        /// </summary>
        public string m_sCryptedText;                                                //
        private int m_nChipherlen;                                                   //密码字节数，控制加密最低为128,最好256,间192，有三种选择种16,24,32
        private const int m_nWord = 32;
        private const int r = 20;
        private const int c = 4;
        private uint[] m_nKeyExpandBox;                                              //系统密钥数组
        uint[] n_WordBox;                                                            //用户私有密钥
        /// <summary>
        /// 编码方式
        /// </summary>
        public Encoding Enc_default = Encoding.Unicode;

        /// <summary>
        /// 左位移运算函数
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="w"></param>
        /// <returns></returns>
        ///
        public uint ROTL(uint x, uint y, int w)
        { return ((x << (int)(y & (0xFF))) | (x >> (int)(w - (y & (0xFF))))); }//或:return ((x << (int)(y & (w-1))) | (x >> (int)(w - (y & (w-1)))));

        /// <summary>
        /// 右位移运算函数
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="w"></param>
        /// <returns></returns>
        public uint ROTR(uint x, uint y, int w)
        { return ((x >> (int)(y & (0xFF))) | (x << (int)(w - (y & (0xFF))))); }//或:return ((x >> (int)(y & (w-1))) | (x << (int)(w - (y & (w-1)))));

        /// <summary>
        /// 构造函数
        /// </summary>
        public RC6()
        {
            IV = 16;                                        //如果用户没有设置加密方式系统默认为128位加密
                                                            //                                                IV返回m_nChipherlen
            m_nKeyExpandBox = new uint[8 * m_nChipherlen];  //密钥数组大小为加IV*8
        }
        /// <summary>
        /// 构造函数可输入加密向量
        /// </summary>
        /// <param name="iv"></param>
        public RC6(int iv)
        {
            IV = iv;            //返顺       m_nChipherlen
            this.m_nKeyExpandBox = new uint[8 * m_nChipherlen];
        }
        /// <summary>
        /// 定义一个属性，通过属性输入用户密钥并返回
        /// 存储到m_sEncryptionKey
        /// </summary>
        public string KEY
        {
            get { return this.m_sEncryptionKey; }
            set { this.m_sEncryptionKey = value; }
        }

        /// <summary>
        /// 加密向量选择
        /// 128方式IV=16
        /// 192方法IV=24
        /// 256方法IV=32
        /// </summary>
        public int IV
        {
            get { return m_nChipherlen; }
            set { m_nChipherlen = value; }
        }

        /// <summary>
        /// 加密向量验证函数
        /// 有错误返回最小或最大的向量设置
        /// </summary>
        /// <returns></returns>
        public int _IV()
        {
            if (m_nChipherlen < 16) m_nChipherlen = 16;
            if (m_nChipherlen > 32) m_nChipherlen = 32;
            return m_nChipherlen;
        }

        /// <summary>
        /// string类型Unicode字符集转为字节流char[];
        /// </summary>
        /// <returns></returns>
        private char[] String_Unicode()
        {
            string prssword = this.m_sEncryptionKey;
            prssword = (prssword.Length % m_nChipherlen != 0 ? prssword.PadRight(prssword.Length + (m_nChipherlen - (prssword.Length % m_nChipherlen)), '\0') : prssword);

            byte[] asciiBytes = Encoding.Convert(Encoding.Unicode, Encoding.ASCII, Encoding.Unicode.GetBytes(prssword));
            char[] asciiChars = new char[Encoding.ASCII.GetCharCount(asciiBytes, 0, asciiBytes.Length)];             //等价=====char[] asciiChars = new char[asciiBytes.Length];
            Encoding.ASCII.GetChars(asciiBytes, 0, asciiBytes.Length, asciiChars, 0);
            return asciiChars;
        }


        /// <summary>
        /// 初始化函数用户密钥
        /// 通过KeySteup函数扩展并混合密钥
        /// </summary>
        ///
        private void KeySteup()
        {
            uint P32 = 0xb7e15163;
            uint Q32 = 0x9e3779b9;
            uint A, B;
            A = B = 0;
            int i, j;
            i = j = 0;

            char[] key = String_Unicode();                       ////密码类型转换,String型转换为char[],下面String_Unicode()函数返回值

            n_WordBox = new uint[m_nChipherlen / 4];                 //选择  16  or  24 or 32  128,192,256加密方式,4,6,8
            uint temp;


            //密钥类型转换  string类型转换为uint
            for (j = 0; j < m_nChipherlen; j++)    //asciiChars[j]作0xFF运算,循环左移,如果 m_nChipherlen=32,第一轮不移位字节占位0-7,第二轮左移8位字节占位8-15, 第三轮位移16位，字节占16-23,    
            {                                      //第四轮位移24位字节占位24-31.第四轮生成一个uint,共循环m_nChipherlen次生成m_nChipherlen/4个Uint, 16只生成4个,24生成6个,32生成8个,每个uint点32位，
                                                   //                                       所以加密的密节数==16时,4*32=加密128位,24时==6*32加密192位,32时==8*32加密256位,这里实现加密方式
                temp = (uint)(key[j] & 0xFF) << (8 * (j % 4));
                n_WordBox[j / 4] += (uint)temp;   //循环四次生成一个uint   

                /* ========================================================说明位移运算方法===================================================================//
                 *                                                               比方:a=97为01100001,0移位Convert.ToString(97, 2).PadLeft(8, '0'),
                 *                                                               b=98为01100010,左移8后变成占位0110001000000000=25088;
                 *                                                               c=99为01100011,左移16位变成占位011000110000000000000000=6488064
                 *                                                               d=100为01100100,左移24变成占位01100100000000000000000000000000=1677721600
                 *                                                               以上abcd左移运算后是以下情况
                 *                                                               abcd相加后变成了一个Uint数据占32字节等于Convert.ToString(100, 2).PadLeft(8, '0') + Convert.ToString(99, 2).PadLeft(8, '0') +
                 *                                                                                                        Convert.ToString(98, 2).PadLeft(8, '0') + Convert.ToString(97, 2).PadLeft(8, '0');
                 *                                                               a+b+c+d=1684234849,其字节为01100100011000110110001001100001;uint value=1684234849请验证下。
                 *                                                               uint转换为字节 string x=Convert.ToString(uint value,2);
                 */
            }

            //密钥扩展                                                              m_nChipherLen=32   or 24 or 16  
            this.m_nKeyExpandBox[0] = P32;                                           //扩展密钥初始化,0位置赋值P32 
            for (j = 1; j < 2 * m_nChipherlen + 4; j++)                              //m_nChipherlen是加密向量，反应加密方式128，192，256三种
            { this.m_nKeyExpandBox[j] = m_nKeyExpandBox[j - 1] + Q32; }

            int k = 3 * Math.Max(n_WordBox.Length, 2 * m_nChipherlen + 4);
            for (j = 0, i = 0; k > 0; k--)
            {
                A = ROTL(m_nKeyExpandBox[i] + A + B, 3, m_nWord);                   //混合密钥
                m_nKeyExpandBox[i] = (byte)A;
                B += A;
                B = ROTL(n_WordBox[j] + A + B, A + B, m_nWord);                   //m_nKeyExpandBox[ii]的值混合到用户密钥中                        
                n_WordBox[j] = B;                                                 //这里是仍然是uint，生成新用户密码，32位值。提取见上面方法  [***  1]
                i = (i + 1) % (2 * m_nChipherlen + 4);                              //取模运算确保对应 m_nKeyExpandBox和 n_WordBox数组不越界                    
                j = (j + 1) % n_WordBox.Length;                                   //n_WordBox.Length  分别代表 4,6,8
            }
        }
        //


        /// <summary>
        /// 加密函数
        /// </summary>
        /// <param name="str">加密的明文</param>
        /// <param name="prssword">返回的密文</param>
        /// <returns></returns>
        public string Encrypt(string str, string prssword)
        {


            //验证明文长度不能小开32,
            str = (str.Length % 32 != 0 ? str.PadRight(str.Length + (32 - (str.Length % 32)), '\0') : str);//不足补空字符串
            KEY = prssword;                                        //Key属性返回m_sEncryptionKey方法
            KeySteup();                                            //初始化

            uint A, B, C, D, T, U, temp; T = U = 0;                 //4个32位循环赋值的uint(A,B,C,D)
            A = B = C = D = 0;
            temp = 0;

            byte[] input = Encoding.Unicode.GetBytes(str);             // 输入的明文this.m_slnClearText;明文string为Unicode字符集,每个输入字符均为两字节16位

            //以Unicode转换Byte是byte[1]奇数值为0,偶数值为0,2,4,6等等为输入的字符十进制值,所以低位值才是要加密的值，高位不管它，因为它为0.
            char[] chars = new char[Encoding.ASCII.GetCharCount(input, 0, input.Length)];
            Encoding.ASCII.GetChars(input, 0, input.Length, chars, 0);
            byte[] output = new Byte[input.Length];

            //                                                    加密的字符串不只128个字符情况该如何, 这需要定义byte多维数据才能实现
            for (int k = 0; k < 4; k++)                           /* 输入明文要求为ASCII,将字节低位进行0xFF运算左移8*k位转换input(uint) */
            {
                A += (uint)(input[2 * k] & 0xFF) << (8 * k);      //第一个元素不移位,填充uint0-7位,第二个左移8位填充8-15,第三个左移16位填充16-23位,第四个左移24位填充24-31位,这样组成一个uint
                B += (uint)(input[2 * k + 8] & 0xFF) << (8 * k);  //第五个位元素填充uint32-39位,六个填充40-47位,七个填充48-55位,八个填充56-63位
                C += (uint)(input[2 * k + 16] & 0xFF) << (8 * k); //第九个位元素填充uint64-71位,十个填充72-79位,十一个填充80-87位,十二个填充88-95位
                D += (uint)(input[2 * k + 24] & 0xFF) << (8 * k); //第十三个位元素填充uint96-103位,十四个填充104-111位,十五个填充112-119位,十六个填充120-127位
            }

            /*                                                    扩展密钥数组n_LocExpandBox和A,B,C,D进行基本加密
            */
            //A,B,C,D变量循环赋值,第一次A,B,C,D第二次B,C,D,A,第三次C,D,A,B,以此类推,共循环m_nChipherlen-1次。
            B += m_nKeyExpandBox[0];
            D += m_nKeyExpandBox[1];
            for (int i = 1; i <= m_nChipherlen; i++)              //左移计算并位置换位，矩阵运算
            {
                U = ROTL(D * (2 * D + 1), 5, m_nWord);            //左移5位，这里其实m_nWord没有多少实用价值,因为本类未使用return ((x << (int)(y & (w-1))) | (x >> (int)(w - (y & (w-1)))))
                T = ROTL(B * (2 * B + 1), 5, m_nWord);
                A = ROTL(A ^ T, U, m_nWord) + m_nKeyExpandBox[2 * i];
                C = ROTL(C ^ U, T, m_nWord) + m_nKeyExpandBox[2 * i + 1];
                temp = A;                                         //中间变量temp的值为A
                A = B;                                            //B的值赋值给A，就是B的位置移动到了A
                B = C;                                            //C的值赋值给B，就是C的位置移动到了B
                C = D;                                            //D的值赋值给C，就是D的位置移动到了C
                D = temp;                                         //A的值赋值给D，就是A的位置移动到了D                               
            }

            uint[] put = new uint[4];

            A += m_nKeyExpandBox[2 * r + 2];
            C += m_nKeyExpandBox[2 * r + 3];

            put[0] = A;
            put[1] = B;
            put[2] = C;
            put[3] = D;

            //转换A,B,C,D为一个字节组output，并进而转换为string型m_sCrytedText，下面是第一步的逆操作.      
            for (int k = 0; k < 4; k++)
            {
                output[2 * k] = (byte)((put[0] >> 8 * k) & 0xFF);
                output[2 * k + 8] = (byte)((put[1] >> 8 * k) & 0xFF);
                output[2 * k + 16] = (byte)((put[2] >> 8 * k) & 0xFF);
                output[2 * k + 24] = (byte)((put[3] >> 8 * k) & 0xFF);

            }

            char[] outarrchar = new char[output.Length];
            Encoding.Unicode.GetChars(output, 0, output.Length, outarrchar, 0);
            this.m_sCryptedText = new string(outarrchar, 0, outarrchar.Length);
            byte[] Output1 = Enc_default.GetBytes(this.m_sCryptedText);
            //this.m_sEncryptionKey = "";
            return m_sCryptedText;
        }
        /// <summary>
        /// 解密函数
        /// </summary>
        /// <param name="str">解密的密文</param>
        /// <param name="prssword">解密后的明文</param>
        /// <returns></returns>
        public string Decrypt(string str, string prssword)
        {
            //使用 " " 替代 '/0'
            str = (str.Length % 32 != 0 ? str.PadRight(str.Length + (32 - (str.Length % 32)), '\0') : str);//验证密文长度不能小开32,不足补空字符串
            KEY = prssword;                                       //Key属性返回m_sEncryptionKey方法
            KeySteup();                                            //初始化

            uint A, B, C, D, T, U, temp; T = U = 0;                //4个32位循环赋值的uint(A,B,C,D)
            A = B = C = D = 0;
            temp = 0;
            byte[] input = Enc_default.GetBytes(str);             // 输入的密文this.m_slnClearText;明文string为Unicode字符集,每个输入字符均为两字节16位

            byte[] output = new Byte[input.Length];

            for (int k = 0; k < 4; k++)
            {
                A += ((uint)input[2 * k] & 0xFF) << (8 * k);
                B += ((uint)input[2 * k + 8] & 0xFF) << (8 * k);
                C += ((uint)input[2 * k + 16] & 0xFF) << (8 * k);
                D += ((uint)input[2 * k + 24] & 0xFF) << (8 * k);

            }



            C -= m_nKeyExpandBox[2 * r + 3];
            A -= m_nKeyExpandBox[2 * r + 2];

            for (int i = 1; i <= m_nChipherlen; i++)
            {
                temp = D;
                D = C;
                C = B;
                B = A;
                A = temp;
                U = ROTL(D * (2 * D + 1), 5, m_nWord);
                T = ROTL(B * (2 * B + 1), 5, m_nWord);
                C = ROTR(C - m_nKeyExpandBox[2 * (m_nChipherlen - i) + 3], T, m_nWord) ^ U;
                A = ROTR(A - m_nKeyExpandBox[2 * (m_nChipherlen - i) + 2], U, m_nWord) ^ T;
            }
            D -= m_nKeyExpandBox[1];
            B -= m_nKeyExpandBox[0];

            //转换A,B,C,D为一个字节组output，并进而转换为string型m_sCrytedText，下面是第一步的逆操作.



            for (int k = 0; k < 4; k++)
            {
                output[2 * k] = (byte)((A >> (8 * k)) & 0xFF);
                output[2 * k + 8] = (byte)((B >> (8 * k)) & 0xFF);
                output[2 * k + 16] = (byte)((C >> (8 * k)) & 0xFF);
                output[2 * k + 24] = (byte)((D >> (8 * k)) & 0xFF);
            }
            char[] outarrchar = new char[Enc_default.GetCharCount(output, 0, output.Length)];
            Enc_default.GetChars(output, 0, output.Length, outarrchar, 0);
            this.m_sCryptedText = new string(outarrchar, 0, outarrchar.Length);
            byte[] Output1 = Enc_default.GetBytes(this.m_sCryptedText);
            //this.m_sEncryptionKey = "";
            return m_sCryptedText;
        }

    }
}
