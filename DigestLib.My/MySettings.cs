using Microsoft.VisualBasic.CompilerServices;
using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Configuration;
using System.Diagnostics;
using System.Runtime.CompilerServices;
namespace DigestLib.My
{
	[GeneratedCode("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "8.0.0.0"), EditorBrowsable(EditorBrowsableState.Advanced), CompilerGenerated]
	internal sealed class MySettings : ApplicationSettingsBase
	{
		private static MySettings defaultInstance = (MySettings)SettingsBase.Synchronized(new MySettings());
		public static MySettings Default
		{
			get
			{
				return MySettings.defaultInstance;
			}
		}
		[ApplicationScopedSetting, DefaultSettingValue("Data Source=localhost;Database=test;User ID=root;Password=root;"), SpecialSetting(SpecialSetting.ConnectionString), DebuggerNonUserCode]
		public string Setting
		{
			get
			{
				return Conversions.ToString(this["Setting"]);
			}
		}
		[DebuggerNonUserCode]
		public MySettings()
		{
		}
	}
}
