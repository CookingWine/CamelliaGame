# Game Framework
### 目录说明

[目录说明](./Other/DirectoryDescription.md)



---
## Game System

### Localization System [多语言系统]



### Backpack System [背包系统]



### Configs Manage System [配置管理系统]



### Special Effects System [特效系统]



### Data Node System [数据节点系统]



### Role System [角色系统]



### Equipment System [装备系统]



### Skill System [技能系统]



### Dialogue System [对话系统]



### Debuff System [增益系统]



### Event System [事件系统]



### Red Dot System [红点系统]



### Friend System [好友系统]



### Prop System [道具系统]



### UI System [界面系统]



### Combat System [战斗系统]



### Input System [输入系统]



### Sound System [音效系统]



### Timer System [时间系统]



### Playback System [回放系统]



## Game Plugin

### [HybridCLR](https://www.hybridclr.cn/docs/intro) 

Hybridclr的Unity Package Manager URL安装地址：

- gitee `https://gitee.com/focus-creative-games/hybridclr_unity.git`
- github `https://github.com/focus-creative-games/hybridclr_unity.git`

### [Obfuz](https://www.obfuz.com/docs/intro)

> Obfuz的Unity Package Manager URL安装地址：
>
> - gitee `https://gitee.com/focus-creative-games/obfuz.git`
>
> - github `https://github.com/focus-creative-games/obfuz.git`
>
> ---
>
> 如果需要与HybridCLR一起进行使用则需要额外安装obfuz4hybridclr
>
> > 用于Unity Package的URL安装地址：
> >
> > - `https://github.com/focus-creative-games/obfuz4hybridclr.git`
> >
> > - `https://gitee.com/focus-creative-games/obfuz4hybridclr.git`
> >
> >   ⚠注意
> >
> >   Obfuz和HybridCLR插件都包含了dnlib插件。在Unity Editor中当两个package中包含同名插件时会产生错误。 解决办法为将HybridCLR下载到本地，移除其中包含的dnlib.dll，再放到Packages目录下。

### [YooAsset](https://www.yooasset.com/docs/guide-editor/AssetBundleBuilder)

``` string
// 输入以下内容（国际版）
Name: package.openupm.com
URL: https://package.openupm.com
Scope(s): com.tuyoogame.yooasset
```

### [UniTask](https://github.com/Cysharp/UniTask)

- 通过 git URL 安装

需要支持 git 包路径查询参数的 Unity 版本（Unity >= 2019.3.4f1、Unity >= 2020.1a21）。可以添加到包管理器`https://github.com/Cysharp/UniTask.git?path=src/UniTask/Assets/Plugins/UniTask`

### [Luban](https://www.datable.cn/docs/intro)

Luban的Unity Package Manager URL安装地址：

- gitee `https://gitee.com/focus-creative-games/luban_unity.git`
- github `https://github.com/focus-creative-games/luban_unity.git`

### [PrimeTween](https://github.com/KyryloKuzyk/PrimeTween)

This installation method also helps to clean the project structure.

- Open 'Edit / Project Settings / Package Manager'.
- Add a new Scoped Registry with Name: `npm` URL: `https://registry.npmjs.org` Scope(s): `com.kyrylokuzyk`.
- Go to 'Window / Package Manager / Packages / My Registries'.
- Install the PrimeTween package.

### [Protobuf](https://github.com/protocolbuffers/protobuf)

- [说明文档](./Other/ProtocBuf.md)

