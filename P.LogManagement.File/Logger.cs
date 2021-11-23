using System;
using System.Configuration;
using System.IO;
using System.Reflection;
using System.Text;

namespace P.LogManagement.File
{
    public abstract class Logger : ILogger
    {
        public abstract string FilePath { get;}
      
        public virtual void Log(ILog log)
        {
            //todo multithread'de ne olacak bir dusun?
            //ayrica AppendAllText iyi mi yoksa stream'de mi tutmak iyi olur?

            var logType = log.GetType();
            var propInfos = logType.GetProperties();

            var fileName = $"{logType.Name}.txt";

            if (string.IsNullOrEmpty(FilePath))
                //e burda da hata firlatirsan donguye girer
                //todo yaparken threadleri kitlememesine dikkat et
                throw new Exception();

            if (!Directory.Exists(FilePath))
            {
                Directory.CreateDirectory(FilePath);
            }

            string fileFullPath = Path.Combine(FilePath, fileName);

            if (!System.IO.File.Exists(fileFullPath))
                System.IO.File.Create(fileFullPath);

            var sb = new StringBuilder();

            foreach (var propInfo in propInfos)
            {
                sb.AppendFormat("{0}: {1}{2}", propInfo.Name, propInfo.GetValue(log),Environment.NewLine);
            }

            sb.AppendLine();//2. satir araligi

            try
            {
                System.IO.File.AppendAllText(fileFullPath, sb.ToString());
            }
            catch (Exception ex)
            {
                //todo bazen patlıyor dosya kullanılıyor dıye
            }
        }
    }
}
