using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Stealer
{
    public class Spy
    {
        public Spy()
        {

        }

        public string StealFieldInfo(string investigatedClass, params string[] requestedFields)
        {
            Type classType = Type.GetType(investigatedClass);
            FieldInfo[] classField = classType.GetFields((BindingFlags)60);

            StringBuilder sb = new StringBuilder();
            object classInstance = Activator.CreateInstance(classType, new object[] { });
            sb.AppendLine($"Class under investigation: {investigatedClass}");
            foreach (FieldInfo field in classField.Where(f => requestedFields.Contains(f.Name)))
            {
                sb.AppendLine($"{field.Name} = {field.GetValue(classInstance)}");
            }
            return sb.ToString().TrimEnd();
        }
        public string AnalyzeAccessModifiers(string className)
        {
            Type classType = Type.GetType(className);
            FieldInfo[] classFields = classType.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.Static);
            MethodInfo[] classPublicMethods = classType.GetMethods(BindingFlags.Instance | BindingFlags.Public);
            MethodInfo[] classNonPublicMethods = classType.GetMethods(BindingFlags.Instance | BindingFlags.NonPublic);
            StringBuilder sb = new StringBuilder();

            foreach (FieldInfo field in classFields)
            {
                sb.AppendLine($"{field.Name} must be private");
            }
            foreach (MethodInfo method in classNonPublicMethods.Where(m => m.Name.StartsWith("set")))
            {
                sb.AppendLine($"{method.Name} have to be public");
            }
            foreach (var method in classPublicMethods.Where(m => m.Name.StartsWith("get")))
            {
                sb.AppendLine($"{method.Name} have to be private");
            }
            return sb.ToString().TrimEnd();
        }
        public string RevealPrivateMethods(string investigatedClass)
        {
            Type classType = Type.GetType(investigatedClass);
            MethodInfo[] methods = classType.GetMethods(BindingFlags.Instance| BindingFlags.NonPublic);

            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"All Private Methods of Class: {investigatedClass}");
            sb.AppendLine($"Base class: {classType.BaseType.Name}");
            foreach (var method in methods)
            {
                sb.AppendLine(method.Name);
            }
            return sb.ToString().TrimEnd();
        }
        public string CollectGettersAndSetters(string investigatedClass)
        {
            Type type = Type.GetType(investigatedClass);
            MethodInfo[] methods = type.GetMethods(BindingFlags.Instance | BindingFlags.Public| BindingFlags.NonPublic);
            
            StringBuilder sb = new StringBuilder();

            foreach (var method in methods.Where(m => m.Name.StartsWith("get")))
            {
                sb.AppendLine($"{method.Name} will return {method.ReturnType}");
            }
            foreach (var method in methods.Where(m => m.Name.StartsWith("set")))
            {
                sb.AppendLine($"{method.Name} will set field of {method.GetParameters().First().ParameterType}");
            }


            return sb.ToString().TrimEnd();
        }
    }
}
