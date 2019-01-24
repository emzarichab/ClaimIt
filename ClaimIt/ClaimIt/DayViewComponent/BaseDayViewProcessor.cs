﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using static Xamarin.Forms.AbsoluteLayout;

#pragma warning disable
namespace ClaimIt.DayViewComponent
{
  public class BaseDayViewProcessor
  {
    public BaseDayViewProcessor(DayView coverFlow)
    {
      CoverFlow = coverFlow;
    }

    public BaseDayViewProcessor()
    {
    }

    public uint AnimationLength { get; set; } = 300;
    public Easing AnimEasing { get; set; } = Easing.CubicOut;
    public DayView CoverFlow { get; set; }

    public void HandleInitViews(IAbsoluteList<View> displayedViews, DayPosition viewPosition)
    {
      var PreviousItemtranslation = 0.0;
      var translate = (CoverFlow.Width / 2 - CoverFlow.MarginBorder) * (int)viewPosition;
      var count = displayedViews.Count();

      foreach (var view in displayedViews)
      {
        if (translate > CoverFlow.Width / 2)
        {
          var position = (count + displayedViews.IndexOf(view) - 1) * CoverFlow.Space;
          translate = PreviousItemtranslation - position;
        }
        --count;
        view.TranslationX = translate;
        PreviousItemtranslation = translate;
        translate = translate + CoverFlow.Space;
      }
    }

    public Task HandleAutoNavigate(List<View> views, AnimationDirection animationDirection, IEnumerable<View> inactiveViews)
    {
      throw new NotImplementedException();
    }

    /*
     * HandlePanApply doesn't managed Recycled Views
     * Seems to work but not correctly
     * 
     * The Idea would be at runtime(during animation)
     * To add old View to recycledList when they are far away(Math.Abs(translate) > maxTranslate)
     * And recycle a view and display it at the other side.
     * And give at this new display view the end of the animatiom(just the end :))
     */
    public void HandlePanApply(IAbsoluteList<View> displayedViews, double dragX, DayPosition position, List<View> recycledViews)
    {
      Animation a = new Animation();

      var maxTranslate = CoverFlow.MaxGraphicAxis + CoverFlow.MarginBorder;
      foreach (var v in displayedViews)
      {
        var translate = v.TranslationX + dragX;
        if (Math.Abs(translate) > maxTranslate
            && displayedViews.Count() > 1)
        {
          var diff = Math.Abs(translate) - maxTranslate;
          var coef = 1 - diff / Math.Abs(dragX);
          a.Add(0, 1, new Animation(f => v.TranslationX = f, v.TranslationX, translate, Easing.SinOut, () =>
          {
            /* Unworking code here but have to be done
            if (direction == AnimationDirection.Prev) // Movement --> right
                CoverFlow.ItemMaxOnAxis = CoverFlow.VerifyIndex(CoverFlow.ItemMaxOnAxis - 1);
            else if (direction == AnimationDirection.Next)  // Movement <-- Left
                CoverFlow.ItemMinOnAxis = CoverFlow.VerifyIndex(CoverFlow.ItemMinOnAxis + 1);
            v.IsVisible = false;
            recycledViews.Add(v);


            // Remove Views from displayed List
            CoverFlow.RemoveUnDisplayedViews();

            // RecyclerView
            var recycledView = CoverFlow.RecyclerView(direction);

            // Add animation at runtime
            a.Add((diff / dragX), 1, new Animation(f => v.TranslationX = f, v.TranslationX, translate, Easing.Linear, null));
            */
          }));
        }
        else
        {
          a.Add(0, 1, new Animation(f => v.TranslationX = f, v.TranslationX, translate, Easing.SinOut, null));
        }
      }
      a.Commit(CoverFlow, "AlignViews", 60, 400, Easing.Linear, (double arg1, bool arg2) =>
      {
        // Console.WriteLine("Test");
      });
    }

    public void HandlePanChanged(IAbsoluteList<View> displayedViews, double dragX, AnimationDirection direction, List<View> recycledViews)
    {
      var maxTranslate = CoverFlow.MaxGraphicAxis + CoverFlow.MarginBorder;
      foreach (var v in displayedViews)
      {
        var translate = v.TranslationX + dragX;
        if (Math.Abs(translate) > maxTranslate
            && displayedViews.Count() > 1)
        {
          if (direction == AnimationDirection.Prev) // Movement --> right
          {
            CoverFlow.ItemMaxOnAxis = CoverFlow.VerifyIndex(CoverFlow.ItemMaxOnAxis - 1);
          }
          else if (direction == AnimationDirection.Next)  // Movement <-- Left
          {
            CoverFlow.ItemMinOnAxis = CoverFlow.VerifyIndex(CoverFlow.ItemMinOnAxis + 1);
          }
          recycledViews.Add(v);
          v.IsVisible = false;
        }
        v.TranslationX = translate;
      }
    }

    public Task HandlePanReset(IEnumerable<View> views, AnimationDirection animationDirection, IEnumerable<View> inactiveViews)
    {
      throw new NotImplementedException();
    }
  }
}
#pragma warning restore
