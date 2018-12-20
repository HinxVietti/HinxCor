using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace HinxCor.Win32
{
    /// <summary>
    /// 此结构是API的DEVMODE结构在联合里的一个匿名结构
    /// </summary>
    [StructLayout(LayoutKind.Explicit, Pack = 2)]
    public struct Anonymity
    {
        [FieldOffset(0)]
        public short dmOrientation;
        [FieldOffset(2)]
        public short dmPaperSize;
        [FieldOffset(4)]
        public short dmPaperLength;
        [FieldOffset(6)]
        public short dmPaperWidth;
        [FieldOffset(8)]
        public short dmScale;
        [FieldOffset(10)]
        public short dmCopies;
        [FieldOffset(12)]
        public short dmDefaultSource;
        [FieldOffset(14)]
        public short dmPrintQuality;
    };//size==16
      //---------------------------------------------------------------
      /// <summary>
      /// POINTL结构
      /// </summary>
    [StructLayout(LayoutKind.Explicit, Pack = 4)]
    public struct POINTL
    {
        [FieldOffset(0)]
        public int X;
        [FieldOffset(4)]
        public int Y;
    };

    #region Ansi
    /// <summary>
    /// 此结构是API的DEVMODE结构的Ascii版本,其中有个匿名结构由Anonymity结构代替
    /// </summary>
    [StructLayout(LayoutKind.Explicit, Pack = 2, CharSet = CharSet.Ansi)]
    public struct DEVMODEA
    {
        [FieldOffset(0)]
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
        public string DeviceName;//32+2
        [FieldOffset(34)]
        public ushort SpecVersion;//34
        [FieldOffset(36)]
        public ushort DriverVersion;//36
        [FieldOffset(38)]
        public ushort structSize;//38
        [FieldOffset(40)]
        public ushort DriverExtra;//40;
        [FieldOffset(42)]
        public int Fields;//44
        [FieldOffset(46)]
        public Anonymity Union1_Anonymity;
        [FieldOffset(46)]
        public POINTL Union1_POINTL;
        [FieldOffset(46)]
        public int Union1_DisplayOrientation;
        [FieldOffset(46)]
        public int Union1_DisplayFixedOutput;
        [FieldOffset(62)]
        public short Color;
        [FieldOffset(64)]
        public short Duplex;
        [FieldOffset(66)]
        public short YResolution;
        [FieldOffset(68)]
        public short TTOption;
        [FieldOffset(70)]
        public short Collate;//70 ??+2 补位
        [FieldOffset(72)]
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
        public string FormName;//104
        [FieldOffset(104)]
        public ushort LogPixels;//106
        [FieldOffset(106)]
        public int BitsPerPel;//110
        [FieldOffset(110)]
        public int PelsWidth;//114
        [FieldOffset(114)]
        public int PelsHeight;//118
        [FieldOffset(118)]
        public int Union2_DisplayFlags;//122
        [FieldOffset(122)]
        public int Union2_Nup;//122
        [FieldOffset(122)]
        public int DisplayFrequency;
        [FieldOffset(126)]//win95以上
        public int ICMMethod;
        [FieldOffset(130)]
        public int ICMIntent;
        [FieldOffset(134)]
        public int MediaType;
        [FieldOffset(138)]
        public int DitherType;
        [FieldOffset(142)]
        public int Reserved1;
        [FieldOffset(146)]
        public int Reserved2;
        [FieldOffset(152)]
        public int PanningWidth;
        [FieldOffset(156)]
        public int PanningHeight;//size==160 补两个定长的char数组各2位*/
    };

    #endregion Ansi


    #region UNICODE
    
    /// <summary>
    /// 此结构是API的DEVMODE结构的Unicode版本，匿名结构由Anonymity结构代替
    /// </summary>
    [StructLayout(LayoutKind.Explicit, Pack = 2, CharSet = CharSet.Unicode)]//160+64
    public struct DEVMODEW
    {
        [FieldOffset(0)]
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
        public string DeviceName;//66
        [FieldOffset(66)]
        public ushort SpecVersion;//68
        [FieldOffset(68)]
        public ushort DriverVersion;//70
        [FieldOffset(70)]
        public ushort structSize;
        [FieldOffset(72)]
        public ushort DriverExtra;//74
        [FieldOffset(74)]
        public int Fields;//78
        [FieldOffset(78)]
        public Anonymity Union1_Anonymity;
        [FieldOffset(78)]
        public POINTL Union1_POINTL;
        [FieldOffset(78)]
        public int Union1_DisplayOrientation;
        [FieldOffset(78)]
        public int Union1_DisplayFixedOutput;//94
        [FieldOffset(94)]
        public short Color;
        [FieldOffset(96)]
        public short Duplex;//98
        [FieldOffset(98)]
        public short YResolution;
        [FieldOffset(100)]
        public short TTOption;//102
        [FieldOffset(102)]
        public short Collate;//104
        [FieldOffset(104)]
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
        public string FormName; //168,补2个位,对齐结构
        [FieldOffset(170)]
        public ushort LogPixels;
        [FieldOffset(172)]
        public int BitsPerPel;//174
        [FieldOffset(176)]
        public int PelsWidth;
        [FieldOffset(180)]
        public int PelsHeight;// 下面联合
        [FieldOffset(184)]
        public int Union2_DisplayFlags;
        [FieldOffset(184)]
        public int Union2_Nup;
        [FieldOffset(188)]
        public int DisplayFrequency;
        [FieldOffset(192)]
        public int ICMMethod;// win95以上版本
        [FieldOffset(196)]
        public int ICMIntent;
        [FieldOffset(200)]
        public int MediaType;
        [FieldOffset(204)]
        public int DitherType;
        [FieldOffset(208)]
        public int Reserved1;
        [FieldOffset(212)]
        public int Reserved2;
        [FieldOffset(216)]
        public int PanningWidth;
        [FieldOffset(220)]
        public int PanningHeight;//224

        //*/size=224==64+160
    };
    
    #endregion UNICODE
}
