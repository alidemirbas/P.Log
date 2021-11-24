Hey, Aren't you tired of injecting a logger everywhere for exceptions? 🤨
_KISS again 😘 (keep it simple, stupid)_
_
_Let's just throw exceptions anywhere 🤪
_Then get a global exception handler (like https://github.com/alidemirbas/P.Error . you have nothing to do with it but a single code line)
_and then go in your exception handler scope, that's all like below.
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

_It can be a "File.Logger" or a "Db.Logger" with a namespace. It's up to you_
_It's that much easy 😏
