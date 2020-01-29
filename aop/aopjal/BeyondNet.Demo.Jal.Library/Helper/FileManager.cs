using System;
using System.IO;
using Jal.Monads;

namespace BeyondNet.Demo.Jal.Library.Helper
{
    public class FileManager : IFileManager
    {
        public Result Write(string message)
        {
            try
            {
                var fileStream = new FileStream("log.txt", FileMode.Append, FileAccess.Write);
                var fileWriter = new StreamWriter(fileStream);

                fileWriter.WriteLine(message);
                fileWriter.Flush();
                fileWriter.Close();

                return Result.Success();
            }
            catch (Exception ex)
            {
                return Result.Failure(new []{$"An erro was ocurred. Error: {ex.Message}"});
            }
         
        }
    }
}
