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

namespace LinccerApi
{
	public delegate void FileCacheStoreCallback(String uri);
	public delegate void LinccerReceiveCallback<T>(T obj);
	public delegate void LinccerContentCallback(string content);
}
