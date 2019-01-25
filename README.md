# Amusoft.UI.WPF

Wpf related UI Controls
- NotificationDisplay
- TintedImage

# CI

| project        | build status           |
| ------------- |-------------|
| master | [![Build status](https://ci.appveyor.com/api/projects/status/65249fbj07k246fd/branch/master?svg=true)](https://ci.appveyor.com/project/taori/amusoft-ui-wpf/branch/master)|
| dev      | [![Build status](https://ci.appveyor.com/api/projects/status/65249fbj07k246fd/branch/dev?svg=true)](https://ci.appveyor.com/project/taori/amusoft-ui-wpf/branch/dev)      |
| release | [![Build status](https://ci.appveyor.com/api/projects/status/65249fbj07k246fd/branch/release?svg=true)](https://ci.appveyor.com/project/taori/amusoft-ui-wpf/branch/release)   |

# How to use it

## You can display window notifications like this

    var host = NotificationHostManager.GetHostByVisual(Window.Content as Visual);
    var notification = new SimpleNotification(DateTime.Now.ToString());
    notification.AutoClose = true;
    host.DisplayAsync(notification, AnchorPosition);

## You can display screen notifications like this

    var host = NotificationHostManager.GetHostByScreen(Screen.PrimaryScreen);
    var notification = new SimpleNotification(DateTime.Now.ToString());
    notification.AutoClose = true;
    host.DisplayAsync(notification, AnchorPosition);

# What does it do

- Display notifications anchored to any element
- Display notifications anchored to the screen

![grafik](https://user-images.githubusercontent.com/5545184/51777941-acd68500-20ff-11e9-8995-e91f36df0dc3.png)
![grafik](https://user-images.githubusercontent.com/5545184/51778028-0ccd2b80-2100-11e9-85bb-72a34fc336bf.png)
