## 使用方式

### 使用命令行参数

```shell
# 默认参数 0 "18:00"
./CountdownToWork.exe

./CountdownToWork.exe 1 "18:00"
```

- 参数1 输出样式：可选值，默认0，范围0-6
- 参数2 下班时间：可选值，默认"18:00"

### 使用配置文件

1. 新建 `config.txt` 配置文件，写入以下内容

```
1 18:00:00
```

2. 执行 `CountdownToWork.exe`