using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace LinccerApi.WindowsPhone
{
	public enum SendMode
	{
		OneToOne,
		OneToMany
	}

	public class SendModeString
	{
		public static string ConvertSendModeToString(SendMode mode)
		{
			if(mode == SendMode.OneToOne) return "one-to-one";

			return "one-to-many";
		}
	}
}
