using System;
using System.Reflection;

namespace BeyondNet.Demo.Polly.App.Samples
{
    public class PersonFunction
    {

        public delegate string[] SomeFunction(int param);

        public string[] GetAll(Func<string[]> func)
        {
            return func();
        }

        public string[] GetById(SomeFunction func, int index)
        {
            return func(index);
        }
    }
}
