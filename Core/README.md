# Rino Game Framework

Unity MVP 架構共用框架，提供 DDD 核心、遊戲資料管理、通用工具等功能。

## 安裝

### 透過 Git URL

在 Unity Package Manager 中選擇 "Add package from git URL..."，輸入：

```
https://github.com/你的帳號/RinoGameFramework.git?path=Core
```

### 依賴套件

本套件需要以下依賴，請先安裝：

| 套件 | 安裝方式 |
|------|----------|
| Zenject | OpenUPM 或 Asset Store |
| DoTween | Asset Store |
| MessagePipe | OpenUPM 或 NuGet |
| UniRx | OpenUPM |
| UniTask | OpenUPM |
| Odin Inspector | Asset Store（付費） |
| Unity Localization | Package Manager |

#### OpenUPM 配置範例

在 `Packages/manifest.json` 中加入：

```json
{
  "scopedRegistries": [
    {
      "name": "OpenUPM",
      "url": "https://package.openupm.com",
      "scopes": [
        "com.cysharp.unitask",
        "com.neuecc.unirx"
      ]
    }
  ]
}
```

## 功能模組

| 模組 | 說明 |
|------|------|
| DDDCore | Entity、Repository、EventBus 基礎架構 |
| GameManager | 遊戲資料管理編輯器視窗 |
| RinoUtility | 通用工具方法 |

## 架構

本框架遵循 MVP（Model-View-Presenter）架構設計，詳細說明請參考 [MVP 架構開發準則](../docs/MVP_Architecture_Guidelines.md)。

## 授權

MIT License
