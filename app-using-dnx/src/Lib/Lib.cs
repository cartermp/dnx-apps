using SomeDependencyLib;

namespace Lib
{
    public static class Library
    {
        public static int Foo()
        {
            return 2 + 2;
        }

        public static string Bar()
        {
            return TheDependencyLib.GetName();
        }
    }
}
