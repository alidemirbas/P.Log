Hey, Aren't you tired of injecting a logger everywhere for exceptions? 🤨_
KISS again 😘 (keep it simple, stupid)_
_
Let's just throw exceptions anywhere 🤪_
Then get a global exception handler (like https://github.com/alidemirbas/P.Error . you have nothing to do with it but a single code line)
and then go in your exception handler scope, that's all like below.
```csharp
ILogger logger = new Logger();
logger.Log(new Log
{
    Namespace = target?.ReflectedType.Namespace,
    ClassName = target?.ReflectedType.Name,
    MethodName = target?.Name,
    DateTime = System.DateTime.Now,
    Description = error.Exception.Message,
    Success = false,
    Title = error.Exception.GetType().Name
});
```

It can be a "File.Logger" or a "Db.Logger" with a namespace. It's up to you
It's that much easy 😏
