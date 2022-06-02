using CommandLine;
using NiTiS.Additions;
using NiTiS.IO;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using static System.Console;

namespace ImageResizer;

public static class Program
{
	public static readonly Version Version = new(1, 0, 1);
	public static void Main(string[] args)
	{
		Parser.Default.ParseArguments<Options>(args)
			.WithParsed(a =>
			{
				if (args.Length is not 0 && args[0] is "--help" or "--version")
				{
					return;
				}
#if DEBUG
				WriteLine("Warning DEBUG mode is enabled!");
				WriteLine(a.OutputDirectory);
				WriteLine(a.Resize);
				WriteLine(a.ImageFilter);
				WriteLine(a.CopyAsNested);
#endif
				if (a.ImageFilter is null) return;

				Directory outDir = new(a.OutputDirectory);
				Directory fileDir = new Directory(Environment.CurrentDirectory);

				IEnumerable<File> files = System.IO.Directory.GetFiles(fileDir.Path, a.ImageFilter, System.IO.SearchOption.AllDirectories).Select(s => new File(s));

				foreach (File file in files)
				{
					Directory topDir = null;
					List<string> dirs = new();
					while(true)
					{
						topDir = topDir is null ? file.Parent : topDir.Parent;
						if (topDir.Path == fileDir.Path)
						{
							break;
						}
						dirs.Add(topDir.Name);
					}
					dirs.Reverse();
					string relPath = Strings.FromArray(dirs, "", "", Directory.DirectorySeparator.ToString());
					Directory outputPath = a.CopyAsNested ? new(outDir, relPath) : outDir;
					outputPath.Create();

					//Parse size
					double x = 1, y = 1;
					string[] _temp = a.Resize.Split(new char[] { 'x', 'X', '*', '%' });
					if (_temp.Length == 1)
					{
						if (Double.TryParse(_temp[0], out double val))
						{
							x = val;
							y = val;
						} 
						else
						{
							WriteError($"Invalid value {_temp[0]} (use comma, not dot)");
						}
					}
					else if (_temp.Length == 2)
					{
						if (Double.TryParse(_temp[0], out double val))
						{
							x = val;
						}
						else
						{
							WriteError($"Invalid value {_temp[0]} (use comma, not dot)");
						}
						if (Double.TryParse(_temp[1], out double val2))
						{
							y = val2;
						}
						else
						{
							WriteError($"Invalid value {_temp[1]} (use comma, not dot)");
						}
					}
					else
					{
						WriteError($"Invalid value: {a.Resize}");
						return;
					}

					//Resize
					try
					{
						using Bitmap originalImage = new(file.Path);
						int width = (int)(originalImage.Width * x);
						int height = (int)(originalImage.Height * y);
						WriteLine("New size are: {0}, {1}", width, height);
						using Bitmap resizedImage = ImageFunctions.ResizeImage(originalImage, width, height, InterpolationMode.NearestNeighbor);
						resizedImage.Save(new File(outputPath.Path, file.Name).Path, originalImage.RawFormat);
						WriteGreen($"Image {file.Name} succsessfully resized");
					}
					catch
					{
						WriteError($"Failed resize on image {file.Name}");
						return;
					}
				}
			});
	}
	public static void WriteGreen(string text)
	{
		ConsoleColor _temp2 = ForegroundColor;
		ForegroundColor = ConsoleColor.Green;
		WriteLine(text);
		ForegroundColor = _temp2;
	}
	public static void WriteError(string error)
	{
		ConsoleColor _temp2 = ForegroundColor;
		ForegroundColor = ConsoleColor.Red;
		WriteLine(error);
		ForegroundColor = _temp2;
	}
}
public class Options
{
	[Option('f', "filter", Required = false, HelpText = "Filter for image (**/*.png)")]
	public string ImageFilter { get; set; }


	[Option('o', "output", Required = false, HelpText = "Path to output directory")]
	public string OutputDirectory { get; set; } = "out" + Path.DirectorySeparator;


	[Option('n', "nested", Required = false, HelpText = "Does create directories in output directory")]
	public bool CopyAsNested { get; set; } = true;


	[Option('s', "size", Required = false, HelpText = "New size ratio \n\t1x1 for origin\n\t2x2 for 2x size\n\t0,5x0,5 for half size")]
	public string Resize { get; set; } = "2x2";

	[Option('m', "mode", Required = false, HelpText = "Set interpolation mode")]
	public InterpolationMode InterpolationMode { get; set; } = InterpolationMode.NearestNeighbor;
}
