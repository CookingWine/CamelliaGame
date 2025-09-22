# CamelliaGame

## 关于游戏异常处理

- IGameException

  1. **接口与继承的目的**：接口 `IGameException` 用于统一处理、日志上报、以及在 catch 时按分类处理；实际异常仍然继承 `System.Exception`，因为只有 Exception 能被 `throw` / `catch`。
  2. **不要把异常当控制流**：在游戏主循环或高频逻辑中不要频繁抛出异常，异常开销高。使用 `TryXxx` 返回布尔值/错误码的模式更适合性能敏感路径。
  3. **错误码与严重级别**：在客户端上报或统计时非常有用；建议建立错误码规范（例如 1xxx: 模块、2xxx: 资源、3xxx: 热更等）。
  4. **Context 辅助定位**：向异常里填入 key/value 上下文（如 moduleName、userId、sceneName）可以极大提高定位效率，但不要把大量二进制或大型对象放进去。
  5. **序列化支持（可选）**：当你需要把异常序列化上传或跨域传输时，提供 Serialization 支持；但序列化会增加代码复杂度与体积，按需启用。
  6. **统一上报点**：通过 `GlobalExceptionHandler` 收集并统一上报。对于 `IGameException` 可做策略化处理（比如 `Severity.Critical` 立刻上报并弹 UI，`Info/Warning` 缓存合并后上报）。
  7. **测试**：写单元测试模拟不同异常抛出/捕获流程，确保序列化、ToString、Context 工作正常。


 - 建议接口：`IGameException`（统一属性：ErrorCode / Severity / Context / CanRetry）。
 - 基础实现：`GameException : Exception, IGameException`。
 - 派生异常：`ModuleNotFoundException`、`ResourceLoadException`、`HotfixException` 等。
 - 全局捕获：使用 `Application.logMessageReceived` + `AppDomain.CurrentDomain.UnhandledException`。
 - 性能注意：避免在热路径频繁抛异常，Prefer `TryXxx`。

  

  