# Amusoft.UI.WPF


## Project state

[![.GitHub](https://github.com/taori/Amusoft.UI.WPF/actions/workflows/dotnet.yml/badge.svg)](https://github.com/taori/Amusoft.UI.WPF/actions/workflows/dotnet.yml)
[![GitHub issues](https://img.shields.io/github/issues/taori/Amusoft.UI.WPF)](https://github.com/taori/Amusoft.UI.WPF/issues)
[![NuGet version (Amusoft.UI.WPF)](https://img.shields.io/nuget/v/Amusoft.UI.WPF.svg)](https://www.nuget.org/packages/Amusoft.UI.WPF/)

## Features

### Controls

- NotificationDisplay
- TintedImage

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
