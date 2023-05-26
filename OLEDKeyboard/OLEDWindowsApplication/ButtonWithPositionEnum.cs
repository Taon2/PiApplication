using System;
using System.Windows;
using System.Windows.Controls;
using OLEDWindowsApplication.Models;

namespace OLEDWindowsApplication;

public class ButtonWithPositionEnum : Button
{
    public static readonly DependencyProperty CurrentPositionProperty =
        DependencyProperty.Register(
            name: nameof(CurrentPosition),
            propertyType: typeof(Position),
            ownerType: typeof(ButtonWithPositionEnum),
            typeMetadata: new FrameworkPropertyMetadata(
                defaultValue: Position.Undefined)
        );

    public Position CurrentPosition
    {
        get => (Position)GetValue(CurrentPositionProperty);
        set => SetValue(CurrentPositionProperty, value);
    }

    public ButtonWithPositionEnum() : base()
    {
        this.CurrentPosition = Position.Undefined;
    }

    public ButtonWithPositionEnum(Position position) : base()
    {
        this.CurrentPosition = position;
    }
}