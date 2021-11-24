Hey, Aren't you tired of injecting a logger everywhere for exceptions? 🤨  
  
Let's just throw exceptions anywhere 🤪  
Then get a global exception handler (like https://github.com/alidemirbas/P.Error . you have nothing to do with it but a single code line)  
and then go in your exception handler scope, that's all like below.  
```csharp
MethodBase target = exception.TargetSite;

ILogger logger = new Logger();
logger.Log(new Log
{
    Namespace = target?.ReflectedType.Namespace,
    ClassName = target?.ReflectedType.Name,
    MethodName = target?.Name,
    DateTime = DateTime.Now,
    Description = exception.Message,
    Success = false,
    Title = exception.GetType().Name
});
```

It can be a "File.Logger" or a "Db.Logger" with a namespace. It's up to you.  
It's that much easy 😏  
