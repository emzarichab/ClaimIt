﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Linq;

namespace ClaimIt.DayViewComponent
{
  public class DayViewModel: INotifyPropertyChanged
  {
    public event PropertyChangedEventHandler PropertyChanged;

    private int _currentIndex;
    private int _ImageCount = 500;

    public DayViewModel()
    {
      var days = new List<string>(
          CultureInfo.CurrentCulture.DateTimeFormat.AbbreviatedDayNames
          .Select(dayName => dayName)
          .ToList());

      Items = new ObservableCollection<object>
            {
                new { Source = CreateSource(), Ind = _ImageCount++, Text = days[0] , Color = Color.Red },
                new { Source = CreateSource(), Ind = _ImageCount++, Text = days[1] , Color = Color.Green },
                new { Source = CreateSource(), Ind = _ImageCount++, Text = days[2] , Color = Color.Gold },
                new { Source = string.Empty, Ind = _ImageCount++, Text = days[3] , Color = Color.Silver },
                new { Source = string.Empty, Ind = _ImageCount++, Text = days[4] , Color = Color.Blue },
                new { Source = string.Empty, Ind = _ImageCount++, Text = days[5] , Color = Color.Violet },
                new { Source = string.Empty, Ind = _ImageCount++, Text = days[6] , Color = Color.SkyBlue }
            };
    }

    public ObservableCollection<object> Items { get; }

    private string CreateSource()
    {
      return $"https://picsum.photos/500/500?image={_ImageCount}";
    }
  }
}
