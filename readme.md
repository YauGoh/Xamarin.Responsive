# Introduction

Simple responsive grid implementation for Xamarin Forms, inspired by the responsive grid from [Twitter Bootstrap](https://getbootstrap.com/docs/4.0/layout/grid/).

# Getting started

Install from package manager

```
Install-Package Xamarin.Responsive
```

Install from dot cli

```
dotnet add package Xamarin.Responsive
```

# Samples

## Simple - Mobile to Large Devices

Mobile devices display full width (12 or 12 columns).
Small devices display half width (6 of 12 columns).
Medium devices display third width (4 of 12 columns).
Large devices display one quarter (3 of 12 columns).

```
<ContentPage xmlns="http://xamarin.com/schemas/2014/forms"
             xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
             xmlns:d="http://xamarin.com/schemas/2014/forms/design"
             xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006"
             xmlns:r="clr-namespace:Xamarin.Responsive;assembly=Xamarin.Responsive"
             mc:Ignorable="d"
             x:Class="XamarinResponsive.Samples.SimplePage">
    <ContentPage.Content>
        <r:Container>
            <r:Row>
                <Label
                    r:Row.Column="12,6,4,3"
                    Text="A Responsive Label"/>
            </r:Row>
        </r:Container>
    </ContentPage.Content>
</ContentPage>
```

## Changing number of columns in Container

By default the grid is 12 Columns, you can change this in the Container control.

```
    <r:Container Columns="4">
        ...
    </r:Container>
```
