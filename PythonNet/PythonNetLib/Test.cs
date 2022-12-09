using Python.Runtime;
using System;
using System.Collections.Generic;
using System.IO;

namespace PythonNetLib
{
    public class Test
    {
        public static void Test1() {
            var PYTHONNET_PYDLL = @$"G:\miniconda\envs\python310\python310.dll";

            var pathToPythonnet = @$"G:\miniconda\envs\python310\Lib\site-packages";


            var pathToPython = @$"G:\miniconda\envs\python310";

            var paths = pathToPython + ";" + pathToPythonnet + ";" + Environment.GetEnvironmentVariable("PATH", EnvironmentVariableTarget.Machine);

            Environment.SetEnvironmentVariable("PYTHONNET_PYDLL", PYTHONNET_PYDLL, EnvironmentVariableTarget.Process);
            Environment.SetEnvironmentVariable("PYTHONHOME", pathToPython, EnvironmentVariableTarget.Process);
            Environment.SetEnvironmentVariable("PYTHONPATH", paths, EnvironmentVariableTarget.Process);

            PythonEngine.Initialize();
            // acquire the GIL before using the Python interpreter
            using (Py.GIL())
            {
                using var _ = Py.GIL();

                dynamic np = Py.Import("numpy");
                Console.WriteLine(np.cos(np.pi * 2));

                dynamic sin = np.sin;
                Console.WriteLine(sin(5));

                double c = (double)(np.cos(5) + sin(5));
                Console.WriteLine(c);

                dynamic a = np.array(new List<float> { 1, 2, 3 });
                Console.WriteLine(a.dtype);

                dynamic b = np.array(new List<float> { 6, 5, 4 }, dtype: np.int32);
                Console.WriteLine(b.dtype);

                Console.WriteLine(a * b);
                Console.ReadKey();
            }
        }
    }
}
