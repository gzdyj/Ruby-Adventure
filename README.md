#### 

# 一个伟大的Unity项目（Ruby大冒险）

![](https://cos.zinzin.cc//images_1/Unity_T9yfyJqAsr.png?imageMogr2/format/webp)

#### 解决角色撞击会发成抖动（改为刚体移动）

![](https://cos.zinzin.cc//images_1/devenv_8XGkSs7VSj-1662791517287.png?imageMogr2/format/webp)

#### 让摄像头跟随移动

首选下载Cimemachine，踩坑记录：下载高版本的cimemachine会缺少组件，应该本人装的2018版本packages下面没有Timeline所以会报错，下载2.29版本即可

![](https://cos.zinzin.cc//images_1/Unity_P3j50Chq80.png?imageMogr2/format/webp)

    然后在导航栏找到刚才安装的cimemachine创建一个2D的相机，然后将ruby拖到follow即可地图跟随角色移动

![](https://cos.zinzin.cc//images_1/Unity_wNrFpl1Dn6.png?imageMogr2/format/webp)

![](https://cos.zinzin.cc//images_1/Unity_w8TzAkbtKG.png?imageMogr2/format/webp)

#### 设置水面不让移动角色进入

先将Tilemap添加一个Tilemap Collider 2D 然后所以添加的都会变成障碍，然后再将除了水面的障碍如地砖等取消碰撞。

![](https://cos.zinzin.cc//images_1/chrome_0Iaf21NUMu.png?imageMogr2/format/webp)

将碰撞类型改为None

![碰撞类型设置](https://cos.zinzin.cc//images_1/chrome_txwraVzqqO.png?imageMogr2/format/webp)

合并碰撞刚体，然后将刚体设置为静态避免发生物理作用

![静态刚体](https://cos.zinzin.cc//images_1/Unity_5msp4y4cAy.png?imageMogr2/format/webp)

#### 创建一个游戏边界

![游戏边界](https://cos.zinzin.cc//images_1/Unity_ZrQwuibgoe.png?imageMogr2/format/webp)

然后edit collider设置四边形碰撞,然后再将这个拖动到相机碰撞刚体，is Trigger一定要勾选，否则会将角色挤出去

![](https://cos.zinzin.cc//images_1/Unity_fhRYPPilZ2.png?imageMogr2/format/webp)

#### 添加生命道具草莓设置碰撞体和触发器

![](https://cos.zinzin.cc//images_1/MessageCenterUI_Ti23vKtGNh.png?imageMogr2/format/webp)

约束玩家的生命值

![](https://cos.zinzin.cc//images_1/devenv_CzChr2RR5n.png?imageMogr2/format/webp)

#### 增加陷阱编写持续掉血脚本

![](https://cos.zinzin.cc//images_1/MessageCenterUI_XzIlCvdEoF.png?imageMogr2/format/webp)

![](https://cos.zinzin.cc//images_1/devenv_keGa25fiG6.png?imageMogr2/format/webp)

#### 给草莓加上动画

Ctrl+6快捷键打开Animation设置每帧x，y轴值达到缩放动画效果

![](https://cos.zinzin.cc//images_1/MessageCenterUI_xurE8VnM7a.png?imageMogr2/format/webp)

#### 给野怪加上移动动画

![](https://cos.zinzin.cc//images_1/MessageCenterUI_3gJjyVehDj.png?imageMogr2/format/webp)

设置控制器

```cs
        anim.SetFloat("moveX",moveDirection.x);
        anim.SetFloat("moveY",moveDirection.y);
```

#### 给角色加上状态移动动画

1、给主角加上控制器

![](https://cos.zinzin.cc//images_1/MessageCenterUI_1tiAhK24j0.png?imageMogr2/format/webp)

![](https://cos.zinzin.cc//images_1/MessageCenterUI_A9EI5jeqNv.png?imageMogr2/format/webp)

2、设置角色朝向移动控制器

```cs
        Vector2 moveVector = new Vector2(moveX, moveY);
        if (moveVector.x != 0 || moveVector.y != 0) {
            lookDirection = moveVector;
        }
        anim.SetFloat("Look X", lookDirection.x);
        anim.SetFloat("Look Y", lookDirection.y);
        anim.SetFloat("Speed", moveVector.magnitude);.y);
```

#### 给角色加上发送子弹效果

![](https://cos.zinzin.cc//images_1/Unity_rIPyBIC8kF.gif?imageMogr2/format/webp)

```cs
        //======按下J键 进行攻击===
        if (Input.GetKeyDown(KeyCode.J)) {
            GameObject bullet = Instantiate(bulletPrefab, rbody.position, Quaternion.identity);
            BulletController bc = bullet.GetComponent<BulletController>();
            if (bc != null) {
                bc.Move(lookDirection, 300);
            }
        }
```

击中机器人进入修复动画

![](https://cos.zinzin.cc//images_1/Unity_95g9i2Tkww.gif?imageMogr2/format/webp)

```cs
     //碰撞检测
    void OnCollisionEnter2D(Collision2D other) {
        EnemyController ec = other.gameObject.GetComponent<EnemyController>();
        if (ec != null) {
            ec.Fixed(); //修复敌人
        }
        Destroy(this.gameObject);
    }
```

添加发射动作

```cs
anim.SetTrigger("Launch");//播放攻击动画
```

#### 切割图片添加烟雾特效

![](https://cos.zinzin.cc//images_1/Unity_2fZYKfGeuA.png?imageMogr2/format/webp)

踩坑项

学校的unity版本Alpha Blended在Mobile的Particles下

![](https://cos.zinzin.cc//images_1/ShareX_QDdFiNWs85.png?imageMogr2/format/webp)

<font color=RED>后面的晚上的没保存好，文件丢失，心疼~~还好图片还在图床存着，下面就放一下完成的效果，后续代码会放到GitHub供参考</font>

解救机器人动画

![Unity_nN1IeIe4mG](https://cos.zinzin.cc//images_1/Unity_nN1IeIe4mG.gif?imageMogr2/format/webp)

拾取草莓特效

![Unity_ypDAJRaTGx](https://cos.zinzin.cc//images_1/Unity_ypDAJRaTGx.png?imageMogr2/format/webp)

绘制角色血条UI

![Unity_tkYPWNp9bO](https://cos.zinzin.cc//images_1/Unity_tkYPWNp9bO.gif?imageMogr2/format/webp)

设置角色命中、行走、受伤音效

![Unity_k6xIB4EXLh](https://cos.zinzin.cc//images_1/Unity_k6xIB4EXLh.png?imageMogr2/format/webp)

NPC动画以及文本框

![Unity_yrkMdx5BTl](https://cos.zinzin.cc//images_1/Unity_yrkMdx5BTl.gif?imageMogr2/format/webp)

打包文件

![](https://cos.zinzin.cc//images_1/Unity_swd0q4gpIf.png?imageMogr2/format/webp)

查看构建的文件目录

![](https://cos.zinzin.cc//images_1/explorer_9GFWxwV4zh.png?imageMogr2/format/webp)

启动Ruby大冒险

![](https://cos.zinzin.cc//images_1/Ruby%E5%A4%A7%E5%86%92%E9%99%A9_S9nEzM9uHS.png?imageMogr2/format/webp)

演示视频（文件较大）：

<video id="video" controls="" preload="none" poster="https://cos.zinzin.cc//images_1/Unity_qp4chrsOaE.png?imageMogr2/format/webp"  width="500" height="500">
    <source id="mp4" src="https://pan.zinzin.cc/directlink/zfile/%E6%BC%94%E7%A4%BA%E6%96%87%E4%BB%B6/68D0BuNEma.mp4" type="video/mp4"><source>
</video>

好啦~暂时就到这里了！
