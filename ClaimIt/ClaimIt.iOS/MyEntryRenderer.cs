﻿using System;
using ClaimIt;
using ClaimIt.iOS;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer (typeof(MyEntry), typeof(MyEntryRenderer))]
namespace ClaimIt.iOS
{
  public class MyEntryRenderer : EntryRenderer
  {
    protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
    {
      base.OnElementChanged(e);

      if (Control != null)
      {
        // do whatever you want to the UITextField here!
        //Control.BackgroundColor = UIColor.FromRGB(204, 153, 255);
        //Control.BorderStyle = UITextBorderStyle.Line;

      }
    }
  }
}
