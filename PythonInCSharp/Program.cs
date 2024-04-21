using Python.Runtime;

SetEnv();
RunScript();
RunScript2("myScript");

static void SetEnv()
{
    Runtime.PythonDLL = GetDLL();
    PythonEngine.Initialize();

    //dodanie katalogu ze skryptami do sys Environment
    dynamic sys = Py.Import("sys");
    sys.path.append(@"D:\PROGRAMMER\C#\CODES\PythonInCSharp\PythonInCSharp\");
}

static void RunScript()
{
    //Runtime.PythonDLL = GetDLL();
    //PythonEngine.Initialize();

    using (Py.GIL())
    {
        //dynamic sys = Py.Import("sys");
        //Console.WriteLine(sys.version);
        //sys.path.append(@"D:\PROGRAMMER\C#\CODES\PythonInCSharp\PythonInCSharp\");

        dynamic pyScript = Py.Import(@"myScript");
        Console.WriteLine(pyScript.InvokeMethod("SayHello"));


        dynamic np = Py.Import("numpy");
        dynamic sin = np.sin;
        Console.WriteLine(sin(5));

        double c = (double)(np.cos(5) + sin(5));
        Console.WriteLine(c);

        dynamic a = np.array(new List<float> { 1, 2, 3 });
        Console.WriteLine(a.dtype);

        dynamic b = np.array(new List<float> { 6, 5, 4 }, dtype: np.int32);
        Console.WriteLine(b.dtype);

        Console.WriteLine(a * b);


        dynamic game = Py.Import(@"main");
        game.InvokeMethod("SayHello");

        using var scope = Py.CreateScope();
        scope.Exec("print('Hello World from Python code inside .cs file!')");

        Console.ReadKey();

    }
}

static void RunScript2(string scriptName)
{
    using (Py.GIL())
    {
        dynamic pyScript = Py.Import(scriptName);
        var msg = new PyString("New Message");
        var result = pyScript.InvokeMethod("Test", new PyObject[] { msg });
        Console.WriteLine(result);
    }
}

static string GetDLL()
{
    return "C:\\Users\\Dell\\AppData\\Local\\Programs\\Python\\Python312\\python312.dll";
}