using System;
using System.Collections.Generic;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Build.Tasks;
using NUnit.Framework;

namespace Microsoft.Maui.Controls.Xaml.UnitTests
{
	[XamlCompilation(XamlCompilationOptions.Skip)]
	public partial class Gh1978 : ContentPage
	{
		public Gh1978()
		{
			InitializeComponent();
		}

		public Gh1978(bool useCompiledXaml)
		{
			//this stub will be replaced at compile time
		}

		[TestFixture]
		class Tests
		{
			[TestCase(true)]
			public void ReportError(bool useCompiledXaml)
			{
				if (!useCompiledXaml)
					return;
				Assert.Throws<BuildException>(() => MockCompiler.Compile(typeof(Gh1978)));
			}
		}
	}
}
