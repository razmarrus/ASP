using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PointSystem.Logger
{
    public class FileLoggerProvider : ILoggerProvider
        {
            private string path;
            public FileLoggerProvider(string _path)
            {
                path = _path;
            }
            public ILogger CreateLogger(string categoryName)  //провайдер логгирования
        {
                return new FileLogger(path);
            }

            public void Dispose()  //ограничения
        {
            }
        
    }
}
