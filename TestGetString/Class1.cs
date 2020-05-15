using System;


public static class Test
{

    public static string GetString()
    {
        return "This is an testString";
    }
}


namespace TestNS
{

    public static class Test
    {

        public static string GetString()
        {
            return "This is an testString From name space testNS";
        }

    }

}