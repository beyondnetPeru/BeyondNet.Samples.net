using System;
using System.Net;
using Polly;

namespace BeyondNet.Demo.Polly.App.Samples
{
    class SampleExecutor
    {
        public static void FuncWrapperPollyGetAll()
        {
            var sampleWrapper = new SampleWrapperPolly();
            var person = new Person();

            var data = sampleWrapper.Handle(person.GetAll, new[] { "" });

            foreach (var item in data)
            {
                Console.WriteLine(item);
            }
        }

        public static void FuncWrapperPollyGetById()
        {
            var sampleWrapper = new SampleWrapperPolly();
            var person = new Person();

            var data = sampleWrapper.Handle(() => person.GetById(2), new[] { "" });

            foreach (var item in data)
            {
                Console.WriteLine(item);
            }
        }

        public static void FuncWrapperPollyGetCriteria()
        {
            var sampleWrapper = new SampleWrapperPolly();
            var person = new Person();

            var data = sampleWrapper.Handle(() => person.GetByCriteria(2, "Alberto Arroyo"), new[] { "" });

            foreach (var item in data)
            {
                Console.WriteLine(item);
            }
        }

        public static void FuncGetAll()
        {
            var personFunction = new PersonFunction();
            var person = new Person();

            var data = personFunction.GetAll(person.GetAll);

            foreach (var item in data)
            {
                Console.WriteLine(item);
            }
        }

        public static void FuncGetById()
        {
            var personFunction = new PersonFunction();

            var data = personFunction.GetById(FunctionToGet, 1);

            foreach (var item in data)
            {
                Console.WriteLine(item);
            }
        }

        internal static string[] FunctionToGet(int id)
        {
            var person = new Person();
            return person.GetById(id);
        }

        private static void WithOutPolly()
        {
            using (var wc = new WebClient())
            {
                string remoteData =
                    wc.DownloadString(
                        "https://raw.githubusercontent.com/jason-roberts/PSPolDemoFiles/master/SomeRemoteData.txt");

                var message = remoteData += Environment.NewLine;

                Console.WriteLine(message);

                Console.Read();
            }
        }
        private static void WithPolly()
        {
            Policy.Handle<WebException>()
                .Retry(2, (exception, retryCount) =>
                {
                    Console.WriteLine("");
                    Console.WriteLine("DoWork threw a " + exception.GetType().Name);
                    Console.WriteLine("About to retry for " + retryCount + " time");
                }).Execute(() =>
                {
                    DownloadString();
                });
        }
        private static void WithLinearPollyImplementation()
        {

        }
        internal static void DownloadString()
        {
            using (var wc = new WebClient())
            {
                var remoteData =
                    wc.DownloadString(
                        "https://raw.githubusercontent.com/jason-roberts/PSPolDemoFiles/master/SomeRemoteData.txte");

                var message = remoteData += Environment.NewLine;

                Console.WriteLine(message);

                Console.Read();
            }
        }
    }

}

