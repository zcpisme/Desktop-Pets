# Desktop-Pets
This is just a record for what I did recently, nothing special, and there are other better projects.

最近突然想起来当初QQ宠物的日子，再加上虚拟主播的那些虚拟形象，想着能不能做一个QQ宠物加制片人的桌宠。一开始想用Python做的（因为比较熟悉python），查了点资料做了点发现和预期相差甚远。

就转战Unity了，但是一点Unity和C#也不会啊，直接裂开，那就从口开始摸索吧。查了些资料，发现已经有这样类似的开发过程了，就拿来用了。整个程序基于和参考：https://blog.csdn.net/ReDreamme/article/details/108329608

实际上，市面上也有类似的软件（Live2dViewerEX等），比我自己做的不知道好到哪里去了，但是这个学习的过程还是很开心的。而且这些软件有一个缺点就是，对我的电脑来说很卡，而且有一些功能是其所没有的，所以决定自己做一下（仅仅）适合自己电脑的。

目前基于参考文章，额外实现了鼠标中键和左键同时按下能拖拽人物窗口（具体实现过程很笨，更优方法以后再探索），右键呼出按钮。按钮分别为，1）静音，2）当前地区室外温度，如果ip获取失败则为默认（我）城市坐标的温度，3）剩余可用内存
![](https://github.com/zcpisme/Desktop-Pets/blob/main/image/example.PNG?raw=true)


使用：通过Unity2018打开，build and run即可。
&nbsp;

关于为什么不做点击穿透，因为不会，刚学实在是操作不来，后续如果继续这个项目会更新。

本来还想做使用openhardwaremonitor获取硬件温度等，但是不知道为什么读取不了，网上的解决方法都试过了，无果。

天气的获取先基于ip，然后通过ip获取到当前城市经纬度，再通过经纬度接入openweather api（`https://openweathermap.org/api`），这里需要申请自己的key填入GetOpenWeatherMapWrapper.cs中,具体代码及文章参考https://blog.csdn.net/u014361280/article/details/108501931

模型用的网上随处可以下载的雷姆live2d模型，老模型了。

禁止商用。
