using System.Reflection;

namespace Stealer
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            Spy spy = new Spy();
            Type type = typeof(Hacker);
            //string result = spy.StealFieldInfo(type.FullName, "username", "password");
            //string result = spy.AnalyzeAccessModifiers(type.FullName);
            //string result = spy.RevealPrivateMethods(type.FullName);
            string result = spy.CollectGettersAndSetters(type.FullName);
            Console.WriteLine(result);
            
            
        }
    }
}