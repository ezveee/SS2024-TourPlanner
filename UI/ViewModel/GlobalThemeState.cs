using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace UI.ViewModel;
public static class GlobalThemeState
{
	public static bool IsDarkMode { get; set; } = false;
	public static Brush ForegroundBrush { get; set; } = new SolidColorBrush(Colors.White);
	public static Brush BackgroundBrush { get; set; } = new SolidColorBrush(Colors.Black);
}
