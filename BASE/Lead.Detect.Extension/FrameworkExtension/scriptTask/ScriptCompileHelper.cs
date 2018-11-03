using System;
using System.CodeDom.Compiler;
using System.Reflection;
using System.Text;
using Microsoft.CodeDom.Providers.DotNetCompilerPlatform;

namespace Lead.Detect.FrameworkExtension.scriptTask
{
    public class ScriptCompileHelper
    {


        static Lazy<CSharpCodeProvider> CodeProvider { get; } = new Lazy<CSharpCodeProvider>(() =>
        {
            var csc = new CSharpCodeProvider();
            var settings = csc
                .GetType()
                .GetField("_compilerSettings", BindingFlags.Instance | BindingFlags.NonPublic)
                .GetValue(csc);

            var path = settings
                .GetType()
                .GetField("_compilerFullPath", BindingFlags.Instance | BindingFlags.NonPublic);

            path.SetValue(settings, ((string)path.GetValue(settings)).Replace(@"bin\roslyn\", @"roslyn\"));

            return csc;
        });


        public static Assembly Compile(string source)
        {
            var provider = CodeProvider;

            var param = new CompilerParameters();

            // Add ALL of the assembly references
            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                try
                {
                    param.ReferencedAssemblies.Add(assembly.Location);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }

            // Add specific assembly references
            //param.ReferencedAssemblies.Add("System.dll");
            //param.ReferencedAssemblies.Add("CSharp.dll");

            // Generate a dll in memory
            param.GenerateExecutable = false;
            param.GenerateInMemory = true;

            // Compile the source
            var result = provider.Value.CompileAssemblyFromSource(param, source);

            if (result.Errors.Count > 0)
            {
                var msg = new StringBuilder();
                foreach (CompilerError error in result.Errors)
                {
                    msg.AppendFormat("Error ({0}): {1}\n",
                        error.ErrorNumber, error.ErrorText);
                }
                throw new Exception(msg.ToString());
            }

            // Return the assembly
            return result.CompiledAssembly;

        }
    }
}