//using System;
//using System.Collections.Generic;

//public class HLYCrypto
//{
//    private class exports
//    {

//    }


//    static void nil_01(object root, object factory)
//    {
//        if (typeof(exports) == null)
//        {

//        }
//    }


//    object pad(object data, int blockSize)
//    {
//        var blockSizeBytes = blockSize * 4;

//        data.Clamp();
//        data.sigBytes += blockSizeBytes - ((data.sigBytes % blockSizeBytes) || blockSizeBytes);

//        return null;
//    }

//    object unpad(object data)
//    {
//        var datawords = data.words;
//        var i = data.sigBytes - 1;
//        while (!((datawords[i >> 2]) >> (24 - (i % 4) * 8)) & 0xff)
//        {
//            i--;
//        }
//        data.sigBytes = i + 1;
//    }

//    object ZeroPadding()
//    {
//        pad();
//        unpad();
//    }



//	return CryptoJS;

//}));
//}

