using Lib;

namespace AppUsingDnx
{
    public static class App
    {
        public static int Foo()
        {
            return 2 + 2;
        }

        public static string UseLib()
        {
            return Library.GetName();
        }
    }
}
